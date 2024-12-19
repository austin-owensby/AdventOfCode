namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/19.txt
    public class Solution2024_19Service : ISolutionDayService
    {
        Dictionary<string, bool> hasSolutionMap = [];
        Dictionary<string, long> solutionCountMap = [];
        List<string> towels = [];

        private bool hasSolution(string pattern) {
            if (pattern.Length == 0) {
                return true;
            }

            if (hasSolutionMap.TryGetValue(pattern, out bool value)) {
                return value;
            }

            foreach (string towel in towels) {
                if (pattern.StartsWith(towel)) {
                    value = hasSolution(pattern[towel.Length..]);
                    if (value) {
                        hasSolutionMap[pattern] = true;
                        return true;
                    }
                }
            }

            hasSolutionMap[pattern] = false;
            return false;
        }

        private long solutionCount(string pattern) {
            if (pattern.Length == 0) {
                return 1;
            }

            if (solutionCountMap.TryGetValue(pattern, out long value)) {
                return value;
            }

            foreach (string towel in towels) {
                if (pattern.StartsWith(towel)) {
                    value += solutionCount(pattern[towel.Length..]);
                }
            }

            solutionCountMap[pattern] = value;
            return value;
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 19, example);
            towels = lines[0].Split(", ").ToList();
            List<string> patterns = lines.Skip(2).ToList();

            int answer = 0;

            foreach (string pattern in patterns)
            {
                if (hasSolution(pattern)) {
                    answer++;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 19, example);
            towels = lines[0].Split(", ").ToList();
            List<string> patterns = lines.Skip(2).ToList();

            long answer = 0;

            foreach (string pattern in patterns)
            {
                answer += solutionCount(pattern);
            }

            return answer.ToString();
        }
    }
}