namespace AdventOfCode.Services
{
    public class Solution2019_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 02, example);
            List<int> program = lines.First().Split(",").ToInts();

            // Fix 1202 program alarm from puzzle
            program[1] = 12;
            program[2] = 2;

            int index = 0;

            while(true) {
                int opcode = program[index];

                if (opcode == 99) {
                    break;
                }
                else{
                    int x = program[index + 1];
                    int y = program[index + 2];
                    int z = program[index + 3];

                    if (opcode == 1) {
                        // Add x + y = z
                        program[z] = program[x] + program[y];
                    }
                    else if (opcode == 2) {
                        // Devide x * y = z
                        program[z] = program[x] * program[y];
                    }
                    else {
                        throw new Exception($"Unknown opcode: {opcode}");
                    }
                }

                index += 4;
            }

            int answer = program[0];

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 02, example);

            int answer = 0;

            for (int i = 0; i <= 99; i++) {
                for (int j = 0; j <= 99; j++) {
                    List<int> program = lines.First().Split(",").ToInts();
                    program[1] = i;
                    program[2] = j;

                    int index = 0;

                    while(true) {
                        int opcode = program[index];

                        if (opcode == 99) {
                            break;
                        }
                        else{
                            int x = program[index + 1];
                            int y = program[index + 2];
                            int z = program[index + 3];

                            if (opcode == 1) {
                                // Add x + y = z
                                program[z] = program[x] + program[y];
                            }
                            else if (opcode == 2) {
                                // Devide x * y = z
                                program[z] = program[x] * program[y];
                            }
                            else {
                                throw new Exception($"Unknown opcode: {opcode}");
                            }
                        }

                        index += 4;
                    }

                    int result = program[0];

                    if (result == 19690720) {
                        answer = i*100 + j;
                        break;
                    }
                }

                if (answer != 0) {
                    break;
                }
            }

            return answer.ToString();
        }
    }
}