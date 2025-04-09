using System;

namespace Final.Logic;

public abstract class BaseHangmanGame : IHangmanGame
{
    protected readonly int maxAttempts = 5;
    protected int attemptsLeft;
    protected string wordToGuess = "";
    protected HashSet<char> guessedLetters = new();
    protected List<string> players = new();
    protected int currentPlayerIndex = 0;
    protected bool started = false;

    public bool IsGameOver => attemptsLeft <= 0 || wordToGuess.All(c => guessedLetters.Contains(char.ToLower(c)));

    public string CurrentPlayer => players.Count > 0 ? players[currentPlayerIndex] : null;

    public string CurrentMaskedWord =>
        new string(wordToGuess.Select(c => guessedLetters.Contains(char.ToLower(c)) ? c : '_').ToArray()); // REQ#1.2.2

    public virtual void AddPlayer(string playerName)
    {
        if (started) throw new InvalidOperationException("Game already started");
        players.Add(playerName);
    }

    public virtual void Start()
    {
        if (!players.Any())
            throw new InvalidOperationException("Cannot start game without players");

        started = true;
        attemptsLeft = maxAttempts;
        wordToGuess = PickRandomWord();
    }

    public virtual bool MakeGuess(char letter, string playerName)
    {
        if (!started) throw new InvalidOperationException("Game not started");
        if (IsGameOver) throw new InvalidOperationException("Game is over");
        if (playerName != CurrentPlayer) throw new InvalidOperationException("It's not your turn");

        letter = char.ToLower(letter);
        if (guessedLetters.Contains(letter))
            throw new InvalidOperationException("Letter already guessed");

        guessedLetters.Add(letter);
        if (!wordToGuess.ToLower().Contains(letter))
        {
            attemptsLeft--;
        }
        if (!IsGameOver && attemptsLeft > 0)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        return true;
    }

    protected abstract string PickRandomWord();
}