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

    private StraightPoolMatch GameState { get; set; }


    [TestInitialize]
    public void Initialize() {
        this.SetDependency(GameState);
        this.GameState = new StraightPoolMatch(5, new List<Player> { new Player("My player 1"), new Player("My player 2") });
    }

    #endregion

    [TestMethod]
    public void AddEvent_AppliesEvent() {
        var @event = Substitute.For<IGameEvent>();
        this.Target.AddNewEvent(@event);
        @event.Received().Apply(Arg.Any<StraightPoolMatch>());
    }


    [TestMethod]
    public void UndoEvent_UndoesLastAddedEvent() {
        var @event = Substitute.For<IGameEvent>();
        this.Target.AddNewEvent(@event);
        this.Target.UndoLastEvent();
        @event.Received().Undo(Arg.Any<StraightPoolMatch>());
    }
}