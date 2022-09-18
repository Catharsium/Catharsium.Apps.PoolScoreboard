using Catharsium.Apps.PoolScoreboard.Core.Events;
using Catharsium.Apps.PoolScoreboard.Core.Models;
using Catharsium.Util.Testing;

namespace Catharsium.Apps.PoolScoreboard.Core.Tests.Events;

[TestClass]
public class SwitchEventTests : TestFixture<SwitchEvent>
{
    #region Fixture

    private ShotType ShotType { get; set; } = ShotType.Savety;
    private GameState GameState { get; set; }


    [TestInitialize]
    public void Initialize() {
        this.SetDependency(this.ShotType);
        this.GameState = new GameState("My player 1", "My player 2");
    }

    #endregion

    [TestMethod]
    public void Apply_NotAllPlayersHaveHadATurn_AddsOneTurnWithCurrentTurnNumber() {
        Assert.AreEqual(1, this.GameState.Turns.Count);
        this.Target.Apply(this.GameState);
        Assert.AreEqual(2, this.GameState.Turns.Count);
        Assert.AreEqual(0, this.GameState.Turns.Last().TurnNumber);
        Assert.AreEqual(0, this.GameState.CurrentTurn);
        Assert.AreEqual(this.ShotType, this.GameState.Turns[^2].LastShot);
    }


    [TestMethod]
    public void Apply_AllPlayersHaveHadATurn_AddsOneTurnWithNextTurnNumber() {
        this.GameState.CurrentPlayer = this.GameState.Players.Length - 1;
        this.GameState.Turns.Add(new Turn(this.GameState.Players[this.GameState.CurrentPlayer].Id, 0));

        Assert.AreEqual(2, this.GameState.Turns.Count);

        this.Target.Apply(this.GameState);
        Assert.AreEqual(3, this.GameState.Turns.Count);
        Assert.AreEqual(1, this.GameState.Turns.Last().TurnNumber);
        Assert.AreEqual(1, this.GameState.CurrentTurn);
        Assert.AreEqual(this.ShotType, this.GameState.Turns[^2].LastShot);
    }


    [TestMethod]
    public void Undo_AllPlayersHaveHadATurn_RemovesLastTurnAndLeavesCurrentTurn() {
        this.GameState.CurrentPlayer = this.GameState.Players.Length - 1;
        this.GameState.Turns.Add(new Turn(this.GameState.Players[this.GameState.CurrentPlayer].Id, 0));

        this.Target.Undo(this.GameState);
        Assert.AreEqual(1, this.GameState.Turns.Count);
        Assert.AreEqual(0, this.GameState.Turns.Last().TurnNumber);
        Assert.AreEqual(0, this.GameState.CurrentTurn);
    }


    [TestMethod]
    public void Undo_OnlyFirstPlayerHasHadATurn_RemovesLastTurnAndSetsCurrentTurn() {
        this.GameState.CurrentPlayer = 0;
        this.GameState.CurrentTurn = 1;
        this.GameState.Turns.Add(new Turn(this.GameState.Players[1].Id, 0));
        this.GameState.Turns.Add(new Turn(this.GameState.Players[0].Id, 1));

        this.Target.Undo(this.GameState);
        Assert.AreEqual(2, this.GameState.Turns.Count);
        Assert.AreEqual(0, this.GameState.Turns.Last().TurnNumber);
        Assert.AreEqual(0, this.GameState.CurrentTurn);
    }
}