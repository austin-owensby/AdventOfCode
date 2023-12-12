namespace AdventOfCode.Services
{
    public class Solution2023_10Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 10, example);

            List<Point> path = [];
            int y = lines.FindIndex(line => line.Contains('S'));
            int x = lines[y].ToList().FindIndex(x => x == 'S');
            path.Add(new Point(x, y));

            int answer = 0;

            while (path.Count == 1 || path.Count > 1 && !(path.First().X == path.Last().X && path.First().Y == path.Last().Y)) {
                Point currentPoint = path.Last();
                char currentValue = lines[currentPoint.Y][currentPoint.X];
                List<Point> neighbors = lines.Select(line => line.ToList()).ToList().GetNeighbors(currentPoint.X, currentPoint.Y);

                Point previousPoint = path.Count == 1 ? currentPoint : path.TakeLast(2).First();
                foreach (Point neighbor in neighbors) {
                    char neightborValue = lines[neighbor.Y][neighbor.X];
                    if (!(previousPoint.X == neighbor.X && previousPoint.Y == neighbor.Y)) {
                        if (neighbor.Y - 1 == currentPoint.Y && neighbor.X == currentPoint.X) {
                            List<char> moveDownValues = ['J', 'L', '|', 'S'];
                            List<char> canMoveDownValues = ['|', 'F', '7', 'S'];
                            if (moveDownValues.Contains(neightborValue) && canMoveDownValues.Contains(currentValue)) {
                                path.Add(neighbor);
                                break;
                            }
                        }
                        else if (neighbor.Y == currentPoint.Y && neighbor.X - 1 == currentPoint.X) {
                            List<char> moveRightValues = ['J', '7', '-', 'S'];
                            List<char> canMoveRightValues = ['-', 'F', 'L', 'S'];
                            if (moveRightValues.Contains(neightborValue) && canMoveRightValues.Contains(currentValue)) {
                                path.Add(neighbor);
                                break;
                            }
                        }
                        else if (neighbor.Y + 1 == currentPoint.Y && neighbor.X == currentPoint.X) {
                            List<char> moveUpValues = ['F', '7', '|', 'S'];
                            List<char> canMoveUpValues = ['|', 'J', 'L', 'S'];
                            if (moveUpValues.Contains(neightborValue) && canMoveUpValues.Contains(currentValue)) {
                                path.Add(neighbor);
                                break;
                            }
                        }
                        else if (neighbor.Y == currentPoint.Y && neighbor.X + 1 == currentPoint.X) {
                            List<char> moveLeftValues = ['L', 'F', '-', 'S'];
                            List<char> canMoveLeftValues = ['-', 'J', '7', 'S'];
                            if (moveLeftValues.Contains(neightborValue) && canMoveLeftValues.Contains(currentValue)) {
                                path.Add(neighbor);
                                break;
                            }
                        }
                    }
                }
            }

            answer = (path.Count - 1) / 2;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 10, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }
    }
}