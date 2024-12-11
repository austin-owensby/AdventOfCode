namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2023/10.txt
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

            // Record the path
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

            // Remove parts that are not part of the loop
            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (!path.Any(point => point.X == x && point.Y == y)) {
                        grid[y][x] = '.';
                    }
                }
            }

            // Remove duplicate last element
            path = path.SkipLast(1).ToList();

            // Rearrange the path so that the start is the top left corner
            Point newStart = path.MinBy(point => point.X + point.Y)!;
            int newStartIndex = path.IndexOf(newStart);
            path = path.Skip(newStartIndex).Concat(path.Take(newStartIndex)).ToList();

            // Loop over the path and paint empty spaces as inside
            // 7 0 1
            // 6 X 2
            // 5 4 3
            int insideDirection = 0;
            char prevValue = grid[path.Last().Y][path.Last().X];

            if (prevValue == '-') {
                insideDirection = 4; 
            }
            else if (prevValue == '7') {
                insideDirection = 5;
            }
            else if (prevValue == 'J') {
                insideDirection = 3;
            }
            else if (prevValue == '|') {
                insideDirection = 2;
            }
            else if (prevValue == 'L') {
                insideDirection = 1;
            }

            // As we move around the path, determine which direction we're facing so that we know where the inside is
            foreach (Point point in path) {
                switch (grid[point.Y][point.X]) {
                    case 'F':
                        if (prevValue == 'J' && insideDirection == 3 || prevValue == '|' && insideDirection == 2 || prevValue == '-' && insideDirection == 4 || prevValue == 'L' && insideDirection == 1 || prevValue == '7' && insideDirection == 5) {
                            insideDirection = 3;
                        }
                        else {
                            insideDirection = 7;
                        }
                        break;
                    case 'L':
                        if (prevValue == 'J' && insideDirection == 7 || prevValue == '|' && insideDirection == 2 || prevValue == '-' && insideDirection == 0 || prevValue == 'F' && insideDirection == 3 || prevValue == '7' && insideDirection == 1) {
                            insideDirection = 1;
                        }
                        else {
                            insideDirection = 5;
                        }
                        break;
                    case '7':
                        if (prevValue == 'J' && insideDirection == 7 || prevValue == '|' && insideDirection == 6 || prevValue == '-' && insideDirection == 4 || prevValue == 'F' && insideDirection == 3 || prevValue == 'L' && insideDirection == 5) {
                            insideDirection = 5;
                        }
                        else {
                            insideDirection = 1;
                        }
                        break;
                    case 'J':
                        if (prevValue == 'F' && insideDirection == 7 || prevValue == '|' && insideDirection == 6 || prevValue == '-' && insideDirection == 0 || prevValue == 'L' && insideDirection == 1 || prevValue == '7' && insideDirection == 5) {
                            insideDirection = 7;
                        }
                        else {
                            insideDirection = 3;
                        }
                        break;
                    case '|':
                        if (prevValue == 'F' && insideDirection == 3 || prevValue == '|' && insideDirection == 2 || prevValue == 'J' && insideDirection == 3 || prevValue == 'L' && insideDirection == 1 || prevValue == '7' && insideDirection == 1) {
                            insideDirection = 2;
                        }
                        else {
                            insideDirection = 6;
                        }
                        break;
                    case '-':
                        if (prevValue == 'F' && insideDirection == 3 || prevValue == '-' && insideDirection == 4 || prevValue == 'J' && insideDirection == 3 || prevValue == 'L' && insideDirection == 5 || prevValue == '7' && insideDirection == 5) {
                            insideDirection = 4;
                        }
                        else {
                            insideDirection = 0;
                        }
                        break;
                }
                prevValue = grid[point.Y][point.X];
                // Based on the direction we're facing, paint empty spaces as the inside
                switch (insideDirection) {
                    case 0:
                        if (point.Y > 0) {
                            if (grid[point.Y - 1][point.X] == '.') {
                                grid[point.Y - 1][point.X] = 'I';
                            }

                            if (point.X > 0 && grid[point.Y - 1][point.X - 1] == '.') {
                                grid[point.Y - 1][point.X - 1] = 'I';
                            }

                            if (point.X < grid.First().Count - 1 && grid[point.Y - 1][point.X + 1] == '.') {
                                grid[point.Y - 1][point.X + 1] = 'I';
                            }
                        }
                        break;
                    case 1:
                        if (point.Y > 0 && grid[point.Y - 1][point.X] == '.') {
                            grid[point.Y - 1][point.X] = 'I';
                        }

                        if (point.Y > 0 && point.X < grid.First().Count - 1 && grid[point.Y - 1][point.X + 1] == '.') {
                            grid[point.Y - 1][point.X + 1] = 'I';
                        }

                        if (point.X < grid.First().Count - 1 && grid[point.Y][point.X + 1] == '.') {
                            grid[point.Y][point.X + 1] = 'I';
                        }
                        break;
                    case 2:
                        if (point.X < grid.First().Count - 1) {
                            if (grid[point.Y][point.X + 1] == '.') {
                                grid[point.Y][point.X + 1] = 'I';
                            }

                            if (point.Y > 0 && grid[point.Y - 1][point.X + 1] == '.') {
                                grid[point.Y - 1][point.X + 1] = 'I';
                            }

                            if (point.Y < grid.Count - 1 && grid[point.Y + 1][point.X + 1] == '.') {
                                grid[point.Y + 1][point.X + 1] = 'I';
                            }
                        }
                        break;
                    case 3:
                        if (point.Y < grid.Count - 1 && grid[point.Y + 1][point.X] == '.') {
                            grid[point.Y + 1][point.X] = 'I';
                        }

                        if (point.Y < grid.Count - 1 && point.X < grid.First().Count - 1 && grid[point.Y + 1][point.X + 1] == '.') {
                            grid[point.Y + 1][point.X + 1] = 'I';
                        }

                        if (point.X < grid.First().Count - 1 && grid[point.Y][point.X + 1] == '.') {
                            grid[point.Y][point.X + 1] = 'I';
                        }
                        break;
                    case 4:
                        if (point.Y < grid.Count - 1) {
                            if (grid[point.Y + 1][point.X] == '.') {
                                grid[point.Y + 1][point.X] = 'I';
                            }

                            if (point.X > 0 && grid[point.Y + 1][point.X - 1] == '.') {
                                grid[point.Y + 1][point.X - 1] = 'I';
                            }

                            if (point.X < grid.First().Count - 1 && grid[point.Y + 1][point.X + 1] == '.') {
                                grid[point.Y + 1][point.X + 1] = 'I';
                            }
                        }
                        break;
                    case 5:
                        if (point.Y < grid.Count - 1 && grid[point.Y + 1][point.X] == '.') {
                            grid[point.Y + 1][point.X] = 'I';
                        }

                        if (point.Y < grid.Count - 1 && point.X > 0 && grid[point.Y + 1][point.X - 1] == '.') {
                            grid[point.Y + 1][point.X - 1] = 'I';
                        }

                        if (point.X > 0 && grid[point.Y][point.X - 1] == '.') {
                            grid[point.Y][point.X - 1] = 'I';
                        }
                        break;
                    case 6:
                        if (point.X > 0) {
                            if (grid[point.Y][point.X - 1] == '.') {
                                grid[point.Y][point.X - 1] = 'I';
                            }

                            if (point.Y > 0 && grid[point.Y - 1][point.X - 1] == '.') {
                                grid[point.Y - 1][point.X - 1] = 'I';
                            }

                            if (point.Y < grid.Count - 1 && grid[point.Y + 1][point.X - 1] == '.') {
                                grid[point.Y + 1][point.X - 1] = 'I';
                            }
                        }
                        break;
                    case 7:
                        if (point.X > 0 && grid[point.Y][point.X - 1] == '.') {
                            grid[point.Y][point.X - 1] = 'I';
                        }

                        if (point.Y > 0 && point.X > 0 && grid[point.Y - 1][point.X - 1] == '.') {
                            grid[point.Y - 1][point.X - 1] = 'I';
                        }

                        if (point.Y > 0 && grid[point.Y - 1][point.X] == '.') {
                            grid[point.Y - 1][point.X] = 'I';
                        }
                        break;
                }
            }

            // Now that we've filled in all of the inside spaces, do a flood fill for any larger gaps
            Queue<Point> pointsQueue = [];

            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == 'I') {
                        pointsQueue.Enqueue(new(x, y));
                    }
                }
            }

            while (pointsQueue.Count > 0) {
                Point point = pointsQueue.Dequeue();
                List<Point> neighbors = grid.GetNeighbors(point.X, point.Y);

                foreach (Point neighbor in neighbors) {
                    if (grid[neighbor.Y][neighbor.X] == '.') {
                        grid[neighbor.Y][neighbor.X] = 'I';
                        pointsQueue.Enqueue(neighbor);
                    }
                }
            }

            int answer = grid.Sum(row => row.Sum(c => c == 'I' ? 1 : 0));
            
            return answer.ToString();
        }
    }
}