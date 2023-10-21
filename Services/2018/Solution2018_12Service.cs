namespace AdventOfCode.Services
{
    public class Solution2018_12Service : ISolutionDayService
    {
        public Solution2018_12Service() { }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 12, example).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
            List<int> currentState = lines.Shift().Remove(0, "initial state: ".Length).FindIndexes(c => c == '#');
            Dictionary<string, string> rules = lines.QuickRegex(@"([\.#]{5}) => ([\.#])").ToDictionary(l => l.First(), l => l.Last());

            for (int i = 0; i < 20; i++) {
                List<int> nextState = new();

                int minIndex = currentState.Min() - 2;
                int maxIndex = currentState.Max() + 2;

                for (int index = minIndex; index <= maxIndex; index++) {
                    string key = "";

                    for (int j = index - 2; j <= index + 2; j++) {
                        key += currentState.Contains(j) ? "#" : ".";
                    }

                    if (rules[key] == "#") {
                        nextState.Add(index);
                    }
                }

                currentState = nextState;
            }

            int answer = currentState.Sum();

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 12, example).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
            List<long> currentState = lines.Shift().Remove(0, "initial state: ".Length).FindIndexes(c => c == '#').ToLongs();
            Dictionary<string, string> rules = lines.QuickRegex(@"([\.#]{5}) => ([\.#])").ToDictionary(l => l.First(), l => l.Last());

            long answer = 0;

            for (long i = 0; i < 50000000000; i++) {
                List<long> nextState = new();

                long minIndex = currentState.Min() - 2;
                long maxIndex = currentState.Max() + 2;

                for (long index = minIndex; index <= maxIndex; index++) {
                    string key = "";

                    for (long j = index - 2; j <= index + 2; j++) {
                        key += currentState.Contains(j) ? "#" : ".";
                    }

                    if (rules[key] == "#") {
                        nextState.Add(index);
                    }
                }

                // The generations eventually stabilize meaning that the next state is identical to the previous advanced by 1 place
                if (currentState.Count == nextState.Count) {
                    bool stabilized = true;
                    for (long j = 0; j < currentState.Count; j++) {
                        if (nextState.ElementAt((int)j) - currentState[(int)j] != 1) {
                            stabilized = false;
                            break;
                        }
                    }

                    if (stabilized) {
                        // Once we've stabilized, we can get the final result by advancing it the remaining amount
                        answer = currentState.Sum() + currentState.Count * (50000000000 - i);
                        break;
                    }
                }

                currentState = nextState;
            }

            return answer.ToString();
        }
    }
}