using Catharsium.Apps.PoolScoreboard.Analytics;
using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Apps.PoolScoreboard.Core.Models.Views;

namespace Catharsium.Apps.PoolScoreboard.Core.Controllers;

public class GameController : IGameController
{
    private readonly GameState GameState;
    private readonly List<IGameEvent> Events;


    public GameController(GameState gameState) {
        GameState = gameState;
        Events = new List<IGameEvent>();
    }


    public void AddNewEvent(IGameEvent @event) {
        Events.Add(@event);
        @event.Apply(GameState);
    }


    public void UndoLastEvent() {
        var @event = Events.Last();
        @event.Undo(GameState);
        Events.Remove(@event);
    }


    public ScoreView GetScoreView() {
        return new ScoreView {
            PlayerScores = this.GameState.Players.Select(p => new PlayerScore(p.Id, p.Name, this.GetScoreForPlayer(p.Id))).ToArray(),
            BallsOnTable = this.GameState.BallsOnTable,
            CurrentPlayerIndex = this.GameState.CurrentPlayer
        };
    }


    public List<Turn> GetTurnsView() {
        return this.GameState.Turns;
    }


    private int GetScoreForPlayer(Guid playerId) {
        var playerTurns = GameState.Turns.Where(t => t.PlayerId == playerId);
        return new ScoreAnalyzer().Total(playerTurns);
    }

    public bool IsCurrentPlayerOnTwoFouls() {
        var currentPlayer = this.GameState.Players[this.GameState.CurrentPlayer];
        var playerTurns = GameState.Turns.Where(t => t.PlayerId == currentPlayer.Id).ToArray();
        return playerTurns[^2].FoulPoints == 1
            && playerTurns[^2].BallsPotted == 0 
            && playerTurns[^3].FoulPoints == 1;
    }
}