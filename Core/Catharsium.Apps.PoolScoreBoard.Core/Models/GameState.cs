namespace Catharsium.Apps.PoolScoreboard.Core.Models;

public class GameState
{
    public Player[] Players { get; set; }
    public List<Turn> Turns { get; set; }
    public int CurrentTurn { get; set; } = 0;
    public int CurrentPlayer { get; set; } = 0;
    public int BallsOnTable { get; set; } = 15;


    public GameState(params string[] players) {
        this.Players = players.Select(p => new Player(p)).ToArray();
        this.Turns = new List<Turn> {
            new Turn(this.Players[0].Id, this.CurrentTurn)
        };
    }
}