using System;

namespace Final.Logic;

    public class MultiplayerHangmanGame : BaseHangmanGame
    {
        private static readonly Dictionary<WordCategory, List<string>> wordsByCategory = new()
        {
            { WordCategory.StarWars, new() { "vader", "yoda", "lightsaber" } },
            { WordCategory.Cosmere, new() { "storm", "shardblade", "fabrial" } },
            { WordCategory.Foods, new() { "pizza", "sushi", "lasagna" } }
        };

        private WordCategory category;

        public MultiplayerHangmanGame(WordCategory category)
        {
            this.category = category;
        }

        protected override string PickRandomWord()
        {
            var wordList = wordsByCategory[category];
            var rnd = new Random();
            return wordList[rnd.Next(wordList.Count)];
        }
    }
