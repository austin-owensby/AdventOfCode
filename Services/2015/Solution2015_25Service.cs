namespace AdventOfCode.Services
{
    public class Solution2015_25Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2015, 25, example);

            List<int> values = lines.First().QuickRegex(@"To continue, please consult the code grid in the manual.  Enter the code at row (\d+), column (\d+).").ToInts();

            int row = values.First();
            int column = values.Last();

            int item = Enumerable.Range(1, column).Sum();
            int step = column;

            for (int i = 1; i < row; i++) {
                item += step;
                step++;
            }

            long answer = 20151125;

            foreach (int i in item - 1) {
                answer = answer * (long)252533 % (long)33554393;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            return "There is no problem for Day 25 part 2, solve all other problems to get the last star.";
        }
    }
}