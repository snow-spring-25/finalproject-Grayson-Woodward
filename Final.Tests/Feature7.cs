using System;
using Final.Logic;
namespace Final.Tests;

public class Feature7
{
    [Test]
    public void GetPlayerScoreExistingPlayerReturnsCorrectScore() // Req 1.7.1
    {   
        var game = new MultiplayerHangmanGame(WordCategory.StarWars, new TestRandomSource(1),new TestRepository());
        game.AddPlayer("Luke");
        game.Start();

        var wordToGuessField = typeof(MultiplayerHangmanGame)
            .GetField("wordToGuess", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var word = (string)wordToGuessField.GetValue(game);

        char firstLetter = word.ToLower().First();
        game.MakeGuess(firstLetter, "Luke");

        int expectedScore = game.PlayerScores["Luke"];
        int score = game.GetPlayerScore("Luke");
        Assert.That(score, Is.EqualTo(expectedScore));
    }

    [Test]
    public void GetPlayerScoreNonExistentPlayerThrowsArgumentException() // Req 1.7.2
    {
        var game = new MultiplayerHangmanGame(WordCategory.StarWars, new TestRandomSource(1), new TestRepository());
        game.AddPlayer("Kaladin");
        game.Start();

        var ex = Assert.Throws<ArgumentException>(() => game.GetPlayerScore("Vin"));
        Assert.That(ex.Message, Does.Contain("Player not found"));
    }
}
