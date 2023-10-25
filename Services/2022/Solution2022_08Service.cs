namespace AdventOfCode.Services
{
    public class Solution2022_08Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 08, example);

            List<List<int>> grid = lines.Select(l => l.ToInts()).ToList();

            List<List<Point>> points = new();

            for (int i = 0; i < grid.Count(); i++)
            {
                List<int> row = grid[i];
                List<Point> pointRow = new();

                for (int j = 0; j < row.Count(); j++)
                {
                    pointRow.Add(new()
                    {
                        X = i,
                        Y = j,
                        Value = row[j]
                    });
                }

                points.Add(pointRow);
            }

            List<Point> visibleTrees = new();

            foreach (List<Point> row in points)
            {
                int maxHeightSoFar = -1;

                foreach (Point cell in row)
                {
                    if (cell.Value > maxHeightSoFar)
                    {
                        visibleTrees.Add(cell);
                        maxHeightSoFar = cell.Value;
                    }
                }
            }

            foreach (List<Point> row in points)
            {
                int maxHeightSoFar = -1;

                row.Reverse();

                foreach (Point cell in row)
                {
                    if (cell.Value > maxHeightSoFar)
                    {
                        visibleTrees.Add(cell);
                        maxHeightSoFar = cell.Value;
                    }
                }
            }

            points = points.Pivot();

            foreach (List<Point> row in points)
            {
                int maxHeightSoFar = -1;

                foreach (Point cell in row)
                {
                    if (cell.Value > maxHeightSoFar)
                    {
                        visibleTrees.Add(cell);
                        maxHeightSoFar = cell.Value;
                    }
                }
            }

            foreach (List<Point> row in points)
            {
                int maxHeightSoFar = -1;

                row.Reverse();

                foreach (Point cell in row)
                {
                    if (cell.Value > maxHeightSoFar)
                    {
                        visibleTrees.Add(cell);
                        maxHeightSoFar = cell.Value;
                    }
                }
            }

            // 2185 is wrong
            // 1106 is wrong

            int answer = visibleTrees.DistinctBy(point => $"{point.X} {point.Y}").Count();

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 08, example);

            List<List<int>> grid = lines.Select(l => l.ToInts()).ToList();

            List<List<Point>> points = new();

            for (int i = 0; i < grid.Count(); i++)
            {
                List<int> row = grid[i];
                List<Point> pointRow = new();

                for (int j = 0; j < row.Count(); j++)
                {
                    pointRow.Add(new()
                    {
                        X = i,
                        Y = j,
                        Value = row[j]
                    });
                }

                points.Add(pointRow);
            }

            int answer = points.SelectMany(p => p).Max(point => GetScenicScore(grid, point));

            return answer.ToString();
        }

        private int GetScenicScore(List<List<int>> grid, Point point)
        {
            int score = 0;

            List<int> listA = grid[point.X].Skip(point.Y + 1).ToList();
            List<int> listA2 = listA.TakeWhile(p => p < point.Value).ToList();

            List<int> listB = grid[point.X].Take(point.Y).Reverse().ToList();
            List<int> listB2 = listB.TakeWhile(p => p < point.Value).ToList();

            int a = listA2.Count() + (listA2.Count < listA.Count ? 1 : 0);
            int b = listB2.Count() + (listB2.Count < listB.Count ? 1 : 0);

            grid = grid.Pivot();

            List<int> listC = grid[point.Y].Skip(point.X + 1).ToList();
            List<int> listC2 = listC.TakeWhile(p => p < point.Value).ToList();

            List<int> listD = grid[point.Y].Take(point.X).Reverse().ToList();
            List<int> listD2 = listD.TakeWhile(p => p < point.Value).ToList();

            int c = listC2.Count() + (listC2.Count < listC.Count ? 1 : 0);
            int d = listD2.Count() + (listD2.Count < listD.Count ? 1 : 0);

            score = a * b * c * d;

            return score;
        }
    }
}