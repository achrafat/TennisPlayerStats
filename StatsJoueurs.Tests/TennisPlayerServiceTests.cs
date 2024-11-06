using Moq;
using Newtonsoft.Json;
using StatsJoueurs.Interfaces;
using StatsJoueurs.Models;
using StatsJoueurs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsJoueurs.Tests
{
    public class TennisPlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _mockRepository;
        private readonly TennisPlayerService _service;
        private readonly List<Player> _TestPlayers;
        public TennisPlayerServiceTests()
        {
            _mockRepository = new Mock<IPlayerRepository>();
            _service = new TennisPlayerService(_mockRepository.Object);


            _TestPlayers = PlayerListForTest.LoadPlayersFromJsonFile(TestHelpers.GetFilePath("TestPlayers.json"));
        }
       
        
        [Fact]
        public void GetStatistics_CalculatesCorrectStatistics()
        {
            // Arrange
           

            _mockRepository.Setup(s => s.GetPlayers()).Returns(_TestPlayers);

            var expectedStatistics = new Statistics
            {
                CountryWithHighestWinRatio = "USA", // serena > Rafa
                AverageBMI = (23.51 + 24.83) / 2,
                MedianHeight = 180 // Median of 175, 180, 185
            };

            // Act
            var statistics = _service.GetStatistics();

            // Assert
            Assert.Equal(expectedStatistics.CountryWithHighestWinRatio, statistics.CountryWithHighestWinRatio);
            Assert.Equal(expectedStatistics.AverageBMI, statistics.AverageBMI, 2); // Test with precision
            Assert.Equal(expectedStatistics.MedianHeight, statistics.MedianHeight);
        }
    }
}
