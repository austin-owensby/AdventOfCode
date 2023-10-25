namespace AdventOfCode.Services
{
    public class Solution2018_01Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string[] data = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018", "01.txt"));
            List<int> changes = data.Select(int.Parse).ToList();

            int resultingFrequency = changes.Sum();

            return resultingFrequency.ToString();
        }

        public string SecondHalf(bool example)
        {
            string[] data = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018", "01.txt"));
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
