namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2023/03.txt
    public class Solution2023_03Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 3, example);

            int answer = 0;

            foreach (int y in lines.Count)
            {
                bool isPartNumber = false;
                int? parsedNumber = null;
                foreach (int x in lines[y].Length)
                {
                    if (char.IsDigit(lines[y][x])) {
                        int charNumber = lines[y][x].ToInt();

                        if (parsedNumber == null) {
                            parsedNumber = charNumber;
                        }
                        else {
                            parsedNumber = parsedNumber * 10 + charNumber;
                        }

                        if (!isPartNumber) {
                            // Check if this new char is a part number
                            List<Point> neighbors = lines.Select(line => line.ToList()).ToList().GetNeighbors(x, y, true);

                            foreach (Point neighbor in neighbors) {
                                char value = lines[neighbor.Y][neighbor.X];
                                if (value != '.' && !char.IsDigit(value)) {
                                    isPartNumber = true;
                                    break;
                                }
                            }
                        }
                    }
                    else {
                        if (isPartNumber) {
                            answer += parsedNumber!.Value;
                            isPartNumber = false;
                        }
                        parsedNumber = null;
                    }
                }

                // Add number if it was at the end of the line
                if (isPartNumber) {
                    answer += parsedNumber!.Value;
                    isPartNumber = false;
                    parsedNumber = null;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 3, example);

            Dictionary<string, Tuple<int,int>> gears = [];

            foreach (int y in lines.Count) {
                foreach (int x in lines[y].Length)
                {
                    if (lines[y][x] == '*') {
                        gears[$"{x},{y}"] = new Tuple<int, int>(0, 0);
                    }
                }
            }

            foreach (int y in lines.Count)
            {
                int? parsedNumber = null;
                string? gearKey = null;
                foreach (int x in lines[y].Length)
                {
                    if (char.IsDigit(lines[y][x])) {
                        int charNumber = lines[y][x].ToInt();

                        if (parsedNumber == null) {
                            parsedNumber  = charNumber;
                        }
                        else {
                            parsedNumber = parsedNumber * 10 + charNumber;
                        }

                        if (gearKey == null) {
                            // Check if this new char is adjacent to a gear
                            List<Point> neighbors = lines.Select(line => line.ToList()).ToList().GetNeighbors(x, y, true);

                            foreach (Point neighbor in neighbors) {
                                char value = lines[neighbor.Y][neighbor.X];
                                if (lines[neighbor.Y][neighbor.X] == '*') {
                                    gearKey = $"{neighbor.X},{neighbor.Y}";
                                    break;
                                }
                            }
                        }
                    }
                    else {
                        if (gearKey != null) {
                            if (gears[gearKey].Item1 == 0) {
                                gears[gearKey] = new Tuple<int, int>(parsedNumber!.Value, 0);
                            }
                            else {
                                gears[gearKey] = new Tuple<int, int>(gears[gearKey].Item1, parsedNumber!.Value);
                            }
                        }
                        gearKey = null;
                        parsedNumber = null;
                    }
                }

                if (gearKey != null) {
                    if (gears[gearKey].Item1 == 0) {
                        gears[gearKey] = new Tuple<int, int>(parsedNumber!.Value, 0);
                    }
                    else {
                        gears[gearKey] = new Tuple<int, int>(gears[gearKey].Item1, parsedNumber!.Value);
                    }
                }

                gearKey = null;
                parsedNumber = null;
            }

            int answer = gears.Select(gear => gear.Value).Sum(gear => gear.Item1 * gear.Item2);

            return answer.ToString();
        }
    }
}