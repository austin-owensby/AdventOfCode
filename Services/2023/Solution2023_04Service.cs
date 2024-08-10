namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2023/04.txt
    public class Solution2023_04Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 4, example);

            int answer = 0;

            foreach (string line in lines)
            {
                List<string> numbers = line.QuickRegex(@"Card\s+\d+: (.+) \| (.+)");
                List<int> winningNumbers = numbers.First().Split(" ").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToInts();
                List<int> myNumbers = numbers.Last().Split(" ").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToInts();

                int matches = myNumbers.Intersect(winningNumbers).Count();

                if (matches > 0)
                {
                    answer += (int)Math.Pow(2, matches - 1);
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 4, example);

            int answer = 0;

            Dictionary<int, int> cardValues = [];

            foreach (int i in lines.Count) {
                cardValues[i + 1] = 1;
            }

            for (int cardNumber = lines.Count; cardNumber > 0; cardNumber--) {
                List<string> numbers = lines[cardNumber - 1].QuickRegex(@"Card\s+\d+: (.+) \| (.+)");
                List<int> winningNumbers = numbers.First().Split(" ").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToInts();
                List<int> myNumbers = numbers.Last().Split(" ").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToInts();

                int matches = myNumbers.Intersect(winningNumbers).Count();

                if (matches > 0)
                {
                    int cardValue = 0;
                    foreach (int i in matches) {
                        if (cardValues.TryGetValue(cardNumber + i + 1, out int value)) {
                            cardValue += value;
                        }
                    }
                    cardValues[cardNumber] += cardValue;
                }
            }

            answer = cardValues.Sum(card => card.Value);

            return answer.ToString();
        }
    }
}