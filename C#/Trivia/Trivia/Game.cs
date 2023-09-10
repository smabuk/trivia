namespace Trivia;

public class Game
{
	private readonly List<Player> _players = new();

	private readonly Queue<string> _popQuestions     = new();
	private readonly Queue<string> _scienceQuestions = new();
	private readonly Queue<string> _sportsQuestions  = new();
	private readonly Queue<string> _rockQuestions    = new();

	private int _currentPlayer;
	private bool _isGettingOutOfPenaltyBox;
	private Player GetCurrentPlayer() => _players[_currentPlayer];

	public Game()
	{
		for (int i = 0; i < 50; i++) {
			_popQuestions    .Enqueue(CreateQuestion("Pop",     i));
			_scienceQuestions.Enqueue(CreateQuestion("Science", i));
			_sportsQuestions .Enqueue(CreateQuestion("Sports",  i));
			_rockQuestions   .Enqueue(CreateQuestion("Rock",    i));
		}
	}

	public bool IsPlayable() => HowManyPlayers() >= 2;

	public bool Add(string playerName)
	{
		Player player = new() { Name = playerName };
		_players.Add(player);

		Console.WriteLine($"{player.Name} was added");
		Console.WriteLine($"They are player number {_players.Count}");
		return true;
	}

	public int HowManyPlayers() => _players.Count;

	public void Roll(int roll)
	{
		Player currentPlayer = GetCurrentPlayer();

		Console.WriteLine($"{currentPlayer.Name} is the current player");
		Console.WriteLine($"They have rolled a {roll}");

		if (currentPlayer.InPenaltyBox) {
			if (roll % 2 != 0) {
				_isGettingOutOfPenaltyBox = true;

				Console.WriteLine($"{currentPlayer.Name} is getting out of the penalty box");
				currentPlayer.NextPlace(roll);

				Console.WriteLine($"{currentPlayer.Name}'s new location is {currentPlayer.Place}");
				Console.WriteLine($"The category is {CurrentCategory}");
				AskQuestion();
			} else {
				Console.WriteLine($"{currentPlayer.Name} is not getting out of the penalty box");
				_isGettingOutOfPenaltyBox = false;
			}
		} else {
			currentPlayer.NextPlace(roll);

			Console.WriteLine($"{currentPlayer.Name}'s new location is {currentPlayer.Place}");
			Console.WriteLine($"The category is {CurrentCategory}");
			AskQuestion();
		}
	}

	private void AskQuestion()
	{
		string question = CurrentCategory switch
		{
			"Pop"     => _popQuestions.Dequeue(),
			"Science" => _scienceQuestions.Dequeue(),
			"Sports"  => _sportsQuestions.Dequeue(),
			"Rock"    => _rockQuestions.Dequeue(),
			_ => throw new NotImplementedException(),
		};
		Console.WriteLine(question);
	}


	public bool WasCorrectlyAnswered()
	{
		Player currentPlayer = GetCurrentPlayer();
		if (currentPlayer.InPenaltyBox) {
			if (_isGettingOutOfPenaltyBox) {
				Console.WriteLine("Answer was correct!!!!");
				currentPlayer.IncrementPurse();
				Console.WriteLine($"{currentPlayer.Name} now has {currentPlayer.Purse} Gold Coins.");

				bool winner = DidPlayerWin();
				NextPlayer();

				return winner;
			} else {
				NextPlayer();

				return false;
			}
		} else {
			Console.WriteLine("Answer was correct!!!!");
			currentPlayer.IncrementPurse();
			Console.WriteLine($"{currentPlayer.Name} now has {currentPlayer.Purse} Gold Coins.");

			bool winner = DidPlayerWin();
			NextPlayer();

			return winner;
		}
	}

	public bool WrongAnswer()
	{
		Player currentPlayer = GetCurrentPlayer();
		Console.WriteLine("Question was incorrectly answered");
		Console.WriteLine($"{currentPlayer.Name} was sent to the penalty box");
		currentPlayer.InPenaltyBox = true;

		NextPlayer();

		return false;
	}

	private string CurrentCategory => PlaceCategory(GetCurrentPlayer().Place);

	void NextPlayer() => _currentPlayer = (_currentPlayer + 1 == HowManyPlayers()) ? 0 : _currentPlayer + 1;

	private bool DidPlayerWin() => (GetCurrentPlayer().Purse == 6);
}
