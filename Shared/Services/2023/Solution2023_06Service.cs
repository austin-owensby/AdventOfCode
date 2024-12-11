namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2023/06.txt
    public class Solution2023_06Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 6, example);

            List<int> times = lines.First().QuickRegex(@"Time:\s+(.*)").First().Split(' ').Where(x => x.Length > 0).ToInts();
            List<int> distances = lines.Last().QuickRegex(@"Distance:\s+(.*)").First().Split(' ').Where(x => x.Length > 0).ToInts();

            int answer = 1;

            foreach (int i in times.Count)
            {
                int time = times[i];
                int distance = distances[i];

                int raceWins = 0;

                foreach (int t in time) {
                    if ((time - t) * t > distance) {
                        raceWins++;
                    }
                }

                answer *= raceWins;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 6, example);

            long time = int.Parse(new string(lines.First().QuickRegex(@"Time:\s+(.*)").First().Where(x => x != ' ').ToArray()));
            long distance = long.Parse(new string(lines.Last().QuickRegex(@"Distance:\s+(.*)").First().Where(x => x != ' ').ToArray()));

            long answer = 0;

            foreach (long t in time) {
                if ((time - t) * t > distance) {
                    answer++;
                }
            }

            return answer.ToString();
        }
    }
}