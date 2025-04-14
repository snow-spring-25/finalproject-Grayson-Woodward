using System;
namespace Final.Logic;
using SQLite;
public class ScoreEntry //Req 1.8.3
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string PlayerName { get; set; }

    public int Score { get; set; }
}
