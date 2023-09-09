﻿using System.IO;

namespace Tests;
public class TriviaGameRunnerTests
{
	[Fact]
	public void GameRunner_Simulation_ShouldMatch()
	{
		bool notAWinner;
		Game aGame = new();

		_ = aGame.Add("Chet");
		_ = aGame.Add("Pat");
		_ = aGame.Add("Sue");

		Random rand = new(0);

		StringWriter writer = new();
		Console.SetOut(writer);

		do {
			aGame.Roll(rand.Next(5) + 1);

			notAWinner = (rand.Next(9) == 7)
				? aGame.WrongAnswer()
				: aGame.WasCorrectlyAnswered();
		} while (notAWinner);

		string output = writer.ToString();

		string expected = """
			Chet is the current player
			They have rolled a 4
			Chet's new location is 4
			The category is Pop
			Pop Question 0
			Question was incorrectly answered
			Chet was sent to the penalty box
			Pat is the current player
			They have rolled a 4
			Pat's new location is 4
			The category is Pop
			Pop Question 1
			Answer was corrent!!!!
			Pat now has 1 Gold Coins.
			Sue is the current player
			They have rolled a 2
			Sue's new location is 2
			The category is Sports
			Sports Question 0
			Answer was corrent!!!!
			Sue now has 1 Gold Coins.
			Chet is the current player
			They have rolled a 5
			Chet is getting out of the penalty box
			Chet's new location is 9
			The category is Science
			Science Question 0
			Answer was correct!!!!
			Chet now has 1 Gold Coins.
			Pat is the current player
			They have rolled a 5
			Pat's new location is 9
			The category is Science
			Science Question 1
			Answer was corrent!!!!
			Pat now has 2 Gold Coins.
			Sue is the current player
			They have rolled a 2
			Sue's new location is 4
			The category is Pop
			Pop Question 2
			Answer was corrent!!!!
			Sue now has 2 Gold Coins.
			Chet is the current player
			They have rolled a 4
			Chet is not getting out of the penalty box
			Pat is the current player
			They have rolled a 5
			Pat's new location is 2
			The category is Sports
			Sports Question 1
			Answer was corrent!!!!
			Pat now has 3 Gold Coins.
			Sue is the current player
			They have rolled a 5
			Sue's new location is 9
			The category is Science
			Science Question 2
			Answer was corrent!!!!
			Sue now has 3 Gold Coins.
			Chet is the current player
			They have rolled a 4
			Chet is not getting out of the penalty box
			Pat is the current player
			They have rolled a 5
			Pat's new location is 7
			The category is Rock
			Rock Question 0
			Question was incorrectly answered
			Pat was sent to the penalty box
			Sue is the current player
			They have rolled a 5
			Sue's new location is 2
			The category is Sports
			Sports Question 2
			Answer was corrent!!!!
			Sue now has 4 Gold Coins.
			Chet is the current player
			They have rolled a 4
			Chet is not getting out of the penalty box
			Pat is the current player
			They have rolled a 5
			Pat is getting out of the penalty box
			Pat's new location is 0
			The category is Pop
			Pop Question 3
			Answer was correct!!!!
			Pat now has 4 Gold Coins.
			Sue is the current player
			They have rolled a 3
			Sue's new location is 5
			The category is Science
			Science Question 3
			Answer was corrent!!!!
			Sue now has 5 Gold Coins.
			Chet is the current player
			They have rolled a 1
			Chet is getting out of the penalty box
			Chet's new location is 10
			The category is Sports
			Sports Question 3
			Answer was correct!!!!
			Chet now has 2 Gold Coins.
			Pat is the current player
			They have rolled a 2
			Pat is not getting out of the penalty box
			Sue is the current player
			They have rolled a 4
			Sue's new location is 9
			The category is Science
			Science Question 4
			Answer was corrent!!!!
			Sue now has 6 Gold Coins.
			
			""";

		output.ShouldBe(expected);

	}

}
