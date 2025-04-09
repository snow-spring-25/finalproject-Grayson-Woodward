using System;
using Final.Logic;
namespace Final.tests;
public class Feature2
{
    [Test] // REQ#1.2.1
    public void MakeGuessShouldAllowMakingCorrectGuessesWhenItIsPlayer1sTurn()
    {

        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category);
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        var result = game.MakeGuess('a', "Player1");

        Assert.That(result, Is.True);
        Assert.That(game.CurrentMaskedWord.Contains('a'), Is.True);
    }
    [Test] // REQ#1.2.2
    public void MakeGuessShouldThrowErrorWhenItIsNotPlayer1sTurn()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category);
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        var ex = Assert.Throws<InvalidOperationException>(() => game.MakeGuess('v', "Player2"));
        Assert.That(ex.Message, Is.EqualTo("It's not your turn"));
    }

}
