namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2016/02.txt
    public class Solution2016_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2016, 02, example);

            string answer = string.Empty;

            int x = 1;
            int y = 1;

            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    switch (c) {
                        case 'U':
                            y = y == 0 ? 0 : y - 1;
                            break;
                        case 'D':
                            y = y == 2 ? 2 : y + 1;
                            break;
                        case 'L':
                            x = x == 0 ? 0 : x - 1;
                            break;
                        case 'R':
                            x = x == 2 ? 2 : x + 1;
                            break;
                    }
                }

                answer += 1 + x + 3 * y;
            }

            return answer;
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2016, 02, example);

            string answer = string.Empty;

            List<string> grid = ["", "", "1", "", "", "", "2", "3", "4", "", "5", "6", "7", "8", "9", "", "X", "B", "C", "", "", "", "D", "", ""];

            int x = -2;
            int y = 0;

            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    bool onEdge = Math.Abs(y) + Math.Abs(x) == 2;
                    switch (c) {
                        case 'U':
                            if (!(y <= 0 && onEdge)) {
                                y--;
                            }
                            break;
                        case 'D':
                            if (!(y >= 0 && onEdge)) {
                                y++;
                            }
                            break;
                        case 'L':
                            if (!(x <= 0 && onEdge)) {
                                x--;
                            }
                            break;
                        case 'R':
                            if (!(x >= 0 && onEdge)) {
                                x++;
                            }
                            break;
                    }
                }

                answer += grid[x + 2 + 5 * (y + 2)];
            }

            return answer;
        }
    }
}