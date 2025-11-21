namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/12.txt
    public class Solution2025_12Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 12, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            return "There is no problem for Day 12 part 2, solve all other problems to get the last star.";
        }
    }
}