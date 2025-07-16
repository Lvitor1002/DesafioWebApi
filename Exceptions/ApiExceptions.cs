namespace GruposPokemonApi.Utils
{
    public class ApiExceptions : Exception
    {
        public string Titulo { get; set; } = "Erro na Api";
        public int StatusCode { get; set; } = 400;


        public ApiExceptions(string message) : base(message){ }
        public ApiExceptions(string message,int statusCode) : base(message) 
        {
            StatusCode = statusCode;
        }
    }
}
