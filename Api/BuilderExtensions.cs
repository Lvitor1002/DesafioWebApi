using GruposPokemonApi.Services;
using Microsoft.OpenApi.Models;

namespace GruposPokemonApi.Api
{
    public static class BuilderExtensions
    {
        public static void AddConfigApi(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<PokemonApiConfig>(builder.Configuration.GetSection("PokemonApiConfig"));
        }
        public static void AddDocumentacao(this WebApplicationBuilder builder)
        {

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n=>n.FullName);
            });
        }

        public static void AddServicoInjecao(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IPokemonService, PokemonService>();
            builder.Services.AddSingleton<ITeamService, TeamService>();
        }
    }
}
