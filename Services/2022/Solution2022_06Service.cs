namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2022/06.txt
    public class Solution2022_06Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 06, example);

            int answer = 0;

            List<char> lastFour = new();

            foreach (char line in lines.First())
            {
                if (lastFour.Count() == 4)
                {
                    lastFour.RemoveAt(0);
                }

                lastFour.Add(line);

                answer++;

                if (lastFour.Distinct().Count() == 4 && lastFour.Count() == 4)
                {
                    break;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 06, example);


            int answer = 0;

            List<char> lastFourteen = new();

            foreach (char line in lines.First())
            {
                if (lastFourteen.Count() == 14)
                {
                    lastFourteen.RemoveAt(0);
                }

                lastFourteen.Add(line);

                answer++;

                if (lastFourteen.Distinct().Count() == 14 && lastFourteen.Count() == 14)
                {
                    break;
                }
            }
            return answer.ToString();
        }
    }
}