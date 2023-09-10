namespace Trivia;
public class Player
{
	public required string Name { get; set; }
	public int Place { get; set; } = 0;
	public int Purse { get; set; } = 0;
	public bool InPenaltyBox { get; set; } = false;
}
