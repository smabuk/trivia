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

			if (rand.Next(9) == 7) {
				_notAWinner = aGame.WrongAnswer();
			} else {
				_notAWinner = aGame.WasCorrectlyAnswered();
			}
		} while (_notAWinner);
	}
}