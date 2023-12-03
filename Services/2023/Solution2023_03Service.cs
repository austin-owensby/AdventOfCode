namespace AdventOfCode.Services
{
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
                    if (char.IsNumber(lines[y][x])) {
                        int charNumber = lines[y][x].ToInt();

                        if (parsedNumber == null) {
                            parsedNumber  = charNumber;
                        }
                        else {
                            parsedNumber = parsedNumber * 10 + charNumber;
                        }

                        if (!isPartNumber) {
                            // Check if this new char is a part number
                            if (y > 0) {
                                if (lines[y - 1][x] != '.' && !char.IsNumber(lines[y - 1][x])) {
                                    isPartNumber = true;
                                    continue;
                                }

                                if (x > 0) {
                                    if (lines[y - 1][x - 1] != '.' && !char.IsNumber(lines[y - 1][x - 1])) {
                                        isPartNumber = true;
                                        continue;
                                    }
                                }

                                if (x < lines[y].Length - 1) {
                                    if (lines[y - 1][x + 1] != '.' && !char.IsNumber(lines[y - 1][x + 1])) {
                                        isPartNumber = true;
                                        continue;
                                    }
                                }
                            }

                            if (y < lines.Count - 1) {
                                if (lines[y + 1][x] != '.' && !char.IsNumber(lines[y + 1][x])) {
                                    isPartNumber = true;
                                    continue;
                                }

                                if (x > 0) {
                                    if (lines[y + 1][x - 1] != '.' && !char.IsNumber(lines[y + 1][x - 1])) {
                                        isPartNumber = true;
                                        continue;
                                    }
                                }

                                if (x < lines[y].Length - 1) {
                                    if (lines[y + 1][x + 1] != '.' && !char.IsNumber(lines[y + 1][x + 1])) {
                                        isPartNumber = true;
                                        continue;
                                    }
                                }
                            }

                            if (x > 0) {
                                if (lines[y][x - 1] != '.' && !char.IsNumber(lines[y][x - 1])) {
                                    isPartNumber = true;
                                    continue;
                                }
                            }

                            if (x < lines.Count - 1) {
                                if (lines[y][x + 1] != '.' && !char.IsNumber(lines[y][x + 1])) {
                                    isPartNumber = true;
                                    continue;
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

            Dictionary<string, Tuple<int,int>> gears = new();

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
                bool isPartNumber = false;
                int? parsedNumber = null;
                string? gearKey = null;
                foreach (int x in lines[y].Length)
                {
                    if (char.IsNumber(lines[y][x])) {
                        int charNumber = lines[y][x].ToInt();

                        if (parsedNumber == null) {
                            parsedNumber  = charNumber;
                        }
                        else {
                            parsedNumber = parsedNumber * 10 + charNumber;
                        }

                        if (!isPartNumber && gearKey == null) {
                            // Check if this new char is a part number
                            if (y > 0) {
                                if (lines[y - 1][x] != '.' && !char.IsNumber(lines[y - 1][x])) {
                                    isPartNumber = true;

                                    if (lines[y - 1][x] == '*') {
                                        gearKey = $"{x},{y - 1}";
                                    }
                                }

                                if (x > 0) {
                                    if (lines[y - 1][x - 1] != '.' && !char.IsNumber(lines[y - 1][x - 1])) {
                                        isPartNumber = true;

                                        if (lines[y - 1][x - 1] == '*') {
                                            gearKey = $"{x - 1},{y - 1}";
                                        }
                                    }
                                }

                                if (x < lines[y].Length - 1) {
                                    if (lines[y - 1][x + 1] != '.' && !char.IsNumber(lines[y - 1][x + 1])) {
                                        isPartNumber = true;

                                        if (lines[y - 1][x + 1] == '*') {
                                            gearKey = $"{x + 1},{y - 1}";
                                        }
                                    }
                                }
                            }

                            if (y < lines.Count - 1) {
                                if (lines[y + 1][x] != '.' && !char.IsNumber(lines[y + 1][x])) {
                                    isPartNumber = true;

                                    if (lines[y + 1][x] == '*') {
                                        gearKey = $"{x},{y + 1}";
                                    }
                                }

                                if (x > 0) {
                                    if (lines[y + 1][x - 1] != '.' && !char.IsNumber(lines[y + 1][x - 1])) {
                                        isPartNumber = true;

                                        if (lines[y + 1][x - 1] == '*') {
                                            gearKey = $"{x - 1},{y + 1}";
                                        }
                                    }
                                }

                                if (x < lines[y].Length - 1) {
                                    if (lines[y + 1][x + 1] != '.' && !char.IsNumber(lines[y + 1][x + 1])) {
                                        isPartNumber = true;

                                        if (lines[y + 1][x + 1] == '*') {
                                            gearKey = $"{x + 1},{y + 1}";
                                        }
                                    }
                                }
                            }

                            if (x > 0) {
                                if (lines[y][x - 1] != '.' && !char.IsNumber(lines[y][x - 1])) {
                                    isPartNumber = true;

                                    if (lines[y][x - 1] == '*') {
                                        gearKey = $"{x - 1},{y}";
                                    }
                                }
                            }

                            if (x < lines.Count - 1) {
                                if (lines[y][x + 1] != '.' && !char.IsNumber(lines[y][x + 1])) {
                                    isPartNumber = true;

                                    if (lines[y][x + 1] == '*') {
                                        gearKey = $"{x + 1},{y}";
                                    }
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
                        isPartNumber = false;
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
                isPartNumber = false;
                parsedNumber = null;
            }

            int answer = gears.Sum(gear => gear.Value.Item1 * gear.Value.Item2);

            return answer.ToString();
        }
    }
}