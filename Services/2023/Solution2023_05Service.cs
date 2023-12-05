namespace AdventOfCode.Services
{
    public class Solution2023_05Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 5, example);

            // Split up the input by sections
            List<string> sections = string.Join("\n", lines).Split("\n\n").ToList();

            // Initialize the current values to the seeds
            List<long> currentValues = sections.First().QuickRegex(@"seeds: (.*)").First().Split(' ').ToLongs();

            // Loop over each section, skip the first since that's the seed section
            foreach (string section in sections.Skip(1)) {
                // Parse the mappings for this section
                List<List<long>> maps = section.Split("\n").Skip(1).Select(line => line.Split(' ').ToLongs()).OrderBy(x => x[1]).ToList();

                // Map each value
                currentValues = currentValues.Select(value => {
                        List<long>? map = maps.LastOrDefault(map => map[1] <= value);

                        if (map == null || value - map[1] >= map[2]) {
                            return value;
                        }
                        else {
                            return value + map[0] - map[1];
                        }
                    }).ToList();
            }

            long answer = currentValues.Min();

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 5, example);

            // Split up the input by sections
            List<string> sections = string.Join("\n", lines).Split("\n\n").ToList();

            // Initialize the current ranges to the seed ranges
            List<List<long>> ranges = sections.First().QuickRegex(@"seeds: (.*)").First().Split(' ').ToLongs().Chunk(2).Select(x => new List<long>([x[0], x[0] + x[1] - 1])).ToList();

            // Loop over each section, skip the first since that's the seed section
            foreach (string section in sections.Skip(1)) {
                // Parse the mappings for this section
                List<List<long>> maps = section.Split("\n").Skip(1).Select(line => line.Split(' ').ToLongs()).OrderBy(x => x[1]).ToList();

                // Start building a new list of ranges
                List<List<long>> newRanges = [];

                // Loop over each existing range to find the new list of ranges
                foreach (List<long> range in ranges) {
                    // We may have to split our range, add it to a queue to process 
                    Queue<List<long>> rangesToProcess = new([range]);

                    while (rangesToProcess.Count > 0)
                    {
                        // Based on the start and end value of the range we're processing find the mapping those values fall into
                        List<long> rangeToProcess = rangesToProcess.Dequeue();
                        long start = rangeToProcess[0];
                        long end = rangeToProcess[1];
                        List<long>? startMap = maps.LastOrDefault(map => map[1] <= start);
                        List<long>? endMap = maps.LastOrDefault(map => map[1] <= end);

                        // Check if our mapping option is correct
                        if (startMap != null && start - startMap[1] >= startMap[2]) {
                            startMap = null;
                        }

                        if (endMap != null && end - endMap[1] >= endMap[2]) {
                            endMap = null;
                        }

                        // Check if the mappings are the same (There's probably a cleaner way to do this)
                        bool sameMap = startMap == null && endMap == null || startMap != null && endMap != null && startMap[0] == endMap[0] && startMap[1] == endMap[1] && startMap[2] == endMap[2];

                        if (sameMap) {
                            if (startMap == null) {
                                // No mapping for both, add range unaffected
                                newRanges.Add(rangeToProcess);
                            }
                            else {
                                // Same mapping for both, map entire range
                                newRanges.Add(rangeToProcess.Select(value => value + startMap[0] - startMap[1]).ToList());
                            }
                        }
                        else {
                            // Different maps, split the range where our 2 maps meet
                            long startMapLastValue = startMap != null ? startMap[1] + startMap[2] - 1 : endMap![1] - 1;
                            rangesToProcess.Enqueue([start, startMapLastValue]);
                            rangesToProcess.Enqueue([startMapLastValue + 1, end]);
                        }
                    }
                }

                ranges = newRanges;
            }

            long answer = ranges.Select(range => range[0]).Min();

            return answer.ToString();
        }
    }
}