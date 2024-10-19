using StatsJoueurs.Models;

namespace StatsJoueurs.Interfaces
{
    public interface IPlayerService
    {
        IEnumerable<Player> GetPlayers();
        Player GetPlayerById(int id);
        Statistics GetStatistics();
    }
}
