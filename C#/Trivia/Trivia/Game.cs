namespace Trivia;

public class Game
{
	private readonly List<Player> _players = new();

	private readonly Queue<string> _popQuestions     = new();
	private readonly Queue<string> _scienceQuestions = new();
	private readonly Queue<string> _sportsQuestions  = new();
	private readonly Queue<string> _rockQuestions    = new();

	private int _currentPlayerIndex;
	
	private string CurrentCategory => PlaceCategory(CurrentPlayer.Place);
	private Player CurrentPlayer => _players[_currentPlayerIndex];
	private bool DidCurrentPlayerWin() => (CurrentPlayer.Purse == 6);
	private int HowManyPlayers() => _players.Count;
	private void NextPlayer() => _currentPlayerIndex = (_currentPlayerIndex + 1 == HowManyPlayers()) ? 0 : _currentPlayerIndex + 1;
	public bool IsPlayable() => HowManyPlayers() >= 2;

	public Game()
	{
		for (int i = 0; i < 50; i++) {
			_popQuestions    .Enqueue(CreateQuestion("Pop",     i));
			_scienceQuestions.Enqueue(CreateQuestion("Science", i));
			_sportsQuestions .Enqueue(CreateQuestion("Sports",  i));
			_rockQuestions   .Enqueue(CreateQuestion("Rock",    i));
		}
	}

	public bool AddPlayer(string playerName)
	{
		Player player = new() { Name = playerName };
		_players.Add(player);

		Console.WriteLine($"{player.Name} was added");
		Console.WriteLine($"They are player number {_players.Count}");
		return true;
	}

	public void Roll(int roll)
	{
		Player currentPlayer = CurrentPlayer;

		Console.WriteLine($"{currentPlayer.Name} is the current player");
		Console.WriteLine($"They have rolled a {roll}");

		if (currentPlayer.InPenaltyBox) {
			if (roll.IsEven()) {
				currentPlayer.IsGettingOutOfPenaltyBox = true;

				Console.WriteLine($"{currentPlayer.Name} is getting out of the penalty box");
				MoveToNextPlace(roll, currentPlayer);
				AskQuestion();
			} else {
				Console.WriteLine($"{currentPlayer.Name} is not getting out of the penalty box");
				currentPlayer.IsGettingOutOfPenaltyBox = false;
			}
		} else {
			MoveToNextPlace(roll, currentPlayer);
			AskQuestion();
		}

		void MoveToNextPlace(int roll, Player currentPlayer)
		{
			currentPlayer.NextPlace(roll);
			Console.WriteLine($"{currentPlayer.Name}'s new location is {currentPlayer.Place}");
			Console.WriteLine($"The category is {CurrentCategory}");
		}
	}

	private void AskQuestion()
	{
		string question = CurrentCategory switch
		{
			"Pop"     => _popQuestions    .Dequeue(),
			"Science" => _scienceQuestions.Dequeue(),
			"Sports"  => _sportsQuestions .Dequeue(),
			"Rock"    => _rockQuestions   .Dequeue(),
			_ => throw new NotImplementedException(),
		};
		Console.WriteLine(question);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns>True if the player won, otherwise false</returns>
	public bool WasCorrectlyAnswered()
	{
		Player currentPlayer = CurrentPlayer;
		if (currentPlayer.InPenaltyBox) {
			if (currentPlayer.IsGettingOutOfPenaltyBox) {
				Console.WriteLine("Answer was correct!!!!");
				currentPlayer.IncrementPurse();
				Console.WriteLine($"{currentPlayer.Name} now has {currentPlayer.Purse} Gold Coins.");

				bool isCurrentPlayerTheWinner = DidCurrentPlayerWin();
				NextPlayer();

				return isCurrentPlayerTheWinner;
			} else {
				NextPlayer();

				return false;
			}
		} else {
			Console.WriteLine("Answer was correct!!!!");
			currentPlayer.IncrementPurse();
			Console.WriteLine($"{currentPlayer.Name} now has {currentPlayer.Purse} Gold Coins.");

			bool isCurrentPlayerTheWinner = DidCurrentPlayerWin();
			NextPlayer();

			return isCurrentPlayerTheWinner;
		}
	}

	/// <summary>
	/// Player got the answer wrong so place him in the Penalty box
	/// </summary>
	/// <returns>False as the player got the question wrong and therefore did not win</returns>
	public bool WrongAnswer()
	{
		Player currentPlayer = CurrentPlayer;
		Console.WriteLine("Question was incorrectly answered");
		Console.WriteLine($"{currentPlayer.Name} was sent to the penalty box");
		currentPlayer.InPenaltyBox = true;

		NextPlayer();

		return false;
	}
}
