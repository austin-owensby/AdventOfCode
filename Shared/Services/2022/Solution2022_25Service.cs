namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2022/25.txt
    public class Solution2022_25Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 25, example);

            long answerDecimal = 0;

            foreach (string line in lines)
            {
                foreach (int i in line.Length) {
                    long value = 0;

                    switch (line[i]) {
                        case '2':
                            value = 2;
                            break;
                        case '1':
                            value = 1;
                            break;
                        case '0':
                            value = 0;
                            break;
                        case '-':
                            value = -1;
                            break;
                        case '=':
                            value = -2;
                            break;
                        default:
                            // Shouldn't happen
                            break;
                    }
                    
                    answerDecimal += value * (long)Math.Pow(5, line.Length - 1 - i);
                }
            }

            string answer = string.Empty;

            // Convert number to SNAFU
            long answerSoFar = 0;
            foreach (int i in Math.Ceiling(Math.Log(answerDecimal, 5))) {
                long value = ((answerDecimal - answerSoFar) / (long)Math.Pow(5, i)) % 5;

                switch (value) {
                    case 0:
                        answer += '0';
                        break;
                    case 1:
                        answer += '1';
                        break;
                    case 2:
                        answer += '2';
                        break;
                    case 3:
                        answer += '=';
                        value = -2;
                        break;
                    case 4:
                        answer += '-';
                        value = -1;
                        break;
                    default:
                        // Shouldn't happen
                        break;
                }

                answerSoFar += value * (long)Math.Pow(5,i);
            }

            answer = new string(answer.Reverse().ToArray());

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            return "There is no problem for Day 25 part 2, solve all other problems to get the last star.";
        }
    }
}