namespace Final.Logic;

    public interface IGame
    {
        void StartGame(WordCategory category, string playerId);
        bool MakeGuess(char letter, string playerId);
        bool IsGameOver { get; }
        event Action<string> OnGameEnd;
    }