using System;
using Final.Logic;
namespace Final.Tests;

public class Feature5
{
    [Test] // Req 1.5.1
    public void Player1MakesTheRightGuessAndGetsPoints()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category, new TestRandomSource(0), new TestRepository());
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        game.MakeGuess('v', "Player1");
        game.MakeGuess('a', "Player2");
        game.MakeGuess('d', "Player1");
        game.MakeGuess('e', "Player2");
        game.MakeGuess('r', "Player1");

        Assert.That(game.PlayerScores["Player1"], Is.EqualTo(25));
    }
    [Test] // Req 1.5.2
    public void Player2DoesntGetPoints()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category, new TestRandomSource(0), new TestRepository());
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        game.MakeGuess('v', "Player1");
        game.MakeGuess('a', "Player2");
        game.MakeGuess('d', "Player1");
        game.MakeGuess('e', "Player2");
        game.MakeGuess('r', "Player1");

        Assert.That(game.PlayerScores["Player2"], Is.EqualTo(0));
    }

    [Test] // Req 1.5.1
    public void Player1MakesTheRightGuessAttemptsLeftDoesntChange()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category, new TestRandomSource(0), new TestRepository());
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        game.MakeGuess('v', "Player1");
        Assert.That(game.AttemptsLeft, Is.EqualTo(5));
    }

    [Test] // Req 1.5.1
    public void Player1MakesTheWrongGuessAndAttemptsLeftDoesChange()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category, new TestRandomSource(0), new TestRepository());
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        game.MakeGuess('t', "Player1");
        Assert.That(game.AttemptsLeft, Is.EqualTo(4));
    }

    [Test] // Req 1.5.1
    public void PlayerCanGuessWord()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category, new TestRandomSource(0), new TestRepository());
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        game.GuessWord("vader", "Player1");

        Assert.That(game.PlayerScores["Player1"], Is.EqualTo(50));
    }

    [Test] // Req 1.5.2
    public void PlayerGuessesWrongWord()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category, new TestRandomSource(0), new TestRepository());
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        game.GuessWord("Luke", "Player1");
        game.GuessWord("vader", "Player2");

        Assert.That(game.PlayerScores["Player1"], Is.EqualTo(0));
    }
}
