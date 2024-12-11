namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/06.txt
    public class Solution2024_06Service : ISolutionDayService
    {
        private enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 6, example);
            List<List<char>> grid = lines.Select(l => l.ToList()).ToList();

            int startY = grid.FindIndex(g => g.Contains('^'));
            int startX = grid[startY].IndexOf('^');

            Direction direction = Direction.Up;
            Point guard = new(startX, startY);

            while (true) {
                grid[guard.Y][guard.X] = 'X';

                Point nextPoint = direction switch
                {
                    Direction.Up => new(guard.X, guard.Y - 1),
                    Direction.Right => new(guard.X + 1, guard.Y),
                    Direction.Down => new(guard.X, guard.Y + 1),
                    Direction.Left => new(guard.X - 1, guard.Y),
                    _ => throw new NotImplementedException()
                };

                // Check if we're out of bounds
                if (nextPoint.X < 0 || nextPoint.Y < 0 || nextPoint.X >= grid[0].Count || nextPoint.Y >= grid.Count) {
                    break;
                }

                // Check if we need to turn
                if (grid[nextPoint.Y][nextPoint.X] == '#') {
                    direction = (Direction)(((int)direction + 1) % 4);
                    // Don't take a step forward since we hit a wall
                    nextPoint = guard;
                }

                guard = nextPoint;
            }

            int answer = grid.SelectMany(x => x).Count(x => x == 'X');

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 6, example);
            List<List<char>> grid = lines.Select(l => l.ToList()).ToList();

            int startY = grid.FindIndex(g => g.Contains('^'));
            int startX = grid[startY].IndexOf('^');

            Direction direction = Direction.Up;
            Point guard = new(startX, startY);

            while (true) {
                grid[guard.Y][guard.X] = 'X';

                Point nextPoint = direction switch
                {
                    Direction.Up => new(guard.X, guard.Y - 1),
                    Direction.Right => new(guard.X + 1, guard.Y),
                    Direction.Down => new(guard.X, guard.Y + 1),
                    Direction.Left => new(guard.X - 1, guard.Y),
                    _ => throw new NotImplementedException()
                };

                // Check if we're out of bounds
                if (nextPoint.X < 0 || nextPoint.Y < 0 || nextPoint.X >= grid[0].Count || nextPoint.Y >= grid.Count) {
                    break;
                }

                // Check if we need to turn
                if (grid[nextPoint.Y][nextPoint.X] == '#') {
                    direction = (Direction)(((int)direction + 1) % 4);
                    // Don't take a step forward since we hit a wall
                    nextPoint = guard;
                }

                guard = nextPoint;
            }

            // Get the distinct list of visited points not including the start 
            List<Point> obstacleOptions = [];

            foreach (int y in grid.Count)
            {
                foreach (int x in grid[y].Count)
                {
                    if (y == startY && x == startX){
                        continue;
                    }

                    if (grid[y][x] == 'X')
                    {
                        obstacleOptions.Add(new Point(x, y));
                    }
                }
            }

            int answer = 0;

            // Loop over each potential obstacle
            foreach (Point obstacle in obstacleOptions) {

                direction = Direction.Up;
                guard = new(startX, startY)
                {
                    Value = (int)direction
                };

                List<Point> visitedPoints = [];

                while (true) {
                    // Check if we're in a loop
                    if (visitedPoints.Any(p => p.X == guard.X && p.Y == guard.Y && p.Value == guard.Value))
                    {
                        answer++;
                        break;
                    }
                
                    visitedPoints.Add(guard);

                    Point nextPoint = direction switch
                    {
                        Direction.Up => new(guard.X, guard.Y - 1),
                        Direction.Right => new(guard.X + 1, guard.Y),
                        Direction.Down => new(guard.X, guard.Y + 1),
                        Direction.Left => new(guard.X - 1, guard.Y),
                        _ => throw new NotImplementedException()
                    };

                    // Check if we're out of bounds
                    if (nextPoint.X < 0 || nextPoint.Y < 0 || nextPoint.X >= grid[0].Count || nextPoint.Y >= grid.Count) {
                        break;
                    }

                    // Check if we need to turn
                    if (grid[nextPoint.Y][nextPoint.X] == '#' || nextPoint.X == obstacle.X && nextPoint.Y == obstacle.Y) {
                        direction = (Direction)(((int)direction + 1) % 4);

                        // Don't take a step forward since we hit a wall
                        nextPoint.X = guard.X;
                        nextPoint.Y = guard.Y;
                    }

                    guard = new(nextPoint.X, nextPoint.Y){
                        Value = (int)direction
                    };
                }
            }

            return answer.ToString();
        }
    }
}