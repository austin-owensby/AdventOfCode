namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/14.txt
    public class Solution2024_14Service : ISolutionDayService
    {
        private class Robot {
            public int px { get; set;}
            public int py { get; set;}
            public int vx { get; set;}
            public int vy { get; set;}
        }
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 14, example);
            List<Robot> robots = lines.QuickRegex(@"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)").Select(l => l.ToInts()).Select(l => new Robot(){
                px = l[0],
                py = l[1],
                vx = l[2],
                vy = l[3]
            }).ToList();

            int answer = 0;

            int width = example ? 11 : 101;
            int height = example ? 7 : 103;
            int iterations = 100;

            foreach (Robot robot in robots)
            {
                robot.px = Utility.Mod(robot.px + iterations * robot.vx, width);
                robot.py = Utility.Mod(robot.py + iterations * robot.vy, height);
            }

            int middleX = (width - 1) / 2;
            int middleY = (height - 1) / 2;

            int quad1 = robots.Count(r => r.px < middleX && r.py < middleY);
            int quad2 = robots.Count(r => r.px > middleX && r.py < middleY);
            int quad3 = robots.Count(r => r.px < middleX && r.py > middleY);
            int quad4 = robots.Count(r => r.px > middleX && r.py > middleY);
            answer = quad1 * quad2 * quad3 * quad4;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 14, example);

            List<Robot> robots = lines.QuickRegex(@"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)").Select(l => l.ToInts()).Select(l => new Robot(){
                px = l[0],
                py = l[1],
                vx = l[2],
                vy = l[3]
            }).ToList();

            List<Robot> start = robots.Select(r => new Robot(){
                px = r.px,
                py = r.py
            }).ToList();

            int answer = 0;

            int width = example ? 11 : 101;
            int height = example ? 7 : 103;
            bool isChristmasTree = false;

            int iterations = 0;
            while (!isChristmasTree) {
                iterations++;
                Console.Clear();
                Console.WriteLine(iterations);
                foreach (Robot robot in robots)
                {
                    robot.px = Utility.Mod(robot.px + robot.vx, width);
                    robot.py = Utility.Mod(robot.py + robot.vy, height);
                }

                bool tryTree = false;
                double density = 0;
                int window = 5;
                double threshold = 0.75;
                foreach (int y in height / window) {
                    foreach (int x in width / window) {
                        int count = robots.Count(r => r.px >= x*window && r.px <= (x + 1)*window && r.py >= y*window && r.py <= (y + 1)*window);
                        density = count / (double)(window * window);
                        if (density >= threshold) {
                            tryTree = true;
                            break;
                        }
                    }
                    if (tryTree) {
                        break;
                    }
                }

                if (tryTree) {
                    foreach (int y in height) {
                        string line = "";
                        foreach (int x in width) {
                            line += robots.Any(r => r.px == x && r.py == y) ? "#" : " ";
                        }
                        Console.WriteLine(line);
                    }

                    Console.WriteLine($"{iterations} {density} Is Christmas Tree? (Y/N):");
                    string? input = Console.ReadLine();
                    isChristmasTree = input!.ToLower() == "y";
                }
            }

            answer = iterations;

            return answer.ToString();
        }
    }
}