namespace AdventOfCode.Services
{
    public class Solution2017_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2017, 02, example);

            int answer = 0;

            foreach (string line in lines)
            {
                List<int> values = line.Split('\t').ToInts();

                answer += values.Max() - values.Min();
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2017, 02, example);

            int answer = 0;

            foreach (string line in lines)
            {
                List<int> values = line.Split('\t').ToInts();

                bool matchFound = false;
                for (int i = 0; i < values.Count - 1; i++) {
                    for (int j = i + 1; j < values.Count; j++) {
                        if (values[i] % values[j] == 0) {
                            answer += values[i] / values[j];
                            matchFound = true;
                            break;
                        }
                        else if (values[j] % values[i] == 0) {
                            answer += values[j] / values[i];
                            matchFound = true;
                            break;
                        }
                    }

                    if (matchFound) {
                        break;
                    }
                }

            }

            return answer.ToString();
        }
    }
}