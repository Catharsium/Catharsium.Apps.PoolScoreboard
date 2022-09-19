using Catharsium.Apps.PoolScoreboard.Core.Events.Complex;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Util.Testing;

namespace Catharsium.Apps.PoolScoreboard.Core.Tests.Events.Complex;

[TestClass]
public class ClearRackEventTests : TestFixture<ClearRackEvent>
{
    #region Fixture

    private GameState GameState { get; set; }


    [TestInitialize]
    public void Initialize() {
        this.GameState = new GameState("My player 1", "My player 2");
    }

    #endregion

    [TestMethod]
    public void Apply_PartOfRackPotted_AddsRemainingBallsMinus1ScoreEvent() {
        var ballsPotted = 4;
        var ballsOnTable = 8;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Apply(this.GameState);
        Assert.AreEqual(ballsPotted + ballsOnTable - 1, this.GameState.Turns.Last().BallsPotted);
        Assert.AreEqual(15, this.GameState.BallsOnTable);
    }


    [TestMethod]
    public void Apply_FullRackOnTable_Adds14ScoreEvent() {
        var ballsPotted = 4;
        var ballsOnTable = 15;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Apply(this.GameState);
        Assert.AreEqual(ballsPotted + 14, this.GameState.Turns.Last().BallsPotted);
        Assert.AreEqual(15, this.GameState.BallsOnTable);
    }


    [TestMethod]
    public void Undo_PartOfRackPotted_AddsBallsPottedToBallsOnTable() {
        var ballsPotted = 4;
        var ballsOnTable = 8;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Apply(this.GameState);
        this.Target.Undo(this.GameState);
        Assert.AreEqual(ballsPotted, this.GameState.Turns.Last().BallsPotted);
        Assert.AreEqual(ballsOnTable, this.GameState.BallsOnTable);
    }


    [TestMethod]
    public void Undo_FullRackPotted_AddsBallsPottedToBallsOnTable() {
        var ballsPotted = 4;
        var ballsOnTable = 15;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Apply(this.GameState);
        this.Target.Undo(this.GameState);
        Assert.AreEqual(ballsPotted, this.GameState.Turns.Last().BallsPotted);
        Assert.AreEqual(ballsOnTable, this.GameState.BallsOnTable);
    }
}