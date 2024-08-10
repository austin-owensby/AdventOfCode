namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2019/07.txt
    public class Solution2019_07Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            IEnumerable<int> phaseSettings = Enumerable.Range(0, 5);
            IEnumerable<IEnumerable<int>> permutations = phaseSettings.GetPermutations();

            List<string> lines = Utility.GetInputLines(2019, 07, example);

            int answer = 0;

            // Test out each permutation of the phase settings to find the max output signal
            foreach (IEnumerable<int> permutation in permutations)
            {
                int inputSignal = 0;

                // Loop over each applifier
                foreach (int phaseSetting in permutation) {
                    // Run the computer instructions for the amplifier
                    List<int> program = lines.First().Split(",").ToInts();
                    bool isFirstInput = true;

                    int index = 0;
                    bool halted = false;

                    while(!halted) {
                        int opcode = program[index];
                        int opcodeValue = opcode % 100;
                        int xParamMode = 0;
                        int yParamMode = 0;
                        int zParamMode = 0;

                        if (opcode >= 10000) {
                            zParamMode = opcode.ToString()[0].ToInt();
                            yParamMode = opcode.ToString()[1].ToInt();
                            xParamMode = opcode.ToString()[2].ToInt();
                        }
                        else if (opcode >= 1000) {
                            yParamMode = opcode.ToString()[0].ToInt();
                            xParamMode = opcode.ToString()[1].ToInt();
                        }
                        else if (opcode >= 100) {
                            xParamMode = opcode.ToString()[0].ToInt();
                        }

                        int x = program[index + 1];
                        int y = program.Count < index + 2 ? -1 : program[index + 2];
                        int z = program.Count < index + 3 ? -1 : program[index + 3];

                        int xValue = xParamMode == 1 ? x : (program.Count <= x ? -1 : program[x]);
                        int yValue = y == -1 ? -1 : (yParamMode == 1 ? y : (program.Count <= y ? -1 : program[y]));
                        int zValue = z == -1 ? -1 : (zParamMode == 1 ? z : (program.Count <= z ? -1 : program[z]));

                        switch (opcodeValue) {
                            case 1:
                                // Add x + y = z
                                program[z] = xValue + yValue;
                                index += 4;
                                break;
                            case 2:
                                // Multiply x * y = z
                                program[z] = xValue * yValue;
                                index += 4;
                                break;
                            case 3:
                                // Write input into x
                                if (isFirstInput) {
                                    program[x] = phaseSetting;
                                    isFirstInput = false;
                                }
                                else {
                                    program[x] = inputSignal;
                                }
                                index += 2;
                                break;
                            case 4:
                                // Output value from x
                                inputSignal = xValue;
                                // For this program, there's no reason to continue once we have our output
                                halted = true;
                                index += 2;
                                break;
                            case 5:
                                // Jump to y if x != 0
                                if (xValue != 0) {
                                    index = yValue;
                                }
                                else {
                                    index += 3;
                                }
                                break;
                            case 6:
                                // Jump to y if x = 0
                                if (xValue == 0) {
                                    index = yValue;
                                }
                                else {
                                    index += 3;
                                }
                                break;
                            case 7:
                                // z = x < y
                                program[z] = xValue < yValue ? 1 : 0;
                                index += 4;
                                break;
                            case 8:
                                // z = x == y
                                program[z] = xValue == yValue ? 1 : 0;
                                index += 4;
                                break; ;
                            case 99:
                                // Halt
                                halted = true;
                                break;
                            default:
                                throw new Exception($"Unknown opcode: {opcodeValue}");
                            }
                    }
                }

                // Look for the max
                answer = Math.Max(answer, inputSignal);
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            IEnumerable<int> phaseSettings = Enumerable.Range(5, 5);
            IEnumerable<IEnumerable<int>> permutations = phaseSettings.GetPermutations();

            List<string> lines = Utility.GetInputLines(2019, 07, example);

            int answer = 0;

            // Test out each permutation of the phase settings to find the max output signal
            foreach (IEnumerable<int> permutation in permutations)
            {
                int inputSignal = 0;

                List<int> program = lines.First().Split(",").ToInts();
                List<Amplifier> amplifiers = permutation.Select(phase => new Amplifier(program, phase)).ToList();

                // Keep going until all the amplifiers have halted
                while (amplifiers.Any(a => !a.Halted)) {
                    // Run each amplifier 1 after another
                    foreach (Amplifier amplifier in amplifiers) {
                        if (amplifier.Halted) {
                            continue;
                        }

                        bool workToDo = !amplifier.Halted;

                        // Keep working until we halt or output
                        while (workToDo) {
                            int opcode = amplifier.Program[amplifier.Index];
                            int opcodeValue = opcode % 100;
                            int xParamMode = 0;
                            int yParamMode = 0;
                            int zParamMode = 0;

                            if (opcode >= 10000) {
                                zParamMode = opcode.ToString()[0].ToInt();
                                yParamMode = opcode.ToString()[1].ToInt();
                                xParamMode = opcode.ToString()[2].ToInt();
                            }
                            else if (opcode >= 1000) {
                                yParamMode = opcode.ToString()[0].ToInt();
                                xParamMode = opcode.ToString()[1].ToInt();
                            }
                            else if (opcode >= 100) {
                                xParamMode = opcode.ToString()[0].ToInt();
                            }

                            int x = amplifier.Program.Count <= amplifier.Index + 1 ? -1 : amplifier.Program[amplifier.Index + 1];
                            int y = amplifier.Program.Count <= amplifier.Index + 2 ? -1 : amplifier.Program[amplifier.Index + 2];
                            int z = amplifier.Program.Count <= amplifier.Index + 3 ? -1 : amplifier.Program[amplifier.Index + 3];

                            int xValue = xParamMode == 1 ? x : (amplifier.Program.Count <= x || x < 0 ? -1 : amplifier.Program[x]);
                            int yValue = y == -1 ? -1 : (yParamMode == 1 ? y : (amplifier.Program.Count <= y || y < 0 ? -1 : amplifier.Program[y]));
                            int zValue = z == -1 ? -1 : (zParamMode == 1 ? z : (amplifier.Program.Count <= z || z < 0 ? -1 : amplifier.Program[z]));

                            // Execute logic based on opcode
                            switch (opcodeValue) {
                                case 1:
                                    // Add x + y = z
                                    amplifier.Program[z] = xValue + yValue;
                                    amplifier.Index += 4;
                                    break;
                                case 2:
                                    // Multiply x * y = z
                                    amplifier.Program[z] = xValue * yValue;
                                    amplifier.Index += 4;
                                    break;
                                case 3:
                                    // Write input into x
                                    if (amplifier.IsFirstInput) {
                                        amplifier.Program[x] = amplifier.Phase;
                                        amplifier.IsFirstInput = false;
                                    }
                                    else {
                                        amplifier.Program[x] = inputSignal;
                                    }
                                    amplifier.Index += 2;
                                    break;
                                case 4:
                                    // Output value from x
                                    inputSignal = xValue;
                                    amplifier.Index += 2;
                                    // Need to wait until the next loop when the previous amplifier outputs again
                                    workToDo = false;
                                    break;
                                case 5:
                                    // Jump to y if x != 0
                                    if (xValue != 0) {
                                        amplifier.Index = yValue;
                                    }
                                    else {
                                        amplifier.Index += 3;
                                    }
                                    break;
                                case 6:
                                    // Jump to y if x = 0
                                    if (xValue == 0) {
                                        amplifier.Index = yValue;
                                    }
                                    else {
                                        amplifier.Index += 3;
                                    }
                                    break;
                                case 7:
                                    // z = x < y
                                    amplifier.Program[z] = xValue < yValue ? 1 : 0;
                                    amplifier.Index += 4;
                                    break;
                                case 8:
                                    // z = x == y
                                    amplifier.Program[z] = xValue == yValue ? 1 : 0;
                                    amplifier.Index += 4;
                                    break; ;
                                case 99:
                                    // Halt
                                    amplifier.Halted = true;
                                    workToDo = false;
                                    break;
                                default:
                                    throw new Exception($"Unknown opcode: {opcodeValue}");
                            }
                        }
                    }
                }

                // Look for the max
                answer = Math.Max(answer, inputSignal);
            }

            return answer.ToString();
        }

        private class Amplifier(List<int> program, int phase)
        {
            public List<int> Program { get; set; } = [.. program];
            public int Index { get; set; } = 0;
            public bool Halted { get; set; } = false;
            
            public bool IsFirstInput { get; set; } = true;
            public int Phase { get; set; } = phase;
        }
    }
}