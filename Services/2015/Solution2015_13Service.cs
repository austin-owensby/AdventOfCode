namespace AdventOfCode.Services
{
    public class Solution2015_13Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "13.txt"));
            string[] lines = data.Split("\n");
            lines = lines.SkipLast(1).ToArray(); // Remove last newline

            // Build a dictionary with 2 keys, (firstPerson, secondPerson) and a value of the happiness
            Dictionary<string, Dictionary<string, int>> values = new();
            foreach (string line in lines)
            {
                string[] words = line.Split(" ");
                string firstPerson = words[0];
                string loseGain = words[2];
                string amountString = words[3];
                string secondPerson = words[^1].Split(".")[0];

                int amount = (loseGain == "lose" ? -1 : 1) * int.Parse(amountString);

                if (!values.ContainsKey(firstPerson))
                {
                    values[firstPerson] = new();
                }

                values[firstPerson][secondPerson] = amount;
            }

            // Get permutations with the first option fixed
            // If the 1st option was not fiexed we would get repeated permutations since we're in a ciruclar table
            string[] keys = values.Keys.Skip(1).ToArray();

            IEnumerable<IEnumerable<string>> permutations = keys.GetPermutations();

            List<List<string>> options = new();
            foreach (IEnumerable<string> permutation in permutations)
            {
                List<string> option = permutation.ToList();
                option.Insert(0, values.Keys.First());

                options.Add(option);
            }

            int maxHappiness = 0;

            foreach (List<string> option in options)
            {
                int netValue = 0;

                foreach (int i in option.Count - 1)
                {
                    string person1 = option[i];
                    string person2 = option[i + 1];
                    netValue += values[person1][person2] + values[person2][person1];

                    if (i == 0)
                    {
                        person2 = option[^1];
                        netValue += values[person1][person2] + values[person2][person1];
                    }
                }

                if (netValue > maxHappiness)
                {
                    maxHappiness = netValue;
                }
            }

            return maxHappiness.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "13.txt"));
            string[] lines = data.Split("\n");
            lines = lines.SkipLast(1).ToArray(); // Remove last newline

            // Build a dictionary with 2 keys, (firstPerson, secondPerson) and a value of the happiness
            Dictionary<string, Dictionary<string, int>> values = new();
            foreach (string line in lines)
            {
                string[] words = line.Split(" ");
                string firstPerson = words[0];
                string loseGain = words[2];
                string amountString = words[3];
                string secondPerson = words[^1].Split(".")[0];

                int amount = (loseGain == "lose" ? -1 : 1) * int.Parse(amountString);

                if (!values.ContainsKey(firstPerson))
                {
                    values[firstPerson] = new();
                }

                values[firstPerson][secondPerson] = amount;
            }

            // Get permutations with the first option fixed
            // If the 1st option was not fiexed we would get repeated permutations since we're in a ciruclar table
            string[] keys = values.Keys.Skip(1).ToArray();

            keys = keys.Append("Me").ToArray();

            IEnumerable<IEnumerable<string>> permutations = keys.GetPermutations();

            List<List<string>> options = new();
            foreach (IEnumerable<string> permutation in permutations)
            {
                List<string> option = permutation.ToList();
                option.Insert(0, values.Keys.First());

                options.Add(option);
            }

            int maxHappiness = 0;

            foreach (List<string> option in options)
            {
                int netValue = 0;

                foreach (int i in option.Count - 1)
                {
                    string person1 = option[i];
                    string person2 = option[i + 1];

                    if (person1 != "Me" && person2 != "Me")
                    {
                        netValue += values[person1][person2] + values[person2][person1];
                    }

                    if (i == 0)
                    {
                        person2 = option[^1];

                        if (person1 != "Me" && person2 != "Me")
                        {
                            netValue += values[person1][person2] + values[person2][person1];
                        }
                    }
                }

                if (netValue > maxHappiness)
                {
                    maxHappiness = netValue;
                }
            }

            return maxHappiness.ToString();
        }
    }
}
