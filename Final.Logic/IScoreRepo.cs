namespace Final.Logic;
public interface IScoreRepo
{
    public void AddScore(string playerName, int score);

    public List<ScoreEntry> GetTopScores(int count = 10);

}
