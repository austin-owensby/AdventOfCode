namespace AdventOfCode.Services
{
    public class Solution2023_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 2, example);

            int redCount = 12;
            int greenCount = 13;
            int blueCount = 14;

            int answer = 0;

            foreach (string line in lines)
            {
                List<string> matches = line.QuickRegex(@"Game (\d+): (.*)");
                int id = int.Parse(matches.First());
                List<string> groups = matches.Last().Split("; ").ToList();

                bool possible = true;

                foreach (string group in groups) {
                    List<Tuple<int, string>> colors = group.Split(", ").QuickRegex(@"(\d+) (\w+)").Select(x => new Tuple<int, string>(int.Parse(x.First()), x.Last())).ToList();

                    foreach (Tuple<int, string> color in colors)
                    {
                        if (color.Item2 == "blue") {
                            if (color.Item1 > blueCount) {
                                possible = false;
                                break;
                            }
                        }
                        else if (color.Item2 == "red") {
                            if (color.Item1 > redCount) {
                                possible = false;
                                break;
                            }
                        }
                        else {
                            if (color.Item1 > greenCount) {
                                possible = false;
                                break;
                            }
                        }
                    }
                }

                if (possible) {
                    answer += id;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 2, example);

            int answer = 0;

            foreach (string line in lines)
            {
                List<string> matches = line.QuickRegex(@"Game \d+: (.*)");
                List<string> groups = matches.First().Split("; ").ToList();

                int blueMax = 0;
                int redMax = 0;
                int greenMax = 0;

                foreach (string group in groups) {
                    List<Tuple<int, string>> colors = group.Split(", ").QuickRegex(@"(\d+) (\w+)").Select(x => new Tuple<int, string>(int.Parse(x.First()), x.Last())).ToList();

                    foreach (Tuple<int, string> color in colors)
                    {
                        if (color.Item2 == "blue") {
                            if (color.Item1 > blueMax) {
                                blueMax = color.Item1;
                            }
                        }
                        else if (color.Item2 == "red") {
                            if (color.Item1 > redMax) {
                                redMax = color.Item1;
                            }
                        }
                        else {
                            if (color.Item1 > greenMax) {
                                greenMax = color.Item1;
                            }
                        }
                    }
                }

                answer += blueMax * greenMax * redMax;
            }

            return answer.ToString();
        }
    }
}