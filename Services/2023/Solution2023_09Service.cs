namespace AdventOfCode.Services
{
    public class Solution2023_09Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 9, example);

            List<List<int>> values = lines.Select(line => line.Split(' ').ToInts()).ToList();

            int answer = 0;

            foreach (List<int> value in values)
            {
                List<List<int>> histories = [value];

                while (!histories.Last().All(v => v == 0)) {
                    List<int> history = [];
                    for (int i = 0; i < histories.Last().Count - 1; i++) {
                        history.Add(histories.Last()[i + 1] - histories.Last()[i]);
                    }
                    histories.Add(history);
                }

                for (int i = histories.Count - 1; i > 0; i--) {
                    histories[i - 1].Add(histories[i - 1].Last() + histories[i].Last());
                }

                answer += histories.First().Last();
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 9, example);

            List<List<int>> values = lines.Select(line => line.Split(' ').ToInts()).ToList();

            int answer = 0;

            foreach (List<int> value in values)
            {
                List<List<int>> histories = [value];

                while (!histories.Last().All(v => v == 0)) {
                    List<int> history = [];
                    for (int i = 0; i < histories.Last().Count - 1; i++) {
                        history.Add(histories.Last()[i + 1] - histories.Last()[i]);
                    }
                    histories.Add(history);
                }

                for (int i = histories.Count - 1; i > 0; i--) {
                    histories[i - 1].Insert(0, histories[i - 1].First() - histories[i].First());
                }

                answer += histories.First().First();
            }

            return answer.ToString();
        }
    }
}