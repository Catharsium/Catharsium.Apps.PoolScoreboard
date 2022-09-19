using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Core.Events.Complex;

public abstract class ComplexEvent : IGameEvent
{
    protected List<IGameEvent> Events { get; set; } = new List<IGameEvent>();


    public abstract void Apply(StraightPoolMatch gameState);


    public void Undo(StraightPoolMatch gameState) {
        this.Events.Reverse();
        foreach (var @event in this.Events) {
            @event.Undo(gameState);
        }
        this.Events.Clear();
    }


    protected void ApplyChildEvent(IGameEvent @event, StraightPoolMatch state) {
        @event.Apply(state);
        this.Events.Add(@event);
    }
}