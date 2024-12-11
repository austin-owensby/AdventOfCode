namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2023/25.txt
    public class Solution2023_25Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 25, example);

            Dictionary<string, List<string>> connections = [];

            // Catalog the list of verticies and their edges
            foreach (string line in lines)
            {
                string[] parts = line.Split(": ");
                string start = parts[0];
                List<string> ends = parts[1].Split(" ").ToList();
                
                if (!connections.ContainsKey(start)) {
                    connections[start] = [];
                }

                foreach (string end in ends) {
                    if (connections.ContainsKey(end)) {
                        connections[end].Add(start);
                    }
                    else {
                        connections[end] = [start];
                    }

                    connections[start].Add(end);
                }
            }

            Dictionary<string, List<string>> connectionsCopy = connections.ToDictionary();

            // Use Karger's algorithm to find the min cut
            int cuts = 0;
            int answer = 0;
            Random rnd = new();

            while (cuts != 3) {
                connections = connectionsCopy.ToDictionary(kv => kv.Key, kv => kv.Value.ToList());
                Dictionary<string, int> nodeSizes = connections.Keys.ToDictionary(k => k, k => 1);

                // Keep merging nodes until we're down to the final 2
                while (connections.Count > 2) {
                    // Randomly merge an edge
                    // Select 2 nodes to merge into 1 new node
                    int index = rnd.Next(connections.Count);
                    string node1 = connections.ElementAt(index).Key;

                    index = rnd.Next(connections[node1].Count);
                    string node2 = connections[node1][index];

                    // Create new node
                    string newNode = $"{node1} {node2}";
                    connections[newNode] = connections[node1].Where(c => c != node2).Concat(connections[node2].Where(c => c != node1)).ToList();

                    // Update connections from old nodes to new node
                    foreach (string node in connections[node1]) {
                        List<int> indexes = connections[node].FindIndexes(x => x == node1);
                        foreach (int i in indexes) {
                            connections[node][i] = newNode;
                        }
                    }

                    foreach (string node in connections[node2]) {
                        List<int> indexes = connections[node].FindIndexes(x => x == node2);
                        foreach (int i in indexes) {
                            connections[node][i] = newNode;
                        }
                    }

                    nodeSizes[newNode] = nodeSizes[node1] + nodeSizes[node2];

                    // Remove old nodes
                    connections.Remove(node1);
                    connections.Remove(node2);                    
                }
                cuts = connections.First().Value.Count;
                answer = nodeSizes[connections.First().Key] * nodeSizes[connections.Last().Key];
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            return "There is no problem for Day 25 part 2, solve all other problems to get the last star.";
        }
    }
}