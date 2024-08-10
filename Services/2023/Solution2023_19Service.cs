namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2023/19.txt
    public class Solution2023_19Service : ISolutionDayService
    {
        private class Rule {
            public int PartIndex { get; set; } = -1;
            public bool LessThan { get; set; }
            public int Number { get; set; }
            public string Result { get; set; } = string.Empty;
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 19, example);

            // Parse input
            Dictionary<string, List<Rule>> rules = lines.TakeWhile(line => !string.IsNullOrWhiteSpace(line)).Select(line => {
                List<string> parts = line.QuickRegex(@"(\w+)\{(.+)\}");
                
                string key = parts.First();
                List<Rule> value = [];

                parts = parts.Last().Split(",").ToList();

                foreach (string part in parts) {
                    if (part.Contains(':')) {
                        List<string> subParts = part.QuickRegex(@"(\w)(<|>)(\d+):(\w+)");
                        value.Add(new(){
                            PartIndex = subParts[0] == "x" ? 0 : subParts[0] == "m" ? 1 : subParts[0] == "a" ? 2 : 3,
                            LessThan = subParts[1] == "<",
                            Number = int.Parse(subParts[2]),
                            Result = subParts[3]
                        });
                    }
                    else {
                        value.Add(new(){Result = part});
                    }
                }

                return new Tuple<string, List<Rule>> (key, value);
            }).ToDictionary(x => x.Item1, x => x.Item2);
            List<List<int>> parts = lines.SkipWhile(line => !string.IsNullOrWhiteSpace(line)).Skip(1).Select(line => line.QuickRegex(@"\{x=(\d+),m=(\d+),a=(\d+),s=(\d+)\}").ToInts()).ToList();

            int answer = 0;

            foreach (List<int> part in parts)
            {
                string currentStep = "in";

                while (currentStep != "A" && currentStep != "R") {
                    List<Rule> ruleSet = rules[currentStep];

                    foreach (Rule rule in ruleSet) {
                        if (rule.PartIndex == -1 || rule.LessThan && part[rule.PartIndex] < rule.Number || !rule.LessThan && part[rule.PartIndex] > rule.Number) {
                            currentStep = rule.Result;
                            break;
                        }
                    }
                }

                if (currentStep == "A") {
                    // Part was approved, update the answer
                    answer += part.Sum();
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 19, example);

            // Parse input
            Dictionary<string, List<Rule>> rules = lines.TakeWhile(line => !string.IsNullOrWhiteSpace(line)).Select(line => {
                List<string> parts = line.QuickRegex(@"(\w+)\{(.+)\}");
                
                string key = parts.First();
                List<Rule> value = [];

                parts = parts.Last().Split(",").ToList();

                foreach (string part in parts) {
                    if (part.Contains(':')) {
                        List<string> subParts = part.QuickRegex(@"(\w)(<|>)(\d+):(\w+)");
                        value.Add(new(){
                            PartIndex = subParts[0] == "x" ? 0 : subParts[0] == "m" ? 1 : subParts[0] == "a" ? 2 : 3,
                            LessThan = subParts[1] == "<",
                            Number = int.Parse(subParts[2]),
                            Result = subParts[3]
                        });
                    }
                    else {
                        value.Add(new(){Result = part});
                    }
                }

                return new Tuple<string, List<Rule>> (key, value);
            }).ToDictionary(x => x.Item1, x => x.Item2);

            long answer = GetUniqueCombinations([(1, 4000),(1, 4000),(1, 4000),(1, 4000)], "in", rules);

            return answer.ToString();
        }

        private long GetUniqueCombinations(List<(long, long)> ranges, string step, Dictionary<string, List<Rule>> rules) {
            if (step == "R") {
                return 0;
            }
            else if (step == "A") {
                // Calculate the unique combinations for the current range
                return ranges.Aggregate((long)1, (total, r) => total * (r.Item2 - r.Item1 + 1));
            }

            List<Rule> ruleSet = rules[step];

            foreach (Rule rule in ruleSet) {
                bool lastStep = rule.PartIndex == -1;
                bool matchStartRange = !lastStep && (rule.LessThan && ranges[rule.PartIndex].Item1 < rule.Number || !rule.LessThan && ranges[rule.PartIndex].Item1 > rule.Number);
                bool matchEndRange = !lastStep && (rule.LessThan && ranges[rule.PartIndex].Item2 < rule.Number || !rule.LessThan && ranges[rule.PartIndex].Item2 > rule.Number);
                if (lastStep || matchStartRange && matchEndRange) {
                    // Our whole range matched on this rule, move on to the next step
                    return GetUniqueCombinations(ranges, rule.Result, rules);
                }
                else if (matchStartRange || matchEndRange) {
                    // Only one end of our range matched, split it up
                    List<(long, long)> newRanges1 = [];
                    List<(long, long)> newRanges2 = [];

                    foreach (int i in 4) {
                        if (i == rule.PartIndex) {
                            long start = ranges[i].Item1;
                            long end = ranges[i].Item2;
                            long midPoint = rule.Number + (rule.LessThan ? -1 : 0);
                            newRanges1.Add((start, midPoint));
                            newRanges2.Add((midPoint + 1, end));
                        }
                        else {
                            newRanges1.Add(ranges[i]);
                            newRanges2.Add(ranges[i]);
                        }
                    }

                    return GetUniqueCombinations(newRanges1, step, rules) + GetUniqueCombinations(newRanges2, step, rules);
                }
            }

            return 0;
        }
    }
}