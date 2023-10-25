namespace AdventOfCode.Services
{
    public class Solution2018_06Service : ISolutionDayService
    {
                private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point() { }

            public Point(string value)
            {
                string[] split = value.Split(", ");
                X = int.Parse(split[0]);
                Y = int.Parse(split[1]);
            }
        }

        public string FirstHalf(bool example)
        {
            List<string> data = Utility.GetInputLines(2018, 06, example);

            List<Point> origins = data.Select(d => new Point(d)).ToList();

            int maxX = origins.Select(p => p.X).Max();
            int maxY = origins.Select(p => p.Y).Max();

            List<List<char>> grid = new();

            for (int y = 0; y <= maxY; y++)
            {
                List<char> row = new();
                for (int x = 0; x <= maxX; x++)
                {
                    Point point = new()
                    {
                        X = x,
                        Y = y
                    };

                    char cell = GetClosestPoint(point, origins);

                    row.Add(cell);
                }
                grid.Add(row);
            }

            List<char> edgeChars = new();
            edgeChars = edgeChars.Concat(grid.First()).ToList();
            edgeChars = edgeChars.Concat(grid.Last()).ToList();
            edgeChars = edgeChars.Concat(grid.Select(g => g.First())).ToList();
            edgeChars = edgeChars.Concat(grid.Select(g => g.Last())).ToList();
            edgeChars = edgeChars.Distinct().ToList();

            List<char> allCells = grid.SelectMany(g => g).ToList();
            List<char> filteredChars = allCells.Where(c => !edgeChars.Contains(c) && c != '.').ToList();
            int largestArea = filteredChars.Distinct().Max(c => filteredChars.Count(a => a == c));

            return largestArea.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> data = Utility.GetInputLines(2018, 06, example);

            List<Point> origins = data.Select(d => new Point(d)).ToList();

            int maxX = origins.Select(p => p.X).Max();
            int maxY = origins.Select(p => p.Y).Max();

            int regionSize = 0;

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    int totalDistance = 0;
                    bool inRegion = true;

                    foreach (Point origin in origins)
                    {
                        Point point = new()
                        {
                            X = x,
                            Y = y
                        };

                        totalDistance += GetDistance(point, origin);

                        if (totalDistance >= 10000)
                        {
                            inRegion = false;
                            break;
                        }
                    }

                    if (inRegion)
                    {
                        regionSize++;
                    }
                }
            }

            return regionSize.ToString();
        }

        private char GetClosestPoint(Point point, List<Point> origins)
        {
            int minDistance = int.MaxValue;
            char minPointName = ' ';

            for (int i = 0; i < origins.Count; i++)
            {
                int distance = GetDistance(point, origins[i]);

                if (distance < minDistance)
                {
                    minPointName = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"[i];
                    minDistance = distance;
                }
                else if (distance == minDistance)
                {
                    minPointName = '.';
                }
            }

            return minPointName;
        }

        private int GetDistance(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
    }
}
