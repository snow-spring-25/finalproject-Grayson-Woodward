using System;

namespace Final.Logic;

    public abstract class GameBase : IGame //Req 1.2.3
    {
        private static readonly Dictionary<WordCategory, List<List<string>>> WordLevels = new()
        {
            { WordCategory.StarWars, new List<List<string>>
                {
                    new() { "Vader", "Yoda", "Luke" },
                    new() { "Lightsaber", "DeathStar", "JediKnight" },
                    new() { "Mandalorian", "Coruscant", "Hyperspace" }
                }
            },
            { WordCategory.Cosmere, new List<List<string>>
                {
                    new() { "Storm", "Mist", "Ash" },
                    new() { "Shardblade", "Lerasium", "Fabrial" },
                    new() { "Investiture", "Hemalurgy", "CognitiveRealm" }
                }
            },
            { WordCategory.Foods, new List<List<string>>
                {
                    new() { "Pizza", "Sushi", "Apple" },
                    new() { "Lasagna", "Croissant", "Tortilla" },
                    new() { "Bouillabaisse", "Cacciatore", "Stroganoff" }
                }
            }
        };

        public string WordToGuess { get; protected set; }
        public HashSet<char> GuessedLetters { get; protected set; } = new();
        public int MaxAttempts { get; protected set; } = 5;
        public int AttemptsLeft { get; protected set; }
        public int CurrentLevel { get; private set; } = 0;
        public WordCategory CurrentCategory { get; private set; }
        public bool IsGameOver => AttemptsLeft <= 0 || WordToGuess.All(c => GuessedLetters.Contains(c));
        public event Action<string> OnGameEnd;

        public void StartGame(WordCategory category)
        {
            CurrentCategory = category;
            CurrentLevel = 0;
            LoadNewWord();
        }

        private void LoadNewWord()
        {
            var words = WordLevels[CurrentCategory][CurrentLevel];
            var random = new Random();
            WordToGuess = words[random.Next(words.Count)];
            GuessedLetters.Clear();
            AttemptsLeft = MaxAttempts;
        }

        public bool MakeGuess(char letter, string playerId)
        {
            if (IsGameOver || GuessedLetters.Contains(letter))
                return false;

            GuessedLetters.Add(letter);

            if (!WordToGuess.Contains(letter))
            {
                AttemptsLeft--;
            }

            if (IsGameOver)
            {
                if (WordToGuess.All(c => GuessedLetters.Contains(c)))
                {
                    if (CurrentLevel < 2)
                    {
                        CurrentLevel++;
                        LoadNewWord();
                    }
                    else
                    {
                        OnGameEnd?.Invoke("You win!");
                    }
                }
                else
                {
                    OnGameEnd?.Invoke("You let the person die.");
                }
            }
            return true;
        }
    }