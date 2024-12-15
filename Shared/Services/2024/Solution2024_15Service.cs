namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/15.txt
    public class Solution2024_15Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 15, example);
            List<List<char>> grid = lines.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToList().ToGrid();
            List<char> instructions = lines.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1).SelectMany(l => l).ToList();

            int answer = 0;

            int robotX = -1;
            int robotY = -1;
            
            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == '@') {
                        robotX = x;
                        robotY = y;
                        break;
                    }
                }
                if (robotX != -1) {
                    break;
                }
            }

            foreach (char instruction in instructions)
            {
                int emptySpaceIndex = -1;
                switch (instruction) {
                    case '^':
                        for (int y = robotY - 1; y > 0; y--) {
                            if (grid[y][robotX] == '.') {
                                emptySpaceIndex = y;
                                break;
                            }
                            if (grid[y][robotX] == '#') {
                                break;
                            }
                        }

                        if (emptySpaceIndex != -1) {
                            if (emptySpaceIndex + 1 != robotY) {
                                grid[emptySpaceIndex][robotX] = 'O';
                            }
                            grid[robotY - 1][robotX] = '@';
                            grid[robotY][robotX] = '.';
                            robotY--;
                        }
                        break;
                    case '>':
                        for (int x = robotX + 1; x < grid[robotY].Count - 1; x++) {
                            if (grid[robotY][x] == '.') {
                                emptySpaceIndex = x;
                                break;
                            }
                            if (grid[robotY][x] == '#') {
                                break;
                            }
                        }

                        if (emptySpaceIndex != -1) {
                            if (emptySpaceIndex - 1 != robotX) {
                                grid[robotY][emptySpaceIndex] = 'O';
                            }
                            grid[robotY][robotX + 1] = '@';
                            grid[robotY][robotX] = '.';
                            robotX++;
                        }
                        break;
                    case 'v':
                        for (int y = robotY + 1; y < grid.Count - 1; y++) {
                            if (grid[y][robotX] == '.') {
                                emptySpaceIndex = y;
                                break;
                            }
                            if (grid[y][robotX] == '#') {
                                break;
                            }
                        }

                        if (emptySpaceIndex != -1) {
                            if (emptySpaceIndex - 1 != robotY) {
                                grid[emptySpaceIndex][robotX] = 'O';
                            }
                            grid[robotY + 1][robotX] = '@';
                            grid[robotY][robotX] = '.';
                            robotY++;
                        }
                        break;
                    case '<':
                        for (int x = robotX - 1; x > 0; x--) {
                            if (grid[robotY][x] == '.') {
                                emptySpaceIndex = x;
                                break;
                            }
                            if (grid[robotY][x] == '#') {
                                break;
                            }
                        }

                        if (emptySpaceIndex != -1) {
                            if (emptySpaceIndex + 1 != robotX) {
                                grid[robotY][emptySpaceIndex] = 'O';
                            }
                            grid[robotY][robotX - 1] = '@';
                            grid[robotY][robotX] = '.';
                            robotX--;
                        }
                        break;
                }
            }

            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == 'O') {
                        answer += 100 * y + x;
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 15, example);
            List<List<char>> originalGrid = lines.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToList().ToGrid();
            List<char> instructions = lines.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1).SelectMany(l => l).ToList();

            List<List<char>> grid = [];

            foreach (List<char> row in originalGrid) {
                List<char> newRow = [];
                foreach (char cell in row) {
                    newRow.AddRange(
                        cell switch {
                            '#' => ['#', '#'],
                            'O' => ['[', ']'],
                            '.' => ['.', '.'],
                            '@' => ['@', '.'],
                            _ => throw new NotImplementedException()
                        }
                    );
                }
                grid.Add(newRow);
            }

            int answer = 0;

            int robotX = -1;
            int robotY = -1;
            
            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == '@') {
                        robotX = x;
                        robotY = y;
                        break;
                    }
                }
                if (robotX != -1) {
                    break;
                }
            }

            foreach (char instruction in instructions)
            {
                int emptySpaceIndex = -1;
                List<Point> affectedPoints = [new(robotX, robotY)];
                switch (instruction) {
                    case '^':
                        for (int y = robotY - 1; y > 0; y--) {
                            List<int> affectedXCoordinates = affectedPoints.Where(p => p.Y == y + 1).Select(p => p.X).ToList();
                            bool allEmpty = true;
                            bool stop = false;
                            foreach (int x in affectedXCoordinates) {
                                if (grid[y][x] == '#') {
                                    stop = true;
                                    break;
                                }
                                
                                if (grid[y][x] == '[') {
                                    if (!affectedPoints.Any(p => p.X == x && p.Y == y)) {
                                        affectedPoints.Add(new(x, y));
                                        affectedPoints.Add(new(x + 1, y));
                                    }
                                    allEmpty = false;
                                }
                                
                                if (grid[y][x] == ']') {
                                    if (!affectedPoints.Any(p => p.X == x && p.Y == y)) {
                                        affectedPoints.Add(new(x, y));
                                        affectedPoints.Add(new(x - 1, y));
                                    }
                                    allEmpty = false;
                                }
                            }
                            if (allEmpty) {
                                emptySpaceIndex = y;
                            }

                            if (stop) {
                                emptySpaceIndex = -1;
                                break;
                            }
                        }

                        if (emptySpaceIndex != -1) {
                            foreach (Point point in affectedPoints.OrderBy(p => p.Y)) {
                                if (point.Y == robotY) {
                                    continue;
                                }
                                grid[point.Y - 1][point.X] = grid[point.Y][point.X];
                                grid[point.Y][point.X] = '.';
                            }
                            grid[robotY - 1][robotX] = '@';
                            grid[robotY][robotX] = '.';
                            robotY--;
                        }
                        break;
                    case '>':
                        for (int x = robotX + 1; x < grid[robotY].Count - 1; x++) {
                            if (grid[robotY][x] == '.') {
                                emptySpaceIndex = x;
                                break;
                            }
                            if (grid[robotY][x] == '#') {
                                break;
                            }
                        }

                        if (emptySpaceIndex != -1) {
                            for (int x = emptySpaceIndex; x > robotX; x--) {
                                grid[robotY][x] = grid[robotY][x - 1];
                                grid[robotY][x - 1] = '.';
                            }
                            robotX++;
                        }
                        break;
                    case 'v':
                        for (int y = robotY + 1; y < grid.Count - 1; y++) {
                            List<int> affectedXCoordinates = affectedPoints.Where(p => p.Y == y - 1).Select(p => p.X).ToList();
                            bool allEmpty = true;
                            bool stop = false;
                            foreach (int x in affectedXCoordinates) {
                                if (grid[y][x] == '#') {
                                    stop = true;
                                    break;
                                }
                                
                                if (grid[y][x] == '[') {
                                    if (!affectedPoints.Any(p => p.X == x && p.Y == y)) {
                                        affectedPoints.Add(new(x, y));
                                        affectedPoints.Add(new(x + 1, y));
                                    }
                                    allEmpty = false;
                                }
                                
                                if (grid[y][x] == ']') {
                                    if (!affectedPoints.Any(p => p.X == x && p.Y == y)) {
                                        affectedPoints.Add(new(x, y));
                                        affectedPoints.Add(new(x - 1, y));
                                    }
                                    allEmpty = false;
                                }
                            }
                            if (allEmpty) {
                                emptySpaceIndex = y;
                            }

                            if (stop) {
                                emptySpaceIndex = -1;
                                break;
                            }
                        }

                        if (emptySpaceIndex != -1) {
                            foreach (Point point in affectedPoints.OrderByDescending(p => p.Y)) {
                                if (point.Y == robotY) {
                                    continue;
                                }
                                grid[point.Y + 1][point.X] = grid[point.Y][point.X];
                                grid[point.Y][point.X] = '.';
                            }
                            grid[robotY + 1][robotX] = '@';
                            grid[robotY][robotX] = '.';
                            robotY++;
                        }
                        break;
                    case '<':
                        for (int x = robotX - 1; x > 0; x--) {
                            if (grid[robotY][x] == '.') {
                                emptySpaceIndex = x;
                                break;
                            }
                            if (grid[robotY][x] == '#') {
                                break;
                            }
                        }

                        if (emptySpaceIndex != -1) {
                            for (int x = emptySpaceIndex; x < robotX; x++) {
                                grid[robotY][x] = grid[robotY][x + 1];
                                grid[robotY][x + 1] = '.';
                            }
                            robotX--;
                        }
                        break;
                }
            }

            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == '[') {
                        answer += 100 * y + x;
                    }
                }
            }


            return answer.ToString();
        }
    }
}