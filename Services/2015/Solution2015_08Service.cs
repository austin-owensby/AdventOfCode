using System.Text.RegularExpressions;

namespace AdventOfCode.Services
{
    public class Solution2015_08Service : ISolutionDayService
    {
        public Solution2015_08Service() { }

        public string FirstHalf()
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "08.txt"));

            int charDifference = 0;

            charDifference += 2 * Regex.Matches(data, "\n").Count; // Each newline means 2 extra single quotes in the code
            charDifference += Regex.Matches(data, "(\\\\\")|(\\\\\\\\)").Count; // Each \" and \\ represents 1 extra character in the code
            charDifference += 3 * Regex.Matches(data, "(\\\\)(x)([0-9a-f])([0-9a-f])").Count; // Each \x represents 3 extra characters in the code

            return charDifference.ToString();
        }

        public string SecondHalf()
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "08.txt"));

            int charDifference = 0;

            charDifference += 2 * Regex.Matches(data, "\n").Count; // Each newline means 2 extra single quotes in the code
            charDifference += Regex.Matches(data, "(\")|(\\\\)").Count; // Each \ and " represents 1 extra character in the code

            return charDifference.ToString();
        }
    }
}
