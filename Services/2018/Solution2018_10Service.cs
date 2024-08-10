namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2018/10.txt
    public class Solution2018_10Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 10, example);
            List<Tuple<Point,Point>> data = lines.QuickRegex(@"position=<\ *([-\d]+),\ *([-\d]+)> velocity=<\ *([-\d]+),\ *([-\d]+)>").ToInts().Select(l => new Tuple<Point,Point>(new(l[0], l[1]), new(l[2], l[3]))).ToList();

            string answer = string.Empty;

            while (string.IsNullOrEmpty(answer)) {
                int minX = data.Min(d => d.Item1.X);
                int minY = data.Min(d => d.Item1.Y);
                int maxX = data.Max(d => d.Item1.X);
                int maxY = data.Max(d => d.Item1.Y);

                if (maxY - minY + 1 == 10) {
                    List<char> ascii = new();
                    for (int y = minY; y <= maxY; y++) {
                        for (int x = minX; x <= maxX; x++) {
                            ascii.Add(data.Any(d => d.Item1.X == x && d.Item1.Y == y) ? '#' : '.');
                        }
                    }

                    answer = Utility.ParseASCIILetters(ascii, 10);
                }

                foreach (Tuple<Point, Point> dataPoint in data) {
                    dataPoint.Item1.X += dataPoint.Item2.X;
                    dataPoint.Item1.Y += dataPoint.Item2.Y;
                }
            }

            return answer;
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 10, example);
            List<Tuple<Point,Point>> data = lines.QuickRegex(@"position=<\ *([-\d]+),\ *([-\d]+)> velocity=<\ *([-\d]+),\ *([-\d]+)>").ToInts().Select(l => new Tuple<Point,Point>(new(l[0], l[1]), new(l[2], l[3]))).ToList();

            int answer = 0;
            bool answerFound = false;

            while (!answerFound) {
                int minX = data.Min(d => d.Item1.X);
                int minY = data.Min(d => d.Item1.Y);
                int maxX = data.Max(d => d.Item1.X);
                int maxY = data.Max(d => d.Item1.Y);

                if (maxY - minY + 1 == 10) {
                    answerFound = true;
                    break;
                }

                foreach (Tuple<Point, Point> dataPoint in data) {
                    dataPoint.Item1.X += dataPoint.Item2.X;
                    dataPoint.Item1.Y += dataPoint.Item2.Y;
                }

                answer++;
            }

            // 10599 is too high
            return answer.ToString();
        }
    }
}