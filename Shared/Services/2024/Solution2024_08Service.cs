namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/08.txt
    public class Solution2024_08Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 8, example);
            List<List<char>> grid = lines.Select(x => x.ToList()).ToList();
            List<char> frequencies = grid.SelectMany(x => x).Where(x => x != '.').Distinct().ToList();

            List<Point> antiNodes = [];

            foreach (char frequency in frequencies)
            {
                List<Point> points = [];

                foreach (int y in grid.Count) {
                    foreach (int x in grid.Count) {
                        if (grid[y][x] == frequency) {
                            points.Add(new Point(x,y));
                        }
                    }
                }

                List<List<int>> indexCombinations = Enumerable.Range(0, points.Count).GetCombinations(2).Select(x => x.ToList()).ToList();

                foreach (List<int> indexCombination in indexCombinations) {
                    Point pointA = points[indexCombination[0]];
                    Point pointB = points[indexCombination[1]];

                    int deltaX = pointA.X - pointB.X;
                    int deltaY = pointA.Y - pointB.Y;

                    int newX1 = pointA.X + deltaX;
                    int newY1 = pointA.Y + deltaY;

                    int newX2 = pointB.X - deltaX;
                    int newY2 = pointB.Y - deltaY;

                    if (newX1 >= 0 && newY1 >= 0 && newX1 < grid[0].Count && newY1 < grid.Count) {
                        antiNodes.Add(new Point(newX1, newY1));
                    }

                    if (newX2 >= 0 && newY2 >= 0 && newX2 < grid[0].Count && newY2 < grid.Count) {
                        antiNodes.Add(new Point(newX2, newY2));
                    }
                }
            }

            int answer = antiNodes.DistinctBy(p => $"{p.X},{p.Y}").Count();

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 8, example);
            List<List<char>> grid = lines.Select(x => x.ToList()).ToList();
            List<char> frequencies = grid.SelectMany(x => x).Where(x => x != '.').Distinct().ToList();

            List<Point> antiNodes = [];

            foreach (char frequency in frequencies)
            {
                List<Point> points = [];

                foreach (int y in grid.Count) {
                    foreach (int x in grid.Count) {
                        if (grid[y][x] == frequency) {
                            points.Add(new Point(x,y));
                        }
                    }
                }

                List<List<int>> indexCombinations = Enumerable.Range(0, points.Count).GetCombinations(2).Select(x => x.ToList()).ToList();

                foreach (List<int> indexCombination in indexCombinations) {
                    Point pointA = points[indexCombination[0]];
                    Point pointB = points[indexCombination[1]];

                    int deltaX = pointA.X - pointB.X;
                    int deltaY = pointA.Y - pointB.Y;

                    bool inBounds = true;
                    int newX1 = pointA.X;
                    int newY1 = pointA.Y;
                    antiNodes.Add(new Point(newX1, newY1));

                    while(inBounds) {
                        newX1 += deltaX;
                        newY1 += deltaY;

                        if (newX1 >= 0 && newY1 >= 0 && newX1 < grid[0].Count && newY1 < grid.Count) {
                            antiNodes.Add(new Point(newX1, newY1));
                        }
                        else {
                            inBounds = false;
                        }
                    }

                    inBounds = true;
                    int newX2 = pointB.X;
                    int newY2 = pointB.Y;
                    antiNodes.Add(new Point(newX2, newY2));

                    while(inBounds) {
                        newX2 -= deltaX;
                        newY2 -= deltaY;

                        if (newX2 >= 0 && newY2 >= 0 && newX2 < grid[0].Count && newY2 < grid.Count) {
                            antiNodes.Add(new Point(newX2, newY2));
                        }
                        else {
                            inBounds = false;
                        }
                    }
                }
            }

            int answer = antiNodes.DistinctBy(p => $"{p.X},{p.Y}").Count();

            return answer.ToString();
        }
    }
}