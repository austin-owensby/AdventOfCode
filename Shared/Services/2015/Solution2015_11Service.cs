namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2015/11.txt
    public class Solution2015_11Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "11.txt"));

            string currentPassword = data.Split('\n')[0];

            char[] invalidLetters = new char[] { 'i', 'o', 'l' };

            while (true)
            {
                currentPassword = IncrementPassword(currentPassword);

                // Cannot contain the letters 'i', 'o', or 'l'
                if (!currentPassword.Intersect(invalidLetters).Any())
                {
                    bool sequenceFound = false;

                    // Find a sequence of 3 consecuitve letters
                    foreach (int i in currentPassword.Length - 2)
                    {
                        if (currentPassword[i] + 1 == currentPassword[i + 1] && currentPassword[i] + 2 == currentPassword[i + 2])
                        {
                            sequenceFound = true;
                            break;
                        }
                    }

                    if (sequenceFound)
                    {
                        bool bothPairsFound = false;
                        bool firstPairFound = false;
                        int firstPairsIndex = 0;

                        // Find 2 pairs of duplciate letters
                        foreach (int i in currentPassword.Length - 1)
                        {
                            if (currentPassword[i] == currentPassword[i + 1])
                            {
                                if (firstPairFound)
                                {
                                    // Don't allow pairs to overlap
                                    if (i == firstPairsIndex + 1)
                                    {
                                        continue;
                                    }
                                    bothPairsFound = true;
                                }
                                else
                                {
                                    firstPairsIndex = i;
                                    firstPairFound = true;
                                }
                            }
                        }

                        if (bothPairsFound)
                        {
                            break;
                        }
                    }
                }
            }

            return currentPassword;
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "11.txt"));

            string currentPassword = data.Split('\n')[0];

            char[] invalidLetters = new char[] { 'i', 'o', 'l' };

            bool firstPasswordFound = false;

            while (true)
            {
                currentPassword = IncrementPassword(currentPassword);

                // Cannot contain the letters 'i', 'o', or 'l'
                if (!currentPassword.Intersect(invalidLetters).Any())
                {
                    bool sequenceFound = false;

                    // Find a sequence of 3 consecuitve letters
                    foreach (int i in currentPassword.Length - 2)
                    {
                        if (currentPassword[i] + 1 == currentPassword[i + 1] && currentPassword[i] + 2 == currentPassword[i + 2])
                        {
                            sequenceFound = true;
                            break;
                        }
                    }

                    if (sequenceFound)
                    {
                        bool bothPairsFound = false;
                        bool firstPairFound = false;
                        int firstPairsIndex = 0;

                        // Find 2 pairs of duplciate letters
                        foreach (int i in currentPassword.Length - 1)
                        {
                            if (currentPassword[i] == currentPassword[i + 1])
                            {
                                if (firstPairFound)
                                {
                                    // Don't allow pairs to overlap
                                    if (i == firstPairsIndex + 1)
                                    {
                                        continue;
                                    }
                                    bothPairsFound = true;
                                }
                                else
                                {
                                    firstPairsIndex = i;
                                    firstPairFound = true;
                                }
                            }
                        }

                        if (bothPairsFound)
                        {
                            // Find the second valid password
                            if (firstPasswordFound)
                            {
                                break;
                            }
                            else
                            {
                                firstPasswordFound = true;
                            }
                        }
                    }
                }
            }

            return currentPassword;
        }


        private string IncrementPassword(string input)
        {
            int[] array = input.ToCharArray().Select(c => (int)c).ToArray();

            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (array[i] == 'z')
                {
                    array[i] = 'a';
                }
                else
                {
                    ++array[i];
                    break;
                }
            }

            return new string(array.Select(a => (char)a).ToArray());
        }
    }
}
