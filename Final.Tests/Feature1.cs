namespace Final.Tests;
using Final.Logic;
public class Feature1 //req1.1.1 & 1.1.2
{
    [Test]
    public void CreateGameThenJoinGameShouldSucceed()
    {
        var game = GameLobby.CreateGame(WordCategory.StarWars);
        game.AddPlayer("PlayerOne");

        var joinedGame = GameLobby.JoinGame(WordCategory.StarWars);

        Assert.That(joinedGame, Is.Not.Null);
        Assert.That(joinedGame.Players, Does.Contain("PlayerOne"));
        Assert.That(joinedGame.Started, Is.False);
    }

    [Test]
    public void JoinGameAfterGameStartedShouldReturnNull()
    {
        var game = GameLobby.CreateGame(WordCategory.Cosmere);
        game.AddPlayer("PlayerOne");
        game.Start(); 

        var joined = GameLobby.JoinGame(WordCategory.Cosmere);

        Assert.That(joined, Is.Null);
    }

    [Test]
    public void StartGameWithoutPlayersShouldThrow()
    {
        var game = GameLobby.CreateGame(WordCategory.Cosmere);
        var ex = Assert.Throws<InvalidOperationException>(() => game.Start());

        Assert.That(ex.Message, Does.Contain("Cannot start game without players"));
    }

    [Test]
    public void AddPlayerAfterGameStartedShouldThrow()
    {
        var game = GameLobby.CreateGame(WordCategory.StarWars);
        game.AddPlayer("PlayerOne");
        game.Start();

        var ex = Assert.Throws<InvalidOperationException>(() => game.AddPlayer("PlayerTwo"));
        Assert.That(ex.Message, Does.Contain("Game already started"));
    }
}
