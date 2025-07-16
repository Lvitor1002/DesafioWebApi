namespace GruposPokemonApi.Api
{
    public static class AppExtensions
    {
        public static void ConfigAmbiente(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
