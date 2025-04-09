using System;
using Final.Logic;
namespace Final.Tests;

public class Feature3
{
    [Test] // Req 1.3.3
    public void MakeGuess_ShouldNotifyPlayers_WhenGameEnds_WithWin()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category);
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        game.MakeGuess('v', "Player1");
        game.MakeGuess('a', "Player2");
        game.MakeGuess('d', "Player1");
        game.MakeGuess('e', "Player2");
        game.MakeGuess('r', "Player1");

        Assert.That(game.GameResult, Is.EqualTo("You won!"));
    }
    [Test] // Req 1.3.3
    public void MakeGuess_ShouldNotifyPlayers_WhenGameEnds_WithLoss()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category);
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        game.MakeGuess('x', "Player1");
        game.MakeGuess('z', "Player2");
        game.MakeGuess('u', "Player1");
        game.MakeGuess('q', "Player2");
        game.MakeGuess('c', "Player1");

        Assert.That(game.GameResult, Is.EqualTo("You let the person die."));
    }
}
