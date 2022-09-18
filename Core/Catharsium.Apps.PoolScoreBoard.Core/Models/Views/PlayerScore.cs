namespace Catharsium.Apps.PoolScoreboard.Core.Models.Views;

public class PlayerScore
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }


    public PlayerScore(Guid id, string name, int score) {
        this.Id = id;
        this.Name = name;
        this.Score = score;
    }
}