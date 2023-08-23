namespace AdventOfCode.Services
{
    public class Solution2019_01Service : ISolutionDayService
    {
        public Solution2019_01Service() { }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 01, example);

            int answer = lines.ToInts().Sum(l => (int)Math.Floor((double)l / 3) - 2);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 01, example);

            int answer = 0;

            foreach (int line in lines.ToInts())
            {
                int mass = line;

                do
                {
                    mass = (int)Math.Floor((double)mass / 3) - 2;

                    if (mass > 0)
                    {
                        answer += mass;
                    }
                } while (mass > 0);
            }

            return answer.ToString();
        }
    }
}