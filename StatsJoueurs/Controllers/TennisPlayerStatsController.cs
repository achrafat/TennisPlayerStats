
using Microsoft.AspNetCore.Mvc;
using StatsJoueurs.Exceptions;
using StatsJoueurs.Interfaces;
using StatsJoueurs.Models;

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
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            var players = _service.GetPlayers();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetPlayerById(int id)
        {
            try
            {
                var player = _service.GetPlayerById(id);
                return Ok(player); 
            }
            catch (PlayerNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
        }
        [HttpGet("stats")]
        public ActionResult<Statistics> GetStatistics()
        {
            var statistics = _service.GetStatistics();
            return Ok(statistics);
        }

    }
}
