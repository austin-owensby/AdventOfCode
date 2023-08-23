namespace AdventOfCode.Services
{
    public class Solution2015_07Service : ISolutionDayService
    {
        public Solution2015_07Service() { }

        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "07.txt"));
            string[] lines = data.Split("\n");
            List<string> knownRegisters = new() { "1" };
            Dictionary<string, ushort> registerValues = new() { { "1", 1 } };
            Dictionary<List<string>, string> unknownRegisters = new();

            // Process the data from the file into unknown and known registers
            foreach (string line in lines)
            {
                string[] instruction = line.Split(" ");

                // # -> a
                // a -> b
                if (instruction.Length == 3)
                {

                    if (ushort.TryParse(instruction[0], out ushort value))
                    {
                        knownRegisters.Add(instruction[2]);
                        registerValues[instruction[2]] = value;
                    }
                    else
                    {
                        unknownRegisters[new List<string> { "ASSIGN", instruction[0] }] = instruction[2];
                    }
                }

                // NOT a -> b
                if (instruction.Length == 4)
                {
                    unknownRegisters[new List<string> { instruction[0], instruction[1] }] = instruction[3];
                }

                // a AND b -> c
                // 1 AND a -> b
                // a OR b -> c
                // a LSHIFT # -> b
                // a RSHIFT # -> b
                if (instruction.Length == 5)
                {
                    if (instruction[1] == "AND")
                    {
                        unknownRegisters[new List<string> { instruction[1], instruction[0], instruction[2] }] = instruction[4];
                    }
                    else if (instruction[1] == "OR")
                    {
                        unknownRegisters[new List<string> { instruction[1], instruction[0], instruction[2] }] = instruction[4];
                    }
                    else if (instruction[1] == "LSHIFT")
                    {
                        unknownRegisters[new List<string> { instruction[1], instruction[0], instruction[2] }] = instruction[4];

                        // If an integer is found, add it as a register
                        if (ushort.TryParse(instruction[2], out ushort value))
                        {
                            if (!knownRegisters.Contains(instruction[2]))
                            {
                                knownRegisters.Add(instruction[2]);
                                registerValues[instruction[2]] = value;
                            }
                        }
                    }
                    else if (instruction[1] == "RSHIFT")
                    {
                        unknownRegisters[new List<string> { instruction[1], instruction[0], instruction[2] }] = instruction[4];

                        // If an integer is found, add it as a register
                        if (ushort.TryParse(instruction[2], out ushort value))
                        {
                            if (!knownRegisters.Contains(instruction[2]))
                            {
                                knownRegisters.Add(instruction[2]);
                                registerValues[instruction[2]] = value;
                            }
                        }
                    }
                }
            }

            while (!knownRegisters.Contains("a"))
            {
                foreach (KeyValuePair<List<string>, string> pair in unknownRegisters)
                {
                    // Get the arguments for the registers for this instruction
                    List<string> registers = pair.Key.Count == 2 ? new List<string> { pair.Key[1] } : new List<string> { pair.Key[1], pair.Key[2] };

                    // If the registers for this instruction are all known, calculate the new value
                    if (!registers.Except(knownRegisters).Any())
                    {
                        registerValues[pair.Value] = (ushort)(pair.Key[0] switch
                        {
                            "ASSIGN" => registerValues[registers[0]],
                            "NOT" => ~registerValues[registers[0]],
                            "AND" => registerValues[registers[0]] & registerValues[registers[1]],
                            "OR" => registerValues[registers[0]] | registerValues[registers[1]],
                            "LSHIFT" => registerValues[registers[0]] << registerValues[registers[1]],
                            "RSHIFT" => registerValues[registers[0]] >> registerValues[registers[1]],
                            _ => 0
                        });

                        knownRegisters.Add(pair.Value);
                    }
                }

                // Remove the known registers from the unknownRegisters
                unknownRegisters = unknownRegisters.Where(r => !knownRegisters.Contains(r.Value)).ToDictionary(r => r.Key, r => r.Value);
            }

            return registerValues["a"].ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "07.txt"));
            string[] lines = data.Split("\n");
            List<string> knownRegisters = new() { "1" };
            Dictionary<string, ushort> registerValues = new() { { "1", 1 } };
            Dictionary<List<string>, string> unknownRegisters = new();

            // Process the data from the file into unknown and known registers
            foreach (string line in lines)
            {
                string[] instruction = line.Split(" ");

                // # -> a
                // a -> b
                if (instruction.Length == 3)
                {

                    if (ushort.TryParse(instruction[0], out ushort value))
                    {
                        knownRegisters.Add(instruction[2]);
                        registerValues[instruction[2]] = value;
                    }
                    else
                    {
                        unknownRegisters[new List<string> { "ASSIGN", instruction[0] }] = instruction[2];
                    }
                }

                // NOT a -> b
                if (instruction.Length == 4)
                {
                    unknownRegisters[new List<string> { instruction[0], instruction[1] }] = instruction[3];
                }

                // a AND b -> c
                // 1 AND a -> b
                // a OR b -> c
                // a LSHIFT # -> b
                // a RSHIFT # -> b
                if (instruction.Length == 5)
                {
                    if (instruction[1] == "AND")
                    {
                        unknownRegisters[new List<string> { instruction[1], instruction[0], instruction[2] }] = instruction[4];
                    }
                    else if (instruction[1] == "OR")
                    {
                        unknownRegisters[new List<string> { instruction[1], instruction[0], instruction[2] }] = instruction[4];
                    }
                    else if (instruction[1] == "LSHIFT")
                    {
                        unknownRegisters[new List<string> { instruction[1], instruction[0], instruction[2] }] = instruction[4];

                        // If an integer is found, add it as a register
                        if (ushort.TryParse(instruction[2], out ushort value))
                        {
                            if (!knownRegisters.Contains(instruction[2]))
                            {
                                knownRegisters.Add(instruction[2]);
                                registerValues[instruction[2]] = value;
                            }
                        }
                    }
                    else if (instruction[1] == "RSHIFT")
                    {
                        unknownRegisters[new List<string> { instruction[1], instruction[0], instruction[2] }] = instruction[4];

                        // If an integer is found, add it as a register
                        if (ushort.TryParse(instruction[2], out ushort value))
                        {
                            if (!knownRegisters.Contains(instruction[2]))
                            {
                                knownRegisters.Add(instruction[2]);
                                registerValues[instruction[2]] = value;
                            }
                        }
                    }
                }
            }

            // Use the value from the first half and set it to b
            string[] response = FirstHalf(example).Split(" ");
            string valueString = response[^1];
            string valueStringNoPeriod = valueString.Remove(valueString.Length - 1, 1);
            ushort calculatedValue = ushort.Parse(valueStringNoPeriod);
            registerValues["b"] = calculatedValue;

            while (!knownRegisters.Contains("a"))
            {
                foreach (KeyValuePair<List<string>, string> pair in unknownRegisters)
                {
                    // Get the arguments for the registers for this instruction
                    List<string> registers = pair.Key.Count == 2 ? new List<string> { pair.Key[1] } : new List<string> { pair.Key[1], pair.Key[2] };

                    // If the registers for this instruction are all known, calculate the new value
                    if (!registers.Except(knownRegisters).Any())
                    {
                        registerValues[pair.Value] = (ushort)(pair.Key[0] switch
                        {
                            "ASSIGN" => registerValues[registers[0]],
                            "NOT" => ~registerValues[registers[0]],
                            "AND" => registerValues[registers[0]] & registerValues[registers[1]],
                            "OR" => registerValues[registers[0]] | registerValues[registers[1]],
                            "LSHIFT" => registerValues[registers[0]] << registerValues[registers[1]],
                            "RSHIFT" => registerValues[registers[0]] >> registerValues[registers[1]],
                            _ => 0
                        });

                        knownRegisters.Add(pair.Value);
                    }
                }

                // Remove the known registers from the unknownRegisters
                unknownRegisters = unknownRegisters.Where(r => !knownRegisters.Contains(r.Value)).ToDictionary(r => r.Key, r => r.Value);
            }

            return registerValues["a"].ToString();
        }
    }
}
