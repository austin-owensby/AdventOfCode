namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/12.txt
    public class Solution2024_12Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 12, example);
            List<List<char>> grid = lines.ToGrid();

            int answer = 0;

            List<Point> visitedPoints = [];

            foreach (int y in grid.Count)
            {
                foreach (int x in grid.Count)
                {
                    if (!visitedPoints.Any(p => p.X == x && p.Y == y)) {
                        // Get the score for this new region
                        int area = 0;
                        int perimeter = 0;
                        char letter = grid[y][x];
                        Queue<Point> queue = new();
                        queue.Enqueue(new Point(x,y));

                        while (queue.Count > 0) {
                            Point point = queue.Dequeue();

                            if (visitedPoints.Any(p => p.X == point.X && p.Y == point.Y)) {
                                continue;
                            }

                            visitedPoints.Add(point);

                            List<Point> neighbors = grid.GetNeighbors(point.X, point.Y).Where(p => grid[p.Y][p.X] == letter).ToList();
                            area++;
                            perimeter += 4 - neighbors.Count;

                            foreach (Point neighborPoint in neighbors) {
                                queue.Enqueue(neighborPoint);
                            }
                        }

                        answer += area * perimeter;
                    }
                }
            }

            return answer.ToString();
        }

        private class NeighborPoint(double x, double y)
        {
            public double X { get; set; } = x;
            public double Y { get; set; } = y;
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 12, example);
            List<List<char>> grid = lines.ToGrid();

            int answer = 0;

            List<Point> visitedPoints = [];

            foreach (int y in grid.Count)
            {
                foreach (int x in grid.Count)
                {
                    if (!visitedPoints.Any(p => p.X == x && p.Y == y)) {
                        // Get the score for this new region
                        List<NeighborPoint> perimeterNeighbors = [];
                        List<List<NeighborPoint>> distinctSides = [];
                        int area = 0;
                        char letter = grid[y][x];
                        Queue<Point> queue = new();
                        queue.Enqueue(new Point(x,y));

                        while (queue.Count > 0) {
                            Point point = queue.Dequeue();

                            if (visitedPoints.Any(p => p.X == point.X && p.Y == point.Y)) {
                                continue;
                            }

                            visitedPoints.Add(point);
                            area++;

                            List<Point> neighbors = grid.GetNeighbors(point.X, point.Y);

                            if (neighbors.Count < 4) {
                                if (point.X == 0) {
                                    NeighborPoint neighbor = new(-0.5, point.Y);
                                    List<NeighborPoint>? side = distinctSides.FirstOrDefault(side => side.Any(p => p.X == neighbor.X && Math.Abs(p.Y - neighbor.Y) == 1));

                                    if (side == null) {
                                        side = [neighbor];
                                        distinctSides.Add(side);
                                    }
                                    else {
                                        side.Add(neighbor);
                                    }
                                }
                                if (point.Y == 0) {
                                    NeighborPoint neighbor = new(point.X, -0.5);
                                    List<NeighborPoint>? side = distinctSides.FirstOrDefault(side => side.Any(p => p.Y == neighbor.Y && Math.Abs(p.X - neighbor.X) == 1));

                                    if (side == null) {
                                        side = [neighbor];
                                        distinctSides.Add(side);
                                    }
                                    else {
                                        side.Add(neighbor);
                                    }
                                }
                                if (point.X == grid[0].Count - 1) {
                                    NeighborPoint neighbor = new(grid[0].Count - 0.5, point.Y);
                                    List<NeighborPoint>? side = distinctSides.FirstOrDefault(side => side.Any(p => p.X == neighbor.X && Math.Abs(p.Y - neighbor.Y) == 1));

                                    if (side == null) {
                                        side = [neighbor];
                                        distinctSides.Add(side);
                                    }
                                    else {
                                        side.Add(neighbor);
                                    }
                                }
                                if (point.Y == grid.Count - 1) {
                                    NeighborPoint neighbor = new(point.X, grid.Count - 0.5);
                                    List<NeighborPoint>? side = distinctSides.FirstOrDefault(side => side.Any(p => p.Y == neighbor.Y && Math.Abs(p.X - neighbor.X) == 1));

                                    if (side == null) {
                                        side = [neighbor];
                                        distinctSides.Add(side);
                                    }
                                    else {
                                        side.Add(neighbor);
                                    }
                                }
                            }

                            foreach (Point neighborPoint in neighbors) {
                                if (grid[neighborPoint.Y][neighborPoint.X] == letter) {
                                    queue.Enqueue(neighborPoint);
                                }
                                else {
                                    if (point.X == neighborPoint.X) {
                                        if (point.Y > neighborPoint.Y) {
                                            NeighborPoint neighbor = new(neighborPoint.X, neighborPoint.Y + 0.5);
                                            List<NeighborPoint>? side = distinctSides.FirstOrDefault(side => side.Any(p => p.Y == neighbor.Y && Math.Abs(p.X - neighbor.X) == 1));

                                            if (side == null) {
                                                side = [neighbor];
                                                distinctSides.Add(side);
                                            }
                                            else {
                                                side.Add(neighbor);
                                            }
                                        }
                                        if (point.Y < neighborPoint.Y) {
                                            NeighborPoint neighbor = new(neighborPoint.X, neighborPoint.Y - 0.5);
                                            List<NeighborPoint>? side = distinctSides.FirstOrDefault(side => side.Any(p => p.Y == neighbor.Y && Math.Abs(p.X - neighbor.X) == 1));

                                            if (side == null) {
                                                side = [neighbor];
                                                distinctSides.Add(side);
                                            }
                                            else {
                                                side.Add(neighbor);
                                            }
                                        }
                                    }
                                    if (point.Y == neighborPoint.Y) {
                                        if (point.X > neighborPoint.X) {
                                            NeighborPoint neighbor = new(neighborPoint.X + 0.5, neighborPoint.Y);
                                            List<NeighborPoint>? side = distinctSides.FirstOrDefault(side => side.Any(p => p.X == neighbor.X && Math.Abs(p.Y - neighbor.Y) == 1));

                                            if (side == null) {
                                                side = [neighbor];
                                                distinctSides.Add(side);
                                            }
                                            else {
                                                side.Add(neighbor);
                                            }
                                        }
                                        if (point.X < neighborPoint.X) {
                                            NeighborPoint neighbor = new(neighborPoint.X - 0.5, neighborPoint.Y);
                                            List<NeighborPoint>? side = distinctSides.FirstOrDefault(side => side.Any(p => p.X == neighbor.X && Math.Abs(p.Y - neighbor.Y) == 1));

                                            if (side == null) {
                                                side = [neighbor];
                                                distinctSides.Add(side);
                                            }
                                            else {
                                                side.Add(neighbor);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        int sides = distinctSides.Count;
                    
                        answer += area * sides;
                    }
                }
            }

            return answer.ToString();
        }
    }
}