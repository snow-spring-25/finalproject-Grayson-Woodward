using System;
using System.Net.Cache;

namespace Final.Logic;

public class MultiplayerHangmanGame : BaseHangmanGame //Req 2.1.2
{
    public static MultiplayerHangmanGame Instance { get; }
    private static readonly Dictionary<WordCategory, List<string>> wordsByCategory = new()
        {
            { WordCategory.StarWars, new() { "vader", "yoda", "lightsaber", "Anakin", "Endor", "Hoth", "Skywalker", "Leia",  "stormtrooper", "Palpatine", "Maul", "Padawan", "Master", "Wookie", "Chewbaca", "blaster", "Mandalorian"} },
            //{ WordCategory.Cosmere, new() { "storm", "shardblade", "fabrial", "Kaladin", "Dalinar", "Windrunner", "Mistborn", "Harmony" } },
            //{ WordCategory.Foods, new() { "pizza", "sushi", "lasagna", "Steak", "Pineapple", "Calazone", "Pretzel", "Hamburger" } }
        }; //couldn't get it to work on time. Ill do it during summer.

    private WordCategory category;
    private readonly IRandomSource random;
    static MultiplayerHangmanGame()
    {
        var repo = new ScoreRepository("data.Db");
        var random = new SystemRandomSource();
        Instance = new(WordCategory.StarWars, random, repo);
    }

    public MultiplayerHangmanGame(WordCategory category, IRandomSource random, IScoreRepo repo) : base(repo)
    {
        this.category = category;
        this.random = random;
    }

    protected override string PickRandomWord()
    {
        var wordList = wordsByCategory[category];
        return wordList[random.Next(wordList.Count)];
    }

    public List<ScoreEntry> GetTopScores(int count = 10) => scoreRepo.GetTopScores(count);
}