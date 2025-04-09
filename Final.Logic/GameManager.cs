using System;

namespace Final.Logic;

public static class GameManager 
{
    private static readonly Dictionary<string, MultiplayerHangman> ActiveGames = new();

    public static MultiplayerHangman CreateGame(string gameId, string playerName) //Req 1.1.3
    {
        if (ActiveGames.ContainsKey(gameId))
            throw new InvalidOperationException("Game already exists.");

        var game = new MultiplayerHangman(gameId);
        game.AddPlayer(playerName);
        ActiveGames[gameId] = game;
        return game;
    }

    public static MultiplayerHangman JoinGame(string gameId, string playerName) //Req 1.1.3
    {
        if (!ActiveGames.TryGetValue(gameId, out var game))
            throw new InvalidOperationException("Game not found.");

        game.AddPlayer(playerName);
        return game;
    }
}