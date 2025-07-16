using GruposPokemonApi.Models;
using GruposPokemonApi.Utils;
using Microsoft.Extensions.Options;

namespace GruposPokemonApi.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IOptions<PokemonApiConfig> _pokemonApiConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        public PokemonService(IOptions<PokemonApiConfig> pokemonApiConfig, IHttpClientFactory httpClientFactory)
        {
            _pokemonApiConfig = pokemonApiConfig;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<Pokemon> BuscarPokemonPorNome(string nome)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var respostaApi = await httpClient.GetAsync($"{_pokemonApiConfig.Value.Url}/{nome}");

            if (!respostaApi.IsSuccessStatusCode){
                throw new ApiExceptions($"Pokemon '{nome}' não existe!");
            }

            var pokemon = await respostaApi.Content.ReadFromJsonAsync<Pokemon>()
                ?? throw new ApiExceptions("Um erro ocorreu durante a conversão Json"); //[??] "Se pokemon for null, então use esse outro valor"
            
            return pokemon;
        } 
    }
}
