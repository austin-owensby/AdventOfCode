namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/03.txt
    public class Solution2025_03Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 3, example);
            List<List<int>> grid = lines.ToIntGrid();

            int answer = 0;

            foreach (List<int> bank in grid)
            {
                int a = bank[0];
                int b = bank[1];

                for (int i = 2; i < bank.Count; i++)
                {
                    int c = bank[i];
                    if (b > a)
                    {
                        a = b;
                        b = c;
                    }
                    else if(c > b)
                    {
                        b = c;
                    }
                }

                int joltage = a * 10 + b;
                answer += joltage;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 3, example);
            List<List<int>> grid = lines.ToIntGrid();

            long answer = 0;

            foreach (List<int> bank in grid)
            {
                List<int> joltageList = bank.Take(12).ToList();

                for (int i = joltageList.Count; i < bank.Count; i++)
                {
                    joltageList.Add(bank[i]);

                    for (int j = 0; j < joltageList.Count; j++)
                    {
                        if (j == joltageList.Count - 1 || joltageList[j] < joltageList[j + 1])
                        {
                            joltageList.RemoveAt(j);
                            break;
                        }
                    }
                }

                long joltage = long.Parse(string.Join("", joltageList));
                answer += joltage;
            }

            return answer.ToString();
        }
    }
}