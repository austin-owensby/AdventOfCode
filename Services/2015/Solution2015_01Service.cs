namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2015/01.txt
    public class Solution2015_01Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "01.txt"));

            int floor = 0;

            foreach (char character in data)
            {
                if (character == '(')
                {
                    floor++;
                }
                else if (character == ')')
                {
                    floor--;
                }
            }

            return floor.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "01.txt"));

            int floor = 0;
            int position = 1;

            foreach (char character in data)
            {
                if (character == '(')
                {
                    floor++;
                }
                else if (character == ')')
                {
                    floor--;
                }

                if (floor == -1)
                {
                    break;
                }

                position++;
            }

            return position.ToString();
        }
    }
}