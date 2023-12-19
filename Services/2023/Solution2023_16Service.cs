namespace AdventOfCode.Services
{
    public class Solution2023_16Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 16, example);
            List<List<char>> grid = lines.Select(line => line.ToList()).ToList();

            int answer = GetEnergizedCount(grid, new(0, 0));

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 16, example);
            List<List<char>> grid = lines.Select(line => line.ToList()).ToList();

            int answer = 0;

            foreach (int y in grid.Count)
            {
                answer = Math.Max(answer, GetEnergizedCount(grid, new(0, y)));
                answer = Math.Max(answer, GetEnergizedCount(grid, new(grid.First().Count - 1, y){Value = 2}));
            }

            foreach (int x in grid.First().Count) {
                answer = Math.Max(answer, GetEnergizedCount(grid, new(x, 0){Value = 1}));
                answer = Math.Max(answer, GetEnergizedCount(grid, new(x, grid.Count - 1){Value = 3}));
            }

            return answer.ToString();
        }

        private int GetEnergizedCount(List<List<char>> grid, Point startingPoint) {
            List<Point> history = [];
            List<Point> beams = [startingPoint];

            // 0 = Right
            // 1 = Down
            // 2 = Left
            // 3 = Up

            int answer = 0;

            while (beams.Count > 0) {
                List<Point> newBeams = [];

                foreach (Point beam in beams) {
                    history.Add(beam);

                    List<int> directionsToMove = [];

                    // Based on our current position and direction, determine which directions the beam moves
                    switch (grid[beam.Y][beam.X]) {
                        case '.':
                            directionsToMove.Add(beam.Value);
                            break;
                        case '/':
                            directionsToMove.Add(beam.Value switch
                            {
                                0 => 3,
                                1 => 2,
                                2 => 1,
                                3 => 0,
                                _ => throw new Exception()
                            });
                            break;
                        case '\\':
                            directionsToMove.Add(beam.Value switch
                            {
                                0 => 1,
                                1 => 0,
                                2 => 3,
                                3 => 2,
                                _ => throw new Exception()
                            });
                            break;
                        case '|':
                            directionsToMove.AddRange(beam.Value switch
                            {
                                0 => [1, 3],
                                1 => [1],
                                2 => [1, 3],
                                3 => [3],
                                _ => throw new Exception()
                            });
                            break;
                        case '-':
                            directionsToMove.AddRange(beam.Value switch
                            {
                                0 => [0],
                                1 => [0, 2],
                                2 => [2],
                                3 => [0, 2],
                                _ => throw new Exception()
                            });
                            break;
                    }
                    
                    List<Point> neighbors = grid.GetNeighbors(beam.X, beam.Y);

                    foreach (int directionToMove in directionsToMove) {
                        switch (directionToMove) {
                            case 0:
                                if (neighbors.Any(p => p.X == beam.X + 1 && p.Y == beam.Y)) {
                                    newBeams.Add(new(beam.X + 1, beam.Y){
                                        Value = 0
                                    });
                                }
                                break;
                            case 1:
                                if (neighbors.Any(p => p.X == beam.X && p.Y == beam.Y + 1)) {
                                    newBeams.Add(new(beam.X, beam.Y + 1){
                                        Value = 1
                                    });
                                }
                                break;
                            case 2:
                                if (neighbors.Any(p => p.X == beam.X - 1 && p.Y == beam.Y)) {
                                    newBeams.Add(new(beam.X - 1, beam.Y){
                                        Value = 2
                                    });
                                }
                                break;
                            case 3:
                                if (neighbors.Any(p => p.X == beam.X && p.Y == beam.Y - 1)) {
                                    newBeams.Add(new(beam.X, beam.Y - 1){
                                        Value = 3
                                    });
                                }
                                break;
                        }
                    }
                }

                // Filter out beams we've already seen since they won't produce anything new
                beams = newBeams.Where(newBeam => !history.Any(p => p.X == newBeam.X && p.Y == newBeam.Y && p.Value == newBeam.Value)).ToList();
            }

            answer = history.DistinctBy(h => h.X + h.Y * 1000).Count();
            return answer;
        }
    }
}