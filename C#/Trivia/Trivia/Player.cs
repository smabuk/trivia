namespace Trivia;
public class Player
{
	public required string Name { get; init; }
	public int Place { get; private set; } = 0;
	public int Purse { get; private set; } = 0;
	public bool InPenaltyBox { get; set; } = false;
	public bool IsGettingOutOfPenaltyBox { get; set; } = false;


	public void IncrementPurse() => Purse++;
	public void NextPlace(int placesToMove)
	{
		Place = (Place + placesToMove > 11)
			? Place + placesToMove - 12
			: Place + placesToMove;
	}
}
