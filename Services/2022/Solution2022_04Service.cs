namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2022/04.txt
    public class Solution2022_04Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 04, example);

            int matches = lines.Select(line => line.QuickRegex(@"(\d+)-(\d+),(\d+)-(\d+)").ToInts()).Sum(digits =>
            {
                int value = 0;

                List<int> set1 = Enumerable.Range(digits[0], digits[1] - digits[0] + 1).ToList();
                List<int> set2 = Enumerable.Range(digits[2], digits[3] - digits[2] + 1).ToList();

                List<int> intersect = set1.Intersect(set2).ToList();

                if (intersect.SequenceEqual(set1) || intersect.SequenceEqual(set2))
                {
                    value = 1;
                }

                return value;
            });

            return matches.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 04, example);

            int matches = lines.Select(line => line.QuickRegex(@"(\d+)-(\d+),(\d+)-(\d+)").ToInts()).Sum(digits =>
            {
                int value = 0;

                List<int> set1 = Enumerable.Range(digits[0], digits[1] - digits[0] + 1).ToList();
                List<int> set2 = Enumerable.Range(digits[2], digits[3] - digits[2] + 1).ToList();

                List<int> intersect = set1.Intersect(set2).ToList();

                if (intersect.Any())
                {
                    value = 1;
                }

                return value;
            });

            return matches.ToString();
        }
    }
}