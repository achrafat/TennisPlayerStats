using StatsJoueurs.Models;

namespace StatsJoueurs.Interfaces
{
    public interface IPlayerService
    {
        IEnumerable<Player> GetPlayers();
    }
}
