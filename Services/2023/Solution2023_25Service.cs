namespace AdventOfCode.Services
{
    public class Solution2023_25Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 25, example);

            Dictionary<string, List<string>> connectionsMap = [];
            List<List<string>> connections = [];

            int answer = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(": ");
                string start = parts[0];
                List<string> ends = parts[1].Split(" ").ToList();

                foreach (string end in ends) {
                    List<string> connection = new List<string>(){start, end}.Order().ToList();
                    connections.Add(connection);
                }
            }

            foreach (string line in lines)
            {
                string[] parts = line.Split(": ");
                string start = parts[0];
                List<string> ends = parts[1].Split(" ").ToList();
                
                if (!connectionsMap.ContainsKey(start)) {
                    connectionsMap[start] = [];
                }

                foreach (string end in ends) {
                    if (connectionsMap.ContainsKey(end)) {
                        connectionsMap[end].Add(start);
                    }
                    else {
                        connectionsMap[end] = [start];
                    }

                    connectionsMap[start].Add(end);

                    List<string> connection = new List<string>(){start, end}.Order().ToList();
                    connections.Add(connection);
                }
            }

            connections = connections.DistinctBy(c => string.Join("", c)).ToList();

            // Check the size of the group
            for (int i = 0; i < connections.Count - 2; i++) {
                for (int j = i + 1; j < connections.Count - 1; j++) {
                    for (int k = j + 1; k < connections.Count; k++) {
                        List<List<string>> removedConnections = [connections[i], connections[j], connections[k]];

                        List<string> history = [];
                        Queue<string> queue = [];

                        queue.Enqueue(connectionsMap.First().Key);
                        history.Add(connectionsMap.First().Key);

                        while (queue.Count > 0) {
                            string point = queue.Dequeue();
                            List<string> nextPoints = connectionsMap[point].Where(s => !history.Contains(s)).ToList();

                            foreach (string nextPoint in nextPoints) {
                                if (!removedConnections.Any(rc => rc.Contains(nextPoint) && rc.Contains(point))) {
                                    queue.Enqueue(nextPoint);
                                    history.Add(nextPoint);
                                }
                            }
                        }

                        answer = history.Count * (connectionsMap.Count - history.Count);

                        if (answer != 0) {
                            break;
                        }
                    }

                    if (answer != 0) {
                        break;
                    }
                }

                if (answer != 0) {
                    break;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            return "There is no problem for Day 25 part 2, solve all other problems to get the last star.";
        }
    }
}