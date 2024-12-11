namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2019/13.txt
    public class Solution2019_13Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 13, example);
            Dictionary<long, long> program = lines.First().Split(",").ToLongs().Select((v, k) => (k, v)).ToDictionary(x => (long)x.k, x => x.v);

            long index = 0;
            long relativeBase = 0;
            bool halted = false;

            long answer = 0;

            List<long> output = [];

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
                        throw new Exception("Unexpected input");
                        program[x] = 1;
                        index += 2;
                        break;
                    case 4:
                        // Output value from x
                        output.Add(xValue);
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

            // Material is the 3rd parameter and block is 2
            answer = output.Chunk(3).Count(c => c.Last() == 2);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            // Set to true to display the game
            bool display = false;

            List<string> lines = Utility.GetInputLines(2019, 13, example);
            Dictionary<long, long> program = lines.First().Split(",").ToLongs().Select((v, k) => (k, v)).ToDictionary(x => (long)x.k, x => x.v);

            // Insert 2 quarters
            program[0] = 2;

            long index = 0;
            long relativeBase = 0;
            bool halted = false;

            long answer = 0;

            List<long> output = [];

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
                        List<long[]> values = output.Chunk(3).ToList();
                        output = [];

                        long paddleX = values.LastOrDefault(v => v[2] == 3)?[0] ?? 0;
                        long ballX = values.LastOrDefault(v => v[2] == 4)?[0] ?? 0;

                        if (display) {
                            long maxX = values.Select(v => v[0]).Max();
                            long maxY = values.Select(v => v[1]).Max();

                            Console.Clear();

                            // Build a new output so we don't have to store every output ever received
                            List<long> newOutput = [];

                            answer = values.LastOrDefault(v => v[0] == -1 && v[1] == 0)?[2] ?? 0;
                            newOutput.AddRange([-1, 0, answer]);
                            Console.WriteLine($"Score: {answer}");

                            for (long yCoordinate = 0; yCoordinate <= maxY; yCoordinate++) {
                                string row = string.Empty;
                                for (long xCoordinate = 0; xCoordinate <= maxX; xCoordinate++) {
                                    long value = values.LastOrDefault(v => v[0] == xCoordinate && v[1] == yCoordinate)?[2] ?? 0;
                                    newOutput.AddRange([xCoordinate, yCoordinate, value]);

                                    row += value;
                                }

                                row = row.Replace('0', ' ').Replace('1', '█').Replace('2','#').Replace('3','-').Replace('4', 'O');
                                Console.WriteLine(row);
                            }

                            output = newOutput;

                            // Adjust this value as needed to smoothly render the output
                            Thread.Sleep(100);
                        }

                        // Move the paddle with the ball
                        long input = 0;

                        if (ballX < paddleX) {
                            input = -1;
                        }
                        else if (ballX > paddleX) {
                            input = 1;
                        }

                        program[x] = input;
                        index += 2;
                        break;
                    case 4:
                        // Output value from x
                        output.Add(xValue);
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

            // Output 1 last time to show the end state
            if (display) {
                List<long[]> values = output.Chunk(3).ToList();
                long maxX = values.Select(v => v[0]).Max();
                long maxY = values.Select(v => v[1]).Max();

                Console.Clear();

                // Build a new output so we don't have to store every output ever received
                List<long> newOutput = [];

                answer = values.LastOrDefault(v => v[0] == -1 && v[1] == 0)?[2] ?? 0;
                newOutput.AddRange([-1, 0, answer]);
                Console.WriteLine($"Score: {answer}");

                for (long yCoordinate = 0; yCoordinate <= maxY; yCoordinate++) {
                    string row = string.Empty;
                    for (long xCoordinate = 0; xCoordinate <= maxX; xCoordinate++) {
                        long value = values.LastOrDefault(v => v[0] == xCoordinate && v[1] == yCoordinate)?[2] ?? 0;
                        newOutput.AddRange([xCoordinate, yCoordinate, value]);

                        row += value;
                    }

                    row = row.Replace('0', ' ').Replace('1', '█').Replace('2','#').Replace('3','-').Replace('4', 'O');
                    Console.WriteLine(row);
                }
            }

            answer = output.Chunk(3).LastOrDefault(v => v[0] == -1 && v[1] == 0)?[2] ?? 0;

            return answer.ToString();
        }
    }
}