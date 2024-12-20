using System.Collections.Specialized;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/20.txt
    public class Solution2024_20Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 20, example);
            List<List<char>> grid = lines.ToGrid();

            Point start = new();

            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == 'S') {
                        start.Y = y;
                        start.X = x;
                    }
                }
            }

            // Get the initial track
            List<Point> track = [start];
            bool atEnd = false;
            Point previousPoint = new();
            Point currentPoint = start;

            while (!atEnd) {
                Point nextPoint = grid.GetNeighbors(currentPoint.X, currentPoint.Y).First(p => grid[p.Y][p.X] != '#' && !(p.X == previousPoint.X && p.Y == previousPoint.Y));

                atEnd = grid[nextPoint.Y][nextPoint.X] == 'E';

                track.Add(nextPoint);
                previousPoint = currentPoint;
                currentPoint = nextPoint;
            }

            int answer = 0;

            // Check shortcuts for each point
            for (int i = 0; i < track.Count; i++) {
                Point point = track[i];

                // Find the indexes on the track that result in cheats and are ahead in the track
                // This includes cheats that aren't actually cheats and go on the path, but the math will just calculate the improvement as 0
                List<int> cheats = track.FindIndexes(t => t.ManhattanDistance(point) == 2).Where(index => index > i).ToList();

                foreach (int cheat in cheats) {
                    // Calculate the improvement of the cheat
                    int score = cheat - i - 2;

                    if (score >= 100) {
                        answer++;
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 20, example);

            List<List<char>> grid = lines.ToGrid();

            Point start = new();

            foreach (int y in grid.Count) {
                foreach (int x in grid[y].Count) {
                    if (grid[y][x] == 'S') {
                        start.Y = y;
                        start.X = x;
                    }
                }
            }

            // Get the initial track
            List<Point> track = [start];
            bool atEnd = false;
            Point previousPoint = new();
            Point currentPoint = start;

            while (!atEnd) {
                Point nextPoint = grid.GetNeighbors(currentPoint.X, currentPoint.Y).First(p => grid[p.Y][p.X] != '#' && !(p.X == previousPoint.X && p.Y == previousPoint.Y));

                atEnd = grid[nextPoint.Y][nextPoint.X] == 'E';

                track.Add(nextPoint);
                previousPoint = currentPoint;
                currentPoint = nextPoint;
            }

            int answer = 0;

            Dictionary<int, int> map = [];

            for (int i = 0; i < track.Count; i++) {
                Point point = track[i];

                // Find the indexes on the track that result in cheats and are ahead in the track
                // This includes cheats that aren't actually cheats and go on the path, but the math will just calculate the improvement as 0
                List<int> cheats = track.FindIndexes(t => t.ManhattanDistance(point) <= 20).Where(index => index > i).ToList();

                foreach (int cheat in cheats) {
                    // Calculate the improvement of the cheat
                    Point cheatPoint = track[cheat];
                    int score = cheat - i - cheatPoint.ManhattanDistance(point);

                    if (score >= 100) {
                        answer++;
                    }
                }
            }

            return answer.ToString();
        }
    }
}