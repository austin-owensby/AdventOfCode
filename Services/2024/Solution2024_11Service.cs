namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/11.txt
    public class Solution2024_11Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 11, example);
            List<long> stones = lines.First().Split(" ").ToLongs();

            foreach (int i in 25) {
                List<long> newStones = [];
                foreach (long stone in stones) {
                    if (stone == 0) {
                        newStones.Add(1);
                    }
                    else if (stone.ToString().Length % 2 == 0) {
                        string stoneString = stone.ToString();
                        string firstStoneString = stoneString.Substring(0, stoneString.Length / 2);
                        string secondStoneString = stoneString.Substring(stoneString.Length / 2, stoneString.Length / 2);
                        newStones.Add(long.Parse(firstStoneString));
                        newStones.Add(long.Parse(secondStoneString));
                    }
                    else {
                        newStones.Add(stone * 2024);
                    }
                }
                stones = newStones;
            }

            int answer = stones.Count;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 11, example);
            List<long> stones = lines.First().Split(" ").ToLongs();

            long answer = 0;

            foreach (long stone in stones) {
                answer += GetStoneResult(stone, 75);
            }

            return answer.ToString();
        }

        private Dictionary<(long, int), long> stoneAndStepResult = [];
    
        private long GetStoneResult(long stone, int step) {
            if (step == 0) {
                return 1;
            }

            if (stoneAndStepResult.TryGetValue((stone, step), out long result))
            {
                return result;
            }

            if (stone == 0) {
                result = GetStoneResult(1, step - 1);
            }
            else if (stone.ToString().Length % 2 == 0) {
                string stoneString = stone.ToString();
                string firstStoneString = stoneString.Substring(0, stoneString.Length / 2);
                string secondStoneString = stoneString.Substring(stoneString.Length / 2, stoneString.Length / 2);

                result = GetStoneResult(long.Parse(firstStoneString), step - 1) + GetStoneResult(long.Parse(secondStoneString), step - 1);
            }
            else {
                result = GetStoneResult(stone * 2024, step - 1);
            }

            stoneAndStepResult[(stone, step)] = result;
            return result;
        }
    }
}