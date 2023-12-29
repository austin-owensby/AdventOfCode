namespace AdventOfCode.Services
{
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
                int pairCount = 0;
                foreach (int i in digits.Count - 1) {
                    if (digits[i] > digits[i + 1]) {
                        // Digits decrease, invalid number
                        validTest = false;
                        break;
                    }

                    if (digits[i] == digits[i + 1]) {
                        // Digits are a pair
                        pairFound = true;

                        pairCount = pairCount == 0 ? 2 : pairCount + 1;
                    }
                    else {
                        // Digits are not a pair (Increase)
                        if (pairCount % 2 != 0) {
                            // If we were in the middle of processing a pair, check if it was valid now that it's ended
                            validTest = false;
                            break;
                        }
                        pairCount = 0;
                    }
                }

                if (pairCount % 2 != 0) {
                    // If we were in the middle of processing a pair, check if it was valid now that it's ended
                    validTest = false;
                }

                if (validTest && pairFound) {
                    answer++;
                }
            }

            // 396 is too high, 302 is too low
            return answer.ToString();
        }
    }
}