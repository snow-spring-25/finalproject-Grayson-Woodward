using System;

namespace Final.Logic;

public abstract class BaseHangmanGame : IHangmanGame // Req 1.2.3
{
    protected readonly int maxAttempts = 5; //Req1.5.3
    protected int attemptsLeft; // Req 1.5.3
    protected string wordToGuess = "";
    protected HashSet<char> guessedLetters = new();
    protected List<string> players = new();
    protected int currentPlayerIndex = 0;
    protected bool started = false;
    protected Dictionary<string, int> playerScores = new Dictionary<string, int>();

    public bool IsGameOver => attemptsLeft <= 0 || wordToGuess.All(c => guessedLetters.Contains(char.ToLower(c)));

    public string CurrentPlayer => players.Count > 0 ? players[currentPlayerIndex] : null;

    public string CurrentMaskedWord =>
        new string(wordToGuess.Select(c => guessedLetters.Contains(char.ToLower(c)) ? c : '_').ToArray()); // Req 1.2.3

    public string GameResult { get; private set; } = string.Empty;
    public Dictionary<string, int> PlayerScores => playerScores;

    protected void EndGame()
    {
        if (IsGameOver)
        {
            if (wordToGuess.All(c => guessedLetters.Contains(char.ToLower(c))))
            {
                GameResult = "You won!";
            }
            else if (attemptsLeft <= 0)
            {
                GameResult = "You let the person die.";
            }
        }
    }

    public virtual void AddPlayer(string playerName) // Req 1.1.3
    {
        if (started) throw new InvalidOperationException("Game already started"); // Req 1.6.3
        players.Add(playerName);
        playerScores[playerName] = 0;
    }

    public virtual void Start() // Req 1.1.3
    {
        if (!players.Any())
            throw new InvalidOperationException("Cannot start game without players"); // Req 1.6.3

        started = true;
        attemptsLeft = maxAttempts;
        wordToGuess = PickRandomWord();
    }

public virtual bool MakeGuess(char letter, string playerName) // Req 1.2.3
{
    if (!started) throw new InvalidOperationException("Game not started");
    if (IsGameOver) throw new InvalidOperationException("Game is over");
    if (playerName != CurrentPlayer) throw new InvalidOperationException("It's not your turn"); // Req 1.4.3, // Req 1.6.3

    letter = char.ToLower(letter);

    if (guessedLetters.Contains(letter)) // Req 1.4.3
        throw new InvalidOperationException("Letter already guessed"); // Req 1.6.3

    guessedLetters.Add(letter);

    bool correctGuess = wordToGuess.ToLower().Contains(letter);
    if (correctGuess && playerName == CurrentPlayer) 
    {
        playerScores[playerName] += wordToGuess.Length * 5;
    }
    else
    {
        attemptsLeft--;
    }

    if (!IsGameOver && attemptsLeft > 0)
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
    }

    EndGame(); 

    return correctGuess;
}

    protected abstract string PickRandomWord();// Req 1.2.3
}