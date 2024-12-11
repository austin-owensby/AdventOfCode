namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/09.txt
    public class Solution2024_09Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 9, example);
            List<int> instructions = lines.First().ToList().Select(c => c.ToInt()).ToList();

            long answer = 0;

            List<int> data = [];

            foreach (int i in instructions.Count)
            {
                if (i % 2 == 0) {
                    // File space
                    data.AddRange(Enumerable.Repeat(i / 2, instructions[i]));
                }
                else {
                    // Free space
                    data.AddRange(Enumerable.Repeat(-1, instructions[i]));
                }
            }

            foreach (int i in data.Count) {
                if (data[i] == -1) {
                    int lastGoodIndex = data.FindLastIndex(x => x != -1);
                    if (lastGoodIndex <= i) {
                        // Data is compact
                        break;
                    }
                    data[i] = data[lastGoodIndex];
                    data[lastGoodIndex] = -1;
                }

                answer += data[i] * (long)i;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 9, example);
            List<int> instructions = lines.First().ToList().Select(c => c.ToInt()).ToList();

            long answer = 0;

            List<int> data = [];

            foreach (int i in instructions.Count)
            {
                if (i % 2 == 0) {
                    // File space
                    data.AddRange(Enumerable.Repeat(i / 2, instructions[i]));
                }
                else {
                    // Free space
                    data.AddRange(Enumerable.Repeat(-1, instructions[i]));
                }
            }

            for(int id = instructions.Count / 2; id >= 0; id--) {
                int length = instructions[id * 2];

                int lengthSoFar = 0;
                int index = 0;

                List<int> indexes = data.FindIndexes(x => x == id);

                foreach (int i in indexes.First()) {
                    if (data[i] == -1) {
                        lengthSoFar++;

                        if (lengthSoFar == 1) {
                            index = i;
                        }

                        if (length == lengthSoFar) {
                            foreach (int j in length) {
                                data[index + j] = id;
                            }
                            foreach (int replaceIndex in indexes) {
                                data[replaceIndex] = -1;
                            }
                            break;
                        }
                    }
                    else {
                        lengthSoFar = 0;
                    }
                }
            }

            foreach (int i in data.Count) {
                if (data[i] == -1) {
                    continue;
                }

                answer += data[i] * (long)i;
            }

            return answer.ToString();
        }
    }
}