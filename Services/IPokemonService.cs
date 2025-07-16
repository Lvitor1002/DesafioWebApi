using GruposPokemonApi.Models;

namespace GruposPokemonApi.Services
{
    public interface IPokemonService
    {
        public Task<Pokemon> BuscarPokemonPorNome(string nome);
    }
}
