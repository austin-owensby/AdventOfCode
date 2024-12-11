namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2015/03.txt
    public class Solution2015_03Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "03.txt"));

            Tuple<int, int> currentLocation = new(0, 0);
            Dictionary<Tuple<int, int>, int> locationsVisited = new() { { currentLocation, 1 } };

            foreach (char character in data)
            {
                // Move to the new location
                currentLocation = character switch
                {
                    '>' => Tuple.Create(currentLocation.Item1 + 1, currentLocation.Item2),
                    '<' => Tuple.Create(currentLocation.Item1 - 1, currentLocation.Item2),
                    '^' => Tuple.Create(currentLocation.Item1, currentLocation.Item2 + 1),
                    'v' => Tuple.Create(currentLocation.Item1, currentLocation.Item2 - 1),
                    _ => currentLocation
                };

                // Update the number of presents at the current location in the dictionary
                locationsVisited[currentLocation] = locationsVisited.ContainsKey(currentLocation) ? locationsVisited[currentLocation] + 1 : 1;
            }

            return locationsVisited.Count.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "03.txt"));

            Tuple<int, int> currentLocation = new(0, 0);
            Tuple<int, int> robotsCurrentLocation = new(0, 0);
            Dictionary<Tuple<int, int>, int> locationsVisited = new() { { currentLocation, 2 } };

            bool robotSantasTurn = false;

            foreach (char character in data)
            {
                // Move to the new location
                if (robotSantasTurn)
                {
                    robotsCurrentLocation = character switch
                    {
                        '>' => Tuple.Create(robotsCurrentLocation.Item1 + 1, robotsCurrentLocation.Item2),
                        '<' => Tuple.Create(robotsCurrentLocation.Item1 - 1, robotsCurrentLocation.Item2),
                        '^' => Tuple.Create(robotsCurrentLocation.Item1, robotsCurrentLocation.Item2 + 1),
                        'v' => Tuple.Create(robotsCurrentLocation.Item1, robotsCurrentLocation.Item2 - 1),
                        _ => robotsCurrentLocation
                    };

                    // Update the number of presents at the current location in the dictionary
                    locationsVisited[robotsCurrentLocation] = locationsVisited.ContainsKey(robotsCurrentLocation) ? locationsVisited[robotsCurrentLocation] + 1 : 1;
                }
                else
                {
                    currentLocation = character switch
                    {
                        '>' => Tuple.Create(currentLocation.Item1 + 1, currentLocation.Item2),
                        '<' => Tuple.Create(currentLocation.Item1 - 1, currentLocation.Item2),
                        '^' => Tuple.Create(currentLocation.Item1, currentLocation.Item2 + 1),
                        'v' => Tuple.Create(currentLocation.Item1, currentLocation.Item2 - 1),
                        _ => currentLocation
                    };

                    // Update the number of presents at the current location in the dictionary
                    locationsVisited[currentLocation] = locationsVisited.ContainsKey(currentLocation) ? locationsVisited[currentLocation] + 1 : 1;
                }


                robotSantasTurn = !robotSantasTurn;
            }

            return locationsVisited.Count.ToString();
        }
    }
}