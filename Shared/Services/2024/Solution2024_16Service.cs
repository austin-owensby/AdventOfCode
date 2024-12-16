namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/16.txt
    public class Solution2024_16Service : ISolutionDayService
    {
        private enum Direction {
            East, 
            South,
            West,
            North
        }

        private class MazePoint {
            public int X { get; set;}
            public int Y { get; set;}
            public Direction Direction {get; set;}
            public int Value {get; set;}

            public string Key() {
                return $"{X},{Y},{Direction}";
            }
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 16, example);
            List<List<char>> grid = lines.ToGrid();

            MazePoint start = new();
            MazePoint end = new();

            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == 'S') {
                        start.X = x;
                        start.Y = y;
                    }
                    else if (grid[y][x] == 'E') {
                        end.X = x;
                        end.Y = y;
                    }
                }
            }

            int answer = int.MaxValue;

            PriorityQueue<MazePoint, int> queue = new();
            queue.Enqueue(start, Utility.ManhattanDistance(start.X - end.X, start.Y - end.Y));

            Dictionary<string, int> bestValueMap = [];

            while (queue.Count > 0) {
                MazePoint point = queue.Dequeue();

                // Check if this is better than the best answer so far
                if (point.Value > answer) {
                    continue;
                }

                // Check if we've already been here before
                string key = point.Key();
                if (bestValueMap.TryGetValue(key, out int bestValue)) {
                    // Ignore this point if it isn't an improvement
                    if (bestValue <= point.Value) {
                        continue;
                    }
                    bestValueMap[key] = point.Value;
                }
                else {
                    bestValueMap[key] = point.Value;
                }

                // Check if we're at the end and update the answer if we are
                if (grid[point.Y][point.X] == 'E') {
                    answer = point.Value;
                    continue;
                }

                // Otherwise, queue up the possible next steps
                switch (point.Direction) {
                    case Direction.East:
                        if (grid[point.Y][point.X + 1] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X + 1,
                                Y = point.Y,
                                Direction = point.Direction,
                                Value = point.Value + 1
                            },
                            Utility.ManhattanDistance(point.X + 1 - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y - 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.North,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y + 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.South,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        break;
                    case Direction.West:
                        if (grid[point.Y][point.X - 1] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X - 1,
                                Y = point.Y,
                                Direction = point.Direction,
                                Value = point.Value + 1
                            },
                            Utility.ManhattanDistance(point.X - 1 - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y - 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.North,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y + 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.South,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        break;
                    case Direction.South:
                        if (grid[point.Y + 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y + 1,
                                Direction = point.Direction,
                                Value = point.Value + 1
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y + 1 - end.Y));
                        }
                        if (grid[point.Y][point.X + 1] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.East,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y][point.X - 1] != '#')
                        {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.West,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        break;
                    case Direction.North:
                        if (point.Y > 0 && grid[point.Y - 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y - 1,
                                Direction = point.Direction,
                                Value = point.Value + 1
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - 1 - end.Y));
                        }
                        if (grid[point.Y][point.X + 1] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.East,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y][point.X - 1] != '#')
                        {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.West,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        break;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 16, example);
            List<List<char>> grid = lines.ToGrid();

            MazePoint start = new();
            MazePoint end = new();

            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == 'S') {
                        start.X = x;
                        start.Y = y;
                    }
                    else if (grid[y][x] == 'E') {
                        end.X = x;
                        end.Y = y;
                    }
                }
            }

            int bestEndPathValue = int.MaxValue;

            PriorityQueue<MazePoint, int> queue = new();
            queue.Enqueue(start, Utility.ManhattanDistance(start.X - end.X, start.Y - end.Y));

            Dictionary<string, int> bestValueMap = [];

            while (queue.Count > 0) {
                MazePoint point = queue.Dequeue();

                // Check if this is better than the best answer so far
                if (point.Value > bestEndPathValue) {
                    continue;
                }

                // Check if we've already been here before
                string key = point.Key();
                if (bestValueMap.TryGetValue(key, out int bestValue)) {
                    // Ignore this point if it isn't an improvement
                    if (bestValue <= point.Value) {
                        continue;
                    }
                    bestValueMap[key] = point.Value;
                }
                else {
                    bestValueMap[key] = point.Value;
                }

                // Check if we're at the end and update the answer if we are
                if (grid[point.Y][point.X] == 'E') {
                    bestEndPathValue = point.Value;
                    end.Value = point.Value;
                    end.Direction = point.Direction;
                    continue;
                }

                // Otherwise, queue up the possible next steps
                switch (point.Direction) {
                    case Direction.East:
                        if (grid[point.Y][point.X + 1] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X + 1,
                                Y = point.Y,
                                Direction = point.Direction,
                                Value = point.Value + 1
                            },
                            Utility.ManhattanDistance(point.X + 1 - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y - 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.North,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y + 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.South,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        break;
                    case Direction.West:
                        if (grid[point.Y][point.X - 1] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X - 1,
                                Y = point.Y,
                                Direction = point.Direction,
                                Value = point.Value + 1
                            },
                            Utility.ManhattanDistance(point.X - 1 - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y - 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.North,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y + 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.South,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        break;
                    case Direction.South:
                        if (grid[point.Y + 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y + 1,
                                Direction = point.Direction,
                                Value = point.Value + 1
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y + 1 - end.Y));
                        }
                        if (grid[point.Y][point.X + 1] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.East,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y][point.X - 1] != '#')
                        {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.West,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        break;
                    case Direction.North:
                        if (point.Y > 0 && grid[point.Y - 1][point.X] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y - 1,
                                Direction = point.Direction,
                                Value = point.Value + 1
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - 1 - end.Y));
                        }
                        if (grid[point.Y][point.X + 1] != '#') {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.East,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        if (grid[point.Y][point.X - 1] != '#')
                        {
                            queue.Enqueue(new MazePoint(){
                                X = point.X,
                                Y = point.Y,
                                Direction = Direction.West,
                                Value = point.Value + 1000
                            },
                            Utility.ManhattanDistance(point.X - end.X, point.Y - end.Y));
                        }
                        break;
                }
            
            }

            int answer = 0;
            // TODO, There's probably a better way of doing this
            // To determine the number of tiles involved in a best path, navigate from the end to the start with only valid steps within the bestPaths
            queue.Enqueue(end, 0);
            List<MazePoint> visitedPoints = [];

            while(queue.Count > 0) {
                MazePoint point = queue.Dequeue();

                if (!visitedPoints.Any(p => p.X == point.X && p.Y == point.Y)) {
                    // Increase the answer if this is a new tile
                    answer++;
                }
                visitedPoints.Add(point);

                // Otherwise, queue up the possible previous steps
                int value;
                switch (point.Direction) {
                    case Direction.East:
                        if (bestValueMap.TryGetValue($"{point.X - 1},{point.Y},{point.Direction}", out value)) {
                            if (value == point.Value - 1) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X - 1,
                                    Y = point.Y,
                                    Direction = point.Direction,
                                    Value = point.Value - 1
                                },0);
                            }
                        }
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y},{Direction.North}", out value)) {
                            if (value == point.Value - 1000) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y,
                                    Direction = Direction.North,
                                    Value = point.Value - 1000
                                },0);
                            }
                        }
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y},{Direction.South}", out value)) {
                            if (value == point.Value - 1000) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y,
                                    Direction = Direction.South,
                                    Value = point.Value - 1000
                                },0);
                            }
                        }
                        break;
                    case Direction.West:
                        if (bestValueMap.TryGetValue($"{point.X + 1},{point.Y},{point.Direction}", out value)) {
                            if (value == point.Value - 1) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X + 1,
                                    Y = point.Y,
                                    Direction = point.Direction,
                                    Value = point.Value - 1
                                },0);
                            }
                        }
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y},{Direction.North}", out value)) {
                            if (value == point.Value - 1000) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y,
                                    Direction = Direction.North,
                                    Value = point.Value - 1000
                                },0);
                            }
                        }
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y},{Direction.South}", out value)) {
                            if (value == point.Value - 1000) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y,
                                    Direction = Direction.South,
                                    Value = point.Value - 1000
                                },0);
                            }
                        }
                        break;
                    case Direction.South:
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y - 1},{point.Direction}", out value)) {
                            if (value == point.Value - 1) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y - 1,
                                    Direction = point.Direction,
                                    Value = point.Value - 1
                                },0);
                            }
                        }
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y},{Direction.West}", out value)) {
                            if (value == point.Value - 1000) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y,
                                    Direction = Direction.West,
                                    Value = point.Value - 1000
                                },0);
                            }
                        }
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y},{Direction.East}", out value)) {
                            if (value == point.Value - 1000) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y,
                                    Direction = Direction.East,
                                    Value = point.Value - 1000
                                },0);
                            }
                        }
                        break;
                    case Direction.North:
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y + 1},{point.Direction}", out value)) {
                            if (value == point.Value - 1) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y + 1,
                                    Direction = point.Direction,
                                    Value = point.Value - 1
                                },0);
                            }
                        }
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y},{Direction.West}", out value)) {
                            if (value == point.Value - 1000) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y,
                                    Direction = Direction.West,
                                    Value = point.Value - 1000
                                },0);
                            }
                        }
                        if (bestValueMap.TryGetValue($"{point.X},{point.Y},{Direction.East}", out value)) {
                            if (value == point.Value - 1000) {
                                queue.Enqueue(new MazePoint(){
                                    X = point.X,
                                    Y = point.Y,
                                    Direction = Direction.East,
                                    Value = point.Value - 1000
                                },0);
                            }
                        }
                        break;
                }
            }

            return answer.ToString();
        }
    }
}