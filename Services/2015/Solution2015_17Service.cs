namespace AdventOfCode.Services
{
    public class Solution2015_17Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "17.txt"));

            List<int> containers = data.Split("\n").SkipLast(1).Select(int.Parse).OrderBy(d => d).ToList();
            int targetTotal = 150;
            int totalCombinations = 0;

            for (int i = 0; i < Math.Pow(2, 20); i++)
            {
                int continerSum = 0;

                for (int j = 0; j < containers.Count; j++)
                {
                    int binaryPower = (int)Math.Pow(2, j);
                    if ((i & binaryPower) == binaryPower)
                    {
                        continerSum += containers[j];
                    }
                }

                if (continerSum == targetTotal)
                {
                    totalCombinations++;
                }
            }

            return totalCombinations.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "17.txt"));

            List<int> containers = data.Split("\n").SkipLast(1).Select(int.Parse).OrderBy(d => d).ToList();
            int targetTotal = 150;
            int totalCombinations = 0;
            int minRequiredContainers = int.MaxValue;

            for (int i = 0; i < Math.Pow(2, 20); i++)
            {
                int continerSum = 0;
                int containersUsed = 0;

                for (int j = 0; j < containers.Count; j++)
                {
                    int binaryPower = (int)Math.Pow(2, j);
                    if ((i & binaryPower) == binaryPower)
                    {
                        continerSum += containers[j];
                        containersUsed++;
                    }
                }

                if (continerSum == targetTotal)
                {
                    if (containersUsed < minRequiredContainers)
                    {
                        minRequiredContainers = containersUsed;
                        totalCombinations = 1;
                    }
                    else if (containersUsed == minRequiredContainers)
                    {
                        totalCombinations++;
                    }
                }
            }

            return totalCombinations.ToString();
        }
    }
}
