namespace AdventOfCode.Services
{
    public class Solution2018_03Service : ISolutionDayService
    {
        public Solution2018_03Service() { }

        public async Task<string> FirstHalf(bool send)
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2018_03.txt"));

            int totalSideLength = 1000;

            int[] usedFabric = new int[totalSideLength * totalSideLength];

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');
                string startString = splitLine[2];
                string dimensionString = splitLine[3];

                int startX = int.Parse(startString.Split(',')[0]);
                int startY = int.Parse(startString.Split(',')[1].Split(':')[0]);
                int length = int.Parse(dimensionString.Split('x')[0]);
                int width = int.Parse(dimensionString.Split('x')[1]);

                for (int x = startX; x < startX + length; x++)
                {
                    for (int y = startY; y < startY + width; y++)
                    {
                        usedFabric[totalSideLength * x + y]++;
                    }
                }
            }

            int duplicateSquares = usedFabric.Count(f => f > 1);

            return await Task.FromResult($"The number of squares with two or more claims is {duplicateSquares}.");
        }

        public async Task<string> SecondHalf(bool send)
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2018_03.txt"));

            int totalSideLength = 1000;

            int[] usedFabric = new int[totalSideLength * totalSideLength];

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');
                string startString = splitLine[2];
                string dimensionString = splitLine[3];

                int startX = int.Parse(startString.Split(',')[0]);
                int startY = int.Parse(startString.Split(',')[1].Split(':')[0]);
                int length = int.Parse(dimensionString.Split('x')[0]);
                int width = int.Parse(dimensionString.Split('x')[1]);

                for (int x = startX; x < startX + length; x++)
                {
                    for (int y = startY; y < startY + width; y++)
                    {
                        usedFabric[totalSideLength * x + y]++;
                    }
                }
            }

            int idWithNoOverlap = 0;

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(' ');
                string startString = splitLine[2];
                string dimensionString = splitLine[3];

                int startX = int.Parse(startString.Split(',')[0]);
                int startY = int.Parse(startString.Split(',')[1].Split(':')[0]);
                int length = int.Parse(dimensionString.Split('x')[0]);
                int width = int.Parse(dimensionString.Split('x')[1]);

                bool noOverlap = true;

                for (int x = startX; x < startX + length; x++)
                {
                    for (int y = startY; y < startY + width; y++)
                    {
                        noOverlap = usedFabric[totalSideLength * x + y] == 1;

                        if (!noOverlap)
                        {
                            break;
                        }
                    }

                    if (!noOverlap)
                    {
                        break;
                    }
                }

                if (noOverlap)
                {
                    idWithNoOverlap = int.Parse(splitLine[0].Split('#')[1]);
                    break;
                }
            }

            return await Task.FromResult($"The only ID without overlap is {idWithNoOverlap}");
        }
    }
}
