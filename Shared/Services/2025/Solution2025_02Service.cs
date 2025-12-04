using System.Text;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/02.txt
    public class Solution2025_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 2, example);
            string[] ranges = lines.First().Split(",");

            long answer = 0;

            foreach (string range in ranges)
            {
                string[] parts = range.Split("-");
                string lower = parts.First();
                string upper = parts.Last();

                // An odd length means we can't have a repeating pair
                if (lower.Length == upper.Length && lower.Length % 2 == 1)
                {
                    continue;
                }

                // A mismatch in lengths where 1 is odd and the other even means we can reduce our search space
                // Based on a visual inspection of the data, our range never differs by a magnitude of 100 or greater
                if (lower.Length != upper.Length)
                {
                    if (lower.Length % 2 == 1)
                    {
                        // Raise the lower limit to the next valid value
                        StringBuilder newLower = new();
                        newLower.Append('1');
                        for (long i = 0; i < lower.Length; i++)
                        {
                            newLower.Append('0');
                        }
                        lower = newLower.ToString();
                    }
                    else
                    {
                        // Decrease the upper limit to the next valid value
                        StringBuilder newUpper = new();
                        for (long i = 0; i < upper.Length - 1; i++)
                        {
                            newUpper.Append('9');
                        }
                        upper = newUpper.ToString();
                    }
                }

                long start = long.Parse(lower[..(lower.Length / 2)]);
                long end = long.Parse(upper[..(upper.Length / 2)]);

                long invalidIds = end - start + 1;

                long invalidStart = long.Parse($"{start}{start}");

                if (invalidStart < long.Parse(lower))
                {
                    start++;
                    invalidStart = long.Parse($"{start}{start}");
                }

                long invalidEnd = long.Parse($"{end}{end}");

                if (invalidEnd > long.Parse(upper))
                {
                    end--;
                    invalidEnd = long.Parse($"{end}{end}");
                }

                long invalidId = invalidStart;
                long invalidPart = start;

                while (invalidId <= invalidEnd)
                {
                    answer += invalidId;
                    invalidPart++;
                    invalidId = long.Parse($"{invalidPart}{invalidPart}");
                }
            }

            return answer.ToString();
        }

        private static long GetInvalidIdSum(string lower, string upper)
        {
            List<long> invalidIds = [];

            // Check each group size
            for (int groupSize = 1; groupSize <= lower.Length / 2; groupSize++)
            {
                // Check if this is a valid group size
                if (lower.Length % groupSize != 0)
                {
                    continue;
                }
                
                int part = int.Parse(lower[..groupSize]);

                StringBuilder invalidIdBuilder = new();
                
                foreach (int i in lower.Length / groupSize)
                {
                    invalidIdBuilder.Append(part);
                }

                string invalidId = invalidIdBuilder.ToString();
                long invalidIdNumber = long.Parse(invalidId);

                if (invalidIdNumber < long.Parse(lower))
                {
                    // Move to the next invalid Id so that we're within the range
                    part++;

                    invalidIdBuilder.Clear();
                
                    foreach (int i in lower.Length / groupSize)
                    {
                        invalidIdBuilder.Append(part);
                    }

                    invalidId = invalidIdBuilder.ToString();
                    invalidIdNumber = long.Parse(invalidId);
                }

                long upperNumber = long.Parse(upper);

                while (invalidIdNumber <= upperNumber)
                {
                    invalidIds.Add(invalidIdNumber);

                    // Move on to the next id
                    part++;

                    invalidIdBuilder.Clear();
                
                    foreach (int i in lower.Length / groupSize)
                    {
                        invalidIdBuilder.Append(part);
                    }

                    invalidId = invalidIdBuilder.ToString();
                    invalidIdNumber = long.Parse(invalidId);
                }
            }

            return invalidIds.Distinct().Sum();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 2, example);
            string[] ranges = lines.First().Split(",");

            long answer = 0;

            foreach (string range in ranges)
            {
                string[] parts = range.Split("-");
                string lower = parts.First();
                string upper = parts.Last();

                if (lower.Length == upper.Length)
                {
                    answer += GetInvalidIdSum(lower, upper);
                }
                else
                {
                    // Split into 2 ranges with the same size groupings
                    StringBuilder newUpper = new();
                    StringBuilder newLower = new();
                    newLower.Append('1');
                    
                    for (int i = 0; i < lower.Length; i++)
                    {
                        newUpper.Append('9');
                        newLower.Append('0');
                    }

                    answer += GetInvalidIdSum(lower, newUpper.ToString());
                    answer += GetInvalidIdSum(newLower.ToString(), upper);
                }
            }

            return answer.ToString();
        }
    }
}