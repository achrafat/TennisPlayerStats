using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using StatsJoueurs.Controllers;
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

    }
}