namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/08.txt
    public class Solution2025_08Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 8, example);
            List<(int x, int y, int z)> points = lines.Select(l => l.Split(",")).Select(p => (int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]))).ToList();

            Dictionary<(int, int), double> distances = [];

            for (int i = 0; i < points.Count - 1; i++)
            {
                (int x1, int y1, int z1) = points[i];

                for (int j = i + 1; j < points.Count; j++)
                {
                    (int x2, int y2, int z2) = points[j];

                    int x = x1 - x2;
                    int y = y1 - y2;
                    int z = z1 - z2;

                    double distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));

                    distances[(i,j)] = distance;
                }
            }

            int limit = example ? 10 : 1000;

            List<(int, int)> connections = distances.OrderBy(d => d.Value).Take(limit).Select(x => x.Key).ToList();

            List<List<int>> circuits = [];

            foreach ((int a, int b) in connections)
            {
                List<List<int>> matchingCircuits = [];

                bool alreadyInCircuit = false;
                foreach (List<int> circuit in circuits)
                {
                    bool aMatch = circuit.Contains(a);
                    bool bMatch = circuit.Contains(b);

                    if (aMatch && bMatch)
                    {
                        alreadyInCircuit = true;
                        break;
                    }
                    else if (aMatch || bMatch)
                    {
                        matchingCircuits.Add(circuit);
                    }
                }

                if (alreadyInCircuit)
                {
                    continue;
                }

                if (matchingCircuits.Count == 0)
                {
                    // Create new circuit
                    circuits.Add([a, b]);
                }
                else if (matchingCircuits.Count == 1)
                {
                    // Add to existing circuit
                    if (matchingCircuits[0].Contains(a))
                    {
                        matchingCircuits[0].Add(b);
                    }
                    else
                    {
                        matchingCircuits[0].Add(a);
                    }
                }
                else
                {
                    // Merge multiple circuits
                    List<int> newCircuit = [];
                    foreach (List<int> circuit in matchingCircuits)
                    {
                        circuits.Remove(circuit);
                        newCircuit.AddRange(circuit);
                    }
                    circuits.Add(newCircuit);
                }            
            }

            int answer = circuits.Select(c => c.Count).OrderDescending().Take(3).Aggregate(1, (a, b) => a * b);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 8, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }
    }
}