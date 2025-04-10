using System;
using Final.Logic;
namespace Final.Tests;

public class feature6
{
    [Test] // Req 1.6.1
    public void StartShouldStartTheGameWhenPlayersExist()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category);
        game.AddPlayer("Player1");

        game.Start();

        Assert.That(game.IsGameOver, Is.False);
        Assert.That(game.CurrentPlayer, Is.EqualTo("Player1"));
    }
    [Test] // Req 1.6.2
    public void StartShouldThrowErrorWhenNoPlayersExist()
    {
        var category = WordCategory.StarWars;
        var game = new MultiplayerHangmanGame(category);

        var ex = Assert.Throws<InvalidOperationException>(() => game.Start());
        Assert.That(ex.Message, Is.EqualTo("Cannot start the game without at least one player. Please add players first.")); // Req 1.6.3
    }
}
