bool _notAWinner;

Game aGame = new();

_ = aGame.AddPlayer("Chet");
_ = aGame.AddPlayer("Pat");
_ = aGame.AddPlayer("Sue");

Random rand = new();

do {
	aGame.Roll(rand.Next(5) + 1);

	_notAWinner = (rand.Next(9) == 7)
		? !aGame.WrongAnswer()
		: !aGame.WasCorrectlyAnswered();
} while (_notAWinner);
