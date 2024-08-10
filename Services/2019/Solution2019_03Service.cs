namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2019/03.txt
    public class Solution2019_03Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 3, example);
            List<(char, int)> directions1 = lines.First().Split(',').Select(x => (x[0], int.Parse(x.Skip(1).ToArray()))).ToList();
            List<(char, int)> directions2 = lines.Last().Split(',').Select(x => (x[0], int.Parse(x.Skip(1).ToArray()))).ToList();

            List<Point> path1 = [];

            int x = 0;
            int y = 0;

            foreach ((char, int) direction in directions1) {
                foreach (int i in direction.Item2) {
                    switch (direction.Item1) {
                        case 'U':
                            y--;
                            break;
                        case 'D':
                            y++;
                            break;
                        case 'L':
                            x--;
                            break;
                        case 'R':
                            x++;
                            break;
                    }
                    path1.Add(new(x, y));
                }
            }

            List<Point> path2 = [];
            x = 0;
            y = 0;

            foreach ((char, int) direction in directions2) {
                foreach (int i in direction.Item2) {
                    switch (direction.Item1) {
                        case 'U':
                            y--;
                            break;
                        case 'D':
                            y++;
                            break;
                        case 'L':
                            x--;
                            break;
                        case 'R':
                            x++;
                            break;
                    }

                    path2.Add(new(x, y));
                }
            }

            int answer = path1.IntersectBy(path2.Select(p => $"{p.X} {p.Y}"), p => $"{p.X} {p.Y}").Select(p => p.ManhattenDistance()).Min();

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 3, example);
            List<(char, int)> directions1 = lines.First().Split(',').Select(x => (x[0], int.Parse(x.Skip(1).ToArray()))).ToList();
            List<(char, int)> directions2 = lines.Last().Split(',').Select(x => (x[0], int.Parse(x.Skip(1).ToArray()))).ToList();

            List<Point> path1 = [];

            int x = 0;
            int y = 0;

            foreach ((char, int) direction in directions1) {
                foreach (int i in direction.Item2) {
                    switch (direction.Item1) {
                        case 'U':
                            y--;
                            break;
                        case 'D':
                            y++;
                            break;
                        case 'L':
                            x--;
                            break;
                        case 'R':
                            x++;
                            break;
                    }
                    path1.Add(new(x, y));
                }
            }

            List<Point> path2 = [];
            x = 0;
            y = 0;

            foreach ((char, int) direction in directions2) {
                foreach (int i in direction.Item2) {
                    switch (direction.Item1) {
                        case 'U':
                            y--;
                            break;
                        case 'D':
                            y++;
                            break;
                        case 'L':
                            x--;
                            break;
                        case 'R':
                            x++;
                            break;
                    }

                    path2.Add(new(x, y));
                }
            }

            int answer = path1.IntersectBy(path2.Select(p => $"{p.X} {p.Y}"), p => $"{p.X} {p.Y}").Select(p => path1.FindIndex(point => p.X == point.X && p.Y == point.Y) + path2.FindIndex(point => p.X == point.X && p.Y == point.Y)).Min() + 2;

            return answer.ToString();
        }
    }
}