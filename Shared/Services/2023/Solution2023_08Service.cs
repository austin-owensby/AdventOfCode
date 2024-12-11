namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2023/08.txt
    public class Solution2023_08Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 8, example);

            string instructions = lines.First();

            Dictionary<string, Tuple<string, string>> mappings = [];

            foreach (string line in lines.Skip(2)) {
                List<string> values = line.QuickRegex(@"(...) = \((...), (...)\)");
                mappings[values[0]] = new(values[1], values[2]);
            }

            string currentStep = "AAA";

            int answer = 0;
            
            while (currentStep != "ZZZ") {
                char direction = instructions[answer % instructions.Length];

                currentStep = direction == 'L' ? mappings[currentStep].Item1 : mappings[currentStep].Item2;

                answer++;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 8, example);

            string instructions = lines.First();

            Dictionary<string, Tuple<string, string>> mappings = [];

            List<string> currentSteps = [];

            foreach (string line in lines.Skip(2)) {
                List<string> values = line.QuickRegex(@"(...) = \((...), (...)\)");
                mappings[values[0]] = new(values[1], values[2]);

                if (values[0].EndsWith('A')) {
                    currentSteps.Add(values[0]);
                }
            }

            List<List<string>> paths = [];
            foreach (string currentStep in currentSteps) {
                paths.Add([currentStep]);
            }

            List<int> periods = Enumerable.Repeat(0, currentSteps.Count).ToList();

            int step = 0;
            
            while (periods.Any(p => p == 0)) {
                char direction = instructions[step % instructions.Length];

                List<string> newCurrentSteps = [];

                foreach (int i in currentSteps.Count) {
                    if (periods[i] != 0) {
                        // No need to process this step, we've already found it's period
                        newCurrentSteps.Add("");
                        continue;
                    }

                    string newCurrentStep = direction == 'L' ? mappings[currentSteps[i]].Item1 : mappings[currentSteps[i]].Item2;

                    if (newCurrentStep.EndsWith('Z') && paths[i].Any(x => x == newCurrentStep)) {
                        // Check if we've found a period
                        int firstSeenIndex = paths[i].IndexOf(newCurrentStep);
                        
                        if (firstSeenIndex % instructions.Length == paths[i].Count % instructions.Length) {
                            periods[i] = paths[i].Count - firstSeenIndex;
                        }
                    }

                    paths[i].Add(newCurrentStep);
                    newCurrentSteps.Add(newCurrentStep);
                }

                currentSteps = newCurrentSteps;
                step++;
            }

            long answer = Utility.LCM(periods[0], periods[1]);
            foreach (long period in periods.Skip(2)) {
                answer = Utility.LCM(answer, period);
            }

            return answer.ToString();
        }
    }
}