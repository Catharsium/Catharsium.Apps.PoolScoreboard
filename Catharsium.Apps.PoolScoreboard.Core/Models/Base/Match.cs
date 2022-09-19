using System.Diagnostics.CodeAnalysis;

namespace Catharsium.Apps.PoolScoreboard.Core.Models.Base;

public abstract class Match
{
    public readonly Player[] Players;
    public List<Turn> Turns { get; set; } = new List<Turn>();
    public int CurrentTurn { get; set; } = 0;
    public int CurrentPlayer { get; set; } = 0;
    public int RaceTo { get; set; } = 0;


    public Match(int raceTo, [NotNull] IEnumerable<Player> players) {
        this.RaceTo = raceTo;
        this.Players = players.ToArray();
        if (players.Any()) {
            Turns.Add(new Turn(Players[0].Id, CurrentTurn));
        }
    }
}