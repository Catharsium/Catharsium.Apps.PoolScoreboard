using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Core.Interfaces;

public interface IGameEvent
{
    void Apply(StraightPoolMatch gameState);
    void Undo(StraightPoolMatch gameState);
}