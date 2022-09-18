using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Core.Events;

public class SwitchEvent : IGameEvent
{
    private readonly ShotType lastShot;


    public SwitchEvent(ShotType lastShot) {
        this.lastShot = lastShot;
    }


    public void Apply(GameState state) {
        state.Turns.Last().LastShot = lastShot;
        state.CurrentPlayer += 1;
        if (state.CurrentPlayer > state.Players.Length - 1) {
            state.CurrentTurn++;
            state.CurrentPlayer = 0;
        }

        var result = state.Players[state.CurrentPlayer];
        state.Turns.Add(new Turn(result.Id, state.CurrentTurn));
    }


    public void Undo(GameState state) {
        state.Turns.Last().LastShot = null;
        state.CurrentPlayer -= 1;
        if (state.CurrentPlayer < 0) {
            state.CurrentTurn--;
            state.CurrentPlayer = state.Players.Length - 1;
        }

        state.Turns.RemoveAt(state.Turns.Count - 1);
    }
}