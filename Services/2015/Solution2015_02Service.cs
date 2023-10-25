namespace AdventOfCode.Services
{
    public class Solution2015_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            int total = 0;

            // Read file contents
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "02.txt"));
            string[] lines = data.Split("\n");
            foreach (string line in lines)
            {
                // Get dimensions of the box
                List<int> dimensions = line.Split('x').Select(int.Parse).ToList();

                int length = dimensions[0];
                int width = dimensions[1];
                int height = dimensions[2];

                // Find min area of side
                int minArea = new List<int>() { length * width, length * height, width * height }.Min();

                // Calculate the used surface area
                int surfaceArea = (2 * ((length * width) + (length * height) + (width * height))) + minArea;

                total += surfaceArea;
            }

            return total.ToString();
        }

        public string SecondHalf(bool example)
        {
            int total = 0;

            // Read file contents
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "02.txt"));
            string[] lines = data.Split("\n");
            foreach (string line in lines)
            {
                // Get dimensions of the box
                List<int> dimensions = line.Split('x').Select(int.Parse).ToList();

                int length = dimensions[0];
                int width = dimensions[1];
                int height = dimensions[2];

                // Find min perimeter of side
                int minPerimeter = new List<int>() { 2 * (length + width), 2 * (length + height), 2 * (width + height) }.Min();

                // Calculate the used surface area
                int surfaceArea = (length * width * height) + minPerimeter;

                total += surfaceArea;
            }

            return total.ToString();
        }
    }
}