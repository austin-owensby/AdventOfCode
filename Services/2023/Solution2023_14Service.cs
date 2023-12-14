namespace AdventOfCode.Services
{
    public class Solution2023_14Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 14, example);
            List<List<char>> grid = lines.Select(line => line.ToList()).ToList();

            int answer = 0;

            foreach (int y in grid.Count)
            {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == 'O') {
                        for (int newY = y - 1; newY >= 0; newY--) {
                            if (grid[newY][x] == '.') {
                                // Move the rock up 1 spot
                                grid[newY + 1][x] = '.';
                                grid[newY][x] = 'O';
                            }
                            else {
                                break;
                            }
                        }
                    }
                }
            }

            foreach (int y in grid.Count)
            {
                foreach (int x in grid.Count) {
                    if (grid[y][x] == 'O') {
                        answer += grid.Count - y;
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 14, example);
            List<List<char>> grid = lines.Select(line => line.ToList()).ToList();

            Dictionary<string, int> map = [];
            Dictionary<int, int> answerMap = [];

            int answer = 0;
            List<int> history = [];
            int period = 0;
            int i = 0;

            while (period == 0) {
                // North
                foreach (int y in grid.Count)
                {
                    foreach (int x in grid[y].Count) {
                        if (grid[y][x] == 'O') {
                            for (int newY = y - 1; newY >= 0; newY--) {
                                if (grid[newY][x] == '.') {
                                    // Move the rock up 1 spot
                                    grid[newY + 1][x] = '.';
                                    grid[newY][x] = 'O';
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                }

                // West
                foreach (int x in grid.First().Count)
                {
                    foreach (int y in grid.Count) {
                        if (grid[y][x] == 'O') {
                            for (int newX = x - 1; newX >= 0; newX--) {
                                if (grid[y][newX] == '.') {
                                    // Move the rock right 1 spot
                                    grid[y][newX + 1] = '.';
                                    grid[y][newX] = 'O';
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                }

                // South
                for (int y = grid.Count - 1; y >= 0; y--)
                {
                    foreach (int x in grid[y].Count) {
                        if (grid[y][x] == 'O') {
                            for (int newY = y + 1; newY < grid.Count; newY++) {
                                if (grid[newY][x] == '.') {
                                    // Move the rock down 1 spot
                                    grid[newY - 1][x] = '.';
                                    grid[newY][x] = 'O';
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                }

                // East
                for (int x = grid.First().Count - 1; x >= 0; x--)
                {
                    foreach (int y in grid.Count) {
                        if (grid[y][x] == 'O') {
                            for (int newX = x + 1; newX < grid[y].Count; newX++) {
                                if (grid[y][newX] == '.') {
                                    // Move the rock right 1 spot
                                    grid[y][newX - 1] = '.';
                                    grid[y][newX] = 'O';
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                }

                // Produce a key for this position
                string key = string.Join("", grid.Select(row => string.Join("", row)));

                if (!map.TryGetValue(key, out int index)) {
                    // If we don't have a key for this index, record the first index with this key and it's associated answer
                    map[key] = i;
                    history.Add(i);
                    
                    int answerValue = 0;
                    foreach (int y in grid.Count)
                    {
                        foreach (int x in grid.Count) {
                            if (grid[y][x] == 'O') {
                                answerValue += grid.Count - y;
                            }
                        }
                    }
                    answerMap[i] = answerValue;
                }
                else {
                    history.Add(index);
                    // Check if we've found a period
                    bool periodFound = true;
                    int offset = 0;
                    while (periodFound) {

                        if (offset > 0 && history[i] == history[i - offset]) {
                            period = offset;
                            break;
                        }
                        if (history[i - offset] != history[index - offset]) {
                            periodFound = false;
                        }

                        offset++;
                    }
                }

                // Now that we know the period, calculate the offset and end value
                if (period != 0) {
                    int periodOffset = index - period + 1;

                    int answerKey = (1000000000 - 1 - periodOffset) % period + periodOffset;

                    answer = answerMap[answerKey];
                    break;
                }

                i++;
            }

            return answer.ToString();
        }
    }
}