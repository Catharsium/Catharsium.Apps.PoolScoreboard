namespace Catharsium.Apps.PoolScoreboard.Core.Models;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }


    public Player(string name) {
        this.Id = Guid.NewGuid();
        this.Name = name;
    }
}