namespace Final.Logic;
    public interface IGame //Req 1.2.3
    {
        void StartGame(WordCategory category);
        bool MakeGuess(char letter, string playerId);
        bool IsGameOver { get; }
        event Action<string> OnGameEnd;
    }