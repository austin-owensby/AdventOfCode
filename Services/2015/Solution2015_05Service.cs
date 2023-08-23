namespace AdventOfCode.Services
{
    public class Solution2015_05Service : ISolutionDayService
    {
        public Solution2015_05Service() { }

        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "05.txt"));
            string[] lines = data.Split("\n");
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            string[] invalidStrings = new string[] { "ab", "cd", "pq", "xy" };

            int niceCount = 0;

            foreach (string line in lines)
            {
                // Check for 3 vowels
                if (line.Count(c => vowels.Contains(c)) >= 3)
                {
                    // Check for 2 of the same char in a row
                    char previousChar = ' ';
                    bool duplicateFound = false;

                    foreach (char currentChar in line)
                    {
                        if (currentChar == previousChar)
                        {
                            duplicateFound = true;
                            break;
                        }

                        previousChar = currentChar;
                    }

                    if (duplicateFound)
                    {
                        bool invalidStringFound = false;
                        foreach (string invalidString in invalidStrings)
                        {
                            if (line.Contains(invalidString))
                            {
                                invalidStringFound = true;
                                break;
                            }
                        }

                        if (!invalidStringFound)
                        {
                            niceCount++;
                        }
                    }
                }
            }

            return niceCount.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "05.txt"));
            string[] lines = data.Split("\n");

            int niceCount = 0;

            foreach (string line in lines)
            {
                // Check for dupklicate letters spaced by 1
                bool duplicateFound = false;
                for (int i = 0; i < line.Length - 2; i++)
                {
                    if (line[i] == line[i + 2])
                    {
                        duplicateFound = true;
                        break;
                    }
                }

                if (duplicateFound)
                {
                    bool duplicatePairFound = false;
                    for (int i = 0; i < line.Length - 3; i++)
                    {
                        string currentString = $"{line[i]}{line[i + 1]}";

                        for (int j = i + 2; j < line.Length - 1; j++)
                        {
                            string comparedString = $"{line[j]}{line[j + 1]}";

                            if (currentString == comparedString)
                            {
                                duplicatePairFound = true;
                                break;
                            }
                        }

                        if (duplicatePairFound)
                        {
                            niceCount++;
                            break;
                        }
                    }
                }
            }

            return niceCount.ToString();
        }
    }
}
