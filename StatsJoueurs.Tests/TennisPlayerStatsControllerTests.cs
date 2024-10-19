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
            _TestPlayers = LoadPlayersFromJsonFile("C:\\Users\\atiaa\\Desktop\\Test Technique L’Atelier - Backend\\StatsJoueurs\\StatsJoueurs.Tests\\TestPlayers.json");
        }
        private List<Player> LoadPlayersFromJsonFile(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            var playerList = JsonConvert.DeserializeObject<PlayerList>(jsonData);
            return playerList.Players;
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

    }
}