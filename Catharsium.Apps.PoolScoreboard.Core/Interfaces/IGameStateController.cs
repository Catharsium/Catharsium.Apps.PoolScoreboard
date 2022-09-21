using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Apps.PoolScoreboard.Core.Models.Base;
using Catharsium.Apps.PoolScoreboard.Core.Models.Views;

namespace Catharsium.Apps.PoolScoreboard.Core.Controllers;

public interface IGameStateController
{
    StraightPoolMatch Match { get; set; }
    void AddNewEvent(IGameEvent @event);
    void UndoLastEvent();

    ScoreView GetScoreView();
    List<Turn> GetTurnsView();
    bool IsCurrentPlayerOnTwoFouls();
}