namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2023/12.txt
    public class Solution2023_12Service : ISolutionDayService
    {
        public Dictionary<string, ulong> mappings = [];
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 12, example);

            ulong answer = 0;

            foreach (string line in lines) {
                string[] parts = line.Split(' ');
                List<int> values = parts.Last().Split(',').ToInts();
                string springs = parts.First();

                answer += GetPossibleCombinations(springs, values);
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {            
            List<string> lines = Utility.GetInputLines(2023, 12, example);

            ulong answer = 0;

            foreach (string line in lines)
            {
                // Setup new input based on unfolding rules
                string[] parts = line.Split(' ');
                List<int> valueSection = parts.Last().Split(',').ToInts();
                List<int> values = [];
                string springSection = parts.First();
                string springs = "";

                foreach (int i in 5) {
                    values.AddRange(valueSection);
                    springs += springSection;

                    if (i != 4) {
                        springs += '?';
                    }
                }

                answer += GetPossibleCombinations(springs, values);
            }

            // Too low 29408289237

            return answer.ToString();
        }
    
        private ulong GetPossibleCombinations(string input, List<int> values) {
            // Try replacing next '?' with either a # or .
            // If a valid solution is still possible, continue
            int index = input.IndexOf('?');
            string testInput = input.Remove(index, 1);
            
            string testInput1 = testInput.Insert(index, "#");
            string testInput2 = testInput.Insert(index, ".");
            ulong combinations1 = GetPossibleCombinationsHalf(testInput1, values);
            ulong combinations2 = GetPossibleCombinationsHalf(testInput2, values);
            return combinations1 + combinations2;
        }

        private ulong GetPossibleCombinationsHalf(string testInput, List<int> values) {
            string testInputKnown = new (testInput.TakeWhile(c => c != '?').ToArray());

            List<int> groups = testInputKnown.ChunkByExclusive(c => c == '.').Select(x => x.Count).ToList();

            bool stillUnknowns = testInput.Contains('?');

            // Check if this is valid by comparing our groups so far to the target
            if (groups.Count <= values.Count && stillUnknowns) {
                // Only valid if we have as much or less groups than our target
                bool groupIsValid = true;
                foreach (int i in groups.Count) {
                    // Check on each step that our groups are equal
                    if (groups[i] != values[i]) {
                        // The last group could be in progress and should not be considered invalid
                        bool lastGroup = i == groups.Count - 1;
                        bool inProgress = testInputKnown.EndsWith('#') && groups[i] < values[i];
                        if (lastGroup && inProgress) {
                            continue;
                        }

                        groupIsValid = false;
                        break;
                    }
                }

                if (groupIsValid) {
                    if (testInput.Contains('?')) {
                        // There's still more to go, keep checking
                        // Decrease the size of the problem by removing any groups we already have solved
                        string newTestInput = testInput;
                        List<int> newTestValues = values.ToList();
                        foreach (int i in groups.Count) {
                            if (groups[i] == values[i]) {
                                string groupValue = new (Enumerable.Repeat('#', groups[i]).ToArray());
                                int index = newTestInput.IndexOf(groupValue);
                                string potentialNewTestInput = newTestInput.Substring(index + groups[i]);

                                // Check if this was a valid break, stop looping
                                if (potentialNewTestInput.StartsWith('?')) {
                                    break;
                                }

                                newTestInput = potentialNewTestInput;
                                newTestValues.RemoveAt(0);
                            }
                        }
                        string key = newTestInput + string.Join(",", newTestValues);

                        // Memoization
                        if (!mappings.TryGetValue(key, out ulong combinations))
                        {
                            combinations = GetPossibleCombinations(newTestInput, newTestValues);
                            mappings[key] = combinations;
                        }

                        return combinations;
                    }
                    else {
                        // We're at the end with a valid solution
                        return 1;
                    }
                }
            }
            else if (groups.Count == values.Count && !stillUnknowns) {
                // Check for an exact match on the group
                bool groupIsValid = true;
                foreach (int i in groups.Count) {
                    // Check on each step that our groups are equal
                    if (groups[i] != values[i]) {
                        groupIsValid = false;
                        break;
                    }
                }

                if (groupIsValid) {
                    return 1;
                }
            }

            // No valid solutions
            return 0;
        }
    }
}