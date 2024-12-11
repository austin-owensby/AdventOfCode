namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2019/10.txt
    public class Solution2019_10Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 10, example);

            int answer = 0;

            int height = lines.Count;
            int width = lines.First().Length;

            // Loop over each row
            foreach (int ySource in height)
            {
                // Only loop over coordinates that contain asteriods 
                List<int> sourceIndexes = lines[ySource].FindIndexes(c => c == '#');

                foreach (int xSource in sourceIndexes) {
                    int detectedAsteroids = 0;

                    // Iterate over each possible asteroid location
                    foreach (int yTarget in height) {
                        // Only loop over coordinates that contain asteriods 
                        List<int> indexes = lines[yTarget].FindIndexes(c => c == '#');

                        foreach (int xTarget in indexes) {
                            if (yTarget == ySource && xTarget == xSource) {
                                // Ignore ourselves
                                continue;
                            }

                            int ySlope = yTarget - ySource;
                            int xSlope = xTarget - xSource;

                            int factor = Math.Abs(Utility.GCF(xSlope, ySlope));
                            xSlope /= factor;
                            ySlope /= factor;

                            // Follow the slope from the target asteroid to the source asteroid and check for interuptions
                            bool interuption = false;
                            for (int i = factor - 1; i > 0; i--) {
                                if (lines[yTarget - ySlope * i][xTarget - xSlope * i] == '#') {
                                    interuption = true;
                                    break;
                                }
                            }

                            if (!interuption) {
                                detectedAsteroids++;
                            }
                        }
                    }

                    answer = Math.Max(answer, detectedAsteroids);
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 10, example);

            int height = lines.Count;
            int width = lines.First().Length;

            int bestDetectedAsteroids = 0;
            int bestX = 0;
            int bestY = 0;

            // First calculate the best starting point
            // Loop over each row
            foreach (int ySource in height)
            {
                // Only loop over coordinates that contain asteriods 
                List<int> sourceIndexes = lines[ySource].FindIndexes(c => c == '#');

                foreach (int xSource in sourceIndexes) {
                    int detectedAsteroids = 0;

                    // Iterate over each possible asteroid location
                    foreach (int yTarget in height) {
                        // Only loop over coordinates that contain asteriods 
                        List<int> indexes = lines[yTarget].FindIndexes(c => c == '#');

                        foreach (int xTarget in indexes) {
                            if (yTarget == ySource && xTarget == xSource) {
                                // Ignore ourselves
                                continue;
                            }

                            int ySlope = yTarget - ySource;
                            int xSlope = xTarget - xSource;

                            int factor = Math.Abs(Utility.GCF(xSlope, ySlope));
                            xSlope /= factor;
                            ySlope /= factor;

                            // Follow the slope from the target asteroid to the source asteroid and check for interuptions
                            bool interuption = false;
                            for (int i = factor - 1; i > 0; i--) {
                                if (lines[yTarget - ySlope * i][xTarget - xSlope * i] == '#') {
                                    interuption = true;
                                    break;
                                }
                            }

                            if (!interuption) {
                                detectedAsteroids++;
                            }
                        }
                    }

                    if (detectedAsteroids > bestDetectedAsteroids)
                    {
                        bestDetectedAsteroids = detectedAsteroids;
                        bestX = xSource;
                        bestY = ySource;
                    }
                }
            }

            int answer = 0;

            // Now that we have the best starting point, vaporize asteroids in order
            int vaporizedCount = 0;

            Point start = new(bestX, bestY);

            List<Asteroid> asteroids = lines
                .SelectMany((line, y) => line.FindIndexes(c => c == '#') // Select each asteroid
                .Select(x => new Asteroid(x, y, start))) // Create an asteroid class that calculates distance and angle to the best asteroid
                .Where(p => p.Distance != 0) // Filter out the origin
                .OrderBy(a => a.Distance) // The closest asteroids should be first
                .ToList();

            List<double> angles = asteroids.Select(a => a.Angle).Distinct().Order().ToList();
            int index = 0;

            while (vaporizedCount < 200) {
                Asteroid? asteroid = asteroids.Where(a => a.Angle == angles[index]).FirstOrDefault();

                if (asteroid != null) {
                    vaporizedCount++;
                    asteroids.Remove(asteroid);

                    answer = asteroid.X * 100 + asteroid.Y;
                    Console.WriteLine($"{asteroid.X},{asteroid.Y}");
                }

                index++;
                index %= angles.Count;
            }

            // 312 is too low

            return answer.ToString();
        }
    
        private class Asteroid
        {
            public int X { get; set; }
            public int Y { get; set; }

            public double Distance { get; set; }
            public double Angle { get; set;}

            public Asteroid(int x, int y, Point origin) {
                X = x;
                Y = y;

                int diffX = X - origin.X;
                int diffY = origin.Y - Y;

                Distance = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));
                Angle = Math.Atan2(diffX,diffY); // Swap x and y to give us Acot2 since we start pointing upwards and move clockwise

                if (Angle < 0) {
                    Angle += Math.PI * 2;
                }
            }
    }
    }
}