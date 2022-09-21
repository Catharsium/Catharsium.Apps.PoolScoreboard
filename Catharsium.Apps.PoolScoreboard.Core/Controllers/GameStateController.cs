using Catharsium.Apps.PoolScoreboard.Analytics;
using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Apps.PoolScoreboard.Core.Models.Base;
using Catharsium.Apps.PoolScoreboard.Core.Models.Views;

namespace Catharsium.Apps.PoolScoreboard.Core.Controllers;

public class GameStateController : IGameStateController
{
    private readonly List<IGameEvent> Events = new();

    public StraightPoolMatch? Match { get; set; }

    public GameStateController() {
    }


    public void AddNewEvent(IGameEvent @event) {
        Events.Add(@event);
        @event.Apply(Match);
    }


    public void UndoLastEvent() {
        var @event = Events.Last();
        @event.Undo(Match);
        Events.Remove(@event);
    }


    public ScoreView GetScoreView() {
        return new ScoreView {
            PlayerScores = this.Match.Players.Select(p => new PlayerScore(p.Id, p.Name, this.GetScoreForPlayer(p.Id))).ToArray(),
            BallsOnTable = this.Match.BallsOnTable,
            CurrentPlayerIndex = this.Match.CurrentPlayer,
            RaceTo = $"Straightpool, race to {this.Match.RaceTo}"
        };
    }


    public List<Turn> GetTurnsView() {
        return this.Match.Turns;
    }


    private int GetScoreForPlayer(Guid playerId) {
        var playerTurns = Match.Turns.Where(t => t.PlayerId == playerId);
        return new ScoreAnalyzer().Total(playerTurns);
    }

    public bool IsCurrentPlayerOnTwoFouls() {
        var currentPlayer = this.Match.Players[this.Match.CurrentPlayer];
        var playerTurns = Match.Turns.Where(t => t.PlayerId == currentPlayer.Id).ToArray();
        return playerTurns.Length > 2
            && playerTurns[^2].FoulPoints == 1
            && playerTurns[^2].BallsPotted == 0
            && playerTurns[^3].FoulPoints == 1;
    }
}