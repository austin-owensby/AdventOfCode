namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2015/09.txt
    public class Solution2015_09Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "09.txt"));

            List<string> lines = data.Split("\n").SkipLast(1).ToList();

            Dictionary<string, int> distances = lines.ToDictionary(d => d.Split(" = ")[0], d => int.Parse(d.Split(" = ")[1]));

            List<string> towns = new();
            lines.ForEach(line =>
            {
                string[] strings = line.Split(" ");
                towns.Add(strings[0]);
                towns.Add(strings[2]);
            });

            IEnumerable<string> townsDistinct = towns.Distinct();

            IEnumerable<IEnumerable<string>> permutations = townsDistinct.GetPermutations();

            int minDistance = int.MaxValue;
            foreach (IEnumerable<string> permutation in permutations)
            {
                int distance = 0;

                foreach (int i in permutation.Count() - 1)
                {
                    string key1 = $"{permutation.ElementAt(i)} to {permutation.ElementAt(i + 1)}";
                    string key2 = $"{permutation.ElementAt(i + 1)} to {permutation.ElementAt(i)}";

                    if (distances.ContainsKey(key1))
                    {
                        distance += distances[key1];
                    }
                    else
                    {
                        distance += distances[key2];
                    }
                }

                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            return minDistance.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "09.txt"));

            List<string> lines = data.Split("\n").SkipLast(1).ToList();

            Dictionary<string, int> distances = lines.ToDictionary(d => d.Split(" = ")[0], d => int.Parse(d.Split(" = ")[1]));

            List<string> towns = new();
            lines.ForEach(line =>
            {
                string[] strings = line.Split(" ");
                towns.Add(strings[0]);
                towns.Add(strings[2]);
            });

            IEnumerable<string> townsDistinct = towns.Distinct();

            IEnumerable<IEnumerable<string>> permutations = townsDistinct.GetPermutations();

            int maxDistance = 0;
            foreach (IEnumerable<string> permutation in permutations)
            {
                int distance = 0;

                foreach (int i in permutation.Count() - 1)
                {
                    string key1 = $"{permutation.ElementAt(i)} to {permutation.ElementAt(i + 1)}";
                    string key2 = $"{permutation.ElementAt(i + 1)} to {permutation.ElementAt(i)}";

                    if (distances.ContainsKey(key1))
                    {
                        distance += distances[key1];
                    }
                    else
                    {
                        distance += distances[key2];
                    }
                }

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }

            return maxDistance.ToString();
        }
    }
}
