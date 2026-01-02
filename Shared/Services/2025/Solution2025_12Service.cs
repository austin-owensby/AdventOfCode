namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/12.txt
    public class Solution2025_12Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 12, example);
            List<List<string>> parts = lines.ChunkByExclusive(l => string.IsNullOrWhiteSpace(l));

            List<List<List<bool>>> presents = parts.SkipLast(1).Select(p => p.Skip(1).Select(l => l.Select(x => x == '#').ToList()).ToList()).ToList();
            List<(List<int>, List<int>)> trees = parts.Last().Select(l => l.Split(": ")).Select(l => (l[0].Split("x").ToInts(), l[1].Split(" ").ToInts())).ToList();

            int answer = 0;

            foreach ((List<int> dimensions, List<int> requirements) in trees)
            {
                int width = dimensions.First();
                int height = dimensions.Last();

                int area = width * height;
                int maxPresentArea = requirements.Sum() * 9;

                // Check the simple case of doing no stacking at all
                if (maxPresentArea < area)
                {
                    answer++;
                    continue;
                }

                List<List<char>> grid = [];
                
                // Maybe we should check if the width vs. height would be better to tile against and swap height and width?

                foreach (int i in height)
                {
                    List<char> row = [];

                    foreach (int j in width)
                    {
                        row.Add(' ');
                    }

                    grid.Add(row);
                }

                int currentX = 0;
                int currentY = 0;

                // Try a simple tiling
                int rowHeight = 3;
                try
                {
                    for (int i = 0; i < requirements.Count; i++)
                    {
                        int requirement = requirements[i];

                        foreach (int j in requirement)
                        {
                            switch (i)
                            {
                                case 0:
                                    // Assuming a row always fits an even number of presents
                                    if (width - currentX <= 2)
                                    {
                                        currentY += rowHeight;
                                        currentX = 0;
                                    }

                                    if (j % 2 == 0)
                                    {
                                        // * *
                                        // * *
                                        // * * *
                                        grid[currentY][currentX] = 'A';
                                        grid[currentY][currentX + 1] = 'A';
                                        grid[currentY + 1][currentX] = 'A';
                                        grid[currentY + 1][currentX + 1] = 'A';
                                        grid[currentY + 2][currentX] = 'A';
                                        grid[currentY + 2][currentX + 1] = 'A';
                                        grid[currentY + 2][currentX + 2] = 'A';
                                        currentX += 2;
                                    }
                                    else
                                    {
                                        // * * *
                                        //   * *
                                        //   * *
                                        grid[currentY][currentX] = 'B';
                                        grid[currentY][currentX + 1] = 'B';
                                        grid[currentY][currentX + 2] = 'B';
                                        grid[currentY + 1][currentX + 1] = 'B';
                                        grid[currentY + 1][currentX + 2] = 'B';
                                        grid[currentY + 2][currentX + 1] = 'B';
                                        grid[currentY + 2][currentX + 2] = 'B';
                                        currentX += 3;
                                    }
                                    break;
                                case 1:
                                    if (width - currentX <= 2)
                                    {
                                        currentY += rowHeight;
                                        currentX = 0;
                                        rowHeight = 4;
                                    }

                                    if (j % 2 == 0)
                                    {
                                        // * * *
                                        //   * *
                                        // * * 
                                        grid[currentY][currentX] = 'D';
                                        grid[currentY][currentX + 1] = 'D';
                                        grid[currentY][currentX + 2] = 'D';
                                        grid[currentY + 1][currentX + 1] = 'D';
                                        grid[currentY + 1][currentX + 2] = 'D';
                                        grid[currentY + 2][currentX] = 'D';
                                        grid[currentY + 2][currentX + 1] = 'D';
                                        currentX += 2;
                                    }
                                    else
                                    {
                                        //   * *
                                        // * *
                                        // * * *
                                        grid[currentY + 1][currentX + 1] = 'C';
                                        grid[currentY + 1][currentX + 2] = 'C';
                                        grid[currentY + 2][currentX] = 'C';
                                        grid[currentY + 2][currentX + 1] = 'C';
                                        grid[currentY + 3][currentX] = 'C';
                                        grid[currentY + 3][currentX + 1] = 'C';
                                        grid[currentY + 3][currentX + 2] = 'C';
                                        currentX += 2;
                                    }
                                    break;
                                case 2:
                                    if (width - currentX <= 2)
                                    {
                                        currentY += rowHeight;
                                        currentX = 0;
                                        rowHeight = 4;
                                    }

                                    if (j % 2 == 0)
                                    {
                                        // * * *
                                        // *
                                        // * * *
                                        grid[currentY][currentX] = 'E';
                                        grid[currentY][currentX + 1] = 'E';
                                        grid[currentY][currentX + 2] = 'E';
                                        grid[currentY + 1][currentX] = 'E';
                                        grid[currentY + 2][currentX] = 'E';
                                        grid[currentY + 2][currentX + 1] = 'E';
                                        grid[currentY + 2][currentX + 2] = 'E';
                                        currentX++;
                                    }
                                    else
                                    {
                                        // * * *
                                        //     *
                                        // * * *
                                        grid[currentY + 1][currentX] = 'F';
                                        grid[currentY + 1][currentX + 1] = 'F';
                                        grid[currentY + 1][currentX + 2] = 'F';
                                        grid[currentY + 2][currentX + 2] = 'F';
                                        grid[currentY + 3][currentX] = 'F';
                                        grid[currentY + 3][currentX + 1] = 'F';
                                        grid[currentY + 3][currentX + 2] = 'F';
                                        currentX += 3;
                                    }
                                    break;
                                case 3:
                                    if (width - currentX <= 2)
                                    {
                                        currentY += rowHeight;
                                        currentX = 0;
                                        rowHeight = 3;
                                    }

                                    if (j % 2 == 0)
                                    {
                                        //   * *
                                        // * *
                                        // * 
                                        grid[currentY][currentX + 1] = 'G';
                                        grid[currentY][currentX + 2] = 'G';
                                        grid[currentY + 1][currentX] = 'G';
                                        grid[currentY + 1][currentX + 1] = 'G';
                                        grid[currentY + 2][currentX] = 'G';
                                        currentX++;
                                    }
                                    else
                                    {
                                        //     *
                                        //   * *
                                        // * * 
                                        grid[currentY][currentX + 2] = 'H';
                                        grid[currentY + 1][currentX + 1] = 'H';
                                        grid[currentY + 1][currentX + 2] = 'H';
                                        grid[currentY + 2][currentX] = 'H';
                                        grid[currentY + 2][currentX + 1] = 'H';
                                        currentX += 3;
                                    }
                                    break;
                                case 4:
                                    if (width - currentX <= 2)
                                    {
                                        currentY += rowHeight;
                                        currentX = 0;
                                        rowHeight = 3;
                                    }

                                    if (j % 2 == 0)
                                    {
                                        // * * *
                                        // * *
                                        // * 
                                        grid[currentY][currentX] = 'I';
                                        grid[currentY][currentX + 1] = 'I';
                                        grid[currentY][currentX + 2] = 'I';
                                        grid[currentY + 1][currentX] = 'I';
                                        grid[currentY + 1][currentX + 1] = 'I';
                                        grid[currentY + 2][currentX] = 'I';
                                        currentX++;
                                    }
                                    else
                                    {
                                        //     *
                                        //   * *
                                        // * * *
                                        grid[currentY][currentX + 2] = 'J';
                                        grid[currentY + 1][currentX + 1] = 'J';
                                        grid[currentY + 1][currentX + 2] = 'J';
                                        grid[currentY + 2][currentX] = 'J';
                                        grid[currentY + 2][currentX + 1] = 'J';
                                        grid[currentY + 2][currentX + 2] = 'J';
                                        currentX += 3;
                                    }
                                    break;
                                case 5:
                                    if (width - currentX <= 2)
                                    {
                                        currentY += rowHeight;
                                        currentX = 0;
                                        rowHeight = 4;
                                    }

                                    if (j % 2 == 0)
                                    {
                                        // * * *
                                        //   *
                                        // * * * 
                                        grid[currentY][currentX] = 'K';
                                        grid[currentY][currentX + 1] = 'K';
                                        grid[currentY][currentX + 2] = 'K';
                                        grid[currentY + 1][currentX + 1] = 'K';
                                        grid[currentY + 2][currentX] = 'K';
                                        grid[currentY + 2][currentX + 1] = 'K';
                                        grid[currentY + 2][currentX + 2] = 'K';
                                        currentX += 2;
                                    }
                                    else
                                    {
                                        // * * *
                                        //   *
                                        // * * * 
                                        grid[currentY + 1][currentX] = 'K';
                                        grid[currentY + 1][currentX + 1] = 'K';
                                        grid[currentY + 1][currentX + 2] = 'K';
                                        grid[currentY + 2][currentX + 1] = 'K';
                                        grid[currentY + 3][currentX] = 'K';
                                        grid[currentY + 3][currentX + 1] = 'K';
                                        grid[currentY + 3][currentX + 2] = 'K';
                                        currentX += 2;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    answer++;
                }
                catch
                {
                    // Not efficient, but oh well...
                    continue;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            return "There is no problem for Day 12 part 2, solve all other problems to get the last star.";
        }
    }
}