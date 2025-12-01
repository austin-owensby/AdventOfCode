namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/01.txt
    public class Solution2025_01Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 1, example);

            int answer = 0;

            int dial = 50;

            foreach (string line in lines)
            {
                int change = int.Parse(line.Replace("R", "").Replace("L", "-"));

                dial = Utility.Mod(dial + change, 100);

                if (dial == 0)
                {
                    answer++;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 1, example);

            int answer = 0;

            int dial = 50;

            foreach (string line in lines)
            {
                int change = int.Parse(line.Replace("R", "").Replace("L", "-"));
                int step = change < 0 ? -1 : 1;

                for (int i = 0; i < Math.Abs(change); i++)
                {
                    dial += step * 1;
                    dial = Utility.Mod(dial, 100);

                    if (dial == 0)
                    {
                        answer++;
                    }
                }

                Console.WriteLine($"{dial} {step} {change} {answer}");

            }

            // 7276

            return answer.ToString();
        }
    }
}