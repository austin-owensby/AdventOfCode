namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/18.txt
    public class Solution2024_18Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 18, example);
            int steps = example ? 12 : 1024;
            int dimensions = example ? 6 : 70;
            List<Point> memory = lines.Take(steps).Select(l => l.Split(",").ToInts()).Select(l => new Point(l[0], l[1])).ToList();
            List<List<char>> grid = [];

            foreach (int y in dimensions + 1) {
                List<char> row = [];
                foreach (int x in dimensions + 1) {
                    if (memory.Any(m => m.X == x && m.Y == y)) {
                        row.Add('#');
                    }
                    else {
                        row.Add('.');
                    }
                }
                grid.Add(row);
            }

            PriorityQueue<Point, int> queue = new();
            queue.Enqueue(new Point(0,0), dimensions * 2);

            int answer = 0;

            Dictionary<string, int> history = [];

            while (answer == 0) {
                Point point = queue.Dequeue();
                
                if (history.TryGetValue($"{point.X},{point.Y}", out int value)) {
                    if (value <= point.Value) {
                        continue;
                    }
                }
                history[$"{point.X},{point.Y}"] = point.Value;

                if (point.X == dimensions && point.Y == dimensions) {
                    answer = point.Value;
                }

                List<Point> validNeighbors = grid.GetNeighbors(point.X, point.Y).Where(p => grid[p.Y][p.X] != '#').ToList();

                foreach (Point validNeighbor in validNeighbors) {
                    validNeighbor.Value = point.Value + 1;
                    queue.Enqueue(validNeighbor, dimensions - point.X + dimensions - point.Y + validNeighbor.Value);
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 18, example);
            int maxSuccess = example ? 12 : 1024;
            int minFailure = lines.Count;
            int dimensions = example ? 6 : 70;

            string answer = "";

            while (minFailure - maxSuccess != 1) {
                int steps = maxSuccess + (minFailure - maxSuccess) / 2;
                List<Point> memory = lines.Take(steps).Select(l => l.Split(",").ToInts()).Select(l => new Point(l[0], l[1])).ToList();
                List<List<char>> grid = [];

                foreach (int y in dimensions + 1) {
                    List<char> row = [];
                    foreach (int x in dimensions + 1) {
                        if (memory.Any(m => m.X == x && m.Y == y)) {
                            row.Add('#');
                        }
                        else {
                            row.Add('.');
                        }
                    }
                    grid.Add(row);
                }

                PriorityQueue<Point, int> queue = new();
                queue.Enqueue(new Point(0,0), dimensions * 2);

                int loopAnswer = 0;

                Dictionary<string, int> history = [];

                while (loopAnswer == 0 && queue.Count > 0) {
                    Point point = queue.Dequeue();
                    
                    if (history.TryGetValue($"{point.X},{point.Y}", out int value)) {
                        if (value <= point.Value) {
                            continue;
                        }
                    }
                    history[$"{point.X},{point.Y}"] = point.Value;

                    if (point.X == dimensions && point.Y == dimensions) {
                        loopAnswer = point.Value;
                    }

                    List<Point> validNeighbors = grid.GetNeighbors(point.X, point.Y).Where(p => grid[p.Y][p.X] != '#').ToList();

                    foreach (Point validNeighbor in validNeighbors) {
                        validNeighbor.Value = point.Value + 1;
                        queue.Enqueue(validNeighbor, dimensions - point.X + dimensions - point.Y + validNeighbor.Value);
                    }
                }
            
                if (loopAnswer == 0) {
                    answer = $"{memory.Last().X},{memory.Last().Y}";
                    minFailure = steps; 
                }
                else {
                    maxSuccess = steps;
                }
            }
            return answer.ToString();
        }
    }
}