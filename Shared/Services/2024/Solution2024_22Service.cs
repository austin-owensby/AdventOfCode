namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/22.txt
    public class Solution2024_22Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 22, example);
            List<long> secretNumbers = lines.ToLongs();
            int loops = 2000;
            int pruneValue = 16777216;

            long answer = 0;

            foreach (int i in secretNumbers.Count) {
                long secretNumber = secretNumbers[i];
                foreach (int loop in loops)
                {
                    secretNumber ^= secretNumber * 64;
                    secretNumber %= pruneValue;
                    secretNumber ^= secretNumber / 32;
                    secretNumber %= pruneValue;
                    secretNumber ^= secretNumber * 2048;
                    secretNumber %= pruneValue;
                }
                answer += secretNumber;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 22, example);
            List<long> secretNumbers = lines.ToLongs();
            int loops = 2000;
            int pruneValue = 16777216;

            long answer = 0;

            List<Dictionary<(long, long, long, long), long>> sequenceValues = [];

            foreach (int i in secretNumbers.Count) {
                Dictionary<(long, long, long, long), long> sequenceValueItem = [];
                List<long> sequenceHistory = [];
                long secretNumber = secretNumbers[i];
                foreach (int loop in loops)
                {
                    long digit = secretNumber % 10;
                    secretNumber ^= secretNumber * 64;
                    secretNumber %= pruneValue;
                    secretNumber ^= secretNumber / 32;
                    secretNumber %= pruneValue;
                    secretNumber ^= secretNumber * 2048;
                    secretNumber %= pruneValue;
                    long nextDigit = secretNumber % 10; 
                    sequenceHistory.Add(nextDigit - digit);

                    sequenceHistory = sequenceHistory.TakeLast(4).ToList();

                    if (sequenceHistory.Count == 4) {
                        (long, long, long, long) key = (sequenceHistory[0], sequenceHistory[1], sequenceHistory[2], sequenceHistory[3]);
                        if (!sequenceValueItem.ContainsKey(key)) {
                            sequenceValueItem[key] = nextDigit;
                        }
                    }
                }
                sequenceValues.Add(sequenceValueItem);
            }

            List<(long, long, long, long)> keys = sequenceValues.SelectMany(x => x.Keys).Distinct().ToList();
            answer = keys.Max(key => sequenceValues.Sum(s => s.ContainsKey(key) ? s[key] : 0));

            return answer.ToString();
        }
    }
}