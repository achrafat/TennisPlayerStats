﻿using Newtonsoft.Json;
using StatsJoueurs.Interfaces;
using StatsJoueurs.Models;

namespace StatsJoueurs.Repositories
{
    public class TennisPlayerRepository : IPlayerRepository
    {

        private readonly string _filePath = "Data/TennisPlayerStats.json";
        private List<Player> _players;
        public TennisPlayerRepository()
        {
            _players = LoadFromFile();
        }

        public IEnumerable<Player> GetPlayers() => _players;

        private List<Player> LoadFromFile()
        {
            if (!File.Exists(_filePath)) return new List<Player>();

            var jsonData = File.ReadAllText(_filePath);

            var playerList = JsonConvert.DeserializeObject<PlayerList>(jsonData);

            return playerList?.Players ?? new List<Player>();
        }
    }
}