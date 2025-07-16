using GruposPokemonApi.Models;
using GruposPokemonApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GruposPokemonApi.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {


        private readonly ITeamService _teamService;
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        public async Task<ActionResult<Teams>> Criar(Objects obj)
        {
            return await _teamService.CriarTime(obj);
        }
        [HttpGet]
        public ActionResult<Dictionary<string, Teams>> BuscarTimes()
        {
            return _teamService.ProcuarTodosTimes();
        }


        [HttpGet("{id:regex(^\\d+$)}")]
        public ActionResult<Teams> BuscarID(string id)
        {
            
            return _teamService.BuscarTimePorID(id);
        }
    }
}
