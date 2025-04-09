namespace Final.Logic;
public class MultiplayerHangman //Req.1.1.3
{
    public string GameId { get; }
    public List<string> Players { get; } = new();

    public MultiplayerHangman(string gameId)
    {
        GameId = gameId;
    }

    public void AddPlayer(string playerName)
    {
        if (!Players.Contains(playerName))
            Players.Add(playerName);
    }
}