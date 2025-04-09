namespace Final.Logic;
    public class HangmanGame //Req1.1.3
    {
        public string GameId { get; } = Guid.NewGuid().ToString(); 
        public WordCategory Category { get; }
        public List<string> Players { get; } = new();
        public bool Started { get; private set; } = false;

        public HangmanGame(WordCategory category)
        {
            Category = category;
        }

        public void AddPlayer(string playerName)
        {
            if (Started)
                throw new InvalidOperationException("REQ#1.1.2: Game already started.");
            
            if (!Players.Contains(playerName))
                Players.Add(playerName);
        }

        public void Start()
        {
            if (Players.Count == 0)
                throw new InvalidOperationException("REQ#1.1.2: Cannot start game without players.");

            Started = true;
        }
    }
