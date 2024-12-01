namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/01.txt
    public class Solution2024_01Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 1, example);
            List<List<int>> inputs = lines.Select(l => l.Split("   ").ToInts()).ToList();
            List<int> leftList = inputs.Select(i => i[0]).Order().ToList();
            List<int> rightList = inputs.Select(i => i[1]).Order().ToList();

            int answer = 0;

            foreach (int i in leftList.Count)
            {
                answer += Math.Abs(leftList[i] - rightList[i]);
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 1, example);
            List<List<int>> inputs = lines.Select(l => l.Split("   ").ToInts()).ToList();
            List<int> leftList = inputs.Select(i => i[0]).Order().ToList();
            List<int> rightList = inputs.Select(i => i[1]).Order().ToList();

            int answer = 0;

            foreach (int x in leftList)
            {
                answer += x * rightList.Count(y => y == x);
            }

            return answer.ToString();
        }
    }
}