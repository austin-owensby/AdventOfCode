namespace AdventOfCode.Services
{
    public class Solution2022_21Service : ISolutionDayService
    {
        public Solution2022_21Service() { }

        private class MonkeyRiddle {
            public long? Answer {get; set;}
            public string Name {get; set;} = string.Empty;
            public string Op {get; set;} = string.Empty;
            public string Var1 {get; set;} = string.Empty;
            public string Var2 { get; set;} = string.Empty;
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 21, example);

            List<MonkeyRiddle> riddles = new();

            foreach (string line in lines)
            {
                MonkeyRiddle riddle = new();

                if (line.Contains('-') || line.Contains('+') || line.Contains('*') || line.Contains('/')) {
                    List<string> matches = line.QuickRegex(@"(.{4}): (.{4}) (.) (.{4})");

                    riddle.Name = matches[0];
                    riddle.Var1 = matches[1];
                    riddle.Op = matches[2];
                    riddle.Var2 = matches[3];
                }
                else {
                    List<string> matches = line.QuickRegex(@"(.{4}): (\d+)");

                    riddle.Name = matches[0];
                    riddle.Answer = int.Parse(matches[1]);
                }

                riddles.Add(riddle);
            }

            while (riddles.First(r => r.Name == "root").Answer == null) {
                List<MonkeyRiddle> solvedRiddles = riddles.Where(r => r.Answer != null).ToList();
                List<MonkeyRiddle> availableRiddles = riddles.Where(riddle => riddle.Answer == null && solvedRiddles.Any(r => r.Name == riddle.Var1) && solvedRiddles.Any(r => r.Name == riddle.Var2)).ToList();

                foreach (MonkeyRiddle riddle in availableRiddles) {
                    long value1 = (long)solvedRiddles.First(r => r.Name == riddle.Var1).Answer!;
                    long value2 = (long)solvedRiddles.First(r => r.Name == riddle.Var2).Answer!;

                    riddle.Answer = riddle.Op switch {
                        "+" => value1 + value2,
                        "-" => value1 - value2,
                        "/" => value1 / value2,
                        "*" => value1 * value2,
                        _ => 0 // Shouldn't happen
                    };
                }
            }
        
            long answer = (long)riddles.First(r => r.Name == "root").Answer!;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 21, example);

            List<MonkeyRiddle> riddles = new();

            foreach (string line in lines)
            {
                MonkeyRiddle riddle = new();

                if (line.Contains('-') || line.Contains('+') || line.Contains('*') || line.Contains('/')) {
                    List<string> matches = line.QuickRegex(@"(.{4}): (.{4}) (.) (.{4})");

                    riddle.Name = matches[0];
                    riddle.Var1 = matches[1];
                    riddle.Op = matches[2];
                    riddle.Var2 = matches[3];
                }
                else {
                    List<string> matches = line.QuickRegex(@"(.{4}): (\d+)");

                    riddle.Name = matches[0];
                    riddle.Answer = int.Parse(matches[1]);
                }

                riddles.Add(riddle);
            }

            // Don't set our own answer from the input
            riddles.First(r => r.Name == "humn").Answer = null;

            while (riddles.First(r => r.Name == "root").Answer == null) {
                List<MonkeyRiddle> solvedRiddles = riddles.Where(r => r.Answer != null).ToList();
                List<MonkeyRiddle> availableRiddles = riddles.Where(riddle => riddle.Answer == null && solvedRiddles.Any(r => r.Name == riddle.Var1) && solvedRiddles.Any(r => r.Name == riddle.Var2)).ToList();

                // Solve as many riddles as we can without picking a value
                if (!availableRiddles.Any()) {
                    break;
                }

                foreach (MonkeyRiddle riddle in availableRiddles) {
                    long value1 = (long)solvedRiddles.First(r => r.Name == riddle.Var1).Answer!;
                    long value2 = (long)solvedRiddles.First(r => r.Name == riddle.Var2).Answer!;

                    riddle.Answer = riddle.Op switch {
                        "+" => value1 + value2,
                        "-" => value1 - value2,
                        "/" => value1 / value2,
                        "*" => value1 * value2,
                        _ => 0 // Shouldn't happen
                    };
                }
            }

            // For this input, we have the value for 1 side of the equation, so now we can work backwards until we get the value of humn
            MonkeyRiddle root = riddles.First(r => r.Name == "root");
            MonkeyRiddle equal1 = riddles.First(r => r.Name == root.Var1);
            MonkeyRiddle equal2 = riddles.First(r => r.Name == root.Var2);
            
            if (equal1.Answer != null) {
                equal2.Answer = equal1.Answer;
            }
            else {
                equal1.Answer = equal2.Answer;
            }

            while(riddles.First(r => r.Name == "humn").Answer == null) {
                List<MonkeyRiddle> solvedRiddles = riddles.Where(r => r.Answer != null && r.Name != "humn").ToList();
                List<MonkeyRiddle> availableRiddles = riddles.Where(riddle => riddle.Answer != null && (solvedRiddles.Any(r => r.Name == riddle.Var1) ^ solvedRiddles.Any(r => r.Name == riddle.Var2))).ToList();

                foreach (MonkeyRiddle riddle in availableRiddles) {
                    MonkeyRiddle riddle1 = riddles.First(r => r.Name == riddle.Var1);
                    MonkeyRiddle riddle2 = riddles.First(r => r.Name == riddle.Var2);

                    if (riddle1.Answer == null) {
                        riddle1.Answer = riddle.Op switch {
                            "+" => riddle.Answer - riddle2.Answer,
                            "-" => riddle.Answer + riddle2.Answer,
                            "/" => riddle.Answer * riddle2.Answer,
                            "*" => riddle.Answer / riddle2.Answer,
                            _ => 0 // Shouldn't happen
                        };
                    }
                    else {
                        riddle2.Answer = riddle.Op switch {
                            "+" => riddle.Answer - riddle1.Answer,
                            "-" => riddle1.Answer - riddle.Answer,
                            "/" => riddle1.Answer / riddle.Answer,
                            "*" => riddle.Answer / riddle1.Answer,
                            _ => 0 // Shouldn't happen
                        };
                    }
                }
            }
        
            long answer = (long)riddles.First(r => r.Name == "humn").Answer!;

            return answer.ToString();
        }
    }
}