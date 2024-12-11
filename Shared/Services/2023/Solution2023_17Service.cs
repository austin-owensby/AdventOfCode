namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2023/17.txt
    public class Solution2023_17Service : ISolutionDayService
    {
        private class ValuePoint: Point {
            public int Direction { get; set; }
            public int StraightAmount { get; set; }
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 17, example);
            List<List<int>> grid = lines.Select(line => line.Select(c => c.ToInt()).ToList()).ToList();

            Dictionary<string, int> costsSoFar = [];

            // A* priority is distance to end + cost so far
            PriorityQueue<ValuePoint, int> queue = new();
            queue.Enqueue(new ValuePoint {
                X = 0,
                Y = 0,
                Direction = 0,
            }, grid.Count + grid.First().Count);

            int answer = 0;
            
            while (queue.Count > 0) {
                ValuePoint point = queue.Dequeue();
                point.Value += grid[point.Y][point.X];

                string key = $"{point.X} {point.Y} {point.StraightAmount} {point.Direction}";

                if (costsSoFar.TryGetValue(key, out int cost)) {
                    if (point.Value < cost) {
                        costsSoFar[key] = point.Value;
                    }
                    else {
                        // We've reached this point faster in the past, ignore it
                        continue;
                    }
                }
                else {
                    costsSoFar[key] = point.Value;
                }

                // If we've reached the end, update our best value
                if (point.X == grid.First().Count - 1 && point.Y == grid.Count - 1) {
                    answer = point.Value;
                    break;
                }
                
                // Otherwise, add the neighboring points to the queue
                foreach (Point n in grid.GetNeighbors(point.X, point.Y)) {
                    ValuePoint neighbor = new(){
                        X = n.X,
                        Y = n.Y,
                        Value = point.Value
                    };

                    if (neighbor.X > point.X) {
                        neighbor.Direction = 0;
                    }
                    else if (neighbor.X < point.X) {
                        neighbor.Direction = 2;
                    }
                    else if (neighbor.Y > point.Y) {
                        neighbor.Direction = 1;
                    }
                    else if (neighbor.Y < point.Y) {
                        neighbor.Direction = 3;
                    }

                    if (neighbor.Direction == point.Direction) {
                        neighbor.StraightAmount = point.StraightAmount + 1;
                        
                        if (neighbor.StraightAmount > 2) {
                            continue;
                        }
                    }

                    if (Math.Abs(neighbor.Direction - point.Direction) == 2) {
                        // Backwards movement is not allowed
                        continue;
                    }

                    queue.Enqueue(neighbor, point.Value + grid[neighbor.Y][neighbor.X] + grid.Count - neighbor.Y + grid.First().Count - neighbor.X);
                }
            }

            // Don't include the first point in our sum
            answer -= grid[0][0];

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 17, example);
            List<List<int>> grid = lines.Select(line => line.Select(c => c.ToInt()).ToList()).ToList();

            Dictionary<string, int> costsSoFar = [];

            // A* priority is distance to end + cost so far
            PriorityQueue<ValuePoint, int> queue = new();
            costsSoFar["0 0 0 0"] = 0;
            queue.Enqueue(new ValuePoint {
                X = 0,
                Y = 1,
                Direction = 1,
            }, grid.Count + grid.First().Count - 1 + grid[1][0]);
            queue.Enqueue(new ValuePoint {
                X = 1,
                Y = 0,
                Direction = 0,
            }, grid.Count + grid.First().Count - 1 + grid[1][0]);

            int answer = 0;
            
            while (queue.Count > 0) {
                ValuePoint point = queue.Dequeue();
                point.Value += grid[point.Y][point.X];

                string key = $"{point.X} {point.Y} {point.StraightAmount} {point.Direction}";

                if (costsSoFar.TryGetValue(key, out int cost)) {
                    if (point.Value < cost) {
                        costsSoFar[key] = point.Value;
                    }
                    else {
                        // We've reached this point faster in the past, ignore it
                        continue;
                    }
                }
                else {
                    costsSoFar[key] = point.Value;
                }

                // If we've reached the end, update our best value
                if (point.X == grid.First().Count - 1 && point.Y == grid.Count - 1 && point.StraightAmount >= 3) {
                    answer = point.Value;
                    break;
                }
                
                // Otherwise, add the neighboring points to the queue
                foreach (Point n in grid.GetNeighbors(point.X, point.Y)) {
                    ValuePoint neighbor = new(){
                        X = n.X,
                        Y = n.Y,
                        Value = point.Value
                    };

                    if (neighbor.X > point.X) {
                        neighbor.Direction = 0;
                    }
                    else if (neighbor.X < point.X) {
                        neighbor.Direction = 2;
                    }
                    else if (neighbor.Y > point.Y) {
                        neighbor.Direction = 1;
                    }
                    else if (neighbor.Y < point.Y) {
                        neighbor.Direction = 3;
                    }

                    if (neighbor.Direction == point.Direction) {
                        neighbor.StraightAmount = point.StraightAmount + 1;
                        
                        if (neighbor.StraightAmount > 9) {
                            continue;
                        }
                    }
                    else if (point.StraightAmount < 3) {
                        continue;
                    }

                    if (Math.Abs(neighbor.Direction - point.Direction) == 2) {
                        // Backwards movement is not allowed
                        continue;
                    }

                    queue.Enqueue(neighbor, point.Value + grid[neighbor.Y][neighbor.X] + grid.Count - neighbor.Y + grid.First().Count - neighbor.X);
                }
            }

            return answer.ToString();
        }
    }
}