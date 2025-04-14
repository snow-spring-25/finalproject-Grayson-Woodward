using System;
using Final.Logic;
using System.Reflection;
namespace Final.Tests;

public class Feature8
{
    [Test]
    public void EndGameAddsScoreWhenPlayerWins()
    {
        var repo = new TestRepository();
        var random = new TestRandomSource(0);
        var game = new MultiplayerHangmanGame(WordCategory.StarWars, random, repo);

        game.AddPlayer("Luke");
        game.Start();

        var wordToGuessField = typeof(MultiplayerHangmanGame)
            .BaseType!
            .GetField("wordToGuess", BindingFlags.Instance | BindingFlags.NonPublic);
        var word = (string)wordToGuessField!.GetValue(game)!;
        Console.WriteLine($"Actual word: {word}");

        foreach (var c in word.ToLower().Distinct())
        {
            game.MakeGuess(c, "Luke");
        }

        var scores = repo.GetTopScores();

        Assert.That(scores.Count, Is.EqualTo(1), "Expected 1 score entry after game ends.");
        Assert.That(scores[0].PlayerName, Is.EqualTo("Luke"));
        Assert.That(scores[0].Score, Is.EqualTo(word.Length * 5));
    }
}
