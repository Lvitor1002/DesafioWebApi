using System.Text;
using System.Text.Json;
using GruposPokemonApi.Models;
using GruposPokemonApi.Utils;
using System.IO; 


namespace GruposPokemonApi.Services
{
    public class TeamService : ITeamService
    {
        private readonly IPokemonService _pokemonService;
        private readonly ILogger<TeamService> _logger; 
        private readonly Dictionary<string, Teams> _teams = new();
        private readonly string PERSISTENCE_PATH;

        public TeamService(
            IPokemonService pokemonService,
            IConfiguration configuration,
            ILogger<TeamService> logger) // Injetar logger
        {
            _pokemonService = pokemonService;
            _logger = logger;

            // Obter o nome do arquivo da configuração
            string fileName = configuration["Persistence"] ?? "default.json";

            PERSISTENCE_PATH = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                fileName
            );

            // Garantir que o diretório pai exista
            string? directory = Path.GetDirectoryName(PERSISTENCE_PATH);
            if (!string.IsNullOrEmpty(directory))
            {
                try
                {
                    Directory.CreateDirectory(directory);
                    _logger.LogInformation($"Diretório criado: {directory}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Erro ao criar diretório: {directory}");
                    throw new ApiExceptions($"Erro ao criar diretório: {ex.Message}");
                }
            }

            CarregarTimes();
        }
        public async Task<Teams> CriarTime(Objects obj)
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            foreach (var nome in obj.Team)
            {
                pokemons.Add(await _pokemonService.BuscarPokemonPorNome(nome));
            }

            var novoTime = new Teams
            {
                Owner = obj.User,
                Pokemons = pokemons,
            };
            var novoId = _teams.Count + 1;

            _teams.Add(
                novoId.ToString(), 
                novoTime
            );

            await SalvarTime(); //Chamando o método

            return novoTime;
        }


        public Teams BuscarTimePorID(string id)
        {
            return _teams.FirstOrDefault(x=>x.Key == id).Value 
                ?? throw new ApiExceptions($"Time com o id[{id}] não foi encontrado!");
        }


        public Dictionary<string, Teams> ProcuarTodosTimes() => _teams;


        //Private porque será chamado aqui nesta classe
        private void CarregarTimes()
        {
            if (!File.Exists(PERSISTENCE_PATH))
            {
                _logger.LogWarning($"Arquivo não encontrado: {PERSISTENCE_PATH}");
                return;
            }

            try
            {
                string json = File.ReadAllText(PERSISTENCE_PATH, Encoding.UTF8);

                if (string.IsNullOrWhiteSpace(json))
                {
                    _logger.LogWarning("Arquivo de persistência vazio");
                    return;
                }

                var doc = JsonSerializer.Deserialize<JsonDocument>(json)!;

                doc.RootElement
                    .EnumerateObject()
                    .ToList()
                    .ForEach(t => {
                        var nomePropriedade = t.Name;
                        var elemento = doc.RootElement.GetProperty(nomePropriedade);

                        _teams.Add(
                            nomePropriedade,
                            elemento.Deserialize<Teams>(
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            )!
                        );
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar times");
                throw new ApiExceptions($"Erro ao carregar times: {ex.Message}");
            }
        }

        //Private porque será chamado aqui nesta classe
        private async Task SalvarTime()
        {
            try
            {
                string? directory = Path.GetDirectoryName(PERSISTENCE_PATH);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonSerializer.Serialize(_teams);
                await File.WriteAllTextAsync(PERSISTENCE_PATH, json, Encoding.UTF8);

                _logger.LogInformation($"Times salvos em: {PERSISTENCE_PATH}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar times");
                throw new ApiExceptions($"Erro ao salvar times: {ex.Message}");
            }
        }

    }
}
