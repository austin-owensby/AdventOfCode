namespace AdventOfCode.Services
{
    public class Solution2015_23Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "23.txt"));
            List<string> lines = data.Split('\n').ToList();
            lines.RemoveAt(lines.Count - 1);

            int instructionPointer = 0;
            int regA = 0;
            int regB = 0;

            while (instructionPointer < lines.Count)
            {
                string line = lines[instructionPointer];
                string instruction = line.Split(' ')[0];
                int jumpAmount = 1;

                string register;
                int offset;
                switch (instruction)
                {
                    // hlf r: sets r to half its value
                    case "hlf":
                        register = line.Split(' ')[1];

                        if (register.StartsWith("a"))
                        {
                            regA /= 2;
                        }
                        else if (register.StartsWith("b"))
                        {
                            regB /= 2;
                        }
                        else
                        {
                            throw new ArgumentException($"Unknown register: '{register}.'");
                        }

                        break;
                    // tpl r: sets r to triple its value
                    case "tpl":
                        register = line.Split(' ')[1];

                        if (register.StartsWith("a"))
                        {
                            regA *= 3;
                        }
                        else if (register.StartsWith("b"))
                        {
                            regB *= 3;
                        }
                        else
                        {
                            throw new ArgumentException($"Unknown register: '{register}.'");
                        }

                        break;
                    // inc r: increments r
                    case "inc":
                        register = line.Split(' ')[1];

                        if (register.StartsWith("a"))
                        {
                            regA++;
                        }
                        else if (register.StartsWith("b"))
                        {
                            regB++;
                        }
                        else
                        {
                            throw new ArgumentException($"Unknown register: '{register}.'");
                        }

                        break;
                    // jmp offset: continues at an offset of offset
                    case "jmp":
                        jumpAmount = int.TryParse(line.Split(' ')[1], out offset) ? offset : throw new ArgumentException($"Couldn't parse offset: '{offset}.'");

                        break;
                    // jie r, offset: continues at an offset of offset if r is even
                    case "jie":
                        register = line.Split(' ')[1];

                        if (int.TryParse(line.Split(' ')[2], out offset))
                        {
                            if (register.StartsWith("a"))
                            {
                                if (regA % 2 == 0)
                                {
                                    jumpAmount = offset;
                                }
                            }
                            else if (register.StartsWith("b"))
                            {
                                if (regB % 2 == 0)
                                {
                                    jumpAmount = offset;
                                }
                            }
                            else
                            {
                                throw new ArgumentException($"Unknown register: '{register}.'");
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Couldn't parse offset: '{offset}.'");
                        }

                        break;
                    // jio r, offset: continues at an offset of offset if r is one
                    case "jio":
                        register = line.Split(' ')[1];

                        if (int.TryParse(line.Split(' ')[2], out offset))
                        {
                            if (register.StartsWith("a"))
                            {
                                if (regA == 1)
                                {
                                    jumpAmount = offset;
                                }
                            }
                            else if (register.StartsWith("b"))
                            {
                                if (regB == 1)
                                {
                                    jumpAmount = offset;
                                }
                            }
                            else
                            {
                                throw new ArgumentException($"Unknown register: '{register}.'");
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Couldn't parse offset: '{offset}.'");
                        }

                        break;
                    default:
                        throw new ArgumentException($"Unknown instruction: '{instruction}.'");
                }

                instructionPointer += jumpAmount;
            }

            return regB.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "23.txt"));
            List<string> lines = data.Split('\n').ToList();
            lines.RemoveAt(lines.Count - 1);

            int instructionPointer = 0;
            int regA = 1;
            int regB = 0;

            while (instructionPointer < lines.Count)
            {
                string line = lines[instructionPointer];
                string instruction = line.Split(' ')[0];
                int jumpAmount = 1;

                string register;
                int offset;
                switch (instruction)
                {
                    // hlf r: sets r to half its value
                    case "hlf":
                        register = line.Split(' ')[1];

                        if (register.StartsWith("a"))
                        {
                            regA /= 2;
                        }
                        else if (register.StartsWith("b"))
                        {
                            regB /= 2;
                        }
                        else
                        {
                            throw new ArgumentException($"Unknown register: '{register}.'");
                        }

                        break;
                    // tpl r: sets r to triple its value
                    case "tpl":
                        register = line.Split(' ')[1];

                        if (register.StartsWith("a"))
                        {
                            regA *= 3;
                        }
                        else if (register.StartsWith("b"))
                        {
                            regB *= 3;
                        }
                        else
                        {
                            throw new ArgumentException($"Unknown register: '{register}.'");
                        }

                        break;
                    // inc r: increments r
                    case "inc":
                        register = line.Split(' ')[1];

                        if (register.StartsWith("a"))
                        {
                            regA++;
                        }
                        else if (register.StartsWith("b"))
                        {
                            regB++;
                        }
                        else
                        {
                            throw new ArgumentException($"Unknown register: '{register}.'");
                        }

                        break;
                    // jmp offset: continues at an offset of offset
                    case "jmp":
                        jumpAmount = int.TryParse(line.Split(' ')[1], out offset) ? offset : throw new ArgumentException($"Couldn't parse offset: '{offset}.'");

                        break;
                    // jie r, offset: continues at an offset of offset if r is even
                    case "jie":
                        register = line.Split(' ')[1];

                        if (int.TryParse(line.Split(' ')[2], out offset))
                        {
                            if (register.StartsWith("a"))
                            {
                                if (regA % 2 == 0)
                                {
                                    jumpAmount = offset;
                                }
                            }
                            else if (register.StartsWith("b"))
                            {
                                if (regB % 2 == 0)
                                {
                                    jumpAmount = offset;
                                }
                            }
                            else
                            {
                                throw new ArgumentException($"Unknown register: '{register}.'");
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Couldn't parse offset: '{offset}.'");
                        }

                        break;
                    // jio r, offset: continues at an offset of offset if r is one
                    case "jio":
                        register = line.Split(' ')[1];

                        if (int.TryParse(line.Split(' ')[2], out offset))
                        {
                            if (register.StartsWith("a"))
                            {
                                if (regA == 1)
                                {
                                    jumpAmount = offset;
                                }
                            }
                            else if (register.StartsWith("b"))
                            {
                                if (regB == 1)
                                {
                                    jumpAmount = offset;
                                }
                            }
                            else
                            {
                                throw new ArgumentException($"Unknown register: '{register}.'");
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Couldn't parse offset: '{offset}.'");
                        }

                        break;
                    default:
                        throw new ArgumentException($"Unknown instruction: '{instruction}.'");
                }

                instructionPointer += jumpAmount;
            }

            return regB.ToString();
        }
    }
}
