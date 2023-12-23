namespace AdventOfCode.Services
{
    public class Solution2023_23Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 23, example);
            List<List<char>> grid = lines.Select(line => line.ToList()).ToList();

            int startX = grid[0].IndexOf('.');
            int endX = grid[0].IndexOf('.');

            List<List<Point>> paths = [[new(startX, 0)]];

            int answer = 0;

            List<bool> pathIsFinished = [false];

            while (pathIsFinished.Any(x => x == false)) {
                foreach (int i in paths.Count) {
                    if (pathIsFinished[i]) {
                        continue;
                    }

                    List<Point> path = paths[i];
                    Point currentPoint = path.Last();
                    char currentValue = grid[currentPoint.Y][currentPoint.X];

                    if (currentPoint.Y == grid.Count - 1) {
                        pathIsFinished[i] = true;
                        continue;
                    }

                    // Get the list of potential neighbors
                    List<Point> neighbors = grid.GetNeighbors(currentPoint.X, currentPoint.Y).Where(neighbor => {
                            bool validNeighbor = true;
                            char value = grid[neighbor.Y][neighbor.X];

                            if (grid[neighbor.Y][neighbor.X] == '#' || path.Any(pathPoint => pathPoint.X == neighbor.X && pathPoint.Y == neighbor.Y)) {
                                validNeighbor = false;
                            }
                            else {
                                if (neighbor.X > currentPoint.X) {
                                    // Move right
                                    if (value == '<' || currentValue == '<') {
                                        validNeighbor = false;
                                    }
                                }
                                else if (neighbor.X < currentPoint.X) {
                                    // Left
                                    if (value == '>' || currentValue == '>') {
                                        validNeighbor = false;
                                    }
                                }
                                else if (neighbor.Y > currentPoint.Y) {
                                    // Down
                                    if (value == '^' || currentValue == '^') {
                                        validNeighbor = false;
                                    }
                                }
                                else if (neighbor.Y < currentPoint.Y) {
                                    // Up
                                    if (value == 'v' || currentValue == 'v') {
                                        validNeighbor = false;
                                    }
                                }
                            }
                            return validNeighbor;
                        }).ToList();

                    // Dead end
                    if (neighbors.Count == 0) {
                        pathIsFinished[i] = true;
                        continue;
                    }

                    // Add the next step to the path, split the path if there are multiple options
                    foreach (int j in neighbors.Count) {
                        Point point = neighbors[j];
                        if (j == neighbors.Count - 1) {
                            path.Add(point);
                        }
                        else {
                            List<Point> pathCopy = path.ToList();
                            pathCopy.Add(point);
                            paths.Add(pathCopy);
                            pathIsFinished.Add(false);
                        }
                    }
                }
            }
            
            List<Point> bestPath = paths.Where(p => p.Last().Y == grid.Count - 1).MaxBy(path => path.Count)!;
            answer = bestPath.Count - 1;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 23, example);
            List<List<char>> grid = lines.Select(line => line.Replace('>', '.').Replace('<', '.').Replace('v', '.').Replace('^', '.').ToList()).ToList();

            int startX = grid[0].IndexOf('.');
            int endX = grid.Last().IndexOf('.');

            int answer = 0;

            // Generate a graph of the maze
            // Calculate the nodes for the graph
            List<Point> intersections = [new(startX, 0), new(endX, grid.Count - 1)];

            foreach (int y in grid.Count) {
                foreach (int x in grid.First().Count) {
                    if (grid[y][x] == '.' && grid.GetNeighbors(x, y).Where(p => grid[p.Y][p.X] == '.').Count() > 2) {
                        intersections.Add(new(x, y));
                    }
                }
            }

            // Calculate the distances between nodes
            Dictionary<string, (Point, int)> distances = [];

            foreach (Point intersection in intersections) {
                List<List<Point>> paths = grid.GetNeighbors(intersection.X, intersection.Y).Where(p => grid[p.Y][p.X] == '.').Select(point => new List<Point>(){point}).ToList();

                List<bool> pathIsFinished = [];
                foreach (List<Point> path in paths) {
                    pathIsFinished.Add(false);
                }

                while (pathIsFinished.Any(x => x == false)) {
                    foreach (int i in paths.Count) {
                        if (pathIsFinished[i]) {
                            continue;
                        }

                        List<Point> path = paths[i];
                        Point endPoint = path.Last();
                        List<Point> neighbors = grid.GetNeighbors(endPoint.X, endPoint.Y).Where(point => grid[point.Y][point.X] != '#' && !(intersection.X == point.X && intersection.Y == point.Y) && !path.Any(x => x.X == point.X && x.Y == point.Y)).ToList();

                        if (path.Count == 1) {
                            // Check if we already know the length
                            Point secondPoint = path.First();
                            string startDirection = "";
                            if (intersection.X < secondPoint.X) {
                                startDirection = "Right";
                            }
                            else if (intersection.X > secondPoint.X) {
                                startDirection = "Left";
                            }
                            else if (intersection.Y < secondPoint.Y) {
                                startDirection = "Down";
                            }
                            else {
                                startDirection = "Up";
                            }
                            if (distances.ContainsKey($"{intersection.X} {intersection.Y} {startDirection}")) {
                                pathIsFinished[i] = true;
                                continue;
                            }
                        }

                        if (neighbors.Count > 1) {
                            // Stop, we're at a new intersection
                            Point secondPoint = path.First();
                            string startDirection = "";
                            if (intersection.X < secondPoint.X) {
                                startDirection = "Right";
                            }
                            else if (intersection.X > secondPoint.X) {
                                startDirection = "Left";
                            }
                            else if (intersection.Y < secondPoint.Y) {
                                startDirection = "Down";
                            }
                            else {
                                startDirection = "Up";
                            }

                            string endDirection = "";
                            Point secondToLastPoint = path.TakeLast(2).First();

                            if (secondToLastPoint.X > endPoint.X) {
                                endDirection = "Right";
                            }
                            else if (secondToLastPoint.X < endPoint.X) {
                                endDirection = "Left";
                            }
                            else if (secondToLastPoint.Y > endPoint.Y) {
                                endDirection = "Down";
                            }
                            else {
                                endDirection = "Up";
                            }

                            distances[$"{intersection.X} {intersection.Y} {startDirection}"] = (endPoint, path.Count);
                            distances[$"{endPoint.X} {endPoint.Y} {endDirection}"] = (intersection, path.Count);

                            pathIsFinished[i] = true;
                        }
                        else if (neighbors.Count != 0) {
                            path.Add(neighbors.First());
                        }
                        else {
                            pathIsFinished[i] = true;
                        }
                    }
                }
            }

            // Find the max distance
            List<List<string>> pathKeys = [[$"{startX} 0 Down"]];
            List<bool> pathsAreFinished = [false];

            while (pathsAreFinished.Any(x => x == false)) {
                foreach (int i in pathKeys.Count) {
                    if (pathsAreFinished[i]) {
                        continue;
                    }

                    List<string> path = pathKeys[i];

                    Point currentPoint = distances[path.Last()].Item1;

                    List<string> neighbors = distances.Where(d => d.Key.StartsWith($"{currentPoint.X} {currentPoint.Y}") && !path.Any(p => p.StartsWith($"{d.Value.Item1.X} {d.Value.Item1.Y}"))).Select(d => d.Key).ToList();
                    
                    // Dead end
                    if (neighbors.Count == 0) {
                        pathsAreFinished[i] = true;
                        continue;
                    }

                    // Add the next step to the path, split the path if there are multiple options
                    foreach (int j in neighbors.Count) {
                        string key = neighbors[j];
                        if (j == neighbors.Count - 1) {
                            path.Add(key);
                        }
                        else {
                            List<string> pathCopy = path.ToList();
                            pathCopy.Add(key);
                            pathKeys.Add(pathCopy);
                            pathsAreFinished.Add(false);
                        }
                    }
                }

                // Filter out extra paths
                /*
                List<int> pathsToDelete = [];
                int bestPathSoFar = 0;
                foreach (int i in pathsAreFinished.FindIndexes(a => a)) {
                    if (distances[pathKeys[i].Last()].Item1.Y != grid.Count - 1) {
                        pathsToDelete.Add(i);
                    }
                    else {
                        int value = pathKeys[i].Sum(key => distances[key].Item2);

                        if (bestPathSoFar > value) {
                            pathsToDelete.Add(i);
                        }
                        else {
                            bestPathSoFar = value;
                        }
                    }
                }

                pathKeys = pathKeys.Where((p, i) => !pathsToDelete.Contains(i)).ToList();
                pathsAreFinished = pathsAreFinished.Where((p, i) => !pathsToDelete.Contains(i)).ToList();
                */
            }

            answer = pathKeys.Max(path => path.Sum(key => distances[key].Item2));

            return answer.ToString();
        }
    }
}