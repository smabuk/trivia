namespace Trivia;
public static class Helpers
{
	public static string CreateQuestion(string category, int index) => $"{category} Question {index}";
	public static string PlaceCategory(int place)
	{
		return place switch
		{
			0 => "Pop",
			4 => "Pop",
			8 => "Pop",
			1 => "Science",
			5 => "Science",
			9 => "Science",
			2 => "Sports",
			6 => "Sports",
			10 => "Sports",
			_ => "Rock"
		};
	}
	public static bool IsEven(this int roll) => (roll % 2 != 0);

}
