namespace AdventOfCode.Services
{
    public class Solution2023_12Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 12, example);

            int answer = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                List<int> values = parts.Last().Split(',').ToInts();
                List<char> springs = parts.First().ToList();

                // Consider each combination
                int mysterySpringCount = springs.Count(s => s == '?');
                int combinations = (int)Math.Pow(2, mysterySpringCount);
                foreach (int i in combinations) {
                    // Generate an the option based on the combination
                    List<bool> combination = Convert.ToString(i, 2).PadLeft(mysterySpringCount, '0').ToList().Select(c => c == '1').ToList();
                    
                    int unknownSpringIndex = 0;
                    List<char> option = [];
                    foreach (char spring in springs) {
                        if (spring == '?') {
                            option.Add(combination[unknownSpringIndex] ? '#' : '.');
                            unknownSpringIndex++;
                        }
                        else {
                            option.Add(spring);
                        }
                    }

                    // Check if this is a valid option
                    List<int> groups = option.ChunkByExclusive(c => c == '.').Select(x => x.Count).ToList();
                    bool validOption = true;
                    if (groups.Count == values.Count) {
                        foreach (int j in groups.Count) {
                            if (groups[j] != values[j]) {
                                validOption = false;
                                break;
                            }
                        }
                    }
                    else {
                        validOption = false;
                    }

                    if (validOption) {
                        answer++;
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 12, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }
    }
}