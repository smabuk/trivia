namespace Trivia;

public class Game
{
	private readonly List<Player> _players = new();

	private readonly Queue<string> _popQuestions = new();
	private readonly Queue<string> _scienceQuestions = new();
	private readonly Queue<string> _sportsQuestions = new();
	private readonly Queue<string> _rockQuestions = new();

	private int _currentPlayer;
	private bool _isGettingOutOfPenaltyBox;
	private Player GetCurrentPlayer() => _players[_currentPlayer];

	public Game()
	{
		for (int i = 0; i < 50; i++) {
			_popQuestions.Enqueue(Helpers.CreateQuestion("Pop", i));
			_scienceQuestions.Enqueue(Helpers.CreateQuestion("Science", i));
			_sportsQuestions.Enqueue(Helpers.CreateQuestion("Sports", i));
			_rockQuestions.Enqueue(Helpers.CreateQuestion("Rock", i));
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
				currentPlayer.Place += roll;
				if (currentPlayer.Place > 11) {
					currentPlayer.Place -= 12;
				}

				Console.WriteLine($"{currentPlayer.Name}'s new location is {currentPlayer.Place}");
				Console.WriteLine($"The category is {CurrentCategory}");
				AskQuestion();
			} else {
				Console.WriteLine($"{currentPlayer.Name} is not getting out of the penalty box");
				_isGettingOutOfPenaltyBox = false;
			}
		} else {
			currentPlayer.Place += roll;
			if (currentPlayer.Place > 11) {
				currentPlayer.Place -= 12;
			}

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

	private string CurrentCategory => Helpers.PlaceCategory(GetCurrentPlayer().Place);

	public bool WasCorrectlyAnswered()
	{
		Player currentPlayer = GetCurrentPlayer();
		if (currentPlayer.InPenaltyBox) {
			if (_isGettingOutOfPenaltyBox) {
				Console.WriteLine("Answer was correct!!!!");
				currentPlayer.Purse++;
				Console.WriteLine($"{currentPlayer.Name} now has {currentPlayer.Purse} Gold Coins.");

				bool winner = DidPlayerWin();
				_currentPlayer++;
				if (_currentPlayer == _players.Count) {
					_currentPlayer = 0;
				}

				return winner;
			} else {
				_currentPlayer++;
				if (_currentPlayer == _players.Count) {
					_currentPlayer = 0;
				}

				return true;
			}
		} else {
			Console.WriteLine("Answer was correct!!!!");
			currentPlayer.Purse++;
			Console.WriteLine($"{currentPlayer.Name} now has {currentPlayer.Purse} Gold Coins.");

			bool winner = DidPlayerWin();
			_currentPlayer++;
			if (_currentPlayer == _players.Count) {
				_currentPlayer = 0;
			}

			return winner;
		}
	}

	public bool WrongAnswer()
	{
		Player currentPlayer = GetCurrentPlayer();
		Console.WriteLine("Question was incorrectly answered");
		Console.WriteLine($"{currentPlayer.Name} was sent to the penalty box");
		GetCurrentPlayer().InPenaltyBox = true;

		_currentPlayer++;
		if (_currentPlayer == _players.Count) {
			_currentPlayer = 0;
		}

		return true;
	}


	private bool DidPlayerWin() => !(GetCurrentPlayer().Purse == 6);
}
