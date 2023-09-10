namespace Trivia;
public class Player
{
	public required string Name { get; set; }
	public int Place { get; private set; } = 0;
	public int Purse { get; set; } = 0;
	public bool InPenaltyBox { get; set; } = false;

	public void NextPlace(int placesToMove)
	{
		Place = (Place + placesToMove > 11)
			? Place + placesToMove - 12
			: Place + placesToMove;
	}
}
