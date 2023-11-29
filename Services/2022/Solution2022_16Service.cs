namespace AdventOfCode.Services
{
    public class Solution2022_16Service : ISolutionDayService
    {
                private class Valve {
            public string Name {get; set;} = string.Empty;
            public int FlowRate {get; set;}
            public List<string> ConnectedValves {get; set;} = new();
        }

        private List<Valve> Valves {get; set;} = new();
        private Dictionary<string, int> Distances {get; set;} = new();

        /// <summary>
        /// Find the distance betwwen 2 valves by checking the starting valves reach and then increasing that reach 1 distance at a time until the end valve is found
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int GetValveDistance(string start, string end) {
            List<string> valveReach = new(){
                start,
            };

            int distance = 0;

            while (!valveReach.Contains(end)) {
                distance++;
                
                // Get all connected valves at one more distance
                List<string> newReach = Valves.Where(v => valveReach.Contains(v.Name)).SelectMany(v => v.ConnectedValves).ToList();
                valveReach.AddRange(newReach);
                valveReach = valveReach.Distinct().ToList();
            }

            return distance;
        }

        /// <summary>
        /// Given a starting valve, time left, and available valves calculate the max amount of released pressure
        /// </summary>
        /// <param name="start"></param>
        /// <param name="availableValves"></param>
        /// <param name="timeLeft"></param>
        /// <returns></returns>
        private int GetPressureReleased(string start, List<string> availableValves, int timeLeft) {
            int maxPressureReleased = availableValves.Max(valve => {
                int pressureReleased = 0;

                List<string> keys = new() {start, valve};
                string key = string.Join(" ", keys.Order());
                int distance = Distances[key];

                int newTimeLeft = timeLeft - distance - 1;

                if (newTimeLeft > 0) {
                    int rate = Valves.First(v => v.Name == valve).FlowRate;
                    pressureReleased += newTimeLeft * rate;
                    List<string> newAvailableValves = availableValves.Where(v => v != valve).ToList();

                    if (newAvailableValves.Any()) {
                        pressureReleased += GetPressureReleased(valve, newAvailableValves, newTimeLeft);
                    }
                }

                return pressureReleased;
            });

            return maxPressureReleased;
        }

        private int GetPressureReleasedWithElephant(string start, string elephantStart, List<string> availableValves, int timeLeft, int elephantTimeLeft) {          
            int maxPressureReleased = availableValves.Max(valve => {
                int pressureReleased = 0;

                if (timeLeft > elephantTimeLeft) {
                    // Our turn

                    // Handle move
                    List<string> keys = new() {start, valve};
                    string key = string.Join(" ", keys.Order());
                    int distance = Distances[key];

                    int newTimeLeft = timeLeft - distance - 1;

                    // Only make the step if there's enough time
                    List<string> newAvailableValves = availableValves.Where(v => v != valve).ToList();

                    int minDistance = newAvailableValves.Any() ? newAvailableValves.Min(v => {
                        List<string> keys = new() {valve, v};
                        string key = string.Join(" ", keys.Order());
                        return Distances[key];
                    }) : 0; 

                    int rate = Valves.First(v => v.Name == valve).FlowRate;

                    if (newTimeLeft > 0) {
                        // Take our step
                        pressureReleased += newTimeLeft * rate;

                        // If we or the elephant can make another step
                        if (newAvailableValves.Any() && (newTimeLeft > minDistance || elephantTimeLeft > minDistance)) {
                            pressureReleased += GetPressureReleasedWithElephant(valve, elephantStart, newAvailableValves, newTimeLeft, elephantTimeLeft);
                        }
                    }
                }
                else {
                    // Elephant's turn

                    // Handle move
                    List<string> keys = new() {elephantStart, valve};
                    string key = string.Join(" ", keys.Order());
                    int distance = Distances[key];

                    int newTimeLeftElephant = elephantTimeLeft - distance - 1;

                    // Only make the step if there's enough time
                    List<string> newAvailableValves = availableValves.Where(v => v != valve).ToList();

                    int minDistance = newAvailableValves.Any() ? newAvailableValves.Min(v => {
                        List<string> keys = new() {valve, v};
                        string key = string.Join(" ", keys.Order());
                        return Distances[key];
                    }) : 0; 

                    int rate = Valves.First(v => v.Name == valve).FlowRate;
                
                    if (newTimeLeftElephant > 0) {
                        // Take the elephant's step
                        pressureReleased += newTimeLeftElephant * rate;

                        // If we or the elephant can make another step
                        if (newAvailableValves.Any() && (newTimeLeftElephant > minDistance || timeLeft > minDistance)) {
                            pressureReleased += GetPressureReleasedWithElephant(start, valve, newAvailableValves, timeLeft, newTimeLeftElephant);
                        }
                    }
                }
                
                return pressureReleased;
            });

            return maxPressureReleased;
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 16, example);

            // Parse the file into a list of valves
            Valves = lines.QuickRegex(@"Valve (.+) has flow rate=(\d+); tunnels? leads? to valves? (.+)").Select(valve => new Valve(){
                Name = valve[0],
                FlowRate = int.Parse(valve[1]),
                ConnectedValves = valve[2].SplitSubstring(", ")
            }).ToList();

            // Find only the valves that are working to visit
            List<string> availableValves = Valves.Where(v => v.FlowRate > 0).Select(v => v.Name).Order().ToList();

            // Calculate the distances between all working valves + the start
            List<string> valves = availableValves.Select(n => n).ToList();
            valves.Add("AA");
            valves = valves.Order().ToList();

            foreach (int i in valves.Count) {
                for (int j = i + 1; j < valves.Count; j++) {
                    string node1 = valves[i];
                    string node2 = valves[j];

                    Distances.Add($"{node1} {node2}", GetValveDistance(node1, node2));
                }
            }

            int answer = GetPressureReleased("AA", availableValves, 30);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 16, example);

            // Parse the file into a list of valves
            Valves = lines.QuickRegex(@"Valve (.+) has flow rate=(\d+); tunnels? leads? to valves? (.+)").Select(valve => new Valve(){
                Name = valve[0],
                FlowRate = int.Parse(valve[1]),
                ConnectedValves = valve[2].SplitSubstring(", ")
            }).ToList();

            // Find only the valves that are working to visit
            List<string> availableValves = Valves.Where(v => v.FlowRate > 0).Select(v => v.Name).Order().ToList();

            // Calculate the distances between all working valves + the start
            List<string> valves = availableValves.Select(n => n).ToList();
            valves.Add("AA");
            valves = valves.Order().ToList();

            foreach (int i in valves.Count) {
                for (int j = i + 1; j < valves.Count; j++) {
                    string node1 = valves[i];
                    string node2 = valves[j];

                    Distances.Add($"{node1} {node2}", GetValveDistance(node1, node2));
                }
            }

            int answer = GetPressureReleasedWithElephant("AA", "AA", availableValves, 26, 26);

            // 2549 Too low

            return answer.ToString();
        }
    }
}