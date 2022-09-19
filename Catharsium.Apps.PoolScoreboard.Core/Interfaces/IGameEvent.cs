using Catharsium.Apps.PoolScoreboard.Core.Models;

namespace Catharsium.Apps.PoolScoreboard.Core.Interfaces;

public interface IGameEvent
{
    void Apply(GameState gameState);
    void Undo(GameState gameState);
}