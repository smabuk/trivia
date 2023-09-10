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
		string actual = Helpers.CreateQuestion(category, index);

		actual.ShouldBe($"{category} Question {index}");
	}

	[Theory]
	[InlineData(0, "Pop")]
	[InlineData(1, "Science")]
	[InlineData(2, "Sports")]
	[InlineData(3, "Rock")]
	[InlineData(4, "Pop")]
	[InlineData(5, "Science")]
	[InlineData(6, "Sports")]
	[InlineData(7, "Rock")]
	[InlineData(8, "Pop")]
	[InlineData(9, "Science")]
	[InlineData(10, "Sports")]
	[InlineData(11, "Rock")]
	public void PlayerCategory_Given_Place_Should_Return_Category(int place, string expectedCategory)
	{
		string actual = Helpers.PlaceCategory(place);

		actual.ShouldBe(expectedCategory);
	}

}
