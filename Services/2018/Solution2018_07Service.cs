using System.Text.RegularExpressions;

namespace AdventOfCode.Services
{
    public class Solution2018_07Service : ISolutionDayService
    {
        public Solution2018_07Service() { }

        private class Instruction
        {
            public char First { get; set; }
            public char Next { get; set; }

            public Instruction(string data)
            {
                Regex rx = new(@"Step (.) must be finished before step (.) can begin\.");
                MatchCollection matches = rx.Matches(data);
                Match match = matches.First();

                First = match.Groups[1].Value[0];
                Next = match.Groups[2].Value[0];
            }
        }

        private class Worker
        {
            public char? Step { get; set; }
            public int TimeLeft { get; set; }
        }

        public string FirstHalf(bool example)
        {
            List<string> data = Utility.GetInputLines(2018, 07, example);

            List<Instruction> instructions = data.Select(d => new Instruction(d)).ToList();

            List<char> startingSteps = instructions.Select(i => i.First).Distinct().ToList();
            List<char> endSteps = instructions.Select(i => i.Next).Distinct().ToList();

            // The initial available steps are the instructions whose first letter never appears in the last letter
            PriorityQueue<char, char> availableSteps = new();
            availableSteps.EnqueueRange(startingSteps.Where(s => !endSteps.Contains(s)).Select(s => (s, s)));

            string order = string.Empty;

            while (availableSteps.Count > 0)
            {
                char step = availableSteps.Dequeue();

                if (!order.Contains(step))
                {
                    order += step;

                    // Get all steps that had the current steps as a requirement
                    List<char> nextSteps = instructions.Where(i => i.First == step).Select(i => i.Next).ToList();

                    foreach (char nextStep in nextSteps)
                    {
                        // Check if the next step has all of it's requirements met
                        List<char> requiredStepsForNextStep = instructions.Where(i => i.Next == nextStep).Select(s => s.First).ToList();
                        if (requiredStepsForNextStep.All(order.Contains))
                        {
                            availableSteps.Enqueue(nextStep, nextStep);
                        }
                    }
                }
            }

            return order.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> data = Utility.GetInputLines(2018, 07, example);

            List<Instruction> instructions = data.Select(d => new Instruction(d)).ToList();

            List<char> startingSteps = instructions.Select(i => i.First).Distinct().ToList();
            List<char> endSteps = instructions.Select(i => i.Next).Distinct().ToList();

            // The initial available steps are the instructions whose first letter never appears in the last letter
            PriorityQueue<char, char> availableSteps = new();
            availableSteps.EnqueueRange(startingSteps.Where(s => !endSteps.Contains(s)).Select(s => (s, s)));

            List<char> history = startingSteps.Where(s => !endSteps.Contains(s)).ToList();

            int timeSpent = 0;

            List<Worker> workers = new() { new(), new(), new(), new(), new() };

            bool keepLooping = true;

            while (keepLooping)
            {
                timeSpent++;

                foreach (Worker worker in workers)
                {
                    if (worker.TimeLeft > 0)
                    {
                        worker.TimeLeft--;
                    }

                    if (worker.TimeLeft == 0 && worker.Step != null)
                    {
                        history.Add((char)worker.Step);
                        worker.Step = null;
                    }

                    if (worker.Step == null && availableSteps.Count > 0)
                    {
                        worker.Step = availableSteps.Dequeue();
                        worker.TimeLeft = (int)worker.Step - 'A' + 1 + 60;
                    }
                }

                /*
                // Calculate new available steps
                availableSteps = p
                // Starts w/ starting letter

                if () {
                    keepLooping = false;
                }
                */
            }

            return timeSpent.ToString();
        }
    }
}
