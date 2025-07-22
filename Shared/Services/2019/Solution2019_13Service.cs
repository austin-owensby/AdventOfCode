namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2019/13.txt
    public class Solution2019_13Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 13, example);
            Dictionary<long, long> data = IntCodeComputer.ParseData(lines);

            long answer = 0;
            List<long> output = [];
            
            long inputFunction() => throw new Exception("Unexpected input");
            void outputFunction(long xValue) => output.Add(xValue);

            IntCodeComputer computer = new(data, inputFunction, outputFunction);

            computer.RunComputer();

            // Material is the 3rd parameter and block is 2
            answer = output.Chunk(3).Count(c => c.Last() == 2);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            // Set to true to display the game
            bool display = false;

            List<string> lines = Utility.GetInputLines(2019, 13, example);
            Dictionary<long, long> data = IntCodeComputer.ParseData(lines);

            // Insert 2 quarters
            data[0] = 2;

            long answer = 0;
            List<long> output = [];

            long inputFunction()
            {
                List<long[]> values = output.Chunk(3).ToList();
                output = [];

                long paddleX = values.LastOrDefault(v => v[2] == 3)?[0] ?? 0;
                long ballX = values.LastOrDefault(v => v[2] == 4)?[0] ?? 0;

                if (display)
                {
                    long maxX = values.Select(v => v[0]).Max();
                    long maxY = values.Select(v => v[1]).Max();

                    Console.Clear();

                    // Build a new output so we don't have to store every output ever received
                    List<long> newOutput = [];

                    long score = values.LastOrDefault(v => v[0] == -1 && v[1] == 0)?[2] ?? 0;
                    newOutput.AddRange([-1, 0, score]);
                    Console.WriteLine($"Score: {score}");

                    for (long yCoordinate = 0; yCoordinate <= maxY; yCoordinate++)
                    {
                        string row = string.Empty;
                        for (long xCoordinate = 0; xCoordinate <= maxX; xCoordinate++)
                        {
                            long value = values.LastOrDefault(v => v[0] == xCoordinate && v[1] == yCoordinate)?[2] ?? 0;
                            newOutput.AddRange([xCoordinate, yCoordinate, value]);

                            row += value;
                        }

                        row = row.Replace('0', ' ').Replace('1', '█').Replace('2', '#').Replace('3', '-').Replace('4', 'O');
                        Console.WriteLine(row);
                    }

                    output = newOutput;

                    // Adjust this value as needed to smoothly render the output
                    Thread.Sleep(100);
                }

                // Move the paddle with the ball
                long input = 0;

                if (ballX < paddleX)
                {
                    input = -1;
                }
                else if (ballX > paddleX)
                {
                    input = 1;
                }

                return input;
            }

            void outputFunction(long xValue) => output.Add(xValue);

            IntCodeComputer computer = new(data, inputFunction, outputFunction);

            computer.RunComputer();

            // Output 1 last time to show the end state
            if (display) {
                List<long[]> values = output.Chunk(3).ToList();
                long maxX = values.Select(v => v[0]).Max();
                long maxY = values.Select(v => v[1]).Max();

                Console.Clear();

                // Build a new output so we don't have to store every output ever received
                List<long> newOutput = [];

                long score = values.LastOrDefault(v => v[0] == -1 && v[1] == 0)?[2] ?? 0;
                newOutput.AddRange([-1, 0, score]);
                Console.WriteLine($"Score: {score}");

                for (long yCoordinate = 0; yCoordinate <= maxY; yCoordinate++) {
                    string row = string.Empty;
                    for (long xCoordinate = 0; xCoordinate <= maxX; xCoordinate++) {
                        long value = values.LastOrDefault(v => v[0] == xCoordinate && v[1] == yCoordinate)?[2] ?? 0;
                        newOutput.AddRange([xCoordinate, yCoordinate, value]);

                        row += value;
                    }

                    row = row.Replace('0', ' ').Replace('1', '█').Replace('2','#').Replace('3','-').Replace('4', 'O');
                    Console.WriteLine(row);
                }
            }

            answer = output.Chunk(3).LastOrDefault(v => v[0] == -1 && v[1] == 0)?[2] ?? 0;

            return answer.ToString();
        }
    }
}