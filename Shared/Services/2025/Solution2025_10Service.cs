namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/10.txt
    public class Solution2025_10Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 10, example);

            List<(List<bool> lights, List<List<int>> buttons)> data = [];

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');

                List<bool> lights = parts[0].TrimStart('[').TrimEnd(']').Select(c => c == '#').ToList();

                List<List<int>> buttons = parts[1..(parts.Length - 1)].Select(p => p.TrimStart('(').TrimEnd(')').Split(',').ToInts()).ToList();

                data.Add((lights, buttons));
            }

            int answer = 0;

            foreach ((List<bool> lights, List<List<int>> buttons) in data)
            {
                int minPresses = int.MaxValue;

                List<bool> state = [];

                foreach (int i in lights.Count)
                {
                    state.Add(false);
                }

                Queue<(List<bool> currentState, List<int> presses)> queue = [];
                queue.Enqueue((state, []));

                while (queue.Count > 0)
                {
                    (List<bool> currentState, List<int> presses) = queue.Dequeue();

                    if (presses.Count + 1 > minPresses)
                    {
                        continue;
                    }

                    foreach (int i in buttons.Count)
                    {
                        if (presses.Contains(i))
                        {
                            continue;
                        }

                        List<int> button = buttons[i];
                        List<bool> newState = currentState.ToList();

                        foreach (int j in newState.Count)
                        {
                            if (button.Contains(j))
                            {
                                newState[j] = !newState[j];
                            }
                        }

                        bool match = true;

                        foreach (int j in newState.Count)
                        {
                            if (newState[j] != lights[j])
                            {
                                match = false;
                                break;
                            }
                        }

                        if (match)
                        {
                            minPresses = presses.Count + 1;
                            break;
                        }
                        else
                        {
                            if (!newState.All(x => x))
                            {
                                queue.Enqueue((newState, [..presses, i]));
                            }
                        }
                    }
                }

                answer += minPresses;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 10, example);

            // Parse the data to pull out the buttons and required joltage
            List<(List<int> joltages, List<List<int>> buttons)> data = [];

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');

                List<int> joltages = parts.Last().TrimStart('{').TrimEnd('}').Split(',').ToInts();

                List<List<int>> buttons = parts[1..(parts.Length - 1)].Select(p => p.TrimStart('(').TrimEnd(')').Split(',').ToInts()).ToList();

                data.Add((joltages, buttons));
            }

            int answer = 0;

            // Loop over each problem
            foreach ((List<int> joltages, List<List<int>> buttons) in data)
            {
                // Build a set of linear equations for the buttons
                List<List<int>> equations = [];

                foreach (int i in joltages.Count)
                {
                    List<int> row = [];

                    foreach (List<int> button in buttons)
                    {
                        if (button.Contains(i))
                        {
                            row.Add(1);
                        }
                        else
                        {
                            row.Add(0);
                        }
                    }

                    row.Add(joltages[i]);

                    equations.Add(row);
                }

                // Reduce the equations with Gaussian Elimination
                List<List<double>> simplifiedEquations = Utility.GaussianElimination(equations);

                // Determine the indices of free variables
                List<int> freeVariableIndexes = [];

                int startIndex = 0;
                foreach (List<double> row in simplifiedEquations)
                {
                    bool leadingOneFound = false;
                    for (int i = startIndex; i < row.Count - 1; i++)
                    {
                        if (row[i] != 0)
                        {
                            if (!leadingOneFound)
                            {
                                leadingOneFound = true;
                                startIndex = i + 1;
                            }
                            else
                            {
                                if (!freeVariableIndexes.Contains(i))
                                {
                                    freeVariableIndexes.Add(i);
                                }
                            }
                        }
                    }
                }

                // Determine the max values for the free variables
                List<int> freeVariableMaxValues = [];

                foreach (int index in freeVariableIndexes)
                {
                    List<int> button = buttons[index];

                    int maxButtonPresses = joltages.Where((x, i) => button.Contains(i)).Min();

                    freeVariableMaxValues.Add(maxButtonPresses);
                }

                // Loop over each free variable combination and determine a minimum
                List<int> freeVariableValues = Enumerable.Repeat(0, freeVariableIndexes.Count).ToList();

                bool looping = true;
                int minPresses = int.MaxValue;

                while (looping)
                {
                    int presses = 0;
                    bool goodSolution = true;

                    // Solve the equations given the fixed values for free variables
                    foreach (List<double> equation in simplifiedEquations)
                    {
                        bool leadingValueFound = false;
                        double solution = equation.Last();

                        foreach (int i in equation.Count - 1)
                        {
                            if (Math.Abs(equation[i]) > 1e-6)
                            {
                                if (!leadingValueFound)
                                {
                                    leadingValueFound = true;
                                }
                                else
                                {
                                    int freeVariableIndex = freeVariableIndexes.IndexOf(i);
                                    int freeVariableValue = freeVariableValues[freeVariableIndex];

                                    solution -= freeVariableValue * equation[i];
                                }
                            }
                        }

                        // Discard negative and non-integer solutions
                        solution = Math.Round(solution, 10);
                        if (solution < 0 || Math.Abs(solution - Math.Round(solution)) > 1e-6)
                        {
                            goodSolution = false;
                            break;
                        }

                        presses += (int)Math.Round(solution);
                    }

                    presses += freeVariableValues.Sum();

                    if (goodSolution && presses < minPresses)
                    {
                        minPresses = presses;
                    }

                    // Increase the free variables
                    looping = false;

                    foreach (int i in freeVariableValues.Count)
                    {
                        if (freeVariableValues[i] == freeVariableMaxValues[i])
                        {
                            // We're at this variable's max reset it and increase the next
                            freeVariableValues[i] = 0;
                        }
                        else
                        {
                            // Increase the value an try the next combination
                            freeVariableValues[i]++;
                            looping = true;
                            break;
                        }
                    }
                }

                answer += minPresses;
            }

            return answer.ToString();
        }
    }
}