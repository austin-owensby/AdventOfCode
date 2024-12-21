namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/21.txt
    public class Solution2024_21Service : ISolutionDayService
    {
        Dictionary<char, int> numPadRowMap = new(){
            {'A', 4},
            {'0', 4},
            {'1', 3},
            {'2', 3},
            {'3', 3},
            {'4', 2},
            {'5', 2},
            {'6', 2},
            {'7', 1},
            {'8', 1},
            {'9', 1}
        };

        Dictionary<char, int> numPadColMap = new(){
            {'A', 2},
            {'0', 1},
            {'1', 0},
            {'2', 1},
            {'3', 2},
            {'4', 0},
            {'5', 1},
            {'6', 2},
            {'7', 0},
            {'8', 1},
            {'9', 2}
        };

        Dictionary<char, int> dirPadRowMap = new(){
            {'^', 0},
            {'A', 0},
            {'<', 1},
            {'v', 1},
            {'>', 1}
        };

        Dictionary<char, int> dirPadColMap = new(){
            {'^', 1},
            {'A', 2},
            {'<', 0},
            {'v', 1},
            {'>', 2}
        };

        private List<char> GetNeededControlsForRobot1(char currentLocation, char desiredLocation) {
            if (currentLocation == desiredLocation) {
                return ['A'];
            }

            int currentRow = numPadRowMap[currentLocation];
            int desiredRow = numPadRowMap[desiredLocation];
            int currentCol = numPadColMap[currentLocation];
            int desiredCol = numPadColMap[desiredLocation];

            bool right = desiredCol > currentCol;
            bool down = desiredRow > currentRow;
            int horizontalDistance = Math.Abs(desiredCol - currentCol);
            int verticalDistance = Math.Abs(currentRow - desiredRow);

            if (verticalDistance == 0) {
                // Just move horizontally to the desired location
                return [..Enumerable.Repeat(right ? '>' : '<', horizontalDistance), 'A'];
            }

            if (horizontalDistance == 0) {
                // Just move vertically to the desired location
                return [..Enumerable.Repeat(down ? 'v' : '^', verticalDistance), 'A'];
            }

            if ((currentLocation == 'A' || currentLocation == '0') && desiredCol == 0) {
                // Move vertically first, then horizontally to avoid the gap
                return [..Enumerable.Repeat('^', verticalDistance), ..Enumerable.Repeat('<', horizontalDistance), 'A'];
            }

            if ((desiredLocation == 'A' || desiredLocation == '0') && currentCol == 0) {
                // Move horizontally first, then vertically to avoid the gap
                return [..Enumerable.Repeat('>', horizontalDistance), ..Enumerable.Repeat('v', verticalDistance), 'A'];
            }

            // Prioritize moving left because... (I don't know, it just works...)
            if (!right) {
                return [..Enumerable.Repeat('<', horizontalDistance), ..Enumerable.Repeat(down ? 'v' : '^', verticalDistance), 'A'];
            }
            
            return [..Enumerable.Repeat(down ? 'v' : '^', verticalDistance), ..Enumerable.Repeat(right ? '>' : '<', horizontalDistance), 'A'];
        }

        private List<char> GetNeededControlsForRobot2(char currentLocation, char desiredLocation) {
            if (currentLocation == desiredLocation) {
                return ['A'];
            }

            int currentRow = dirPadRowMap[currentLocation];
            int desiredRow = dirPadRowMap[desiredLocation];
            int currentCol = dirPadColMap[currentLocation];
            int desiredCol = dirPadColMap[desiredLocation];

            bool right = desiredCol > currentCol;
            bool down = desiredRow > currentRow;
            int horizontalDistance = Math.Abs(currentCol - desiredCol);
            int verticalDistance = Math.Abs(currentRow - desiredRow);

            if (verticalDistance == 0) {
                // Just move horizontally to the desired location
                return [..Enumerable.Repeat(right ? '>' : '<', horizontalDistance), 'A'];
            }

            if (horizontalDistance == 0) {
                // Just move vertically to the desired location
                return [..Enumerable.Repeat(down ? 'v' : '^', verticalDistance), 'A'];
            }

            if (currentLocation == '<') {
                // Move vertically first, then horizontally to avoid the gap
                return [..Enumerable.Repeat('>', horizontalDistance), '^', 'A'];
            }

            if (desiredLocation == '<') {
                // Move horizontally first, then vertically to avoid the gap
                return ['v', ..Enumerable.Repeat('<', horizontalDistance), 'A'];
            }

            // Prioritize moving left because... (I don't know, it just works...)
            if (!right) {
                return [..Enumerable.Repeat('<', horizontalDistance), ..Enumerable.Repeat(down ? 'v' : '^', verticalDistance), 'A'];
            }
            
            return [..Enumerable.Repeat(down ? 'v' : '^', verticalDistance), ..Enumerable.Repeat(right ? '>' : '<', horizontalDistance), 'A'];
        }

        private List<char> robots = [];
        Dictionary<(char, char, int), long> valueMap = [];

        private long GetRobotLength(List<char> goal, int robotIndex) {
            if (robotIndex == robots.Count) {
                return goal.Count;
            }

            long total = 0;

            foreach (char digit in goal) {
                char robot = robots[robotIndex];
                if (!valueMap.TryGetValue((robot, digit, robotIndex), out long value)) {
                    List<char> nextGoal = GetNeededControlsForRobot2(robot, digit);
                    value = GetRobotLength(nextGoal, robotIndex + 1);
                    valueMap[(robot, digit, robotIndex)] = value;
                }

                total += value;
                robots[robotIndex] = digit;
            }

            return total;
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 21, example);

            int answer = 0;

            char robot1 = 'A';
            char robot2 = 'A';
            char robot3 = 'A';

            foreach (string robot1Goal in lines) {
                int value = int.Parse(robot1Goal.TrimEnd('A'));
                foreach (char digit1 in robot1Goal) {
                    List<char> robot2Goal = GetNeededControlsForRobot1(robot1, digit1);
                    robot1 = digit1;

                    foreach (char digit2 in robot2Goal) {
                        List<char> robot3Goal = GetNeededControlsForRobot2(robot2, digit2);
                        robot2 = digit2;

                        foreach (char digit3 in robot3Goal) {
                            List<char> myGoal = GetNeededControlsForRobot2(robot3, digit3);
                            robot3 = digit3;

                            answer += myGoal.Count * value;
                        }
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 21, example);

            long answer = 0;

            robots = Enumerable.Repeat('A', 26).ToList();

            foreach (string robot1Goal in lines) {
                long value = long.Parse(robot1Goal.TrimEnd('A'));
                foreach (char digit1 in robot1Goal) {
                    List<char> robot2Goal = GetNeededControlsForRobot1(robots[0], digit1);
                    answer += value * GetRobotLength(robot2Goal, 1);
                    robots[0] = digit1;
                }
            }

            return answer.ToString();
        }
    }
}