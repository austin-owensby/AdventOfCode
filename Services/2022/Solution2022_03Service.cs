namespace AdventOfCode.Services
{
    public class Solution2022_03Service : ISolutionDayService
    {
        public Solution2022_03Service() { }

        public string FirstHalf()
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2022", "03.txt")).ToList();

            int sum = lines.Sum(line =>
            {
                List<char> firstHalf = line.Take(line.Length / 2).ToList();
                List<char> secondHalf = line.TakeLast(line.Length / 2).ToList();

                char itemType = firstHalf.Intersect(secondHalf).First();

                return itemType.GetCharValue();
            });

            return sum.ToString();
        }

        public string SecondHalf()
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2022", "03.txt")).ToList();

            int sum = lines.Chunk(3).Sum(c =>
            {
                char itemType = c[0].Intersect(c[1]).Intersect(c[2]).First();

                return itemType.GetCharValue();
            });

            return sum.ToString();
        }
    }
}