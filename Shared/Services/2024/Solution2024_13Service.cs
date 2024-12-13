namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/13.txt
    public class Solution2024_13Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
                List<string> lines = Utility.GetInputLines(2024, 13, example);
                List<List<string>> machines = lines.ChunkByExclusive(l => string.IsNullOrWhiteSpace(l));

                double answer = 0;

                foreach (List<string> machine in machines)
                {
                    List<double> aValues = Utility.QuickRegex(machine[0], @"Button A: X\+(\d+), Y\+(\d+)").Select(double.Parse).ToList();
                    List<double> bValues = Utility.QuickRegex(machine[1], @"Button B: X\+(\d+), Y\+(\d+)").Select(double.Parse).ToList();
                    List<double> goal = Utility.QuickRegex(machine[2], @"Prize: X=(\d+), Y=(\d+)").Select(double.Parse).ToList();

                    // Math, trust me bro
                    // TODO, why didn't this work?
                    // double b = (goal[0] - aValues[0] * goal[1] / aValues[1]) / (bValues[0] - aValues[0] * bValues[1] / aValues[1]);
                    double b = (aValues[1] * goal[0] - aValues[0] * goal[1]) / (aValues[1] * bValues[0] - aValues[0] * bValues[1]);
                    double a = (goal[0] - bValues[0] * b)/aValues[0];

                    // Check for valid solution
                    if (a >= 0 && b >= 0 && a % 1 == 0 && b % 1 == 0) {
                        answer += a * 3 + b;
                    }
                }

                return answer.ToString();
        }

        // 10000000000000
        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 13, example);
                List<List<string>> machines = lines.ChunkByExclusive(l => string.IsNullOrWhiteSpace(l));

                double answer = 0;

                foreach (List<string> machine in machines)
                {
                    List<double> aValues = Utility.QuickRegex(machine[0], @"Button A: X\+(\d+), Y\+(\d+)").Select(double.Parse).ToList();
                    List<double> bValues = Utility.QuickRegex(machine[1], @"Button B: X\+(\d+), Y\+(\d+)").Select(double.Parse).ToList();
                    List<double> goal = Utility.QuickRegex(machine[2], @"Prize: X=(\d+), Y=(\d+)").Select(x => double.Parse(x) + 10000000000000).ToList();

                    // Math, trust me bro
                    double b = (aValues[1] * goal[0] - aValues[0] * goal[1]) / (aValues[1] * bValues[0] - aValues[0] * bValues[1]);
                    double a = (goal[0] - bValues[0] * b)/aValues[0];

                    // Check for valid solution
                    if (a >= 0 && b >= 0 && a % 1 == 0 && b % 1 == 0) {
                        answer += a * 3 + b;
                    }
                }

                return answer.ToString();
        }
    }
}