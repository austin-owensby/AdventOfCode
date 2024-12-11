namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/10.txt
    public class Solution2024_10Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 10, example);
            List<List<int>> grid = lines.Select(l => l.Select(c => c.ToInt()).ToList()).ToList();

            int answer = 0;

            List<Point> trailheads = [];

            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == 0) {
                        trailheads.Add(new(x, y){
                            Value = 0
                        });
                    }
                }
            }

            foreach (Point trailhead in trailheads)
            {
                Queue<Point> queue = new();
                queue.Enqueue(trailhead);

                List<Point> history = [];

                while (queue.Count > 0) {
                    Point point = queue.Dequeue();

                    if (history.Any(h => h.X == point.X && h.Y == point.Y)) {
                        continue;
                    }
                    history.Add(point);

                    if (point.Value == 9) {
                        answer++;
                        continue;
                    }

                    List<Point> nextPoints = grid.GetNeighbors(point.X, point.Y).Where(p => grid[p.Y][p.X] == point.Value + 1 && !history.Any(h => h.Y == p.Y && h.X == p.X)).ToList();

                    foreach (Point nextPoint in nextPoints) {
                        nextPoint.Value = point.Value + 1;
                        queue.Enqueue(nextPoint);
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 10, example);
            List<List<int>> grid = lines.Select(l => l.Select(c => c.ToInt()).ToList()).ToList();

            int answer = 0;

            List<Point> trailheads = [];

            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == 0) {
                        trailheads.Add(new(x, y){
                            Value = 0
                        });
                    }
                }
            }

            foreach (Point trailhead in trailheads)
            {
                Queue<Point> queue = new();
                queue.Enqueue(trailhead);

                while (queue.Count > 0) {
                    Point point = queue.Dequeue();

                    if (point.Value == 9) {
                        answer++;
                        continue;
                    }

                    List<Point> nextPoints = grid.GetNeighbors(point.X, point.Y).Where(p => grid[p.Y][p.X] == point.Value + 1).ToList();

                    foreach (Point nextPoint in nextPoints) {
                        nextPoint.Value = point.Value + 1;
                        queue.Enqueue(nextPoint);
                    }
                }
            }

            return answer.ToString();
        }
    }
}