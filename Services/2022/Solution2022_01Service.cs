namespace AdventOfCode.Services
{
    public class Solution2022_01Service : ISolutionDayService
    {
        public Solution2022_01Service() { }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 01, example);

            int answer = lines.ChunkByExclusive(string.IsNullOrWhiteSpace).Select(elf => elf.ToInts().Sum()).Max();

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 01, example);

            List<int> calories = lines.ChunkByExclusive(string.IsNullOrWhiteSpace).Select(elf => elf.ToInts().Sum()).ToList();

            int total3 = calories.OrderDescending().Take(3).Sum();

            return total3.ToString();
        }
    }
}