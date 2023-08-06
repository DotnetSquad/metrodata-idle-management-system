using API.Utilities.Enums;

namespace API.Utilities.Handlers;

public class GenerateHandler
{
    public static string GenerateNik(string? lastNik = null)
    {
        if (lastNik is null)
        {
            return "111111"; // First employee
        }

        var generateNik = Convert.ToInt32(lastNik) + 1;

        return generateNik.ToString();
    }

    public static double GenerateTotalScore(int scoreSegment1, int scoreSegment2, int scoreSegment3, int scoreSegment4)
    {
        var totalScore = (((double)(scoreSegment1 + scoreSegment2 + scoreSegment3) / 3) / 2) + ((double)scoreSegment4 / 2);
        return totalScore;
    }

    public static GradeEnum GenerateGradeLevel(int scoreSegment1, int scoreSegment2, int scoreSegment3, int scoreSegment4)
    {
        var grade = GenerateTotalScore(scoreSegment1, scoreSegment2, scoreSegment3, scoreSegment4);
        return grade < 80 ? GradeEnum.B : GradeEnum.A;
    }

    private static Random random = new Random();

    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
