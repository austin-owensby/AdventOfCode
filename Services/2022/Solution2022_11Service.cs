namespace AdventOfCode.Services
{
    public class Solution2022_11Service : ISolutionDayService
    {
                private class Monkey
        {
            public List<long> Items { get; set; } = new();
            public int? AddAmount { get; set; }
            public int? MultiplyAmount { get; set; }
            public bool Square { get; set; }
            public int divisorTest { get; set; }
            public int testPassMonkey { get; set; }
            public int testFailMonkey { get; set; }
            public int Inspects { get; set; }
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 11, example);

            List<Monkey> monkies = new();

            // Process monkey data
            foreach (List<string> monkeyData in lines.ChunkByExclusive(string.IsNullOrWhiteSpace))
            {
                Monkey monkey = new()
                {
                    Items = monkeyData[1].QuickRegex(@"Starting items: (.+)").First().Split(", ").ToLongs()
                };

                if (monkeyData[2].Contains("old * old"))
                {
                    monkey.Square = true;
                }
                else if (monkeyData[2].Contains("*"))
                {
                    monkey.MultiplyAmount = monkeyData[2].QuickRegex(@"old \* (\d+)").ToInts().First();
                }
                else if (monkeyData[2].Contains("+"))
                {
                    monkey.AddAmount = monkeyData[2].QuickRegex(@"old \+ (\d+)").ToInts().First();
                }

                monkey.divisorTest = monkeyData[3].QuickRegex(@"Test: divisible by (\d+)").ToInts().First();
                monkey.testPassMonkey = monkeyData[4].QuickRegex(@"If true: throw to monkey (\d+)").ToInts().First();
                monkey.testFailMonkey = monkeyData[5].QuickRegex(@"If false: throw to monkey (\d+)").ToInts().First();

                monkies.Add(monkey);
            }

            // Handle monkey business
            foreach (int i in 20)
            {
                foreach (Monkey monkey in monkies)
                {
                    foreach (long item in monkey.Items)
                    {
                        long newItem = item;

                        if (monkey.Square)
                        {
                            newItem = item * item;
                        }
                        else if (monkey.AddAmount != null)
                        {
                            newItem = item + (long)monkey.AddAmount;
                        }
                        else if (monkey.MultiplyAmount != null)
                        {
                            newItem = item * (long)monkey.MultiplyAmount;
                        }

                        newItem = (long)Math.Floor((double)newItem / 3);

                        if (newItem % monkey.divisorTest == 0)
                        {
                            monkies[monkey.testPassMonkey].Items.Add(newItem);
                        }
                        else
                        {
                            monkies[monkey.testFailMonkey].Items.Add(newItem);
                        }

                        monkey.Inspects++;
                    }

                    monkey.Items.Clear();
                }
            }

            List<int> values = monkies.Select(m => m.Inspects).OrderByDescending(x => x).Take(2).ToList();

            int answer = values[0] * values[1];

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 11, example);

            List<Monkey> monkies = new();

            // Process monkey data
            foreach (List<string> monkeyData in lines.ChunkByExclusive(string.IsNullOrWhiteSpace))
            {
                Monkey monkey = new()
                {
                    Items = monkeyData[1].QuickRegex(@"Starting items: (.+)").First().Split(", ").ToLongs()
                };

                if (monkeyData[2].Contains("old * old"))
                {
                    monkey.Square = true;
                }
                else if (monkeyData[2].Contains("*"))
                {
                    monkey.MultiplyAmount = monkeyData[2].QuickRegex(@"old \* (\d+)").ToInts().First();
                }
                else if (monkeyData[2].Contains("+"))
                {
                    monkey.AddAmount = monkeyData[2].QuickRegex(@"old \+ (\d+)").ToInts().First();
                }

                monkey.divisorTest = monkeyData[3].QuickRegex(@"Test: divisible by (\d+)").ToInts().First();
                monkey.testPassMonkey = monkeyData[4].QuickRegex(@"If true: throw to monkey (\d+)").ToInts().First();
                monkey.testFailMonkey = monkeyData[5].QuickRegex(@"If false: throw to monkey (\d+)").ToInts().First();

                monkies.Add(monkey);
            }

            long lcm = monkies.Select(m => m.divisorTest).Aggregate((a, b) => a * b);

            // Handle monkey business
            foreach (int i in 10000)
            {
                foreach (Monkey monkey in monkies)
                {
                    foreach (long item in monkey.Items)
                    {
                        long newItem = item;

                        if (monkey.Square)
                        {
                            newItem = item * item;
                        }
                        else if (monkey.AddAmount != null)
                        {
                            newItem = item + (long)monkey.AddAmount;
                        }
                        else if (monkey.MultiplyAmount != null)
                        {
                            newItem = item * (long)monkey.MultiplyAmount;
                        }

                        if (newItem % monkey.divisorTest == 0)
                        {
                            // TODO fully understand this
                            newItem %= lcm;
                            monkies[monkey.testPassMonkey].Items.Add(newItem);
                        }
                        else
                        {
                            // TODO fully understand this
                            newItem %= lcm;
                            monkies[monkey.testFailMonkey].Items.Add(newItem);
                        }

                        monkey.Inspects++;
                    }

                    monkey.Items.Clear();
                }
            }

            List<int> values = monkies.Select(m => m.Inspects).OrderByDescending(x => x).Take(2).ToList();

            long answer = values[0] * (long)values[1];

            return answer.ToString();
        }
    }
}