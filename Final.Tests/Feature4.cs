using System;
using Final.Logic;
namespace Final.Tests;
public class Feature4
{
    [Test] //Req 1.4.1
    public void MakeGuessShouldThrowErrorWhenItIsNotPlayer1sTurn()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category, new TestRandomSource(1),new TestRepository());
        game.AddPlayer("Player1");
        game.AddPlayer("Player2");
        game.Start();

        var ex = Assert.Throws<InvalidOperationException>(() => game.MakeGuess('v', "Player2"));
        Assert.That(ex.Message, Is.EqualTo("It's not your turn, Player2. Please wait for Player1 to make a move."));
    }
    [Test] //Req 1.4.2
    public void MakeGuessShouldThrowErrorWhenLetterIsAlreadyGuessed()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category, new TestRandomSource(0),new TestRepository());
        game.AddPlayer("Player1");
        game.Start();

        game.MakeGuess('v', "Player1");

        var ex = Assert.Throws<InvalidOperationException>(() => game.MakeGuess('v', "Player1"));
        Assert.That(ex.Message, Is.EqualTo("The letter 'v' has already been guessed. Please try a different letter."));
    }
}
