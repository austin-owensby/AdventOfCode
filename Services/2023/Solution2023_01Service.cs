namespace AdventOfCode.Services
{
    public class Solution2023_01Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 1, example);

            int answer = 0;

            foreach (string line in lines)
            {                
                answer += int.Parse($"{line.First(c => char.IsNumber(c))}{line.Last(c => char.IsNumber(c))}");
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 1, example);

            int answer = 0;

            string[] numbers = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

            foreach (string line in lines)
            {
                char minValue = line.First(c => char.IsNumber(c));
                int minIndex = line.IndexOf(minValue);
                char maxValue = line.Last(c => char.IsNumber(c));
                int maxIndex = line.LastIndexOf(maxValue);

                foreach (int i in numbers.Length) {
                    int newMinIndex = line.IndexOf(numbers[i]);
                    if (newMinIndex != -1 && newMinIndex < minIndex) {
                        minIndex = newMinIndex;
                        minValue = $"{i + 1}"[0];
                    }

                    int newMaxIndex = line.LastIndexOf(numbers[i]);
                    if (newMaxIndex > maxIndex) {
                        maxIndex = newMaxIndex;
                        maxValue = $"{i + 1}"[0];
                    }
                }

                int value = int.Parse($"{minValue}{maxValue}");

                answer += value;
            }

            return answer.ToString();
        }
    }
}