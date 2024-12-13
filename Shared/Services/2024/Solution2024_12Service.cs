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

        private class NeighborPoint(double x, double y, int direction)
        {
            public double X { get; set; } = x;
            public double Y { get; set; } = y;
            public int Direction { get; set; } = direction;
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
                                    NeighborPoint neighbor = new(-0.5, point.Y, 1);
                                    perimeterNeighbors.Add(neighbor);
                                }
                                if (point.Y == 0) {
                                    NeighborPoint neighbor = new(point.X, -0.5, 2);
                                    perimeterNeighbors.Add(neighbor);
                                }
                                if (point.X == grid[0].Count - 1) {
                                    NeighborPoint neighbor = new(grid[0].Count - 0.5, point.Y, 3);
                                    perimeterNeighbors.Add(neighbor);
                                }
                                if (point.Y == grid.Count - 1) {
                                    NeighborPoint neighbor = new(point.X, grid.Count - 0.5, 4);
                                    perimeterNeighbors.Add(neighbor);
                                }
                            }

                            foreach (Point neighborPoint in neighbors) {
                                if (grid[neighborPoint.Y][neighborPoint.X] == letter) {
                                    queue.Enqueue(neighborPoint);
                                }
                                else {
                                    if (point.X == neighborPoint.X) {
                                        if (point.Y > neighborPoint.Y) {
                                            NeighborPoint neighbor = new(neighborPoint.X, neighborPoint.Y + 0.5, 4);
                                            perimeterNeighbors.Add(neighbor);
                                        }
                                        if (point.Y < neighborPoint.Y) {
                                            NeighborPoint neighbor = new(neighborPoint.X, neighborPoint.Y - 0.5, 2);
                                            perimeterNeighbors.Add(neighbor);
                                        }
                                    }
                                    if (point.Y == neighborPoint.Y) {
                                        if (point.X > neighborPoint.X) {
                                            NeighborPoint neighbor = new(neighborPoint.X + 0.5, neighborPoint.Y, 3);
                                            perimeterNeighbors.Add(neighbor);
                                        }
                                        if (point.X < neighborPoint.X) {
                                            NeighborPoint neighbor = new(neighborPoint.X - 0.5, neighborPoint.Y, 1);
                                            perimeterNeighbors.Add(neighbor);
                                        }
                                    }
                                }
                            }
                        }

                        // Based on the neighbors, calculate the distinct side
                        List<List<NeighborPoint>> distinctSides = [];
                        foreach (NeighborPoint neighbor in perimeterNeighbors) {
                            // Check if we've already accounted for this neighbor
                            if (!distinctSides.SelectMany(s => s).Any(p => p.X == neighbor.X && p.Y == neighbor.Y && p.Direction == neighbor.Direction)) {
                                // Get the point that are aligned with this point
                                List<NeighborPoint> potentialSide = perimeterNeighbors.Where(p => p.Direction == neighbor.Direction && (neighbor.X % 1 == 0 && p.Y == neighbor.Y || neighbor.Y % 1 == 0 && p.X == neighbor.X)).ToList();

                                // For the simple case of 1, instantly return the side
                                if (potentialSide.Count == 1) {
                                    distinctSides.Add(potentialSide);
                                    continue;
                                }

                                // Check if this side is vertical or horizontal
                                if (potentialSide[0].X == potentialSide[1].X) {
                                    potentialSide = potentialSide.OrderBy(p => p.Y).ToList();
                                    // Loop over the points, if there is a gap, detect a new side
                                    List<NeighborPoint> side = [];
                                    foreach (int i in potentialSide.Count - 1) {
                                        side.Add(potentialSide[i]);

                                        if (potentialSide[i].Y + 1 != potentialSide[i + 1].Y) {
                                            distinctSides.Add(side);
                                            side = [];
                                        }
                                    }
                                    side.Add(potentialSide.Last());
                                    distinctSides.Add(side);
                                }
                                else {
                                    potentialSide = potentialSide.OrderBy(p => p.X).ToList();
                                    // Loop over the points, if there is a gap, detect a new side
                                    List<NeighborPoint> side = [];
                                    foreach (int i in potentialSide.Count - 1) {
                                        side.Add(potentialSide[i]);

                                        if (potentialSide[i].X + 1 != potentialSide[i + 1].X) {
                                            distinctSides.Add(side);
                                            side = [];
                                        }
                                    }
                                    side.Add(potentialSide.Last());
                                    distinctSides.Add(side);
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