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
                List<string> steps = data.QuickRegex(@"Step (.) must be finished before step (.) can begin\.");

                First = steps[0][0];
                Next = steps[1][0];
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

            List<char> startedSteps = new();
            List<char> finishedSteps = new();  

            int timeSpent = 0;

            List<Worker> workers = new(){ new(), new(), new(), new(), new() };

            while (availableSteps.Count > 0 || workers.Any(w => w.TimeLeft != 0))
            {
                List<char> recentlyFinishedSteps = new();

                foreach (Worker worker in workers) {
                    // The worker needs work, get the next step from the queue
                    if (worker.Step == null && availableSteps.Count > 0)
                    {
                        worker.Step = availableSteps.Dequeue();
                        worker.TimeLeft = (int)worker.Step - 'A' + 1 + 60;
                        startedSteps.Add((char)worker.Step);
                    }
                    
                    // The worker does work on their step
                    if (worker.TimeLeft > 0)
                    {
                        worker.TimeLeft--;
                    }

                    // The worker just completed work for their step
                    if (worker.TimeLeft == 0 && worker.Step != null)
                    {
                        finishedSteps.Add((char)worker.Step);
                        recentlyFinishedSteps.Add((char)worker.Step);
                        worker.Step = null;
                    }
                }

                List<char> nextSteps = instructions.Where(i => recentlyFinishedSteps.Contains(i.First)).Select(i => i.Next).Distinct().ToList();

                foreach (char nextStep in nextSteps)
                {
                    // Check if the next step has all of it's requirements met
                    List<char> requiredStepsForNextStep = instructions.Where(i => i.Next == nextStep).Select(s => s.First).ToList();
                    if (requiredStepsForNextStep.All(finishedSteps.Contains))
                    {
                        availableSteps.Enqueue(nextStep, nextStep);
                    }
                }

                timeSpent++;
            }

            return timeSpent.ToString();
        }
    }
}
