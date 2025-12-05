namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/05.txt
    public class Solution2025_05Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 5, example);
            List<List<string>> parts = lines.ChunkByExclusive(l => string.IsNullOrWhiteSpace(l));
            List<(long, long)> ranges = parts.First().Select(p => p.Split('-')).Select(p => (long.Parse(p.First()), long.Parse(p.Last()))).ToList();
            List<long> ids = parts.Last().ToLongs();

            int answer = 0;

            foreach (long id in ids)
            {
                bool fresh = false;

                foreach ((long lower, long upper) in ranges)
                {
                    if (lower <= id && id <= upper)
                    {
                        fresh = true;
                        break;
                    }
                }

                if (fresh)
                {
                    answer++;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 5, example);
            List<(long lower, long upper)> ranges = lines.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).Select(p => p.Split('-')).Select(p => (long.Parse(p.First()), long.Parse(p.Last()))).OrderBy(p => p.Item1).ToList();

            long answer = 0;

            (long lower, long upper) = ranges.First();

            for (int i = 1; i < ranges.Count; i++)
            {
                (long nextLower, long nextUpper) = ranges[i];

                if (upper >= nextLower)
                {
                    // These ranges overlap
                    if (upper < nextUpper)
                    {
                        // This extend our current range with the next range's end
                        // |----A----|
                        //   |--B------|
                        upper = nextUpper;
                    }
                    // Otherwise, this range completely overlaps the current range
                }
                else
                {
                    // There is no overlap between these ranges, move on to the next range
                    answer += upper - lower + 1;
                    lower = nextLower;
                    upper = nextUpper;
                }
            }

            // Add the last range
            answer += upper - lower + 1;

            return answer.ToString();
        }
    }
}