using Catharsium.Apps.PoolScoreboard.Core.Events;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Util.Testing;

namespace Catharsium.Apps.PoolScoreboard.Core.Tests.Events;

[TestClass]
public class FoulEventTests : TestFixture<FoulEvent>
{
    #region Fixture

    private readonly int Points = 4;
    private GameState GameState { get; set; } = new GameState("My player 1", "My player 2");


    [TestInitialize]
    public void Initialize() {
        this.SetDependency(this.Points);
    }

    #endregion

    [TestMethod]
    public void Apply_SetsFoulPointsAndEndsTurn() {
        this.Target.Apply(this.GameState);
        Assert.AreEqual(this.Points, this.GameState.Turns.Last().FoulPoints);
    }


    [TestMethod]
    public void Undo_RemovesLastTurnAndSetsFoulPointsTo0() {
        this.Target.Undo(this.GameState);
        Assert.AreEqual(0, this.GameState.Turns.Last().FoulPoints);
    }
}