namespace AdventOfCode.Services
{
    public class Solution2018_05Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018", "05.txt"));
            data = data.Replace("\n", "").Replace("\r", "");

            bool keepLooping = true;

            while (keepLooping)
            {
                string filteredData = string.Empty;
                bool matchFound = false;
                bool matchRecentlyFound = false;

                char previousChar = char.MaxValue;

                foreach (char currentChar in data)
                {
                    // Check if we should eliminate a pair
                    if (previousChar != currentChar && char.ToLower(previousChar) == char.ToLower(currentChar))
                    {
                        matchFound = true;
                        matchRecentlyFound = true;

                        previousChar = char.MaxValue;
                        filteredData = filteredData.Remove(filteredData.Length - 1); // Remove the previous char
                    }
                    else if (matchRecentlyFound)
                    {
                        matchRecentlyFound = false;
                        previousChar = currentChar;
                        filteredData += currentChar;
                    }
                    else
                    {
                        previousChar = currentChar;
                        filteredData += currentChar;
                    }
                }

                // If there was no match found, stop looping
                keepLooping = matchFound;

                data = filteredData;
            }

            return data.Length.ToString();
        }

        public string SecondHalf(bool example)
        {
            string originalData = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018", "05.txt"));
            originalData = originalData.Replace("\n", "").Replace("\r", "");

            string letters = "abcdefghijklmnopqrstuvwxyz";

            int minValue = int.MaxValue;

            foreach (char letter in letters)
            {
                string data = new(originalData.Where(c => c != letter && c != char.ToUpper(letter)).ToArray());

                bool keepLooping = true;

                while (keepLooping)
                {
                    string filteredData = string.Empty;
                    bool matchFound = false;
                    bool matchRecentlyFound = false;

                    char previousChar = char.MaxValue;

                    foreach (char currentChar in data)
                    {
                        // Check if we should eliminate a pair
                        if (previousChar != currentChar && char.ToLower(previousChar) == char.ToLower(currentChar))
                        {
                            matchFound = true;
                            matchRecentlyFound = true;

                            previousChar = char.MaxValue;
                            filteredData = filteredData.Remove(filteredData.Length - 1); // Remove the previous char
                        }
                        else if (matchRecentlyFound)
                        {
                            matchRecentlyFound = false;
                            previousChar = currentChar;
                            filteredData += currentChar;
                        }
                        else
                        {
                            previousChar = currentChar;
                            filteredData += currentChar;
                        }
                    }

                    // If there was no match found, stop looping
                    keepLooping = matchFound;

                    data = filteredData;
                }

                if (data.Length < minValue)
                {
                    minValue = data.Length;
                }
            }

            return minValue.ToString();
        }
    }
}
