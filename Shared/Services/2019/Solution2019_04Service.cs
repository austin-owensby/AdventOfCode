namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2019/04.txt
    public class Solution2019_04Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 04, example);
            int min = int.Parse(lines.First().Split('-')[0]);
            int max = int.Parse(lines.First().Split('-')[1]);

            int answer = 0;

            for (int test = min; test <= max; test++)
            {
                List<int> digits = test.ToString().ToList().Select(c => c.ToInt()).ToList();

                bool validTest = true;
                bool pairFound = false;
                foreach (int i in digits.Count - 1) {
                    if (digits[i] > digits[i + 1]) {
                        validTest = false;
                        break;
                    }

                    if (digits[i] == digits[i + 1]) {
                        pairFound = true;
                    }
                }

                if (validTest && pairFound) {
                    answer++;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 04, example);
            int min = int.Parse(lines.First().Split('-')[0]);
            int max = int.Parse(lines.First().Split('-')[1]);

            int answer = 0;

            for (int test = min; test <= max; test++)
            {
                List<int> digits = test.ToString().ToList().Select(c => c.ToInt()).ToList();

                bool validTest = true;
                bool pairFound = false;
                bool pairsValid = true;
                foreach (int i in digits.Count - 1) {
                    if (digits[i] > digits[i + 1]) {
                        // Digits decrease, invalid number
                        validTest = false;
                    }

                    if (digits[i] == digits[i + 1] && digits.Count(d => d == digits[i]) == 2) {
                        pairFound = true;
                    }
                }

                if (validTest && pairFound && pairsValid) {
                    answer++;
                }
            }

            // 396 is too high, 302 is too low
            return answer.ToString();
        }
    }
}