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

        public Statistics GetStatistics()
        {
            // Récupérer tous les joueurs depuis le dépôt
            var players = _repository.GetPlayers();

            // 1. Pays avec le plus grand ratio de parties gagnées
            var countryWinRatios = players
                .GroupBy(p => p.Country.Code) // Regroupement par code de pays
                .Select(g => new
                {
                    Country = g.Key,
                    WinRatio = g.Sum(p => p.Data.Last.Count(result => result == 1)) / (double)(g.Sum(p => p.Data.Last.Count()))
                });

            var countryWithHighestWinRatio = countryWinRatios
                .OrderByDescending(cr => cr.WinRatio)
                .FirstOrDefault()?.Country;

            // 2. IMC moyen
            double averageBMI = players
                .Select(p => (p.Data.Weight/1000.0) / (Math.Pow(p.Data.Height/100.0, 2))) // IMC = poids / (taille**2) (taille en m)
                .Average();

            // 3. Médiane de la taille
            var heights = players.Select(p => p.Data.Height).OrderBy(h => h).ToList();
            double medianHeight;

            if (heights.Count % 2 == 0)
            {
                // Nombre pair d'éléments
                medianHeight = (heights[heights.Count / 2 - 1] + heights[heights.Count / 2]) / 2;
            }
            else
            {
                // Nombre impair d'éléments
                medianHeight = heights[heights.Count / 2];
            }

            // Format de retour des statistiques
            return new Statistics
            {
                CountryWithHighestWinRatio = countryWithHighestWinRatio,
                AverageBMI = averageBMI,
                MedianHeight = medianHeight
            };
        }
    }
}
