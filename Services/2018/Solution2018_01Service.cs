namespace AdventOfCode.Services
{
    public class Solution2018_01Service : ISolutionDayService
    {
        public Solution2018_01Service() { }

        public string FirstHalf()
        {
            string[] data = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2018_01.txt"));
            List<int> changes = data.Select(int.Parse).ToList();

            int resultingFrequency = changes.Sum();

            return resultingFrequency.ToString();
        }

        public string SecondHalf()
        {
            string[] data = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2018_01.txt"));
            List<int> changes = data.Select(int.Parse).ToList();

            List<int> reachedFrequencies = new();
            int currentFrequency = 0;
            bool duplicateFrequencyFound = false;

            while (!duplicateFrequencyFound)
            {
                foreach (int change in changes)
                {
                    if (reachedFrequencies.Contains(currentFrequency))
                    {
                        duplicateFrequencyFound = true;
                        break;
                    }

                    reachedFrequencies.Add(currentFrequency);

                    currentFrequency += change;
                }
            }

            return currentFrequency.ToString();
        }
    }
}
