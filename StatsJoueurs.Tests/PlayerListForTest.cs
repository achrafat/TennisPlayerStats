using Newtonsoft.Json;
using StatsJoueurs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StatsJoueurs.Tests
{
    public static class PlayerListForTest
    {
       public static List<Player> LoadPlayersFromJsonFile(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            var playerList = JsonConvert.DeserializeObject<PlayerList>(jsonData);
            return playerList.Players;
        }
    }
}
