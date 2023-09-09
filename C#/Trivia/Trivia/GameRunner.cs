namespace Trivia;

public class GameRunner
{
	private static bool _notAWinner;

	public static void Main(string[] args)
	{
		Game aGame = new();

		_ = aGame.Add("Chet");
		_ = aGame.Add("Pat");
		_ = aGame.Add("Sue");

		Random rand = new();

		do {
			aGame.Roll(rand.Next(5) + 1);

			_notAWinner = (rand.Next(9) == 7)
				? aGame.WrongAnswer()
				: aGame.WasCorrectlyAnswered();
		} while (_notAWinner);
	}
}