using StatsJoueurs.Interfaces;
using StatsJoueurs.Models;

namespace StatsJoueurs.Services
{
    public class TennisPlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repository;
        public TennisPlayerService(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public Player GetPlayerById(int id) => _repository.GetPlayerById(id);

        public IEnumerable<Player> GetPlayers() => _repository.GetPlayers()
            .OrderBy(p => p.Sex == "M" ? 0 : 1) 
            .ThenBy(p => p.Data.Rank)
            .ToList();

    }
}
