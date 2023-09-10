namespace Trivia;

public class Game
{
	private readonly List<string> _players = new();

	private readonly int[] _places = new int[6];
	private readonly int[] _purses = new int[6];

	private readonly bool[] _inPenaltyBox = new bool[6];

	private readonly Queue<string> _popQuestions = new();
	private readonly Queue<string> _scienceQuestions = new();
	private readonly Queue<string> _sportsQuestions = new();
	private readonly Queue<string> _rockQuestions = new();

	private int _currentPlayer;
	private bool _isGettingOutOfPenaltyBox;

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
		_players.Add(playerName);
		_places[HowManyPlayers()] = 0;
		_purses[HowManyPlayers()] = 0;
		_inPenaltyBox[HowManyPlayers()] = false;

		Console.WriteLine($"{playerName} was added");
		Console.WriteLine($"They are player number {_players.Count}");
		return true;
	}

	public int HowManyPlayers() => _players.Count;

	public void Roll(int roll)
	{
		Console.WriteLine($"{_players[_currentPlayer]} is the current player");
		Console.WriteLine($"They have rolled a {roll}");

		if (_inPenaltyBox[_currentPlayer]) {
			if (roll % 2 != 0) {
				_isGettingOutOfPenaltyBox = true;

				Console.WriteLine($"{_players[_currentPlayer]} is getting out of the penalty box");
				_places[_currentPlayer] = _places[_currentPlayer] + roll;
				if (_places[_currentPlayer] > 11) {
					_places[_currentPlayer] = _places[_currentPlayer] - 12;
				}

				Console.WriteLine($"{_players[_currentPlayer]}'s new location is {_places[_currentPlayer]}");
				Console.WriteLine($"The category is {CurrentCategory}");
				AskQuestion();
			} else {
				Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
				_isGettingOutOfPenaltyBox = false;
			}
		} else {
			_places[_currentPlayer] = _places[_currentPlayer] + roll;
			if (_places[_currentPlayer] > 11) {
				_places[_currentPlayer] = _places[_currentPlayer] - 12;
			}

			Console.WriteLine($"{_players[_currentPlayer]}'s new location is {_places[_currentPlayer]}");
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

	private string CurrentCategory => Helpers.PlaceCategory(_places[_currentPlayer]);

	public bool WasCorrectlyAnswered()
	{
		if (_inPenaltyBox[_currentPlayer]) {
			if (_isGettingOutOfPenaltyBox) {
				Console.WriteLine("Answer was correct!!!!");
				_purses[_currentPlayer]++;
				Console.WriteLine($"{_players[_currentPlayer]} now has {_purses[_currentPlayer]} Gold Coins.");

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
			_purses[_currentPlayer]++;
			Console.WriteLine($"{_players[_currentPlayer]} now has {_purses[_currentPlayer]} Gold Coins.");

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
		Console.WriteLine("Question was incorrectly answered");
		Console.WriteLine($"{_players[_currentPlayer]} was sent to the penalty box");
		_inPenaltyBox[_currentPlayer] = true;

		_currentPlayer++;
		if (_currentPlayer == _players.Count) {
			_currentPlayer = 0;
		}

		return true;
	}


	private bool DidPlayerWin() => !(_purses[_currentPlayer] == 6);
}
