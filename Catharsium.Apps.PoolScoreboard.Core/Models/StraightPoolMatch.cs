using Catharsium.Apps.PoolScoreboard.Core.Models.Base;

namespace Catharsium.Apps.PoolScoreboard.Core.Models;

public class StraightPoolMatch : Match
{
    public int BallsOnTable { get; set; } = 15;


    public StraightPoolMatch(int raceTo, IEnumerable<Player> players)
        : base(raceTo, players) {
    }
}