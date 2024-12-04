namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/04.txt
    public class Solution2024_04Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 4, example);
            List<List<char>> grid = lines.Select(l => l.ToList()).ToList();

            int answer = 0;

            foreach (int y in grid.Count)
            {
                foreach (int x in grid[y].Count)
                {
                    // Look for the start of 'XMAS'
                    if (lines[y][x] == 'X') {
                        // Get neighbors of this point
                        List<Point> points = grid.GetNeighbors(x, y, true);

                        // Filter down to just neighbors that are 'M'
                        points = points.Where(p => grid[p.Y][p.X] == 'M').ToList();

                        foreach (Point point in points)
                        {
                            // Check the direction of the point and then check if the next letter in that direction is an 'A' and then 'S'
                            // Up
                            int xDelta = point.X - x;
                            int yDelta = point.Y - y;

                            try {
                                if (grid[point.Y + yDelta][point.X + xDelta] == 'A' && grid[point.Y + 2 * yDelta][point.X + 2 * xDelta] == 'S') {
                                    answer++;
                                }
                            }
                            // TODO: Throwing and catching exceptions like this is expensive, if we cared about performance we'd manually check if the index was out of bounds
                            catch (ArgumentOutOfRangeException) {
                                continue;
                            }
                        }
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 4, example);
            List<List<char>> grid = lines.Select(l => l.ToList()).ToList();

            int answer = 0;

            foreach (int y in grid.Count)
            {
                foreach (int x in grid[y].Count)
                {
                    // Look for the center of an X 'MAS'
                    if (lines[y][x] == 'A') {
                        // Get neighbors of this point
                        List<Point> points = grid.GetNeighbors(x, y, true);

                        // Filter down to just diagonal neighbors that are 'M' and 'S'
                        points = points.Where(p => p.X != x && p.Y != y && (grid[p.Y][p.X] == 'M' || grid[p.Y][p.X] == 'S')).ToList();

                        if (points.Count == 4)
                        {
                            // There's enough to make an X, check if the letters are diagonal from each other
                            List<Point> mPoints = points.Where(p => grid[p.Y][p.X] == 'M').ToList();

                            if (mPoints.Count == 2)
                            {
                                // We have exactly 2 M points required to make an X
                                if (mPoints[0].X == mPoints[1].X || mPoints[0].Y == mPoints[1].Y) {
                                    // The M points are not diagonal from each other since they share at least 1 X or Y coordinate
                                    answer++;
                                }
                            }
                        }
                    }
                }
            }

            return answer.ToString();
        }
    }
}