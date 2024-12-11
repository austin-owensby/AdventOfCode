namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2015/16.txt
    public class Solution2015_16Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "16.txt"));

            string[] lines = data.Split("\n").SkipLast(1).ToArray();

            Dictionary<string, int> mfcsam = new(){
                {"children:", 3},
                {"cats:", 7},
                {"samoyeds:", 2},
                {"pomeranians:", 3},
                {"akitas:", 0},
                {"vizslas:", 0},
                {"goldfish:", 5},
                {"trees:", 3},
                {"cars:", 2},
                {"perfumes:", 1}
            };

            int correctAunt = 0;

            foreach (string line in lines)
            {
                string[] words = line.Split(" ");

                string word = words[2];
                int value = int.Parse(words[3].Split(",")[0]);

                if (mfcsam[word] == value)
                {
                    word = words[4];
                    value = int.Parse(words[5].Split(",")[0]);

                    if (mfcsam[word] == value)
                    {
                        word = words[6];
                        value = int.Parse(words[7]);

                        if (mfcsam[word] == value)
                        {
                            correctAunt = int.Parse(words[1].Split(":")[0]);
                        }
                    }
                }
            }

            return correctAunt.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "16.txt"));

            string[] lines = data.Split("\n").SkipLast(1).ToArray();

            Dictionary<string, int> mfcsam = new(){
                {"children:", 3},
                {"cats:", 7},
                {"samoyeds:", 2},
                {"pomeranians:", 3},
                {"akitas:", 0},
                {"vizslas:", 0},
                {"goldfish:", 5},
                {"trees:", 3},
                {"cars:", 2},
                {"perfumes:", 1}
            };

            int correctAunt = 0;

            foreach (string line in lines)
            {
                string[] words = line.Split(" ");

                string word = words[2];
                int value = int.Parse(words[3].Split(",")[0]);

                if (word is "cats:" or "tress:")
                {
                    if (mfcsam[word] >= value)
                    {
                        continue;
                    }
                }
                else if (word is "pomeranians:" or "goldfish:")
                {
                    if (mfcsam[word] <= value)
                    {
                        continue;
                    }
                }
                else
                {
                    if (mfcsam[word] != value)
                    {
                        continue;
                    }
                }

                word = words[4];
                value = int.Parse(words[5].Split(",")[0]);

                if (word is "cats:" or "tress:")
                {
                    if (mfcsam[word] >= value)
                    {
                        continue;
                    }
                }
                else if (word is "pomeranians:" or "goldfish:")
                {
                    if (mfcsam[word] <= value)
                    {
                        continue;
                    }
                }
                else
                {
                    if (mfcsam[word] != value)
                    {
                        continue;
                    }
                }


                word = words[6];
                value = int.Parse(words[7]);

                if (word is "cats:" or "tress:")
                {
                    if (mfcsam[word] >= value)
                    {
                        continue;
                    }
                }
                else if (word is "pomeranians:" or "goldfish:")
                {
                    if (mfcsam[word] <= value)
                    {
                        continue;
                    }
                }
                else
                {
                    if (mfcsam[word] != value)
                    {
                        continue;
                    }
                }

                correctAunt = int.Parse(words[1].Split(":")[0]);
            }

            return correctAunt.ToString();
        }
    }
}
