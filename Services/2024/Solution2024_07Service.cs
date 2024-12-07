namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/07.txt
    public class Solution2024_07Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 7, example);
            List<(long, List<long>)> equations = lines.Select(l => l.Split(": ")).Select(p => (long.Parse(p[0]), p[1].Split(" ").ToLongs())).ToList();

            long answer = 0;

            foreach ((long, List<long>) equation in equations)
            {
                // TODO, is there a better way to do this?
                int flags = (int)Math.Pow(2, equation.Item2.Count) - 1;
                while (flags > 0) {
                    string flagsString = Convert.ToString(flags, 2).PadLeft(equation.Item2.Count, '0');

                    long result = 0;

                    foreach (int i in equation.Item2.Count)
                    {
                        if (i == 0) {
                            result += equation.Item2[i];
                        }
                        else if (flagsString[i] == '0') {
                            result += equation.Item2[i];
                        }
                        else {
                            result *= equation.Item2[i];
                        }

                        if (result > equation.Item1) {
                            break;
                        }
                    }

                    if (result == equation.Item1) {
                        answer += result;
                        flags = 0;
                    }

                    flags--;
                }
            }
            
            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 7, example);
            List<(long, List<long>)> equations = lines.Select(l => l.Split(": ")).Select(p => (long.Parse(p[0]), p[1].Split(" ").ToLongs())).ToList();

            long answer = 0;

            foreach ((long, List<long>) equation in equations)
            {
                int flags = (int)Math.Pow(3, equation.Item2.Count) - 1;
                while (flags > 0) {
                    // Can't convert to base 3 like we can base 2
                    string flagsString = "";
                    int remainder = flags;
                    foreach (int i in equation.Item2.Count) {
                        int magnitude = (int)Math.Pow(3, equation.Item2.Count - 1 - i);

                        if (remainder >= magnitude * 2) {
                            flagsString += "2";
                            remainder -= magnitude * 2;
                        }
                        else if (remainder >= magnitude) {
                            flagsString += "1";
                            remainder -= magnitude;
                        }
                        else {
                            flagsString += "0";
                        }
                    }

                    long result = 0;

                    foreach (int i in equation.Item2.Count)
                    {
                        if (i == 0) {
                            result += equation.Item2[i];
                        }
                        else if (flagsString[i] == '0') {
                            result += equation.Item2[i];
                        }
                        else if (flagsString[i] == '1')
                        {
                            result *= equation.Item2[i];
                        }
                        else {
                            result = long.Parse($"{result}{equation.Item2[i]}");
                        }

                        if (result > equation.Item1) {
                            break;
                        }
                    }

                    if (result == equation.Item1) {
                        answer += result;
                        flags = 0;
                    }

                    flags--;
                }
            }

            return answer.ToString();
        }
    }
}