using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Core.Events;

public class FoulEvent : IGameEvent
{
    private readonly int points;


    public FoulEvent(int points) {
        this.points = points;
    }


    public void Apply(StraightPoolMatch state) {
        state.Turns.Last().FoulPoints = this.points;
    }


    public void Undo(StraightPoolMatch state) {
        state.Turns.Last().FoulPoints = 0;
    }
}