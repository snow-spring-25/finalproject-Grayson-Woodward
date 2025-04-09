namespace Final.Tests;
using Final.Logic;
public class Feature1
{
    [Test]
    public void CreateGame_ThenJoinGame_ShouldSucceed()
    {
        var game = GameLobby.CreateGame(WordCategory.StarWars);
        game.AddPlayer("PlayerOne");

        var joinedGame = GameLobby.JoinGame(WordCategory.StarWars);

        Assert.That(joinedGame, Is.Not.Null);
        Assert.That(joinedGame.Players, Does.Contain("PlayerOne"));
        Assert.That(joinedGame.Started, Is.False);
    }

    [Test]
    public void JoinGame_AfterGameStarted_ShouldReturnNull()
    {
        var game = GameLobby.CreateGame(WordCategory.Foods);
        game.AddPlayer("PlayerOne");
        game.Start(); // Start the game

        var joined = GameLobby.JoinGame(WordCategory.Foods);

        Assert.That(joined, Is.Null);
    }

    [Test]
    public void StartGame_WithoutPlayers_ShouldThrow()
    {
        var game = GameLobby.CreateGame(WordCategory.Cosmere);
        var ex = Assert.Throws<InvalidOperationException>(() => game.Start());

        Assert.That(ex.Message, Does.Contain("Cannot start game without players"));
    }

    [Test]
    public void AddPlayer_AfterGameStarted_ShouldThrow()
    {
        var game = GameLobby.CreateGame(WordCategory.StarWars);
        game.AddPlayer("PlayerOne");
        game.Start();

        var ex = Assert.Throws<InvalidOperationException>(() => game.AddPlayer("PlayerTwo"));
        Assert.That(ex.Message, Does.Contain("Game already started"));
    }
}
