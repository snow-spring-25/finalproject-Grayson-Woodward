using System;

namespace Final.Logic;

public abstract class BaseHangmanGame : IHangmanGame // Req 1.2.3
{
    protected readonly int maxAttempts = 5; // Req 1.5.3
    protected int attemptsLeft; // Req 1.5.3
    protected string wordToGuess = "";
    protected HashSet<char> guessedLetters = new();
    public List<string> players = new();
    protected int currentPlayerIndex = 0;
    protected bool started = false;
    protected Dictionary<string, int> playerScores = new Dictionary<string, int>();
    protected readonly IScoreRepo scoreRepo;
    public event Action? OnGameStateChanged;
    protected HashSet<char> incorrectGuesses = new();
    public int AttemptsLeft => attemptsLeft;

    public BaseHangmanGame(IScoreRepo scoreRepo)
    {
        this.scoreRepo = scoreRepo;
    }

    public bool IsGameOver => attemptsLeft <= 0 || wordToGuess.All(c => guessedLetters.Contains(char.ToLower(c)));

    public string CurrentPlayer => players.Count > 0 ? players[currentPlayerIndex] : null;

    public string CurrentMaskedWord =>
        new string(wordToGuess.Select(c => guessedLetters.Contains(char.ToLower(c)) ? c : '_').ToArray()); // Req 1.2.3

    public string GameResult { get; private set; } = string.Empty;
    public Dictionary<string, int> PlayerScores => playerScores;

    protected void EndGame() // Req 1.3.3
    {
        if (IsGameOver)
        {
            if (wordToGuess.All(c => guessedLetters.Contains(char.ToLower(c))))
            {
                GameResult = "You won! The person didn't die";
            }
            else if (attemptsLeft <= 0)
            {
                GameResult = "You let the person die.";
            }
            
            foreach (var player in playerScores) //Req 1.8.3
            {
                scoreRepo.AddScore(player.Key, player.Value);
            }

            OnGameStateChanged?.Invoke();
        }
    }

    public virtual void AddPlayer(string playerName) // Req 1.1.3
    {
        if (started)
            throw new InvalidOperationException("You cannot add players after the game has started. Please wait for a new round to join."); // Req 1.6.3

        players.Add(playerName);
        playerScores[playerName] = 0;

        OnGameStateChanged?.Invoke();
    }

    public virtual void Start() // Req 1.1.3
    {
        if (!players.Any())
            throw new InvalidOperationException("Cannot start the game without at least one player. Please add players first."); // Req 1.6.3

        started = true;
        ResetGame();
        OnGameStateChanged?.Invoke();
    }

    public bool MakeGuess(char letter, string playerName)
    {
        if (!started)
            throw new InvalidOperationException("The game has not started yet. Please start the game first."); // Req 1.6.3

        if (IsGameOver)
            throw new InvalidOperationException("The game is over. Please start a new game."); // Req 1.6.3

        if (playerName != CurrentPlayer)
            throw new InvalidOperationException($"It's not your turn, {playerName}. Please wait for {CurrentPlayer} to make a move."); // Req 1.6.3

        letter = char.ToLower(letter);

        if (guessedLetters.Contains(letter))
            throw new InvalidOperationException($"The letter '{letter}' has already been guessed. Please try a different letter."); // Req 1.6.3

        guessedLetters.Add(letter);

        bool correctGuess = wordToGuess.ToLower().Contains(letter);

        bool wordCompleted = wordToGuess.All(c => guessedLetters.Contains(char.ToLower(c))); // Req 1.5.3

        if (correctGuess && wordCompleted) // Req 1.5.3
        {
            playerScores[playerName] += wordToGuess.Length * 5;
        }
        else if (!correctGuess)
        {
            incorrectGuesses.Add(letter);
            attemptsLeft--;
        }

        if (!IsGameOver && attemptsLeft > 0)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        EndGame();
        OnGameStateChanged?.Invoke();

        return correctGuess;
    }

    public IEnumerable<char> GetIncorrectGuesses() => incorrectGuesses;

    public int GetPlayerScore(string playerName) // Req 1.7.3
    {
        if (!playerScores.ContainsKey(playerName))
            throw new ArgumentException("Player not found."); 

        return playerScores[playerName];
    }

    protected abstract string PickRandomWord(); // Req 1.2.3

    public void StartNewRound()
    {
        players.Clear();
        PlayerScores.Clear();
        started = false;
        ResetGame();
    }

    public void ResetGame()
    {
        attemptsLeft = maxAttempts;
        wordToGuess = PickRandomWord();
        guessedLetters.Clear();
        incorrectGuesses.Clear();
        GameResult = string.Empty;

        OnGameStateChanged?.Invoke();
    }
}