using System.IO;

namespace Tests;
public class TriviaGameTests
{
	[Fact]
	public void Game_ShouldBe_Unplayable()
	{
		Game aGame = new();

		_ = aGame.Add("Anna");

		aGame.IsPlayable().ShouldBeFalse();
	}

	[Fact]
	public void Game_ShouldBe_Playable()
	{
		Game aGame = new();

		_ = aGame.Add("Anna");
		_ = aGame.Add("Brett");

		aGame.IsPlayable().ShouldBeTrue();
	}

}
