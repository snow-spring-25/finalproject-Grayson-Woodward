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

    public void AddScore(string playerName, int score)  //Req 4.1.1
    {
        var allScores = db.Table<ScoreEntry>().ToList(); //Req 5.1.1
        var existingScore = allScores.FirstOrDefault(s => s.PlayerName == playerName);
        if (existingScore != null)
        {
            existingScore.Score += score;
            db.Update(existingScore);
            return;
        }

        db.Insert(new ScoreEntry { PlayerName = playerName, Score = score });
    }

    public List<ScoreEntry> GetTopScores(int count = 10) //Req 4.1.2 also happens in leaderboard razor
    {
        return db.Table<ScoreEntry>()//Req 5.1.1
                 .OrderByDescending(s => s.Score)
                 .Take(count)
                 .ToList();
    }
}