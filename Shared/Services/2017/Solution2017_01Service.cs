namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2017/01.txt
    public class Solution2017_01Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2017, 01, example);
            List<int> values = lines.First().ToInts();

            int answer = values.Last() == values.First() ? values.First() : 0;

            foreach (int i in values.Count - 1)
            {
                if (values[i] == values[i + 1]) {
                    answer += values[i];
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2017, 01, example);
            List<int> values = lines.First().ToInts();

            int answer = 0;

            foreach (int i in values.Count - 1)
            {
                int nextIndex = (i + values.Count / 2) % values.Count;
                if (values[i] == values[nextIndex]) {
                    answer += values[i];
                }
            }
            
            return answer.ToString();
        }
    }
}