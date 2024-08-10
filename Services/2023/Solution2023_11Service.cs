namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2023/11.txt
    public class Solution2023_11Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 11, example);

            // Enumerate the galaxies
            List<Point> points = [];
            foreach (int y in lines.Count) {
                foreach (int x in lines[y].Length) {
                    if (lines[y][x] == '#') {
                        points.Add(new(x, y));
                    }
                }
            }

            // Expand the empty rows
            List<int> emptyRows = lines.FindIndexes(line => line.All(c => c == '.'));
            List<int> emptyColumns = lines.Pivot().FindIndexes(line => line.All(c => c == '.'));

            foreach (Point point in points) {
                point.Y -= emptyRows.Count(y => y > point.Y);
                point.X -= emptyColumns.Count(x => x > point.X);
            }

            int answer = 0;

            // Sum the distances between each combination
            foreach (int i in points.Count) {
                for (int j = i + 1; j < points.Count; j++) {
                    answer += Math.Abs(points[i].Y - points[j].Y) + Math.Abs(points[i].X - points[j].X);
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 11, example);

            // Enumerate the galaxies
            List<Point> points = [];
            foreach (int y in lines.Count) {
                foreach (int x in lines[y].Length) {
                    if (lines[y][x] == '#') {
                        points.Add(new(x, y));
                    }
                }
            }

            // Expand the empty rows
            List<int> emptyRows = lines.FindIndexes(line => line.All(c => c == '.'));
            List<int> emptyColumns = lines.Pivot().FindIndexes(line => line.All(c => c == '.'));

            int extraRows = 1000000 - 1;

            foreach (Point point in points) {
                point.Y -= extraRows * emptyRows.Count(y => y > point.Y);
                point.X -= extraRows * emptyColumns.Count(x => x > point.X);
            }

            long answer = 0;

            // Sum the distances between each combination
            foreach (int i in points.Count) {
                for (int j = i + 1; j < points.Count; j++) {
                    answer += Math.Abs(points[i].Y - points[j].Y) + Math.Abs(points[i].X - points[j].X);
                }
            }

            return answer.ToString();
        }
    }
}