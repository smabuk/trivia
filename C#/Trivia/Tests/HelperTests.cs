namespace Tests;
public class HelperTests
{
	[Theory]
	[InlineData("Pop", 3)]
	[InlineData("Rock", 2)]
	[InlineData("Science", 50)]
	[InlineData("Sports", 24)]
	public void CreateQuestion_Should_Return_Expected_String(string category, int index)
	{
		string actual = Game.CreateQuestion(category, index);

		actual.ShouldBe($"{category} Question {index}");
	}

}
