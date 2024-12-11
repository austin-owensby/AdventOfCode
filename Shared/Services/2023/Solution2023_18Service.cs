using System.Linq;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2023/18.txt
    public class Solution2023_18Service : ISolutionDayService
    {
        private class LongPoint {
            public long X { get; set; }
            public long Y { get; set; }
            public long Value { get; set; }

            public LongPoint() {}

            public LongPoint (long x, long y) {
                X = x;
                Y = y;
            }
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 18, example);

            Point currentVertex = new();
            List<Point> vertexes = [];
            int perimeter = 0;

            // Calculate the vertexes of the outline
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                char direction = parts.First()[0];
                int length = int.Parse(parts[1]);
                perimeter += length;

                currentVertex = direction switch {
                    'R' => new(currentVertex.X + length, currentVertex.Y),
                    'D' => new(currentVertex.X, currentVertex.Y + length) { Value = 3 },
                    'L' => new(currentVertex.X - length, currentVertex.Y),
                    'U' => new(currentVertex.X, currentVertex.Y - length) { Value = 1 },
                    _ => throw new Exception()
                };

                if (direction == 'D' || direction == 'U') {
                    Point previousPoint = vertexes.Last();
                    previousPoint.Value = currentVertex.Value == 1 ? 3 : 1;
                }

                vertexes.Add(currentVertex);
            }

            // Calculate the number of interior points with the shoelace formula
            int maxY = vertexes.MaxBy(v => v.Y)!.Y;
            int interiorArea = 0;

            vertexes.ReverseInPlace();

            foreach (int i in vertexes.Count) {
                Point thisPoint = vertexes[i];
                Point nextPoint = i == vertexes.Count - 1 ? vertexes[0] : vertexes[i + 1];

                // Shoelace formula
                interiorArea += thisPoint.X * (maxY - nextPoint.Y) - (maxY - thisPoint.Y) * nextPoint.X;
            }

            interiorArea /= 2;

            // Pick's theorem
            int answer = interiorArea + perimeter / 2 + 1;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 18, example);

            LongPoint currentVertex = new();
            List<LongPoint> vertexes = [];
            long perimeter = 0;

            // Calculate the vertexes of the outline
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                char direction = parts.Last().TakeLast(2).First();
                string hexString = new string(parts.Last().Skip(1).SkipLast(2).ToArray()).Replace("#", "0x");
                int length = Convert.ToInt32(hexString, 16);
                perimeter += length;

                currentVertex = direction switch {
                    '0' => new(currentVertex.X + length, currentVertex.Y),
                    '1' => new(currentVertex.X, currentVertex.Y + length) { Value = 3 },
                    '2' => new(currentVertex.X - length, currentVertex.Y),
                    '3' => new(currentVertex.X, currentVertex.Y - length) { Value = 1 },
                    _ => throw new Exception()
                };

                if (direction == 'D' || direction == 'U') {
                    LongPoint previousPoint = vertexes.Last();
                    previousPoint.Value = currentVertex.Value == 1 ? 3 : 1;
                }

                vertexes.Add(currentVertex);
            }
            

            // Calculate the number of interior points with the shoelace formula
            long maxY = vertexes.MaxBy(v => v.Y)!.Y;
            long interiorArea = 0;

            vertexes.ReverseInPlace();

            foreach (int i in vertexes.Count) {
                LongPoint thisPoint = vertexes[i];
                LongPoint nextPoint = i == vertexes.Count - 1 ? vertexes[0] : vertexes[i + 1];

                // Shoelace formula
                interiorArea += thisPoint.X * (maxY - nextPoint.Y) - (maxY - thisPoint.Y) * nextPoint.X;
            }

            interiorArea /= 2;

            // Pick's theorem
            long answer = interiorArea + perimeter / 2 + 1;

            return answer.ToString();
        }
    }
}