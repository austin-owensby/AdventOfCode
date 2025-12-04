namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/04.txt
    public class Solution2025_04Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 4, example);
            List<List<char>> grid = lines.ToGrid();

            int answer = 0;

            foreach (int y in grid.Count)
            {
                foreach (int x in grid[y].Count)
                {
                    if (grid[y][x] == '@')
                    {
                        List<Point> neighbors = grid.GetNeighbors(x, y, true);
                        int fullNeighbors = neighbors.Count(p => grid[p.Y][p.X] == '@');

                        if (fullNeighbors < 4)
                        {
                            answer++;
                        }
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 4, example);
            List<List<char>> grid = lines.ToGrid();

            int answer = 0;

            bool removed = true;

            while (removed)
            {
                removed = false;

                foreach (int y in grid.Count)
                {
                    foreach (int x in grid[y].Count)
                    {
                        if (grid[y][x] == '@')
                        {
                            List<Point> neighbors = grid.GetNeighbors(x, y, true);
                            int fullNeighbors = neighbors.Count(p => grid[p.Y][p.X] == '@');

                            if (fullNeighbors < 4)
                            {
                                answer++;
                                removed = true;
                                grid[y][x] = '.';
                            }
                        }
                    }
                }
                
            }

            return answer.ToString();
        }
    }
}