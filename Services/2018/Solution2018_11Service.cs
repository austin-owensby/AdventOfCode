namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2018/11.txt
    public class Solution2018_11Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 11, example);
            int serialNumber = lines.ToInts().First();

            List<List<int>> cells = new();

            for (int y = 1; y <= 300; y++) {
                List<int> row = new();
                for (int x = 1; x <= 300; x++) {
                    int rackId = x + 10;
                    int powerLevel = rackId * y;
                    powerLevel += serialNumber;
                    powerLevel *= rackId;
                    powerLevel = (powerLevel - powerLevel % 100) / 100 % 10;
                    powerLevel -= 5;

                    row.Add(powerLevel);
                }
                cells.Add(row);
            }

            int maxPowerLevel = 0;
            string answer = string.Empty;

            foreach (int y in 297) {
                foreach (int x in 297) {
                    int powerLevel = cells[y][x] + cells[y][x+1] + cells[y][x+2] + cells[y+1][x] + cells[y+1][x+1] + cells[y+1][x+2] + cells[y+2][x] + cells[y+2][x+1] + cells[y+2][x+2];

                    if (powerLevel > maxPowerLevel) {
                        maxPowerLevel = powerLevel;
                        answer = $"{x + 1},{y + 1}";
                    }
                }
            }

            return answer;
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 11, example);
            int serialNumber = lines.ToInts().First();

            List<List<int>> cells = new();

            for (int y = 1; y <= 300; y++) {
                List<int> row = new();
                for (int x = 1; x <= 300; x++) {
                    int rackId = x + 10;
                    int powerLevel = rackId * y;
                    powerLevel += serialNumber;
                    powerLevel *= rackId;
                    powerLevel = (powerLevel - powerLevel % 100) / 100 % 10;
                    powerLevel -= 5;

                    row.Add(powerLevel);
                }
                cells.Add(row);
            }

            int maxPowerLevel = 0;
            string answer = string.Empty;

            for (int size = 1; size <= 300; size++) {
                foreach (int y in 300 - size) {
                    foreach (int x in 300 - size) {
                        int powerLevel = 0;
                        foreach (int i in size) {
                            foreach (int j in size) {
                                powerLevel += cells[y + i][x + j];
                            }
                        }

                        if (powerLevel > maxPowerLevel) {
                            maxPowerLevel = powerLevel;
                            answer = $"{x + 1},{y + 1},{size}";
                        }
                    }
                }
            }

            return answer;
        }
    }
}