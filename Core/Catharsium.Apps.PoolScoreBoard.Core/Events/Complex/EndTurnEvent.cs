using Catharsium.Apps.PoolScoreboard.Core.GameActions;
using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Core.Events.Complex;

public class EndTurnEvent : ComplexEvent
{
    private readonly int ballsOnTable;
    private readonly int foulPoints;
    private readonly ShotType lastShot;


    public EndTurnEvent(int ballsOnTable, int foulPoints, ShotType lastShot) {
        this.ballsOnTable = ballsOnTable;
        this.foulPoints = foulPoints;
        this.lastShot = lastShot;
    }


    public override void Apply(GameState gameState) {
        if (gameState.BallsOnTable < ballsOnTable) {
            gameState.BallsOnTable += 14;
        }
        var score = gameState.BallsOnTable - ballsOnTable;
        this.ApplyChildEvent(new ScoreEvent(score), gameState);

        if (foulPoints > 0) {
            this.ApplyChildEvent(new FoulEvent(foulPoints), gameState);
        }

        this.ApplyChildEvent(new SwitchEvent(this.lastShot), gameState);
    }
}