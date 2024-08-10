namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2023/21.txt
    public class Solution2023_21Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 21, example);
            List<List<char>> grid = lines.Select(line => line.ToList()).ToList();
            int startY = grid.FindIndex(row => row.Contains('S'));
            int startX = grid[startY].FindIndex(cell => cell == 'S');

            int answer = 0;

            List<Point> points = [new(startX, startY)];

            foreach (int i in 64)
            {
                points = points.SelectMany(point => grid.GetNeighbors(point.X, point.Y).Where(neighbor => grid[neighbor.Y][neighbor.X] != '#')).DistinctBy(point => $"{point.X} {point.Y}").ToList();
            }

            answer = points.Count;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 21, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }
    }
}