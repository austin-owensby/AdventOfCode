namespace AdventOfCode.Services
{
    public class Solution2015_06Service : ISolutionDayService
    {
        public Solution2015_06Service() { }

        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "06.txt"));
            string[] lines = data.Split("\n");

            bool[,] lightArray = new bool[1000, 1000];

            foreach (string line in lines)
            {
                // Parse the instruction operation and coordinates
                string[] instruction = line.Split(" ");

                if (instruction.Length < 4)
                {
                    continue; // This is probably the newline at the end of the file
                }

                string operation = instruction[^4];
                string[] firstCoordinates = instruction[^3].Split(',');
                string[] secondCoordinates = instruction[^1].Split(',');

                int x1 = int.Parse(firstCoordinates[0]);
                int y1 = int.Parse(firstCoordinates[1]);
                int x2 = int.Parse(secondCoordinates[0]);
                int y2 = int.Parse(secondCoordinates[1]);

                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        lightArray[x, y] = operation switch
                        {
                            "on" => true,
                            "off" => false,
                            "toggle" => !lightArray[x, y],
                            _ => false
                        };
                    }
                }
            }

            int lightOnCount = 0;

            for (int x = 0; x < lightArray.GetLength(0); x++)
            {
                for (int y = 0; y < lightArray.GetLength(1); y++)
                {
                    if (lightArray[x, y])
                    {
                        lightOnCount++;
                    }
                }
            }

            return lightOnCount.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "06.txt"));
            string[] lines = data.Split("\n");

            int[,] lightArray = new int[1000, 1000];

            foreach (string line in lines)
            {
                // Parse the instruction operation and coordinates
                string[] instruction = line.Split(" ");

                if (instruction.Length < 4)
                {
                    continue; // This is probably the newline at the end of the file
                }

                string operation = instruction[^4];
                string[] firstCoordinates = instruction[^3].Split(',');
                string[] secondCoordinates = instruction[^1].Split(',');

                int x1 = int.Parse(firstCoordinates[0]);
                int y1 = int.Parse(firstCoordinates[1]);
                int x2 = int.Parse(secondCoordinates[0]);
                int y2 = int.Parse(secondCoordinates[1]);

                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        lightArray[x, y] = operation switch
                        {
                            "on" => lightArray[x, y] + 1,
                            "off" => Math.Max(lightArray[x, y] - 1, 0),
                            "toggle" => lightArray[x, y] + 2,
                            _ => 0
                        };
                    }
                }
            }

            int totalBrightness = 0;

            for (int x = 0; x < lightArray.GetLength(0); x++)
            {
                for (int y = 0; y < lightArray.GetLength(1); y++)
                {
                    totalBrightness += lightArray[x, y];
                }
            }

            return totalBrightness.ToString();
        }
    }
}
