using Catharsium.Apps.PoolScoreboard.Core.GameActions;
using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Core.Events.Complex;

public class EndTurnEvent : ComplexEvent
{
    private readonly int ballsOnTable;
    private readonly int foulPoints;
    private readonly ShotType lastShot;


    public EndTurnEvent(ShotType lastShot, int ballsOnTable = 0, int foulPoints = 0) {
        this.lastShot = lastShot;

        this.ballsOnTable = ballsOnTable;
        this.foulPoints = foulPoints;
    }


    public override void Apply(StraightPoolMatch gameState) {
        var actualBallsOnTable = this.ballsOnTable;
        if (actualBallsOnTable == 0) {
            actualBallsOnTable = gameState.BallsOnTable;
        }

        var score = gameState.BallsOnTable - actualBallsOnTable;
        this.ApplyChildEvent(new ScoreEvent(score), gameState);

        if (foulPoints > 0) {
            this.ApplyChildEvent(new FoulEvent(foulPoints), gameState);
        }

        this.ApplyChildEvent(new SwitchEvent(this.lastShot), gameState);
    }
}