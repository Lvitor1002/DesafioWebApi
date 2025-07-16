using GruposPokemonApi.Models;

namespace GruposPokemonApi.Services
{
    public interface ITeamService
    {

        public Task<Teams> CriarTime(Objects obj);
        public Dictionary<string, Teams> ProcuarTodosTimes();
        public Teams BuscarTimePorID(string id);
    }
}
