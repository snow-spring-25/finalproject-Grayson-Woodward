namespace Final.Tests;
using Final.Logic;
public class Feature1
{
    [Test]
    public void CreateGame_HappyPath_GameIsCreatedWithPlayer() //Req 1.1.1
    {
        string gameId = "happyGame";
        string playerName = "PlayerOne";

        var game = GameManager.CreateGame(gameId, playerName);

        Assert.That(game != null);
        Assert.That(gameId == game.GameId);
        Assert.That(game.Players, Does.Contain(playerName));
    }
        [Test]
        public void CreateGame_DuplicateGameId_ThrowsInvalidOperationException() //Req1.1.2
        {
            string gameId = "duplicateGame";
            GameManager.CreateGame(gameId, "PlayerOne");

            var exception = Assert.Throws<InvalidOperationException>(() =>
                GameManager.CreateGame(gameId, "PlayerTwo"));

            Assert.That(exception.Message, Is.EqualTo("Game already exists."));
        }

        [Test]
        public void JoinGame_InvalidGameId_ThrowsInvalidOperationException() //Req1.1.2
        {
            string invalidGameId = "nonexistent";

            var exception = Assert.Throws<InvalidOperationException>(() =>
                GameManager.JoinGame(invalidGameId, "PlayerGhost"));

            Assert.That(exception.Message, Is.EqualTo("Game not found."));
        }
    }

