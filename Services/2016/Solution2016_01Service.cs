using System.Security.Cryptography;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2016/01.txt
    public class Solution2016_01Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2016, 01, example);
            List<Tuple<char, int>> instructions = lines.First().Split(", ").QuickRegex(@"(\w)(\d+)").Select(x => new Tuple<char, int>(x[0].ToCharArray()[0], int.Parse(x[1]))).ToList();

            int x = 0;
            int y = 0;
            int direction = 0; // 0 = North, 1 = East, 2 = South, 3 = West

            foreach (Tuple<char, int> instruction in instructions)
            {
                char rotation = instruction.Item1;
                int distance = instruction.Item2;

                if (rotation == 'R') {
                    direction = Utility.Mod(direction + 1, 4);
                }
                else {
                    direction = Utility.Mod(direction - 1, 4);
                }

                switch (direction) {
                    case 0:
                        y += distance;
                        break;
                    case 1:
                        x += distance;
                        break;
                    case 2:
                        y -= distance;
                        break;
                    case 3:
                        x -= distance;
                        break;
                }
            }

            int answer = Math.Abs(x) + Math.Abs(y);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2016, 01, example);
            List<Tuple<char, int>> instructions = lines.First().Split(", ").QuickRegex(@"(\w)(\d+)").Select(x => new Tuple<char, int>(x[0].ToCharArray()[0], int.Parse(x[1]))).ToList();

            int x = 0;
            int y = 0;
            int direction = 0; // 0 = North, 1 = East, 2 = South, 3 = West
            List<Point> history = [];
            int answer = 0;            

            foreach (Tuple<char, int> instruction in instructions)
            {
                char rotation = instruction.Item1;
                int distance = instruction.Item2;

                if (rotation == 'R') {
                    direction = Utility.Mod(direction + 1, 4);
                }
                else {
                    direction = Utility.Mod(direction - 1, 4);
                }

                for (int i = 1; i <= distance; i++) {
                    switch (direction) {
                        case 0:
                            y++;
                            break;
                        case 1:
                            x++;
                            break;
                        case 2:
                            y--;
                            break;
                        case 3:
                            x--;
                            break;
                    }

                    if (history.Any(h => h.X == x && h.Y == y)) {
                        answer = Math.Abs(x) + Math.Abs(y);
                        break;
                    }
                    else {
                        history.Add(new(x, y));
                    }
                }

                if (answer != 0) {
                    break;
                }
            }

            return answer.ToString();
        }
    }
}