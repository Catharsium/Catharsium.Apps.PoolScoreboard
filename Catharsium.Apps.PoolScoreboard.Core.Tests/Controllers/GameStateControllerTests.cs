using Catharsium.Apps.PoolScoreboard.Core.Controllers;
using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Util.Testing;
using NSubstitute;

namespace Catharsium.Apps.PoolScoreboard.Core.Tests.Controllers;

[TestClass]
public class GameStateControllerTests : TestFixture<GameStateController>
{
    #region Fixture

    private GameState GameState { get; set; } = new GameState("Player 1", "Player 2");


    [TestInitialize]
    public void Initialize() {
        this.SetDependency(GameState);
    }

    #endregion

    [TestMethod]
    public void AddEvent_AppliesEvent() {
        var @event = Substitute.For<IGameEvent>();
        this.Target.AddNewEvent(@event);
        @event.Received().Apply(Arg.Any<GameState>());
    }


    [TestMethod]
    public void UndoEvent_UndoesLastAddedEvent() {
        var @event = Substitute.For<IGameEvent>();
        this.Target.AddNewEvent(@event);
        this.Target.UndoLastEvent();
        @event.Received().Undo(Arg.Any<GameState>());
    }
}