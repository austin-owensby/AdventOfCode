namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2015/10.txt
    public class Solution2015_10Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "10.txt"));

            List<int> currentList = data.Split("\n")[0].Select(s => (int)char.GetNumericValue(s)).ToList();
            List<int> newList = new();
            int numberOfLoops = 40;

            foreach (int i in numberOfLoops)
            {
                int currentNumber = currentList[0];
                int currentLength = 0;

                foreach (int j in currentList.Count)
                {
                    int number = currentList[j];

                    if (currentNumber != number)
                    {
                        newList.Add(currentLength);
                        newList.Add(currentNumber);
                        currentNumber = number;
                        currentLength = 1;
                    }
                    else
                    {
                        currentLength++;
                    }

                    if (j == currentList.Count - 1)
                    {
                        newList.Add(currentLength);
                        newList.Add(number);
                    }
                }

                currentList = newList;
                newList = new();
            }

            return currentList.Count.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "10.txt"));

            List<int> currentList = data.Split("\n")[0].Select(s => (int)char.GetNumericValue(s)).ToList();
            List<int> newList = new();
            int numberOfLoops = 50;

            foreach (int i in numberOfLoops)
            {
                int currentNumber = currentList[0];
                int currentLength = 0;

                foreach (int j in currentList.Count)
                {
                    int number = currentList[j];

                    if (currentNumber != number)
                    {
                        newList.Add(currentLength);
                        newList.Add(currentNumber);
                        currentNumber = number;
                        currentLength = 1;
                    }
                    else
                    {
                        currentLength++;
                    }

                    if (j == currentList.Count - 1)
                    {
                        newList.Add(currentLength);
                        newList.Add(number);
                    }
                }

                currentList = newList;
                newList = new();
            }

            return currentList.Count.ToString();
        }
    }
}
