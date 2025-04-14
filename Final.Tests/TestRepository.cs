using Final.Logic;
namespace Final.Tests;
public class TestRepository : IScoreRepo
{
    List<ScoreEntry> entries = new List<ScoreEntry>();
    public void AddScore(string playerName, int score)
    {
        entries.Add(new ScoreEntry(){
            PlayerName = playerName, 
            Score = score,
        });
    }

    public List<ScoreEntry> GetTopScores(int count = 10)
    {
        return entries;
    }
}