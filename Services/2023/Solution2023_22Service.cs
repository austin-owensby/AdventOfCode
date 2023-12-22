namespace AdventOfCode.Services
{
    public class Solution2023_22Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 22, example);
            List<List<int>> bricks = lines.Select(line => line.QuickRegex(@"(\d+),(\d+),(\d+)~(\d+),(\d+),(\d+)").ToInts()).ToList();

            int maxZ = bricks.Max(brick => brick[5]);

            int answer = 0;

            // Move the bricks starting from the bottom
            for (int z = 2; z <= maxZ; z++)
            {
                List<List<int>> bricksToMove = bricks.Where(brick => brick[2] == z).ToList();

                foreach (List<int> brickToMove in bricksToMove) {
                    bool canDrop = true;
                    // Keep dropping the brick while we're able
                    while (canDrop) {
                        foreach (List<int> innerBrick in bricks.Where(brick => brick[5] == brickToMove[2] - 1)) {
                            // Check for a potential overlap on each brick
                            if (brickToMove[0] <= innerBrick[3] && brickToMove[1] <= innerBrick[4] && innerBrick[0] <= brickToMove[3] && innerBrick[1] <= brickToMove[4]) {
                                canDrop = false;
                                break;
                            }
                        }

                        if (canDrop) {
                            brickToMove[2]--;
                            brickToMove[5]--;
                            canDrop = brickToMove[2] > 1;
                        }
                    }
                }
            }

            // Test if we can remove each brick
            foreach (int i in bricks.Count) {
                List<List<int>> testBricks = bricks.Where((brick, index) => index != i).ToList();

                int z = bricks[i][5] + 1;
                List<List<int>> bricksToMove = testBricks.Where(brick => brick[2] == z).ToList();

                bool safe = true;

                foreach (List<int> brickToMove in bricksToMove) {
                    bool canDrop = true;
                    // Keep dropping the brick while we're able
                    while (canDrop) {
                        foreach (List<int> innerBrick in testBricks.Where(brick => brick[5] == brickToMove[2] - 1)) {
                            // Check for a potential overlap on each brick
                            if (brickToMove[0] <= innerBrick[3] && brickToMove[1] <= innerBrick[4] && innerBrick[0] <= brickToMove[3] && innerBrick[1] <= brickToMove[4]) {
                                canDrop = false;
                                break;
                            }
                        }

                        if (canDrop) {
                            safe = false;
                            break;
                        }
                    }

                    if (!safe){
                        break;
                    }
                }

                if (safe) {
                    answer++;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 22, example);
            List<List<int>> bricks = lines.Select(line => line.QuickRegex(@"(\d+),(\d+),(\d+)~(\d+),(\d+),(\d+)").ToInts()).ToList();

            int maxZ = bricks.Max(brick => brick[5]);

            long answer = 0;

            // Move the bricks starting from the bottom
            for (int z = 2; z <= maxZ; z++)
            {
                List<List<int>> bricksToMove = bricks.Where(brick => brick[2] == z).ToList();

                foreach (List<int> brickToMove in bricksToMove) {
                    bool canDrop = true;
                    // Keep dropping the brick while we're able
                    while (canDrop) {
                        foreach (List<int> innerBrick in bricks.Where(brick => brick[5] == brickToMove[2] - 1)) {
                            // Check for a potential overlap on each brick
                            if (brickToMove[0] <= innerBrick[3] && brickToMove[1] <= innerBrick[4] && innerBrick[0] <= brickToMove[3] && innerBrick[1] <= brickToMove[4]) {
                                canDrop = false;
                                break;
                            }
                        }

                        if (canDrop) {
                            brickToMove[2]--;
                            brickToMove[5]--;
                            canDrop = brickToMove[2] > 1;
                        }
                    }
                }
            }

            // Test the amount of movement from removing each brick
            foreach (int i in bricks.Count) {
                List<int> bricksMoved = [];
                List<List<int>> testBricks = bricks.Where((brick, index) => index != i).Select(brick => brick.ToList()).ToList();

                for (int z = bricks[i][5] + 1; z <= maxZ; z++)
                {
                    List<List<int>> bricksToMove = testBricks.Where(brick => brick[2] == z).ToList();

                    foreach (List<int> brickToMove in bricksToMove) {
                        bool canDrop = true;
                        // Keep dropping the brick while we're able
                        while (canDrop) {
                            foreach (List<int> innerBrick in testBricks.Where(brick => brick[5] == brickToMove[2] - 1)) {
                                // Check for a potential overlap on each brick
                                if (brickToMove[0] <= innerBrick[3] && brickToMove[1] <= innerBrick[4] && innerBrick[0] <= brickToMove[3] && innerBrick[1] <= brickToMove[4]) {
                                    canDrop = false;
                                    break;
                                }
                            }

                            if (canDrop) {
                                brickToMove[2]--;
                                brickToMove[5]--;
                                canDrop = brickToMove[2] > 1;

                                int brickIndex = testBricks.IndexOf(brickToMove);
                                if (!bricksMoved.Contains(brickIndex)) {
                                    bricksMoved.Add(brickIndex);
                                }
                            }
                        }
                    }
                }

                answer += bricksMoved.Count;
            }

            return answer.ToString();
        }
    }
}