using Catharsium.Apps.PoolScoreboard.Core.Controllers;
using Catharsium.Apps.PoolScoreboard.Core.Interfaces;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Util.Testing;
using NSubstitute;

namespace Catharsium.Apps.PoolScoreboard.Core.Tests.Controllers;

[TestClass]
public class GameControllerTests : TestFixture<GameController>
{
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