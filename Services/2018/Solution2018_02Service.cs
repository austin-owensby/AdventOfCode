namespace AdventOfCode.Services
{
    public class Solution2018_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018", "02.txt"));

            int twiceCount = 0;
            int thriceCount = 0;

            foreach (string line in lines)
            {
                bool twoPairFound = false;
                bool threePairFound = false;

                foreach (int i in line.Length - 1)
                {
                    char letter = line[i];

                    if (!twoPairFound)
                    {
                        if (line.Count(l => l == letter) == 2)
                        {
                            twoPairFound = true;
                            twiceCount++;
                        }
                    }

                    if (!threePairFound)
                    {
                        if (line.Count(l => l == letter) == 3)
                        {
                            threePairFound = true;
                            thriceCount++;
                        }
                    }

                    if (twoPairFound && threePairFound)
                    {
                        break;
                    }
                }
            }

            int checksum = twiceCount * thriceCount;

            return checksum.ToString();
        }

        public string SecondHalf(bool example)
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018", "02.txt"));

            List<char> commonLetters = new();

            // Loop over each id
            foreach (int i in lines.Count() - 1)
            {
                // If this is not empty, we've found the answer and should stop
                if (commonLetters.Any())
                {
                    break;
                }

                // Loop over each id after the current one
                for (int j = i + 1; j < lines.Count(); j++)
                {
                    string id1 = lines[i];
                    string id2 = lines[j];

                    int numberOfDifferences = 0;

                    // Compare the two ids char by char
                    foreach (int k in id1.Count())
                    {
                        // If we've found more than 1 difference then this is not the answer, continue to the next pair of ids
                        if (numberOfDifferences > 1)
                        {
                            break;
                        }

                        if (id1[k] != id2[k])
                        {
                            numberOfDifferences++;
                        }
                        else
                        {
                            commonLetters.Add(id1[k]);
                        }
                    }

                    // If we have found exactly 1 difference, we have our answer and should stop
                    if (numberOfDifferences == 1)
                    {
                        break;
                    }

                    commonLetters = new();
                }
            }

            return string.Join("", commonLetters);
        }
    }
}
