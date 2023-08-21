namespace AdventOfCode.Services
{
    public class Solution2019_01Service : ISolutionDayService
    {
        public Solution2019_01Service() { }

        public string FirstHalf()
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2019_01.txt")).ToList();

            int answer = lines.ToInts().Sum(l => (int)Math.Floor((double)l / 3) - 2);

            return answer.ToString();
        }

        public string SecondHalf()
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2019_01.txt")).ToList();

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