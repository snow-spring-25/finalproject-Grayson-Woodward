using System;
using SQLite;
namespace Final.Logic;

public class ScoreRepository : IScoreRepo //Req 1.8.3
{
    private readonly SQLiteConnection db;

    public ScoreRepository(string dbPath)
    {
        db = new SQLiteConnection(dbPath);
        db.CreateTable<ScoreEntry>();
    }

    public void AddScore(string playerName, int score)
    {
        db.Insert(new ScoreEntry { PlayerName = playerName, Score = score });
    }

    public List<ScoreEntry> GetTopScores(int count = 10)
    {
        return db.Table<ScoreEntry>()
                 .OrderByDescending(s => s.Score)
                 .Take(count)
                 .ToList();
    }
}

