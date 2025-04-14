namespace Final.Logic;
public interface IHangmanGame //req 1.2.3
{
    void AddPlayer(string playerName);
    bool MakeGuess(char letter, string playerName);
    void Start();
    string CurrentMaskedWord { get; }
    bool IsGameOver { get; }
    string CurrentPlayer { get; }
}