namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/25.txt
    public class Solution2024_25Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 25, example);
            List<List<string>> schematics = lines.ChunkByExclusive((l) => string.IsNullOrEmpty(l)).ToList();
            List<List<int>> keys = schematics.Where(s => s[0] == ".....").Select(s => s.Pivot().Select(x => x.Count(y => y == '#')).ToList()).ToList();
            List<List<int>> locks = schematics.Where(s => s[0] == "#####").Select(s => s.Pivot().Select(x => x.Count(y => y == '#')).ToList()).ToList();

            int height = schematics.First().Count;

            int answer = 0;

            foreach (List<int> key in keys) {
                foreach (List<int> lockItem in locks) {
                    bool fits = true;
                    foreach (int pin in key.Count) {
                        if (key[pin] + lockItem[pin] > height) {
                            fits = false;
                            break;
                        }
                    }

                    if (fits) {
                        answer++;
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            return "There is no problem for Day 25 part 2, solve all other problems to get the last star.";
        }
    }
}