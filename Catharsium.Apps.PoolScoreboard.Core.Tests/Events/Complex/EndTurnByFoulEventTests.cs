using Catharsium.Apps.PoolScoreboard.Core.Events.Complex;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Util.Testing;

namespace Catharsium.Apps.PoolScoreboard.Core.Tests.Events.Complex;

[TestClass]
public class EndTurnByFoulEventTests : TestFixture<EndTurnEvent>
{
    #region Fixture

    private int BallsOnTable { get; set; } = 10;
    private ShotType ShotType { get; set; } = ShotType.Miss;
    private StraightPoolMatch GameState { get; set; }


    [TestInitialize]
    public void Initialize() {
        this.SetDependency(this.ShotType);
        this.GameState = new StraightPoolMatch(5, new List<Player> { new Player("My player 1"), new Player("My player 2") });
        GameState.BallsOnTable = this.BallsOnTable;
        GameState.CurrentPlayer = 0;
    }

    #endregion

    [TestMethod]
    public void Apply_WithoutFoulPoints_AddsScoreAndSwitchEvent() {
        var previousBallsPotted = 3;
        var newBallsPotted = 6;
        this.GameState.Turns.Last().BallsPotted = previousBallsPotted;
        this.SetDependency(this.BallsOnTable - newBallsPotted, "ballsOnTable");
        this.SetDependency(0, "foulPoints");

        this.Target.Apply(this.GameState);
        Assert.AreEqual(previousBallsPotted + newBallsPotted, this.GameState.Turns[^2].BallsPotted);
        Assert.AreEqual(this.BallsOnTable - newBallsPotted, this.GameState.BallsOnTable);
        Assert.AreEqual(1, this.GameState.CurrentPlayer);
    }


    [TestMethod]
    public void Apply_WithFoulPoints_IncludesFoulEvent() {
        var foulPoints = 2;
        this.SetDependency(this.BallsOnTable, "ballsOnTable");
        this.SetDependency(foulPoints, "foulPoints");

        this.Target.Apply(this.GameState);
        Assert.AreEqual(foulPoints, this.GameState.Turns[^2].FoulPoints);
        Assert.AreEqual(this.BallsOnTable, this.GameState.BallsOnTable);
        Assert.AreEqual(1, this.GameState.CurrentPlayer);
    }


    [TestMethod]
    public void Undo_RemovesLastTurn() {
        var currentBallsPotted = 3;
        var newBallsPotted = 6;
        var foulPoints = 2;
        this.GameState.Turns.Last().BallsPotted = currentBallsPotted;
        this.SetDependency(this.BallsOnTable - newBallsPotted, "ballsOnTable");
        this.SetDependency(foulPoints, "foulPoints");

        this.Target.Apply(this.GameState);
        this.Target.Undo(this.GameState);
        Assert.AreEqual(currentBallsPotted, this.GameState.Turns[^1].BallsPotted);
        Assert.AreEqual(this.BallsOnTable, this.GameState.BallsOnTable);
        Assert.AreEqual(0, this.GameState.CurrentPlayer);
    }
}