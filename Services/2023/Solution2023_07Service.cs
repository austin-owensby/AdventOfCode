namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2023/07.txt
    public class Solution2023_07Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 7, example);

            Dictionary<char, int> cardValueMap = new(){
                {'A', 14},
                {'K', 13},
                {'Q', 12},
                {'J', 11},
                {'T', 10},
                {'9', 9},
                {'8', 8},
                {'7', 7},
                {'6', 6},
                {'5', 5},
                {'4', 4},
                {'3', 3},
                {'2', 2}
            };

            lines = lines.OrderBy(line => {
                string value = line.Split(' ')[0];

                // Find the number of identical cards ordered by frequency
                // Ex. KK677 returns [2, 2, 1]
                List<int> counts = value.GroupBy(c => c).Select(x => x.Count()).OrderDescending().ToList();

                // Assign a value to the type based on the card rules
                int type = 0;

                if (counts[0] == 5) {
                    type = 6;
                }
                else if (counts[0] == 4) {
                    type =  5;
                }
                else if (counts[0] == 3 && counts[1] == 2) {
                    type =  4;
                }
                else if (counts[0] == 3) {
                    type =  3;
                }
                else if (counts[0] == 2 && counts[1] == 2) {
                    type =  2;
                }
                else if (counts[0] == 2) {
                    type =  1;
                }

                string lineValue = $"{type}";

                // Add the value of each individual card, pad by 2 so that the sorting works as expected
                foreach (int i in value.Length) {
                    lineValue += $"{cardValueMap[value[i]]:D2}";
                }

                return lineValue;
            }).ToList();

            int answer = 0;

            for (int i = 0; i < lines.Count; i++) {
                int bid = int.Parse(lines[i].Split(' ')[1]);
                int rank = i + 1;
                answer += bid * rank;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 7, example);

            Dictionary<char, int> cardValueMap = new(){
                {'A', 14},
                {'K', 13},
                {'Q', 12},
                {'T', 10},
                {'9', 9},
                {'8', 8},
                {'7', 7},
                {'6', 6},
                {'5', 5},
                {'4', 4},
                {'3', 3},
                {'2', 2},
                {'J', 1},
            };

            lines = lines.OrderBy(line => {
                string value = line.Split(' ')[0];

                // Replace jokers with the most frequent card
                string valueCopy = value;

                // Ignore the case of all jokers
                if (value != "JJJJJ" && value.Contains('J')) {
                    char bestCard = value.Where(c => c != 'J').GroupBy(c => c).OrderByDescending(x => x.Count()).First().Key;
                    valueCopy = value.Replace('J', bestCard);
                }

                // Find the number of identical cards ordered by frequency
                // Ex. KK677 returns [2, 2, 1]
                List<int> counts = valueCopy.GroupBy(c => c).Select(x => x.Count()).OrderDescending().ToList();
                
                // Assign a value to the type based on the card rules
                int type = 0;

                if (counts[0] == 5) {
                    type = 6;
                }
                else if (counts[0] == 4) {
                    type =  5;
                }
                else if (counts[0] == 3 && counts[1] == 2) {
                    type =  4;
                }
                else if (counts[0] == 3) {
                    type =  3;
                }
                else if (counts[0] == 2 && counts[1] == 2) {
                    type =  2;
                }
                else if (counts[0] == 2) {
                    type =  1;
                }

                string lineValue = $"{type}";
                
                // Add the value of each individual card, pad by 2 so that the sorting works as expected
                foreach (int i in value.Length) {
                    lineValue += $"{cardValueMap[value[i]]:D2}";
                }

                return lineValue;
            }).ToList();

            int answer = 0;

            for (int i = 0; i < lines.Count; i++) {
                int bid = int.Parse(lines[i].Split(' ')[1]);
                int rank = i + 1;
                answer += bid * rank;
            }

            return answer.ToString();
        }
    }
}