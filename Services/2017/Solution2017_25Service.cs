namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2017/25.txt
    public class Solution2017_25Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2017, 25, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            return "There is no problem for Day 25 part 2, solve all other problems to get the last star.";
        }
    }
}