using Catharsium.Apps.PoolScoreboard.Core.GameActions;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Util.Testing;

namespace Catharsium.Apps.PoolScoreboard.Core.Tests.Events;

[TestClass]
public class ScoreEventTests : TestFixture<ScoreEvent>
{
    #region Fixture

    private int Points { get; set; } = 4;

    private GameState GameState { get; set; }


    [TestInitialize]
    public void Initialize() {
        this.SetDependency(this.Points);
        this.GameState = new GameState("My player 1", "My player 2");
    }

    #endregion

    [TestMethod]
    public void Apply_PlentyOfBallsOnTable_MovesBallFromTableToTurn() {
        var ballsPotted = 6;
        var ballsOnTable = 8;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Apply(this.GameState);
        Assert.AreEqual(ballsPotted + this.Points, this.GameState.Turns.Last().BallsPotted);
        Assert.AreEqual(ballsOnTable - this.Points, this.GameState.BallsOnTable);
    }


    [TestMethod]
    public void Apply_TooFewBallsOnTable_AddsNewRack() {
        var ballsPotted = 6;
        var ballsOnTable = 2;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Apply(this.GameState);
        Assert.AreEqual(ballsOnTable - this.Points + 14, this.GameState.BallsOnTable);
    }


    [TestMethod]
    public void Apply_FullRack_AddsNewRack() {
        this.SetDependency(14);
        var ballsPotted = 6;
        var ballsOnTable = 15;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Apply(this.GameState);
        Assert.AreEqual(ballsOnTable, this.GameState.BallsOnTable);
    }


    [TestMethod]
    public void Undo_FewEnoughBallsOnTable_MovesBallFromTurnToTable() {
        var ballsPotted = 6;
        var ballsOnTable = 8;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Undo(this.GameState);
        Assert.AreEqual(ballsPotted - this.Points, this.GameState.Turns.Last().BallsPotted);
        Assert.AreEqual(ballsOnTable + this.Points, this.GameState.BallsOnTable);
    }


    [TestMethod]
    public void Undo_TooManyBallsOnTable_RemovesNewRack() {
        var ballsPotted = 6;
        var ballsOnTable = 13;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Undo(this.GameState);
        Assert.AreEqual(ballsPotted - this.Points, this.GameState.Turns.Last().BallsPotted);
        Assert.AreEqual((ballsOnTable + this.Points) % 14, this.GameState.BallsOnTable);
    }


    [TestMethod]
    public void Undo_FullRack_DoesNotRemoveLastRack() {
        this.SetDependency(14);
        var ballsPotted = 14;
        var ballsOnTable = 15;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Undo(this.GameState);
        Assert.AreEqual(ballsPotted - 14, this.GameState.Turns.Last().BallsPotted);
        Assert.AreEqual(15, this.GameState.BallsOnTable);
    }


    [TestMethod]
    public void Undo_TwoFullRacks_DoesNotRemoveLastRack() {
        this.SetDependency(14);
        var ballsPotted = 28;
        var ballsOnTable = 15;
        this.GameState.Turns.Last().BallsPotted = ballsPotted;
        this.GameState.BallsOnTable = ballsOnTable;

        this.Target.Undo(this.GameState);
        Assert.AreEqual(ballsPotted - 14, this.GameState.Turns.Last().BallsPotted);
        Assert.AreEqual(15, this.GameState.BallsOnTable);
    }
}