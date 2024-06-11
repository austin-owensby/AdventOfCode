namespace AdventOfCode.Services
{
    public class Solution2019_05Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 05, example);
            List<int> program = lines.First().Split(",").ToInts();

            int index = 0;
            bool halted = false;

            int answer = 0;

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

                int xValue = xParamMode == 1 ? x : (program.Count < x ? -1 : program[x]);
                int yValue = y == -1 ? -1 : (yParamMode == 1 ? y : (program.Count < y ? -1 : program[y]));
                int zValue = z == -1 ? -1 : (zParamMode == 1 ? z : (program.Count < z ? -1 : program[z]));

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
                        // The last output is the answer
                        answer = xValue;
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
            List<string> lines = Utility.GetInputLines(2019, 05, example);
            List<int> program = lines.First().Split(",").ToInts();

            int index = 0;
            bool halted = false;

            int answer = 0;

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
                int y = program.Count <= index + 2 ? -1 : program[index + 2];
                int z = program.Count <= index + 3 ? -1 : program[index + 3];

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
                        // Just use 5 as the input for this puzzle
                        program[x] = 5;
                        index += 2;
                        break;
                    case 4:
                        // Output value from x
                        Console.WriteLine(xValue);
                        // The last output is the answer
                        answer = xValue;
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