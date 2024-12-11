namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2022/14.txt
    public class Solution2022_14Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 14, example);

            List<List<Point>> rockLines = lines.Select(line => line.SplitSubstring(" -> ").QuickRegex(@"(\d+),(\d+)").ToInts().Select(l => new Point()
            {
                X = l[0],
                Y = l[1]
            }).ToList()).ToList();

            int maxX = rockLines.SelectMany(r => r.Select(r => r.X)).Max();
            int maxY = rockLines.SelectMany(r => r.Select(r => r.Y)).Max();

            List<List<int>> grid = Enumerable.Repeat(0, maxY + 1).Select(x => Enumerable.Repeat(0, maxX + 1).ToList()).ToList();

            Point? prevPoint = null;

            foreach (List<Point> rockLine in rockLines)
            {
                foreach (Point rock in rockLine)
                {
                    if (prevPoint != null)
                    {
                        if (prevPoint.X == rock.X)
                        {
                            if (rock.Y > prevPoint.Y)
                            {
                                for (int y = prevPoint.Y; y <= rock.Y; y++)
                                {
                                    grid[y][rock.X] = 1;
                                }
                            }
                            else
                            {
                                for (int y = rock.Y; y <= prevPoint.Y; y++)
                                {
                                    grid[y][rock.X] = 1;
                                }
                            }
                        }
                        else
                        {
                            if (rock.X > prevPoint.X)
                            {
                                for (int x = prevPoint.X; x <= rock.X; x++)
                                {
                                    grid[rock.Y][x] = 1;
                                }
                            }
                            else
                            {
                                for (int x = rock.X; x <= prevPoint.X; x++)
                                {
                                    grid[rock.Y][x] = 1;
                                }
                            }
                        }
                    }

                    prevPoint = rock;
                }

                prevPoint = null;
            }

            // Display output for fun
            string output = "";

            foreach (List<int> rockLine in grid)
            {
                output += string.Join("", rockLine.Select(r => r == 0 ? " " : "#"));
                output += "\n";
            }

            bool sandFalling = true;

            Point grain = new()
            {
                X = 500,
                Y = 0,
            };

            int answer = 0;

            while (sandFalling)
            {
                if (grid[grain.Y + 1][grain.X] == 0)
                {
                    grain.Y++;
                }
                else if (grid[grain.Y + 1][grain.X - 1] == 0)
                {
                    grain.Y++;
                    grain.X--;
                }
                else if (grid[grain.Y + 1][grain.X + 1] == 0)
                {
                    grain.Y++;
                    grain.X++;
                }
                else
                {
                    // The grain has stopped falling, add it to the grid
                    grid[grain.Y][grain.X] = 2;

                    grain = new()
                    {
                        X = 500,
                        Y = 0,
                    };
                    answer++;
                }

                if (grain.Y == maxY)
                {
                    sandFalling = false;
                }
            }

            // Display output for fun
            output = "";

            foreach (List<int> rockLine in grid)
            {
                output += string.Join("", rockLine.Select(r => r == 0 ? " " : r == 1 ? "#" : "o"));
                output += "\n";
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 14, example);

            List<List<Point>> rockLines = lines.Select(line => line.SplitSubstring(" -> ").QuickRegex(@"(\d+),(\d+)").ToInts().Select(l => new Point()
            {
                X = l[0],
                Y = l[1]
            }).ToList()).ToList();

            int maxX = rockLines.SelectMany(r => r.Select(r => r.X)).Max();
            int maxY = rockLines.SelectMany(r => r.Select(r => r.Y)).Max();

            List<List<int>> grid = Enumerable.Repeat(0, maxY + 2).Select(x => Enumerable.Repeat(0, maxX + maxY).ToList()).ToList();

            // Add floor
            grid.Add(Enumerable.Repeat(1, maxX + maxY).ToList());

            Point? prevPoint = null;

            foreach (List<Point> rockLine in rockLines)
            {
                foreach (Point rock in rockLine)
                {
                    if (prevPoint != null)
                    {
                        if (prevPoint.X == rock.X)
                        {
                            if (rock.Y > prevPoint.Y)
                            {
                                for (int y = prevPoint.Y; y <= rock.Y; y++)
                                {
                                    grid[y][rock.X] = 1;
                                }
                            }
                            else
                            {
                                for (int y = rock.Y; y <= prevPoint.Y; y++)
                                {
                                    grid[y][rock.X] = 1;
                                }
                            }
                        }
                        else
                        {
                            if (rock.X > prevPoint.X)
                            {
                                for (int x = prevPoint.X; x <= rock.X; x++)
                                {
                                    grid[rock.Y][x] = 1;
                                }
                            }
                            else
                            {
                                for (int x = rock.X; x <= prevPoint.X; x++)
                                {
                                    grid[rock.Y][x] = 1;
                                }
                            }
                        }
                    }

                    prevPoint = rock;
                }

                prevPoint = null;
            }

            // Display output for fun
            string output = "";

            foreach (List<int> rockLine in grid)
            {
                output += string.Join("", rockLine.Select(r => r == 0 ? " " : "#"));
                output += "\n";
            }

            bool sandFalling = true;

            Point grain = new()
            {
                X = 500,
                Y = 0,
            };

            int answer = 0;

            while (sandFalling)
            {
                if (grid[grain.Y + 1][grain.X] == 0)
                {
                    grain.Y++;
                }
                else if (grid[grain.Y + 1][grain.X - 1] == 0)
                {
                    grain.Y++;
                    grain.X--;
                }
                else if (grid[grain.Y + 1][grain.X + 1] == 0)
                {
                    grain.Y++;
                    grain.X++;
                }
                else
                {
                    // The grain has stopped falling, add it to the grid
                    grid[grain.Y][grain.X] = 2;

                    if (grain.Y == 0)
                    {
                        sandFalling = false;
                    }

                    grain = new()
                    {
                        X = 500,
                        Y = 0,
                    };
                    answer++;
                }
            }

            // Display output for fun
            output = "";

            foreach (List<int> rockLine in grid)
            {
                output += string.Join("", rockLine.Select(r => r == 0 ? " " : r == 1 ? "#" : "o"));
                output += "\n";
            }

            return answer.ToString();
        }
    }
}