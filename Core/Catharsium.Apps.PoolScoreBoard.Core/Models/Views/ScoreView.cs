namespace Catharsium.Apps.PoolScoreboard.Core.Models.Views;

public class ScoreView
{
    public PlayerScore[] PlayerScores { get; set; }
    public int BallsOnTable { get; set; }
    public int CurrentPlayerIndex { get; set; }
}