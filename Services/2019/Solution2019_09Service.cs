namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2019/09.txt
    public class Solution2019_09Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 09, example);
            Dictionary<long, long> program = lines.First().Split(",").ToLongs().Select((v, k) => (k, v)).ToDictionary(x => (long)x.k, x => x.v);

            long index = 0;
            long relativeBase = 0;
            bool halted = false;

            long answer = 0;

            while(!halted) {
                long opcode = program[index];
                long opcodeValue = opcode % 100;
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

                long x = program.GetValueOrDefault(index + 1);
                if (xParamMode == 2) {
                    x += relativeBase;
                }

                long y = program.GetValueOrDefault(index + 2);
                if (yParamMode == 2) {
                    y += relativeBase;
                }

                long z = program.GetValueOrDefault(index + 3);
                if (zParamMode == 2) {
                    z += relativeBase;
                }

                long xValue = xParamMode switch
                {
                    0 or 2 => program.GetValueOrDefault(x),
                    1 => x,
                    _ => throw new Exception($"Unknown param mode: {xParamMode}"),
                };

                long yValue = yParamMode switch
                {
                    0 or 2 => program.GetValueOrDefault(y),
                    1 => y,
                    _ => throw new Exception($"Unknown param mode: {yParamMode}"),
                };

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
                        // Just use 1 as the input for this puzzle
                        program[x] = 1;
                        index += 2;
                        break;
                    case 4:
                        // Output value from x
                        Console.WriteLine(xValue);
                        index += 2;
                        // The output is the answer
                        answer = xValue;
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
                        break;
                    case 9:
                        // Adjust relative base by x
                        relativeBase += xValue;
                        index += 2;
                        break;
                    case 99:
                        // Halt
                        halted = true;
                        break;
                    default:
                        throw new Exception($"Unknown opcode: {opcodeValue}");
                    }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {

            List<string> lines = Utility.GetInputLines(2019, 09, example);
            Dictionary<long, long> program = lines.First().Split(",").ToLongs().Select((v, k) => (k, v)).ToDictionary(x => (long)x.k, x => x.v);

            long index = 0;
            long relativeBase = 0;
            bool halted = false;

            long answer = 0;

            while(!halted) {
                long opcode = program[index];
                long opcodeValue = opcode % 100;
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

                long x = program.GetValueOrDefault(index + 1);
                if (xParamMode == 2) {
                    x += relativeBase;
                }

                long y = program.GetValueOrDefault(index + 2);
                if (yParamMode == 2) {
                    y += relativeBase;
                }

                long z = program.GetValueOrDefault(index + 3);
                if (zParamMode == 2) {
                    z += relativeBase;
                }

                long xValue = xParamMode switch
                {
                    0 or 2 => program.GetValueOrDefault(x),
                    1 => x,
                    _ => throw new Exception($"Unknown param mode: {xParamMode}"),
                };

                long yValue = yParamMode switch
                {
                    0 or 2 => program.GetValueOrDefault(y),
                    1 => y,
                    _ => throw new Exception($"Unknown param mode: {yParamMode}"),
                };

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
                        // Just use 2 as the input for this puzzle
                        program[x] = 2;
                        index += 2;
                        break;
                    case 4:
                        // Output value from x
                        Console.WriteLine(xValue);
                        index += 2;
                        // The output is the answer
                        answer = xValue;
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
                        break;
                    case 9:
                        // Adjust relative base by x
                        relativeBase += xValue;
                        index += 2;
                        break;
                    case 99:
                        // Halt
                        halted = true;
                        break;
                    default:
                        throw new Exception($"Unknown opcode: {opcodeValue}");
                    }
            }

            return answer.ToString();
        }
    }
}