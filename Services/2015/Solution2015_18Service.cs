namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2015/18.txt
    public class Solution2015_18Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "18.txt"));
            string[] lights = data.Split("\n").SkipLast(1).ToArray();
            string[] nextState = new string[100];
            int numberOfStages = 100;

            foreach (int i in numberOfStages)
            {
                foreach (int j in lights.Length)
                {
                    foreach (int k in lights[j].Length)
                    {
                        int liveNeighbors = GetNumberOfLiveNeighbors(j, k, lights);
                        if (lights[j][k] == '#')
                        {
                            if (liveNeighbors is 2 or 3)
                            {
                                nextState[j] += '#';
                            }
                            else
                            {
                                nextState[j] += '.';
                            }
                        }
                        else
                        {
                            if (liveNeighbors == 3)
                            {
                                nextState[j] += '#';
                            }
                            else
                            {
                                nextState[j] += '.';
                            }
                        }
                    }
                }

                lights = nextState;
                nextState = new string[100];
            }

            int totalLights = 0;

            foreach (string line in lights)
            {
                foreach (char character in line)
                {
                    if (character == '#')
                    {
                        totalLights++;
                    }
                }
            }

            return totalLights.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "18.txt"));
            string[] lights = data.Split("\n").SkipLast(1).ToArray();
            string[] nextState = new string[100];
            int numberOfStages = 100;

            // Set corners to '#' initially
            char[] firstRow = lights[0].ToCharArray();
            firstRow[0] = '#';
            firstRow[^1] = '#';
            lights[0] = new string(firstRow);

            char[] lastRow = lights[^1].ToCharArray();
            lastRow[0] = '#';
            lastRow[firstRow.Length - 1] = '#';
            lights[^1] = new string(lastRow);

            foreach (int i in numberOfStages)
            {
                foreach (int j in lights.Length)
                {
                    foreach (int k in lights[j].Length)
                    {
                        int liveNeighbors = GetNumberOfLiveNeighbors(j, k, lights);

                        if ((j == 0 || j == lights.Length - 1) && (k == 0 || k == lights[j].Length - 1))
                        {
                            nextState[j] += '#';
                        }
                        else if (lights[j][k] == '#')
                        {
                            if (liveNeighbors is 2 or 3)
                            {
                                nextState[j] += '#';
                            }
                            else
                            {
                                nextState[j] += '.';
                            }
                        }
                        else
                        {
                            if (liveNeighbors == 3)
                            {
                                nextState[j] += '#';
                            }
                            else
                            {
                                nextState[j] += '.';
                            }
                        }
                    }
                }

                lights = nextState;
                nextState = new string[100];
            }

            int totalLights = 0;

            foreach (string line in lights)
            {
                foreach (char character in line)
                {
                    if (character == '#')
                    {
                        totalLights++;
                    }
                }
            }

            return totalLights.ToString();
        }

        private int GetNumberOfLiveNeighbors(int j, int k, string[] board)
        {
            // Check each of the possible neighbors without going out of the limits of the board
            int neighbors = 0;

            if (j != 0)
            {
                if (k != 0)
                {
                    if (board[j - 1][k - 1] == '#')
                    {
                        neighbors++;
                    }
                }

                if (k != board[j - 1].Length - 1)
                {
                    if (board[j - 1][k + 1] == '#')
                    {
                        neighbors++;
                    }
                }

                if (board[j - 1][k] == '#')
                {
                    neighbors++;
                }
            }

            if (k != 0)
            {
                if (board[j][k - 1] == '#')
                {
                    neighbors++;
                }
            }

            if (j != board.Length - 1)
            {
                if (k != board[j + 1].Length - 1)
                {
                    if (board[j + 1][k + 1] == '#')
                    {
                        neighbors++;
                    }
                }

                if (k != 0)
                {
                    if (board[j + 1][k - 1] == '#')
                    {
                        neighbors++;
                    }
                }

                if (board[j + 1][k] == '#')
                {
                    neighbors++;
                }
            }

            if (k != board[j].Length - 1)
            {
                if (board[j][k + 1] == '#')
                {
                    neighbors++;
                }
            }

            return neighbors;
        }
    }
}
