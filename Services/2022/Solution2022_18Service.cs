namespace AdventOfCode.Services
{
    public class Solution2022_18Service : ISolutionDayService
    {
        public Solution2022_18Service() { }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 18, example);

            List<List<int>> drops = lines.QuickRegex(@"(\d+),(\d+),(\d+)").ToInts();

            int answer = drops.Count * 6;

            foreach (List<int> drop in drops)
            {
                answer -= drops.Any(d => {
                    return d[0] - drop[0] == 1 && d[1] == drop[1] && d[2] == drop[2];
                }) ? 1 : 0;

                answer -= drops.Any(d => {
                    return d[0] == drop[0] && d[1] - drop[1] == 1 && d[2] == drop[2];
                }) ? 1 : 0;

                answer -= drops.Any(d => {
                    return d[0] == drop[0] && d[1] == drop[1] && d[2] - drop[2] == 1;
                }) ? 1 : 0;
                
                answer -= drops.Any(d => {
                    return d[0] - drop[0] == -1 && d[1] == drop[1] && d[2] == drop[2];
                }) ? 1 : 0;

                answer -= drops.Any(d => {
                    return d[0] == drop[0] && d[1] - drop[1] == -1 && d[2] == drop[2];
                }) ? 1 : 0;

                answer -= drops.Any(d => {
                    return d[0] == drop[0] && d[1] == drop[1] && d[2] - drop[2] == -1;
                }) ? 1 : 0;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 18, example);

            List<List<int>> drops = lines.QuickRegex(@"(\d+),(\d+),(\d+)").ToInts();

            int maxX = drops.Max(drop => drop[0]);
            int maxY = drops.Max(drop => drop[1]);
            int maxZ = drops.Max(drop => drop[2]);

            // Find a list of outside points (Probably not the best way to do this)
            List<List<int>> outsidePoints = new();

            Queue<List<int>> pointsToExplore = new();
            pointsToExplore.Enqueue(new() { 0, 0, 0 });

            bool isAir = true;
            bool isInBounds = true;

            while (pointsToExplore.Any()) {
                List<int> point = pointsToExplore.Dequeue();

                outsidePoints.Add(point);

                List<int> point1 = new(){
                    point[0] - 1,
                    point[1],
                    point[2]
                };

                if (!pointsToExplore.Any(p => p[0] == point1[0] && p[1] == point1[1] && p[2] == point1[2]) && !outsidePoints.Any(p => p[0] == point1[0] && p[1] == point1[1] && p[2] == point1[2])) {
                    isAir = !drops.Any(d => d[0] == point1[0] && d[1] == point1[1] && d[2] == point1[2]);
                    isInBounds = point1[0] >= 0 && point1[1] >= 0 && point1[2] >= 0 && point1[0] <= maxX + 1 && point1[1] <= maxY + 1 && point1[2] <= maxZ + 1;

                    if (isAir && isInBounds) {
                        pointsToExplore.Enqueue(point1);
                    }
                }

                List<int> point2 = new(){
                    point[0] + 1,
                    point[1],
                    point[2]
                };

                if (!pointsToExplore.Any(p => p[0] == point2[0] && p[1] == point2[1] && p[2] == point2[2]) && !outsidePoints.Any(p => p[0] == point2[0] && p[1] == point2[1] && p[2] == point2[2])) {
                    isAir = !drops.Any(d => d[0] == point2[0] && d[1] == point2[1] && d[2] == point2[2]);
                    isInBounds = point2[0] >= 0 && point2[1] >= 0 && point2[2] >= 0 && point2[0] <= maxX + 1 && point2[1] <= maxY + 1 && point2[2] <= maxZ + 1;

                    if (isAir && isInBounds) {
                        pointsToExplore.Enqueue(point2);
                    }
                }

                List<int> point3 = new(){
                    point[0],
                    point[1] - 1,
                    point[2]
                };

                if (!pointsToExplore.Any(p => p[0] == point3[0] && p[1] == point3[1] && p[2] == point3[2]) && !outsidePoints.Any(p => p[0] == point3[0] && p[1] == point3[1] && p[2] == point3[2])) {
                    isAir = !drops.Any(d => d[0] == point3[0] && d[1] == point3[1] && d[2] == point3[2]);
                    isInBounds = point3[0] >= 0 && point3[1] >= 0 && point3[2] >= 0 && point3[0] <= maxX + 1 && point3[1] <= maxY + 1 && point3[2] <= maxZ + 1;

                    if (isAir && isInBounds) {
                        pointsToExplore.Enqueue(point3);
                    }
                }

                List<int> point4 = new(){
                    point[0],
                    point[1] + 1,
                    point[2]
                };

                if (!pointsToExplore.Any(p => p[0] == point4[0] && p[1] == point4[1] && p[2] == point4[2]) && !outsidePoints.Any(p => p[0] == point4[0] && p[1] == point4[1] && p[2] == point4[2])) {
                    isAir = !drops.Any(d => d[0] == point4[0] && d[1] == point4[1] && d[2] == point4[2]);
                    isInBounds = point4[0] >= 0 && point4[1] >= 0 && point4[2] >= 0 && point4[0] <= maxX + 1 && point4[1] <= maxY + 1 && point4[2] <= maxZ + 1;

                    if (isAir && isInBounds) {
                        pointsToExplore.Enqueue(point4);
                    }
                }

                List<int> point5 = new(){
                    point[0],
                    point[1],
                    point[2] - 1
                };

                if (!pointsToExplore.Any(p => p[0] == point5[0] && p[1] == point5[1] && p[2] == point5[2]) && !outsidePoints.Any(p => p[0] == point5[0] && p[1] == point5[1] && p[2] == point5[2])) {
                    isAir = !drops.Any(d => d[0] == point5[0] && d[1] == point5[1] && d[2] == point5[2]);
                    isInBounds = point5[0] >= 0 && point5[1] >= 0 && point5[2] >= 0 && point5[0] <= maxX + 1 && point5[1] <= maxY + 1 && point5[2] <= maxZ + 1;

                    if (isAir && isInBounds) {
                        pointsToExplore.Enqueue(point5);
                    }
                }

                List<int> point6 = new(){
                    point[0],
                    point[1],
                    point[2] + 1
                };

                if (!pointsToExplore.Any(p => p[0] == point6[0] && p[1] == point6[1] && p[2] == point6[2]) && !outsidePoints.Any(p => p[0] == point6[0] && p[1] == point6[1] && p[2] == point6[2])) {
                    isAir = !drops.Any(d => d[0] == point6[0] && d[1] == point6[1] && d[2] == point6[2]);
                    isInBounds = point6[0] >= 0 && point6[1] >= 0 && point6[2] >= 0 && point6[0] <= maxX + 1 && point6[1] <= maxY + 1 && point6[2] <= maxZ + 1;

                    if (isAir && isInBounds) {
                        pointsToExplore.Enqueue(point6);
                    }
                }
            }

            // The inside points know where they are because they know everywhere they aren't ;)
            // https://www.youtube.com/watch?v=bZe5J8SVCYQ&ab_channel=Jeff7181
            List<List<int>> insidePoints = new();

            for (int x = 1; x < maxX; x++) {
                for (int y = 1; y < maxY; y++) {
                    for (int z = 1; z < maxZ; z++) {
                        if (!outsidePoints.Any(p => p[0] == x && p[1] == y && p[2] == z) && !drops.Any(p => p[0] == x && p[1] == y && p[2] == z)) {
                            insidePoints.Add(new(){
                                x,
                                y,
                                z
                            });
                        }
                    }
                }
            }

            // Fill in the gaps inside the drop
            drops.AddRange(insidePoints);

            int answer = drops.Count * 6;

            foreach (List<int> drop in drops)
            {
                answer -= drops.Any(d => {
                    return d[0] - drop[0] == 1 && d[1] == drop[1] && d[2] == drop[2];
                }) ? 1 : 0;

                answer -= drops.Any(d => {
                    return d[0] == drop[0] && d[1] - drop[1] == 1 && d[2] == drop[2];
                }) ? 1 : 0;

                answer -= drops.Any(d => {
                    return d[0] == drop[0] && d[1] == drop[1] && d[2] - drop[2] == 1;
                }) ? 1 : 0;
                
                answer -= drops.Any(d => {
                    return d[0] - drop[0] == -1 && d[1] == drop[1] && d[2] == drop[2];
                }) ? 1 : 0;

                answer -= drops.Any(d => {
                    return d[0] == drop[0] && d[1] - drop[1] == -1 && d[2] == drop[2];
                }) ? 1 : 0;

                answer -= drops.Any(d => {
                    return d[0] == drop[0] && d[1] == drop[1] && d[2] - drop[2] == -1;
                }) ? 1 : 0;
            }

            return answer.ToString();
        }
    }
}