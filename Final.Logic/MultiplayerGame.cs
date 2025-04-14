using System;

namespace Final.Logic;

public class MultiplayerHangmanGame : BaseHangmanGame
{
    private static readonly Dictionary<WordCategory, List<string>> wordsByCategory = new()
        {
            { WordCategory.StarWars, new() { "vader", "yoda", "lightsaber", "Anakin", "Endor", "Hoth", "Skywalker"} },
            { WordCategory.Cosmere, new() { "storm", "shardblade", "fabrial", "Kaladin", "Dalinar", "Windrunner", "Mistborn", "Harmony" } },
            { WordCategory.Foods, new() { "pizza", "sushi", "lasagna", "Steak", "Pineapple", "Calazone", "Pretzel", "Hamburger" } }
        };

    private WordCategory category;
    private readonly IRandomSource random;

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
}
