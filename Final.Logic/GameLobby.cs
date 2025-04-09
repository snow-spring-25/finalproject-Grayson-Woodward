using System;

namespace Final.Logic;

    public static class GameLobby
    {
        private static readonly List<HangmanGame> games = new();

        public static HangmanGame CreateGame(WordCategory category)
        {
            var game = new HangmanGame(category);
            games.Add(game);
            return game;
        }

        public static HangmanGame? JoinGame(WordCategory category)
        {
            return games.FirstOrDefault(g => g.Category == category && !g.Started);
        }

        public static List<HangmanGame> GetAllGames() => games;
    }