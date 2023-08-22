namespace AdventOfCode.Services
{
    public class Solution2016_08Service : ISolutionDayService
    {
        public Solution2016_08Service() { }

        public string FirstHalf()
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2016", "08.txt")).ToList();

            List<List<string>> instructions = new();
            List<string> currentInstruction = new();

            foreach (string line in lines) {
                if (line.StartsWith("rect")) {
                    if (currentInstruction.Any()) {
                        instructions.Add(currentInstruction);
                        currentInstruction = new();
                    }
                }

                currentInstruction.Add(line);
            }
            instructions.Add(currentInstruction);

            int screenLength = 50;
            int screenHeight = 6;

            string screenString = new('.', screenLength * screenHeight);
            char[] screen = screenString.ToCharArray();

            foreach (List<string> instruction in instructions)
            { 
                List<int> coordinates = instruction.First().QuickRegex(@"rect (\d+)x(\d+)").ToInts();
                int width = coordinates.First();
                int height = coordinates.Last();

                for (int x = 0; x < width; x++) {
                    for (int y = 0; y < height; y++) {
                        screen[x + y * screenLength] = '#';
                    }
                }

                foreach (string step in instruction.Skip(1)) {
                    if (step.StartsWith("rotate row")) {
                        List<int> values = step.QuickRegex(@"rotate row y=(\d+) by (\d+)").ToInts();
                        int row = values.First();
                        int amount = values.Last();

                        List<int> indexes = screen.ToList().FindIndexes(x => x == '#').Where(i => i >= row * screenLength && i < (row + 1) * screenLength).ToList();

                        foreach (int index in indexes) {
                            screen[index] = '.';
                        }

                        foreach (int index in indexes) {
                            screen[(index + amount) % screenLength + screenLength * row] = '#';
                        }
                    }
                    else {
                        List<int> values = step.QuickRegex(@"rotate column x=(\d+) by (\d+)").ToInts();
                        int column = values.First();
                        int amount = values.Last();

                        List<int> indexes = screen.ToList().FindIndexes(x => x == '#').Where(i => i % screenLength == column).ToList();

                        foreach (int index in indexes) {
                            screen[index] = '.';
                        }

                        foreach (int index in indexes) {
                            screen[(index + amount * screenLength) % screen.Length] = '#';
                        }
                    }
                }
            }

            int answer = screen.Count(s => s == '#');

            return answer.ToString();
        }

        public string SecondHalf()
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2016", "08.txt")).ToList();

            List<List<string>> instructions = new();
            List<string> currentInstruction = new();

            foreach (string line in lines) {
                if (line.StartsWith("rect")) {
                    if (currentInstruction.Any()) {
                        instructions.Add(currentInstruction);
                        currentInstruction = new();
                    }
                }

                currentInstruction.Add(line);
            }
            instructions.Add(currentInstruction);

            int screenLength = 50;
            int screenHeight = 6;

            string screenString = new('.', screenLength * screenHeight);
            char[] screen = screenString.ToCharArray();

            foreach (List<string> instruction in instructions)
            { 
                List<int> coordinates = instruction.First().QuickRegex(@"rect (\d+)x(\d+)").ToInts();
                int width = coordinates.First();
                int height = coordinates.Last();

                for (int x = 0; x < width; x++) {
                    for (int y = 0; y < height; y++) {
                        screen[x + y * screenLength] = '#';
                    }
                }

                foreach (string step in instruction.Skip(1)) {
                    if (step.StartsWith("rotate row")) {
                        List<int> values = step.QuickRegex(@"rotate row y=(\d+) by (\d+)").ToInts();
                        int row = values.First();
                        int amount = values.Last();

                        List<int> indexes = screen.ToList().FindIndexes(x => x == '#').Where(i => i >= row * screenLength && i < (row + 1) * screenLength).ToList();

                        foreach (int index in indexes) {
                            screen[index] = '.';
                        }

                        foreach (int index in indexes) {
                            screen[(index + amount) % screenLength + screenLength * row] = '#';
                        }
                    }
                    else {
                        List<int> values = step.QuickRegex(@"rotate column x=(\d+) by (\d+)").ToInts();
                        int column = values.First();
                        int amount = values.Last();

                        List<int> indexes = screen.ToList().FindIndexes(x => x == '#').Where(i => i % screenLength == column).ToList();

                        foreach (int index in indexes) {
                            screen[index] = '.';
                        }

                        foreach (int index in indexes) {
                            screen[(index + amount * screenLength) % screen.Length] = '#';
                        }
                    }
                }
            }

            return Utility.ParseASCIILetters(screen, screenHeight);
        }
    }
}