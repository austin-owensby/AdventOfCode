namespace AdventOfCode.Services
{
    public class Solution2015_10Service : ISolutionDayService
    {
        public Solution2015_10Service() { }

        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "10.txt"));

            List<int> currentList = data.Split("\n")[0].Select(s => (int)char.GetNumericValue(s)).ToList();
            List<int> newList = new();
            int numberOfLoops = 40;

            for (int i = 0; i < numberOfLoops; i++)
            {
                int currentNumber = currentList[0];
                int currentLength = 0;

                for (int j = 0; j < currentList.Count; j++)
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

            for (int i = 0; i < numberOfLoops; i++)
            {
                int currentNumber = currentList[0];
                int currentLength = 0;

                for (int j = 0; j < currentList.Count; j++)
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
