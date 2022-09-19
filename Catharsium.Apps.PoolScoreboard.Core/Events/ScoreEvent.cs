using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Core.GameActions;

public class ScoreEvent : IGameEvent
{
    private readonly int points;


    public ScoreEvent(int points) {
        this.points = points;
    }


    public void Apply(GameState gameState) {
        gameState.Turns.Last().BallsPotted += this.points;
        gameState.BallsOnTable -= this.points;
        if (gameState.BallsOnTable <= 1) {
            gameState.BallsOnTable += 14;
        }
    }


    public void Undo(GameState gameState) {
        gameState.Turns.Last().BallsPotted -= this.points;
        gameState.BallsOnTable += this.points;
        gameState.BallsOnTable %= 14;
        if (gameState.BallsOnTable < 2) {
            gameState.BallsOnTable += 14;
        }
    }
}