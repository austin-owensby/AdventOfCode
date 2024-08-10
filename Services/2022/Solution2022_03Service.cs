namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2022/03.txt
    public class Solution2022_03Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 03, example);

            int sum = lines.Sum(line =>
            {
                List<char> firstHalf = line.Take(line.Length / 2).ToList();
                List<char> secondHalf = line.TakeLast(line.Length / 2).ToList();

                char itemType = firstHalf.Intersect(secondHalf).First();

                return itemType.GetCharValue();
            });

            return sum.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 03, example);

            int sum = lines.Chunk(3).Sum(c =>
            {
                char itemType = c[0].Intersect(c[1]).Intersect(c[2]).First();

                return itemType.GetCharValue();
            });

            return sum.ToString();
        }
    }
}