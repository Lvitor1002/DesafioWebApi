
using GruposPokemonApi.Api;

namespace GruposPokemonApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddConfigApi();
            builder.AddDocumentacao();
            builder.Services.AddControllers();
            builder.Services.AddAuthorization();
            builder.AddServicoInjecao();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.ConfigAmbiente();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
