namespace Catharsium.Apps.PoolScoreboard.Core.Models;

public class Turn
{
    public Guid PlayerId { get; set; }
    public int TurnNumber { get; set; }
    public int BallsPotted { get; set; }
    public int FoulPoints { get; set; }
    public ShotType? LastShot { get; set; }


    public Turn(Guid playerId, int turnNumber) {
        this.PlayerId = playerId;
        this.TurnNumber = turnNumber;
    }
}