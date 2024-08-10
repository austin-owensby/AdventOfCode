namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2019/06.txt
    public class Solution2019_06Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 06, example);
            List<(string, string)> orbits = lines.Select(x => (x.Split(")")[0], x.Split(")")[1])).ToList();
            Queue<string> queue = new();
            queue.Enqueue("COM");
            Dictionary<string, int> values = [];
            values["COM"] = 0;

            while (queue.Count > 0) {
                string currentOrbit = queue.Dequeue();
                List<string> nextOrbits = orbits.Where(o => o.Item1 == currentOrbit).Select(o => o.Item2).ToList();

                foreach (string orbit in nextOrbits) {
                    queue.Enqueue(orbit);
                    values[orbit] = values[currentOrbit] + 1;
                }
            }

            int answer = values.Sum(v => v.Value);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 06, example);
            List<(string, string)> orbits = lines.Select(x => (x.Split(")")[0], x.Split(")")[1])).ToList();

            List<List<string>> paths = [["YOU"]];
            List<string> history = ["YOU"];

            int answer = 0;

            bool endNodeFound = false;
            
            while (!endNodeFound) {
                List<List<string>> newPaths = [];
                foreach (List<string> path in paths) {
                    List<string> nextNodes = orbits.Where(o => o.Item1 == path.Last()).Select(o => o.Item2).ToList();
                    nextNodes.AddRange(orbits.Where(o => o.Item2 == path.Last()).Select(o => o.Item1));
                    nextNodes = nextNodes.Where(o => !history.Contains(o)).ToList();

                    List<string> originalPath = path.ToList();
                    foreach (int i in nextNodes.Count) {
                        history.Add(nextNodes[i]);

                        if (nextNodes[i] == "SAN") {
                            endNodeFound = true;
                            // Subtract 2 to exclude the endpoints
                            answer = path.Count - 2;
                            break;
                        }

                        if (i == 0) {
                            path.Add(nextNodes[i]);
                        }
                        else {
                            List<string> newPath = originalPath.ToList();
                            newPath.Add(nextNodes[i]);
                            newPaths.Add(newPath);
                        }
                    }

                    if (endNodeFound) {
                        break;
                    }
                }
                paths.AddRange(newPaths);
            }

            return answer.ToString();
        }
    }
}