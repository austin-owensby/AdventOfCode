namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2022/24.txt
    public class Solution2022_24Service : ISolutionDayService
    {
                private enum Direction {
            Up,
            Down,
            Left,
            Right
        }

        private class BlizzardPoint {
            public int X {get; set;}
            public int Y {get; set;}
            public Direction? Direction {get; set;}
        }

        private class State { 
            public BlizzardPoint CurrentPoint {get; set;} = new();
            public List<BlizzardPoint> Blizzard {get; set;} = new();
            // Tracks the unique state of the blizzard to avoid checking every point in the list
            public int BlizzardIndex {get; set;} = 0;
            public int Score {get; set;}

            public bool IsIdentical(State otherState) {
                return CurrentPoint.X == otherState.CurrentPoint.X
                    && CurrentPoint.Y == otherState.CurrentPoint.Y
                    && BlizzardIndex == otherState.BlizzardIndex;
            }
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 24, example);

            List<BlizzardPoint> blizardPoints = new();

            BlizzardPoint startingPoint = new(){
                Y = 0,
                X = lines.First().IndexOf('.')
            };

            BlizzardPoint endingPoint = new(){
                Y = lines.Count - 1,
                X = lines.Last().IndexOf('.')
            };

            int maxYIndex = lines.Count - 1;
            int maxXIndex = lines.First().Length - 1;

            for (int y = 1; y < maxYIndex; y++)
            {
                for (int x = 1; x < maxXIndex; x++) {
                    if (lines[y][x] != '.') {
                        BlizzardPoint point = new(){
                            X = x, 
                            Y = y,
                        };

                        point.Direction = lines[y][x] switch {
                            '^' => Direction.Up,
                            'v' => Direction.Down,
                            '<' => Direction.Left,
                            '>' => Direction.Right,
                            _ => null, // Shouldn't be possible
                        };

                        blizardPoints.Add(point);
                    }
                }
            }

            int answer = int.MaxValue;

            List<State> queue = new();
            queue.Add(new State(){
                CurrentPoint = startingPoint,
                Blizzard = blizardPoints,
                BlizzardIndex = 0,
                Score = 0,
            });
            List<State> visitedStates = new();

            while (queue.Any()) {
                State nextState = queue.First();
                queue.RemoveAt(0);
                visitedStates.Add(nextState);

                // Move blizzard points
                List<BlizzardPoint> blizzard = nextState.Blizzard.Select(b => new BlizzardPoint(){
                    X = b.X,
                    Y = b.Y,
                    Direction = b.Direction
                }).ToList();

                foreach (BlizzardPoint blizzardPoint in blizzard) {
                    switch (blizzardPoint.Direction) {
                        case Direction.Up:
                            blizzardPoint.Y--;
                            break;
                        case Direction.Down:
                            blizzardPoint.Y++;
                            break;
                        case Direction.Left:
                            blizzardPoint.X--;
                            break;
                        case Direction.Right:
                            blizzardPoint.X++;
                            break;
                    }

                    // Handle wrapping to the other side
                    blizzardPoint.X = Utility.Mod(blizzardPoint.X - 1, maxXIndex - 1) + 1;
                    blizzardPoint.Y = Utility.Mod(blizzardPoint.Y - 1, maxYIndex - 1) + 1;
                }

                int newBlizzardIndex = (nextState.BlizzardIndex + 1) % ((maxXIndex - 1) * (maxYIndex - 1));

                // Check each player option
                if (nextState.CurrentPoint.Y == endingPoint.Y - 1 && nextState.CurrentPoint.X == endingPoint.X) {
                    // We can move to the exit, check if this was the quickest path
                    if (answer > nextState.Score + 1) {
                        // If so, update the quickest path and remove all slower paths
                        answer = nextState.Score + 1;
                        queue = queue.Where(s => s.Score >= nextState.Score).ToList();
                    }
                }
                else if (nextState.Score < answer - 1){
                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y - 1) && nextState.CurrentPoint.Y > 1) {
                        // Player moves up
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y - 1
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y + 1) && nextState.CurrentPoint.Y < maxYIndex - 1) {
                        // Player moves down
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y + 1
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X - 1 && p.Y == nextState.CurrentPoint.Y) && nextState.CurrentPoint.X > 1 && nextState.CurrentPoint.Y != 0) {
                        // Player moves left
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X - 1,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X + 1 && p.Y == nextState.CurrentPoint.Y) && nextState.CurrentPoint.X < maxXIndex - 1 && nextState.CurrentPoint.Y != 0) {
                        // Player moves right
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X + 1,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y)) {
                        // Playerdoesn't move
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 24, example);

            List<BlizzardPoint> blizardPoints = new();

            BlizzardPoint startingPoint = new(){
                Y = 0,
                X = lines.First().IndexOf('.')
            };

            BlizzardPoint endingPoint = new(){
                Y = lines.Count - 1,
                X = lines.Last().IndexOf('.')
            };

            int maxYIndex = lines.Count - 1;
            int maxXIndex = lines.First().Length - 1;

            for (int y = 1; y < maxYIndex; y++)
            {
                for (int x = 1; x < maxXIndex; x++) {
                    if (lines[y][x] != '.') {
                        BlizzardPoint point = new(){
                            X = x, 
                            Y = y,
                        };

                        point.Direction = lines[y][x] switch {
                            '^' => Direction.Up,
                            'v' => Direction.Down,
                            '<' => Direction.Left,
                            '>' => Direction.Right,
                            _ => null, // Shouldn't be possible
                        };

                        blizardPoints.Add(point);
                    }
                }
            }

            int bestToFinish = int.MaxValue;
            List<BlizzardPoint> blizzardAtFinish = new();

            List<State> queue = new();
            queue.Add(new State(){
                CurrentPoint = startingPoint,
                Blizzard = blizardPoints,
                BlizzardIndex = 0,
                Score = 0,
            });
            List<State> visitedStates = new();

            while (queue.Any()) {
                State nextState = queue.First();
                queue.RemoveAt(0);
                visitedStates.Add(nextState);

                // Move blizzard points
                List<BlizzardPoint> blizzard = nextState.Blizzard.Select(b => new BlizzardPoint(){
                    X = b.X,
                    Y = b.Y,
                    Direction = b.Direction
                }).ToList();

                foreach (BlizzardPoint blizzardPoint in blizzard) {
                    switch (blizzardPoint.Direction) {
                        case Direction.Up:
                            blizzardPoint.Y--;
                            break;
                        case Direction.Down:
                            blizzardPoint.Y++;
                            break;
                        case Direction.Left:
                            blizzardPoint.X--;
                            break;
                        case Direction.Right:
                            blizzardPoint.X++;
                            break;
                    }

                    // Handle wrapping to the other side
                    blizzardPoint.X = Utility.Mod(blizzardPoint.X - 1, maxXIndex - 1) + 1;
                    blizzardPoint.Y = Utility.Mod(blizzardPoint.Y - 1, maxYIndex - 1) + 1;
                }

                int newBlizzardIndex = (nextState.BlizzardIndex + 1) % ((maxXIndex - 1) * (maxYIndex - 1));

                // Check each player option
                if (nextState.CurrentPoint.Y == endingPoint.Y - 1 && nextState.CurrentPoint.X == endingPoint.X) {
                    // We can move to the exit, check if this was the quickest path
                    if (bestToFinish > nextState.Score + 1) {
                        // If so, update the quickest path and remove all slower paths
                        bestToFinish = nextState.Score + 1;
                        blizzardAtFinish = blizzard;
                        queue = queue.Where(s => s.Score >= nextState.Score).ToList();
                    }
                }
                else if (nextState.Score < bestToFinish - 1){
                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y - 1) && nextState.CurrentPoint.Y > 1) {
                        // Player moves up
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y - 1
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y + 1) && nextState.CurrentPoint.Y < maxYIndex - 1) {
                        // Player moves down
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y + 1
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X - 1 && p.Y == nextState.CurrentPoint.Y) && nextState.CurrentPoint.X > 1 && nextState.CurrentPoint.Y != 0) {
                        // Player moves left
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X - 1,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X + 1 && p.Y == nextState.CurrentPoint.Y) && nextState.CurrentPoint.X < maxXIndex - 1 && nextState.CurrentPoint.Y != 0) {
                        // Player moves right
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X + 1,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y)) {
                        // Playerdoesn't move
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }
                }
            }

            visitedStates = new();

            int bestToStart = int.MaxValue;
            List<BlizzardPoint> blizzardAtStart = new();

            queue.Add(new(){
                CurrentPoint = endingPoint,
                Blizzard = blizzardAtFinish,
                Score = 0,
                BlizzardIndex = 0,
            });

            while (queue.Any()) {
                State nextState = queue.First();
                queue.RemoveAt(0);
                visitedStates.Add(nextState);

                // Move blizzard points
                List<BlizzardPoint> blizzard = nextState.Blizzard.Select(b => new BlizzardPoint(){
                    X = b.X,
                    Y = b.Y,
                    Direction = b.Direction
                }).ToList();

                foreach (BlizzardPoint blizzardPoint in blizzard) {
                    switch (blizzardPoint.Direction) {
                        case Direction.Up:
                            blizzardPoint.Y--;
                            break;
                        case Direction.Down:
                            blizzardPoint.Y++;
                            break;
                        case Direction.Left:
                            blizzardPoint.X--;
                            break;
                        case Direction.Right:
                            blizzardPoint.X++;
                            break;
                    }

                    // Handle wrapping to the other side
                    blizzardPoint.X = Utility.Mod(blizzardPoint.X - 1, maxXIndex - 1) + 1;
                    blizzardPoint.Y = Utility.Mod(blizzardPoint.Y - 1, maxYIndex - 1) + 1;
                }

                int newBlizzardIndex = (nextState.BlizzardIndex + 1) % ((maxXIndex - 1) * (maxYIndex - 1));

                // Check each player option
                if (nextState.CurrentPoint.Y == startingPoint.Y + 1 && nextState.CurrentPoint.X == startingPoint.X) {
                    // We can move to the exit, check if this was the quickest path
                    if (bestToStart > nextState.Score + 1) {
                        // If so, update the quickest path and remove all slower paths
                        bestToStart = nextState.Score + 1;
                        blizzardAtStart = blizzard;
                        queue = queue.Where(s => s.Score >= nextState.Score).ToList();
                    }
                }
                else if (nextState.Score < bestToStart - 1){
                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y - 1) && nextState.CurrentPoint.Y > 1) {
                        // Player moves up
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y - 1
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y + 1) && nextState.CurrentPoint.Y < maxYIndex - 1) {
                        // Player moves down
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y + 1
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X - 1 && p.Y == nextState.CurrentPoint.Y) && nextState.CurrentPoint.X > 1 && nextState.CurrentPoint.Y != maxYIndex) {
                        // Player moves left
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X - 1,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X + 1 && p.Y == nextState.CurrentPoint.Y) && nextState.CurrentPoint.X < maxXIndex - 1 && nextState.CurrentPoint.Y != maxYIndex) {
                        // Player moves right
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X + 1,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y)) {
                        // Playerdoesn't move
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }
                }
            }

            visitedStates = new();

            int bestToFinish2 = int.MaxValue;

            queue.Add(new(){
                CurrentPoint = startingPoint,
                Blizzard = blizzardAtStart,
                Score = 0,
                BlizzardIndex = 0,
            });

            while (queue.Any()) {
                State nextState = queue.First();
                queue.RemoveAt(0);
                visitedStates.Add(nextState);

                // Move blizzard points
                List<BlizzardPoint> blizzard = nextState.Blizzard.Select(b => new BlizzardPoint(){
                    X = b.X,
                    Y = b.Y,
                    Direction = b.Direction
                }).ToList();

                foreach (BlizzardPoint blizzardPoint in blizzard) {
                    switch (blizzardPoint.Direction) {
                        case Direction.Up:
                            blizzardPoint.Y--;
                            break;
                        case Direction.Down:
                            blizzardPoint.Y++;
                            break;
                        case Direction.Left:
                            blizzardPoint.X--;
                            break;
                        case Direction.Right:
                            blizzardPoint.X++;
                            break;
                    }

                    // Handle wrapping to the other side
                    blizzardPoint.X = Utility.Mod(blizzardPoint.X - 1, maxXIndex - 1) + 1;
                    blizzardPoint.Y = Utility.Mod(blizzardPoint.Y - 1, maxYIndex - 1) + 1;
                }

                int newBlizzardIndex = (nextState.BlizzardIndex + 1) % ((maxXIndex - 1) * (maxYIndex - 1));

                // Check each player option
                if (nextState.CurrentPoint.Y == endingPoint.Y - 1 && nextState.CurrentPoint.X == endingPoint.X) {
                    // We can move to the exit, check if this was the quickest path
                    if (bestToFinish2 > nextState.Score + 1) {
                        // If so, update the quickest path and remove all slower paths
                        bestToFinish2 = nextState.Score + 1;
                        queue = queue.Where(s => s.Score >= nextState.Score).ToList();
                    }
                }
                else if (nextState.Score < bestToFinish2 - 1){
                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y - 1) && nextState.CurrentPoint.Y > 1) {
                        // Player moves up
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y - 1
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y + 1) && nextState.CurrentPoint.Y < maxYIndex - 1) {
                        // Player moves down
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y + 1
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X - 1 && p.Y == nextState.CurrentPoint.Y) && nextState.CurrentPoint.X > 1 && nextState.CurrentPoint.Y != 0) {
                        // Player moves left
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X - 1,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X + 1 && p.Y == nextState.CurrentPoint.Y) && nextState.CurrentPoint.X < maxXIndex - 1 && nextState.CurrentPoint.Y != 0) {
                        // Player moves right
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X + 1,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }

                    if (!blizzard.Any(p => p.X == nextState.CurrentPoint.X && p.Y == nextState.CurrentPoint.Y)) {
                        // Playerdoesn't move
                        State newState = new() {
                            CurrentPoint = new(){
                                X = nextState.CurrentPoint.X,
                                Y = nextState.CurrentPoint.Y
                            },
                            Score = nextState.Score + 1,
                            Blizzard = blizzard,
                            BlizzardIndex = newBlizzardIndex
                        };

                        // Add to queue if not already there
                        if (!queue.Any(s => s.IsIdentical(newState))) {
                            queue.Add(newState);
                        }
                    }
                }
            }

            int answer = bestToStart + bestToFinish + bestToFinish2;

            return answer.ToString();
        }
    }
}