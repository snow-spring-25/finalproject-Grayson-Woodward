using System;
using Final.Logic;
namespace Final.Tests;

public class Feature8
{
    private string dbPath;
    private ScoreRepository repo;

    [SetUp]
    public void Setup()
    {
        dbPath = Path.GetTempFileName();
        repo = new ScoreRepository(dbPath);
    }

    [TearDown]
    public void Cleanup()
    {
        if (File.Exists(dbPath))
            File.Delete(dbPath);
    }

    [Test]
    public void GetTopScores_ReturnsScoresInDescendingOrder()
    {
        repo.AddScore("Alice", 120);
        repo.AddScore("Bob", 200);
        repo.AddScore("Charlie", 150);

        List<ScoreEntry> topScores = repo.GetTopScores();

        Assert.That(topScores, Has.Count.EqualTo(3));
        Assert.That(topScores[0].PlayerName, Is.EqualTo("Bob"));
        Assert.That(topScores[1].PlayerName, Is.EqualTo("Charlie"));
        Assert.That(topScores[2].PlayerName, Is.EqualTo("Alice"));
    }

    [Test]
    public void GetTopScores_WhenNoScores_ReturnsEmptyList()
    {
        List<ScoreEntry> topScores = repo.GetTopScores();

        Assert.That(topScores, Is.Empty);
    }
}
