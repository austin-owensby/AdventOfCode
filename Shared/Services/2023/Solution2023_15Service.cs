namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2023/15.txt
    public class Solution2023_15Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 15, example);
            List<List<char>> steps = lines.First().Split(',').Select(s => s.ToList()).ToList();

            int answer = 0;

            foreach (List<char> step in steps)
            {
                int currentValue = 0;
                foreach (char value in step) {
                    currentValue += value;
                    currentValue *= 17;
                    currentValue %= 256;
                }
                answer += currentValue;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 15, example);
            List<List<char>> steps = lines.First().Split(',').Select(s => s.ToList()).ToList();

            long answer = 0;

            List<List<(string, int)>> maps = [];

            foreach (int i in 256) {
                maps.Add([]);
            }

            foreach (List<char> step in steps)
            {
                List<char> label = step.TakeWhile(c => c != '-' && c != '=').ToList();
                int box = 0;
                foreach (char value in label) {
                    box += value;
                    box *= 17;
                    box %= 256;
                }

                string labelValue = new(label.ToArray());

                if (char.IsDigit(step.Last())) {
                    // Add the new lense
                    int focalLength = $"{step.Last()}"[0].ToInt();
                    int index = maps[box].FindIndex(x => x.Item1 == labelValue);
                    if (index != -1) {
                        maps[box][index] = (labelValue, focalLength);
                    }
                    else {
                        maps[box].Add((labelValue, focalLength));
                    }
                }
                else {
                    // Remove the lense
                    (string, int)? value = maps[box].FirstOrDefault(x => x.Item1 == labelValue);
                    if (value != null) {
                        maps[box].Remove(value.Value);
                    }
                }
            }

            foreach (int i in maps.Count) {
                List<(string, int)> map = maps[i];

                foreach (int j in map.Count) {
                    answer += (i + 1) * (j + 1) * map[j].Item2;
                }
            }

            return answer.ToString();
        }
    }
}