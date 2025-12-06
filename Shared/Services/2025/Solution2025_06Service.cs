namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/06.txt
    public class Solution2025_06Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 6, example);

            long answer = 0;

            List<List<string>> equations = [];

            foreach (string line in lines)
            {
                equations.Add(line.ToList().ChunkByExclusive(c => c == ' ').Select(c => new string(c.ToArray())).ToList());
            }

            List<string> signs = equations.Last();
            List<List<long>> equationValues = equations.Take(equations.Count - 1).Select(s => s.Select(t => long.Parse(t)).ToList()).ToList();
            equationValues = equationValues.Pivot();

            foreach (int i in equationValues.Count)
            {
                string sign = signs[i];
                long result = equationValues[i][0];

                if (sign == "+")
                {
                    for (int j = 1; j < equationValues[i].Count; j++)
                    {
                        long value = equationValues[i][j];
                        result += value;
                    }
                }
                else
                {
                    for (int j = 1; j < equationValues[i].Count; j++)
                    {
                        long value = equationValues[i][j];
                        result *= value;
                    }
                }

                answer += result;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 6, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }
    }
}