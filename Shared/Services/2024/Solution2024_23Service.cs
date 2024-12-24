namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/23.txt
    public class Solution2024_23Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 23, example);
            List<(string, string)> connections = lines.Select(l => l.Split("-")).Select(l => (l[0], l[1])).ToList();
            List<string> potentialStarts = connections.Select(l => l.Item1).Concat(connections.Select(l => l.Item2)).Where(l => l.StartsWith("t")).Distinct().ToList();

            List<string> targetConnections = [];

            foreach (string potentialStart in potentialStarts)
            {
                List<string> connection1s = connections.Where(c => c.Item1 == potentialStart || c.Item2 == potentialStart).Select(l => l.Item1 == potentialStart ? l.Item2 : l.Item1).ToList();

                for (int i = 0; i < connection1s.Count - 1; i++) {
                    for (int j = i + 1; j < connection1s.Count; j++) {
                        if (connections.Any(c => c.Item1 == connection1s[i] && c.Item2 == connection1s[j] || c.Item2 == connection1s[i] && c.Item1 == connection1s[j])) {
                            List<string> targetConnection = [potentialStart, connection1s[i], connection1s[j]];
                            string value = string.Join("", targetConnection.Order());

                            if (!targetConnections.Contains(value)) {
                                targetConnections.Add(value);
                            }
                        }
                    }
                }
            }

            int answer = targetConnections.Count;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 23, example);
            List<(string, string)> connections = lines.Select(l => l.Split("-")).Select(l => (l[0], l[1])).ToList();
            List<string> potentialStarts = connections.Select(l => l.Item1).Concat(connections.Select(l => l.Item2)).Distinct().ToList();

            List<List<string>> networks = [];

            int i = 0;

            foreach (string start in potentialStarts)
            {
                Console.WriteLine($"{++i}/{potentialStarts.Count}");
                List<string> startConnections = connections.Where(c => c.Item1 == start || c.Item2 == start).Select(l => l.Item1 == start ? l.Item2 : l.Item1).ToList();

                PriorityQueue<List<string>, int> queue = new();

                foreach (string startConnection in startConnections)
                {
                    queue.Enqueue([start, startConnection], -1);
                }

                while (queue.Count > 0) {
                    List<string> network = queue.Dequeue();

                    List<string> nextConnections = potentialStarts.Where(s => network.All(n => connections.Any(c => c.Item1 == s && c.Item2 == n || c.Item1 == n && c.Item2 == s))).ToList();

                    if (nextConnections.Count == 0){
                        networks.Add(network);
                        continue;
                    }

                    foreach (string nextConnection in nextConnections) {
                        List<string> newNetwork = [..network, nextConnection];
                        if (!networks.Any(n => newNetwork.All(x => n.Contains(x)))) {
                            queue.Enqueue(newNetwork, -newNetwork.Count);
                        }
                    }
                }
            }

            string answer = string.Join(",", networks.MaxBy(n => n.Count)!.Order());

            return answer;
        }
    }
}