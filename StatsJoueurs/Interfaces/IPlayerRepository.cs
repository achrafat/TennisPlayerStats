using StatsJoueurs.Models;

namespace StatsJoueurs.Interfaces
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetPlayers();
        Player GetPlayerById(int id);
    }
}
