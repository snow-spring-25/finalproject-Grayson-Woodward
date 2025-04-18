using System;
using Final.Logic;

namespace Final.Tests;

public class Feature1
{
    [Test] // REQ#1.1.1
    public void AddPlayerShouldIncludePlayerInGame()
    {
        var game = new MultiplayerHangmanGame(WordCategory.StarWars, new TestRandomSource(0), new TestRepository());
        game.AddPlayer("PlayerOne");

        Assert.That(game.players, Does.Contain("PlayerOne"));
        Assert.That(game.CurrentPlayer, Is.EqualTo("PlayerOne")); 
    }

    [Test] // REQ#1.1.2
    public void AddPlayerAfterGameStartedShouldThrow()
    {
        var game = new MultiplayerHangmanGame(WordCategory.StarWars, new TestRandomSource(0), new TestRepository());
        game.AddPlayer("PlayerOne");
        game.Start();

        var ex = Assert.Throws<InvalidOperationException>(() => game.AddPlayer("PlayerTwo"));

        Assert.That(ex.Message, Does.Contain("You cannot add players after the game has started. Please wait for a new round to join."));
    }

    [Test] // REQ#1.1.3
    public void StartGameWithoutPlayersShouldThrow()
    {
        var game = new MultiplayerHangmanGame(WordCategory.StarWars, new TestRandomSource(0), new TestRepository());

        var ex = Assert.Throws<InvalidOperationException>(() => game.Start());

        Assert.That(ex.Message, Does.Contain("Cannot start the game without at least one player. Please add players first"));
    }

    [Test] // REQ#1.1.4
    public void StartGameWithMultiplePlayersShouldInitializeCurrentPlayer()
    {
        var game = new MultiplayerHangmanGame(WordCategory.StarWars, new TestRandomSource(0), new TestRepository());
        game.AddPlayer("PlayerOne");
        game.AddPlayer("PlayerTwo");

        game.Start();

        Assert.That(game.CurrentPlayer, Is.EqualTo("PlayerOne")); 
        Assert.That(game.CurrentMaskedWord.Length, Is.GreaterThan(0)); 
    }
}

