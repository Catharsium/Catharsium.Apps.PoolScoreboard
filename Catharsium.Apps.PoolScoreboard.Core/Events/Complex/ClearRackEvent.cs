using Catharsium.Apps.PoolScoreboard.Core.GameActions;
using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Core.Events.Complex;

public class ClearRackEvent : ComplexEvent
{
    public override void Apply(StraightPoolMatch gameState) {
        var score = gameState.BallsOnTable - 1;
        this.ApplyChildEvent(new ScoreEvent(score), gameState);
    }
}