namespace AdventOfCode.Services
{
    public class Solution2017_03Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {            
            List<string> lines = Utility.GetInputLines(2017, 03, example);
            int value = int.Parse(lines.First());

            int answer = 0;
            
            // Use this formula to calculate which layer of the square we're on
            int layer = 2 * (int)Math.Round(Math.Sqrt(value - 1) / 2.0) - 1;

            foreach (int i in 7) {

            }

            /*
            int index = (value - sequenceValue * sequenceValue - 1) % (sequenceValue + 1);

            if (index == sequenceValue) {
                answer = sequenceValue + 1; 
            }
            else if (index < sequenceValue / 2.0) {
                answer = sequenceValue - index;
            }
            else {
                answer = (sequenceValue + 1) / 2 + index - sequenceValue + 1;
            }
            */

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2017, 03, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }
    }
}