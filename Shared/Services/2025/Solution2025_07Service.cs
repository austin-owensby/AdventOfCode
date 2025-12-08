namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/07.txt
    public class Solution2025_07Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 7, example);

            int answer = 0;

            Queue<Point> queue = [];

            int x = lines[0].IndexOf('S');
            Point start = new(x, 0);
            queue.Enqueue(start);

            List<string> visitedPoints = [$"{start.X},{start.Y}"];

            while (queue.Count > 0)
            {
                Point point = queue.Dequeue();

                if (lines[point.Y][point.X] == '^')
                {
                    answer++;
                    
                    // Split
                    Point nextPoint = new(point.X - 1, point.Y);
                    if (!visitedPoints.Contains($"{nextPoint.X},{nextPoint.Y}"))
                    {
                        queue.Enqueue(nextPoint);
                        visitedPoints.Add($"{nextPoint.X},{nextPoint.Y}");
                    }

                    nextPoint = new(point.X + 1, point.Y);
                    if (!visitedPoints.Contains($"{nextPoint.X},{nextPoint.Y}"))
                    {
                        queue.Enqueue(nextPoint);
                        visitedPoints.Add($"{nextPoint.X},{nextPoint.Y}");
                    }
                }
                else
                {
                    // Move down
                    Point nextPoint = new(point.X, point.Y + 1);
                    if (point.Y + 1 != lines.Count && !visitedPoints.Contains($"{nextPoint.X},{nextPoint.Y}"))
                    {
                        visitedPoints.Add($"{nextPoint.X},{nextPoint.Y}");
                        queue.Enqueue(nextPoint);
                    }
                }
            }

            return answer.ToString();
        }

        private Dictionary<string, long> paths {get; set;} = [];
        private List<string> grid { get; set; } = [];

        private long GetPaths(int x, int y)
        {
            string key = $"{x},{y}";

            if (!paths.TryGetValue(key, out long pathCount))
            {
                if (grid[y][x] == '^')
                {
                    // Split
                    pathCount = GetPaths(x - 1, y) + GetPaths(x + 1, y);
                }
                else
                {
                    // Move down
                    if (y + 1 != grid.Count)
                    {
                        pathCount = GetPaths(x, y + 1);
                    }
                    else
                    {
                        pathCount = 1;
                    }
                }
                paths[key] = pathCount;
            }

            return pathCount;
        }

        public string SecondHalf(bool example)
        {
            grid = Utility.GetInputLines(2025, 7, example);
            paths = [];

            int x = grid[0].IndexOf('S');

            long answer = GetPaths(x, 0);

            return answer.ToString();
        }
    }
}