using System.Text.RegularExpressions;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/03.txt
    public class Solution2024_03Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 3, example);

            int answer = 0;

            foreach (string line in lines)
            {
                string regex = @"mul\(\d+,\d+\)";

                Regex rx = new(regex);

                MatchCollection matches = rx.Matches(line);

                foreach (Match match in matches)
                {
                    List<int> values = match.Value.Replace("mul(", "").Replace(")","").Split(",").ToInts();
                    answer += values[0] * values[1];
                }

            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 3, example);

            int answer = 0;

            bool enabled = true;

            foreach (string line in lines)
            {
                string regex = @"mul\(\d+,\d+\)|do\(\)|don't\(\)";

                Regex rx = new(regex);

                MatchCollection matches = rx.Matches(line);

                foreach (Match match in matches)
                {
                    if (match.Value.StartsWith("do")) {
                        enabled = match.Value == "do()";
                        continue;
                    }
                    
                    if (enabled) {
                        List<int> values = match.Value.Replace("mul(", "").Replace(")","").Split(",").ToInts();
                        answer += values[0] * values[1];
                    }
                }

            }

            return answer.ToString();
        }
    }
}