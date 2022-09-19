using Catharsium.Apps.PoolScoreboard.Analytics;
using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Apps.PoolScoreboard.Core.Models.Base;
using Catharsium.Apps.PoolScoreboard.Core.Models.Views;

namespace Catharsium.Apps.PoolScoreboard.Core.Controllers;

public class GameStateController : IGameStateController
{
    private readonly List<IGameEvent> Events = new();

    public StraightPoolMatch? GameState { get; set; }

    public GameStateController() {
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
            CurrentPlayerIndex = this.GameState.CurrentPlayer,
            RaceTo = $"Straightpool, race to {this.GameState.RaceTo}"
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
        return playerTurns.Length > 2
            && playerTurns[^2].FoulPoints == 1
            && playerTurns[^2].BallsPotted == 0
            && playerTurns[^3].FoulPoints == 1;
    }
}