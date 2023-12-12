namespace AdventOfCode.Services
{
    public class Solution2023_10Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 10, example);
            List<List<char>> grid = lines.Select(line => line.ToList()).ToList();

            List<Point> path = [];

            // Find the starting point
            int startY = grid.FindIndex(line => line.Contains('S'));
            int startX = grid[startY].FindIndex(x => x == 'S');
            Point startPoint = new (startX, startY);
            path.Add(startPoint);

            List<char> downValues = ['J', 'L', '|'];
            List<char> rightValues = ['J', '7', '-'];
            List<char> upValues = ['F', '7', '|'];
            List<char> leftValues = ['L', 'F', '-'];

            // Replace the starting point
            List<Point> startNeighbors = grid.GetNeighbors(startX, startY);
            bool canMoveUp = false;
            bool canMoveRight = false;
            bool canMoveDown = false;
            bool canMoveLeft = false;

            foreach (Point neighbor in startNeighbors) {
                char neightborValue = grid[neighbor.Y][neighbor.X];
                // Consider moving down
                if (neighbor.Y - 1 == startPoint.Y && neighbor.X == startPoint.X) {
                    if (downValues.Contains(neightborValue)) {
                        canMoveDown = true;
                    }
                }
                // Consider moving right
                else if (neighbor.Y == startPoint.Y && neighbor.X - 1 == startPoint.X) {
                    if (rightValues.Contains(neightborValue)) {
                        canMoveRight = true;
                    }
                }
                // Consider moving up
                else if (neighbor.Y + 1 == startPoint.Y && neighbor.X == startPoint.X) {
                    if (upValues.Contains(neightborValue)) {
                        canMoveUp = true;
                    }
                }
                // Consider moving left
                else if (neighbor.Y == startPoint.Y && neighbor.X + 1 == startPoint.X) {
                    if (leftValues.Contains(neightborValue)) {
                        canMoveLeft = true;
                    }
                }
            }

            if (canMoveUp && canMoveDown) {
                grid[startY][startX] = '|';
            }
            else if (canMoveRight && canMoveLeft) {
                grid[startY][startX] = '-';
            }
            else if (canMoveRight && canMoveUp) {
                grid[startY][startX] = 'L';
            }
            else if (canMoveLeft && canMoveUp) {
                grid[startY][startX] = 'J';
            }
            else if (canMoveRight && canMoveDown) {
                grid[startY][startX] = 'F';
            }
            else if (canMoveLeft && canMoveDown) {
                grid[startY][startX] = '7';
            }

            do {
                Point currentPoint = path.Last();
                Point previousPoint = path.Count == 1 ? currentPoint : path.TakeLast(2).First();

                char currentValue = grid[currentPoint.Y][currentPoint.X];
                List<Point> neighbors = grid.GetNeighbors(currentPoint.X, currentPoint.Y);

                // Loop over each potential neighbor an evaluate which step to take
                foreach (Point neighbor in neighbors) {
                    char neightborValue = grid[neighbor.Y][neighbor.X];
                    // Don't consider the previous point as a neighbor
                    if (!(previousPoint.X == neighbor.X && previousPoint.Y == neighbor.Y)) {
                        // Consider moving down
                        if (neighbor.Y - 1 == currentPoint.Y && neighbor.X == currentPoint.X) {
                            if (downValues.Contains(neightborValue) && upValues.Contains(currentValue)) {
                                path.Add(neighbor);
                                break;
                            }
                        }
                        // Consider moving right
                        else if (neighbor.Y == currentPoint.Y && neighbor.X - 1 == currentPoint.X) {
                            if (rightValues.Contains(neightborValue) && leftValues.Contains(currentValue)) {
                                path.Add(neighbor);
                                break;
                            }
                        }
                        // Consider moving up
                        else if (neighbor.Y + 1 == currentPoint.Y && neighbor.X == currentPoint.X) {
                            if (upValues.Contains(neightborValue) && downValues.Contains(currentValue)) {
                                path.Add(neighbor);
                                break;
                            }
                        }
                        // Consider moving left
                        else if (neighbor.Y == currentPoint.Y && neighbor.X + 1 == currentPoint.X) {
                            if (leftValues.Contains(neightborValue) && rightValues.Contains(currentValue)) {
                                path.Add(neighbor);
                                break;
                            }
                        }
                    }
                }
            }
            // Keep looping until we get back to the start point
            while (!(path.First().X == path.Last().X && path.First().Y == path.Last().Y));

            // Calculate the halfway point of the path
            int answer = (path.Count - 1) / 2;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 10, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }
    }
}