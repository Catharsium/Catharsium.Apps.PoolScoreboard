using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Analytics;

public class ScoreAnalyzer
{
    public int Total(IEnumerable<Turn> turns) {
        return turns.Sum(t => t.BallsPotted - t.FoulPoints);
    }


    public double Average(IEnumerable<Turn> turns) {
        return this.Total(turns.Where(t => t.BallsPotted > 0));
    }
}