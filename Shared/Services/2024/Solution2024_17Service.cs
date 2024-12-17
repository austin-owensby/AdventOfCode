namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/17.txt
    public class Solution2024_17Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 17, example);
            int regA = int.Parse(lines[0].Split(": ")[1]);
            int regB = int.Parse(lines[1].Split(": ")[1]);
            int regC = int.Parse(lines[2].Split(": ")[1]);
            List<int> program = lines.Last().Split(": ")[1].Split(",").ToInts();

            int instruction_pointer = 0;

            List<int> outputs = [];

            while (true) {
                if (instruction_pointer >= program.Count) {
                    break;
                }

                int opCode = program[instruction_pointer];
                int literal_operand = program[instruction_pointer + 1];

                int combo_operand = literal_operand switch {
                    4 => regA,
                    5 => regB,
                    6 => regC,
                    _ => literal_operand
                };

                switch (opCode) {
                    case 0:
                        regA = (int)(regA / Math.Pow(2, combo_operand));
                        instruction_pointer += 2;
                        break;
                    case 1:
                        regB ^= literal_operand;
                        instruction_pointer += 2;
                        break;
                    case 2:
                        regB = combo_operand % 8;
                        instruction_pointer += 2;
                        break;
                    case 3:
                        instruction_pointer = regA == 0 ? instruction_pointer + 2 : literal_operand;
                        break;
                    case 4:
                        regB ^= regC;
                        instruction_pointer += 2;
                        break;
                    case 5:
                        outputs.Add(combo_operand % 8);
                        instruction_pointer += 2;
                        break;
                    case 6:
                        regB = (int)(regA / Math.Pow(2, combo_operand));
                        instruction_pointer += 2;
                        break;
                    case 7:
                        regC = (int)(regA / Math.Pow(2, combo_operand));
                        instruction_pointer += 2;
                        break;
                }
            }

            string answer = string.Join(",", outputs);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 17, example);

            long answer = 0;

            // TODO, there must be a more elegant solution
            // The iteration and loop values are based off of observations while running the code
            long iteration = 4144625026562;

            int i = 0;
            //List<int> loop = [20217, 1028359, 8192, 12025, 1028359];
            List<long> loop = [20217, 3307124797703, 20217, 1090921672967, 20217, 4398046490887];

            while (answer == 0) {
                long regA = iteration;
                long regB = long.Parse(lines[1].Split(": ")[1]);
                long regC = long.Parse(lines[2].Split(": ")[1]);
                List<long> program = lines.Last().Split(": ")[1].Split(",").ToLongs();

                int instruction_pointer = 0;

                List<long> outputs = [];

                bool keepLooping = true;
                while (keepLooping) {
                    if (instruction_pointer >= program.Count) {
                        break;
                    }

                    long opCode = program[instruction_pointer];
                    long literal_operand = program[instruction_pointer + 1];

                    long combo_operand = literal_operand switch {
                        4 => regA,
                        5 => regB,
                        6 => regC,
                        _ => literal_operand
                    };                    

                    switch (opCode) {
                        case 0:
                            regA = (long)(regA / Math.Pow(2, combo_operand));
                            instruction_pointer += 2;
                            break;
                        case 1:
                            regB ^= literal_operand;
                            instruction_pointer += 2;
                            break;
                        case 2:
                            regB = combo_operand % 8;
                            instruction_pointer += 2;
                            break;
                        case 3:
                            instruction_pointer = regA == 0 ? instruction_pointer + 2 : (int)literal_operand;
                            break;
                        case 4:
                            regB ^= regC;
                            instruction_pointer += 2;
                            break;
                        case 5:
                            // Check if we match the program so far
                            if (program[outputs.Count] != combo_operand % 8) {
                                keepLooping = false;
                                break;
                            }

                            outputs.Add(combo_operand % 8);
                            instruction_pointer += 2;
                            break;
                        case 6:
                            regB = (long)(regA / Math.Pow(2, combo_operand));
                            instruction_pointer += 2;
                            break;
                        case 7:
                            regC = (long)(regA / Math.Pow(2, combo_operand));
                            instruction_pointer += 2;
                            break;
                    }
                }

                if (outputs.SequenceEqual(program)) {
                    answer = iteration;
                }

                iteration += loop[i % loop.Count];
                i++;
            }
            return answer.ToString();
        }
    }
}