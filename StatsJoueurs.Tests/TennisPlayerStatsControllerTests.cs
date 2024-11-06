using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using StatsJoueurs.Controllers;
using StatsJoueurs.Exceptions;
using StatsJoueurs.Interfaces;
using StatsJoueurs.Models;

namespace StatsJoueurs.Tests
{
    public class TennisPlayerStatsControllerTests
    {
        private readonly Mock<IPlayerService> _mockService;
        private readonly TennisPlayerStatsController _controller_sut;
        private readonly List<Player> _TestPlayers;
        public TennisPlayerStatsControllerTests()
        {
            _mockService = new Mock<IPlayerService>();
            _controller_sut = new TennisPlayerStatsController(_mockService.Object);
            _TestPlayers = PlayerListForTest.LoadPlayersFromJsonFile(TestHelpers.GetFilePath("TestPlayers.json"));
        }
        
        [Fact]
        public void GetPlayers_ReturnsPlayers()
        {
            // Arrange
            _mockService.Setup(s => s.GetPlayers()).Returns(_TestPlayers);

            // Act
            var result = _controller_sut.GetPlayers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var players = Assert.IsAssignableFrom<IEnumerable<Player>>(okResult.Value);
           
        }
        [Fact]
        public void GetPlayers_ReturnsNotFound_WhenNoPlayersExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetPlayers()).Returns(new List<Player>());

            // Act
            var result = _controller_sut.GetPlayers();

            // Assert
            var notFoundResult = Assert.IsType<OkObjectResult>(result.Result);
            var players = Assert.IsAssignableFrom<IEnumerable<Player>>(notFoundResult.Value);
            Assert.Empty(players);
        }
        [Fact]
        public void GetPlayerById_ReturnsPlayer_WhenPlayerExists()
        {
            // Arrange
            var playerId = 17; 
            var expectedPlayer = _TestPlayers.First(p => p.Id == playerId);

            _mockService.Setup(s => s.GetPlayerById(playerId)).Returns(expectedPlayer);

            // Act
            var result = _controller_sut.GetPlayerById(playerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var player = Assert.IsAssignableFrom<Player>(okResult.Value);
            Assert.Equal(expectedPlayer.Id, player.Id);
        }
        [Fact]
        public void GetPlayerById_ReturnsNotFound_WhenPlayerDoesNotExist()
        {
            // Arrange
            var playerId = 21; // ID d'un joueur qui n'existe pas

            _mockService.Setup(s => s.GetPlayerById(playerId)).Throws(new PlayerNotFoundException(playerId));

            // Act
            var result = _controller_sut.GetPlayerById(playerId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"Player with ID {playerId} not found.", notFoundResult.Value);
        }
        [Fact]
        public void GetStatistics_ReturnsStatistics()
        {
            // Arrange
            var expectedStatistics = new Statistics
            {
                CountryWithHighestWinRatio = "ESP",
                AverageBMI = 24.5,
                MedianHeight = 185
            };

            _mockService.Setup(s => s.GetStatistics()).Returns(expectedStatistics);

            // Act
            var result = _controller_sut.GetStatistics();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var statistics = Assert.IsType<Statistics>(okResult.Value);
            Assert.Equal(expectedStatistics.CountryWithHighestWinRatio, statistics.CountryWithHighestWinRatio);
            Assert.Equal(expectedStatistics.AverageBMI, statistics.AverageBMI);
            Assert.Equal(expectedStatistics.MedianHeight, statistics.MedianHeight);
        }

    }
}