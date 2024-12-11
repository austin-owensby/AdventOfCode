namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2019/12.txt
    public class Solution2019_12Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 12, example);
            List<List<int>> planets = lines.QuickRegex("<x=(-?\\d+), y=(-?\\d+), z=(-?\\d+)>").ToInts();
             // Add values for the velocities
            planets.ForEach(planet => {planet.AddRange([0, 0, 0]);});

            List<List<int>> interactions = Utility.GetCombinations(Enumerable.Range(0, planets.Count), 2).Select(c => c.ToList()).ToList();

            foreach (int i in 1000)
            {
                foreach (List<int> interaction in interactions) {
                    List<int> planet1 = planets[interaction[0]];
                    List<int> planet2 = planets[interaction[1]];

                    if (planet1[0] < planet2[0]) {
                        planet1[3]++;
                        planet2[3]--;
                    }
                    else if (planet1[0] > planet2[0]) {
                        planet1[3]--;
                        planet2[3]++;
                    }

                    if (planet1[1] < planet2[1]) {
                        planet1[4]++;
                        planet2[4]--;
                    }
                    else if (planet1[1] > planet2[1]) {
                        planet1[4]--;
                        planet2[4]++;
                    }

                    if (planet1[2] < planet2[2]) {
                        planet1[5]++;
                        planet2[5]--;
                    }
                    else if (planet1[2] > planet2[2]) {
                        planet1[5]--;
                        planet2[5]++;
                    }
                }
            
                foreach (List<int> planet in planets) {
                    planet[0] += planet[3];
                    planet[1] += planet[4];
                    planet[2] += planet[5];
                }
            }

            int answer = planets.Sum(planet => (Math.Abs(planet[0]) + Math.Abs(planet[1]) + Math.Abs(planet[2])) * (Math.Abs(planet[3]) + Math.Abs(planet[4]) + Math.Abs(planet[5])));

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 12, example);
            List<List<int>> planets = lines.QuickRegex("<x=(-?\\d+), y=(-?\\d+), z=(-?\\d+)>").ToInts();
             // Add values for the velocities
            planets.ForEach(planet => {planet.AddRange([0, 0, 0]);});

            List<List<int>> originalPlanets = planets.Select(p => p.ToList()).ToList();

            List<List<int>> interactions = Utility.GetCombinations(Enumerable.Range(0, planets.Count), 2).Select(c => c.ToList()).ToList();

            long answer = 0;

            long xPeriod = 0;
            long yPeriod = 0;
            long zPeriod = 0;

            long i = 0;

            while (answer == 0)
            {
                // Update velocities based on each planet interacting with each other
                foreach (List<int> interaction in interactions) {
                    List<int> planet1 = planets[interaction[0]];
                    List<int> planet2 = planets[interaction[1]];

                    if (planet1[0] < planet2[0]) {
                        planet1[3]++;
                        planet2[3]--;
                    }
                    else if (planet1[0] > planet2[0]) {
                        planet1[3]--;
                        planet2[3]++;
                    }

                    if (planet1[1] < planet2[1]) {
                        planet1[4]++;
                        planet2[4]--;
                    }
                    else if (planet1[1] > planet2[1]) {
                        planet1[4]--;
                        planet2[4]++;
                    }

                    if (planet1[2] < planet2[2]) {
                        planet1[5]++;
                        planet2[5]--;
                    }
                    else if (planet1[2] > planet2[2]) {
                        planet1[5]--;
                        planet2[5]++;
                    }
                }
            
                // Update positions based on velocity
                foreach (List<int> planet in planets) {
                    planet[0] += planet[3];
                    planet[1] += planet[4];
                    planet[2] += planet[5];
                }

                i++;

                // Check in each dimension if we match our original positions and velocities
                bool xMatches = true;
                bool yMatches = true;
                bool zMatches = true;

                foreach (int j in planets.Count) {
                    List<int> planet = planets[j];
                    List<int> originalPlanet = originalPlanets[j];

                    if (xMatches && (planet[0] != originalPlanet[0] || planet[3] != originalPlanet[3])) {
                        xMatches = false;
                    }

                    if (yMatches && (planet[1] != originalPlanet[1] || planet[4] != originalPlanet[4])) {
                        yMatches = false;
                    }

                    if (zMatches && (planet[2] != originalPlanet[2] || planet[5] != originalPlanet[5])) {
                        zMatches = false;
                    }

                    if (!xMatches && !yMatches && !zMatches) {
                        break;
                    }
                }

                if (xPeriod == 0 && xMatches) {
                    xPeriod = i;
                }

                if (yPeriod == 0 && yMatches) {
                    yPeriod = i;
                }

                if (zPeriod == 0 && zMatches) {
                    zPeriod = i;
                }

                // If we've found the period for each dimension, calculate when they overlap
                if (xPeriod != 0 && yPeriod != 0 && zPeriod != 0) {
                    answer = Utility.LCM([xPeriod, yPeriod, zPeriod]);
                }
            }

            return answer.ToString();
        }
    }
}