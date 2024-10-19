
using Microsoft.AspNetCore.Mvc;
using StatsJoueurs.Interfaces;

namespace StatsJoueurs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TennisPlayerStatsController : ControllerBase
    {
        private readonly IPlayerService _service;

        public TennisPlayerStatsController(IPlayerService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.Player>> GetPlayers()
        {
            var players = _service.GetPlayers();
            return Ok(players);
        }

    }
}
