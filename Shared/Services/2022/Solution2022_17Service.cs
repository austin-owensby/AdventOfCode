namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2022/17.txt
    public class Solution2022_17Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 17, example);
            List<char> jets = lines.First().ToList();

            List<string> tower = new();

            List<Point> rock = new(){
                new() {
                    X = 2,
                    Y = tower.Count + 3,
                    Value = 0,
                },
                new() {
                    X = 3,
                    Y = tower.Count + 3,
                    Value = 0,
                },
                new() {
                    X = 4,
                    Y = tower.Count + 3,
                    Value = 0,
                },
                new() {
                    X = 5,
                    Y = tower.Count + 3,
                    Value = 0,
                }
            };
 
            int i = 0;
            int rockCount = 0;

            while (rockCount < 2022) {
                if (i % 2 == 0) {
                    // Rock moves
                    char movement = jets[(i / 2) % jets.Count];

                    // Check if the rock can move
                    if (movement == '>') {
                        // Check tower and wall
                        bool canMove = true;

                        foreach (Point point in rock) {
                            canMove &= point.X < 6;

                            if (canMove && tower.Count > point.Y) {
                                // We're within the height of the tower
                                canMove &= tower[point.Y][point.X + 1] != '#';
                            }

                            if (!canMove) {
                                break;
                            }
                        }

                        if (canMove) {
                            rock.ForEach(r => r.X++);
                        }
                    }
                    else {
                        // Check tower and wall
                        bool canMove = true;

                        foreach (Point point in rock) {
                            canMove &= point.X > 0;

                            if (canMove && tower.Count > point.Y) {
                                // We're within the height of the tower
                                canMove &= tower[point.Y][point.X - 1] != '#';
                            }

                            if (!canMove) {
                                break;
                            }
                        }

                        if (canMove) {
                            rock.ForEach(r => r.X--);
                        }
                    }
                }
                else {
                    // Rock drops
                    // Check if rock stops
                    bool rockStopped = rock
                        .Any(p => {
                            if (!tower.Any() && p.Y == 0) {
                                return true;
                            }
                            else if (tower.Count > p.Y - 1) {
                                return tower[p.Y - 1][p.X] == '#';
                            }
                            else {
                                return false;
                            }
                        });

                    if (rockStopped) {
                        rockCount++;
                        // Add rock to tower
                        foreach (Point p in rock) {
                            if (p.Y >= tower.Count) {
                                // Add a new row to the tower
                                tower.Add(".......");
                            }
                            if (p.Y >= tower.Count) {
                                // Add a new row to the tower
                                tower.Add(".......");
                            }
                            if (p.Y >= tower.Count) {
                                // Add a new row to the tower
                                tower.Add(".......");
                            }

                            char[] row = tower[p.Y].ToCharArray();
                            row[p.X] = '#';
                            tower[p.Y] = new string(row);
                        }

                        // Switch rocks
                        int rockType = (rock.First().Value + 1) % 5;
                        if (rockType == 0) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                },
                                new() {
                                    X = 5,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                }
                            };
                        }
                        else if (rockType == 1) {
                            rock = new(){
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 1,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 4,
                                    Value = 1,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 4,
                                    Value = 1,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 4,
                                    Value = 1,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 5,
                                    Value = 1,
                                }
                            };
                        }
                        else if (rockType == 2) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 2,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 2,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 3,
                                    Value = 2,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 4,
                                    Value = 2,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 5,
                                    Value = 2,
                                }
                            };
                        }
                        else if (rockType == 3) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 3,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 4,
                                    Value = 3,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 5,
                                    Value = 3,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 6,
                                    Value = 3,
                                },
                            };
                        }
                        else if (rockType == 4) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 4,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 4,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 4,
                                    Value = 4,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 4,
                                    Value = 4,
                                },
                            };
                        }
                    }
                    else {
                        // Move rock downwards
                        rock.ForEach(r => r.Y--);
                    }
                }
                i++;
            }

            int answer = tower.Count;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 17, example);
            List<char> jets = lines.First().ToList();

            long answer = 0;

            List<string> tower = new();

            List<Point> rock = new(){
                new() {
                    X = 2,
                    Y = tower.Count + 3,
                    Value = 0,
                },
                new() {
                    X = 3,
                    Y = tower.Count + 3,
                    Value = 0,
                },
                new() {
                    X = 4,
                    Y = tower.Count + 3,
                    Value = 0,
                },
                new() {
                    X = 5,
                    Y = tower.Count + 3,
                    Value = 0,
                }
            };

            int i = 0;
            int rockCount = 0;

            List<int> heights = new();
            List<int> rocksDropped = new();

            // Using 5 (Number of piece and jets count because that's how the input cycles)
            // We never end up with a flat line so we can't cycle after the first part
            // Anayalzing the results showed that every 2 cycles after the frist repeats
            while (i <= 5 * jets.Count * 3) {
                if (i % 2 == 0) {
                    // Rock moves
                    char movement = jets[(i / 2) % jets.Count];

                    // Check if the rock can move
                    if (movement == '>') {
                        // Check tower and wall
                        bool canMove = true;

                        foreach (Point point in rock) {
                            canMove &= point.X < 6;

                            if (canMove && tower.Count > point.Y) {
                                // We're within the height of the tower
                                canMove &= tower[point.Y][point.X + 1] != '#';
                            }

                            if (!canMove) {
                                break;
                            }
                        }

                        if (canMove) {
                            rock.ForEach(r => r.X++);
                        }
                    }
                    else {
                        // Check tower and wall
                        bool canMove = true;

                        foreach (Point point in rock) {
                            canMove &= point.X > 0;

                            if (canMove && tower.Count > point.Y) {
                                // We're within the height of the tower
                                canMove &= tower[point.Y][point.X - 1] != '#';
                            }

                            if (!canMove) {
                                break;
                            }
                        }

                        if (canMove) {
                            rock.ForEach(r => r.X--);
                        }
                    }
                }
                else {
                    // Rock drops
                    // Check if rock stops
                    bool rockStopped = rock
                        .Any(p => {
                            if (!tower.Any() && p.Y == 0) {
                                return true;
                            }
                            else if (tower.Count > p.Y - 1) {
                                return tower[p.Y - 1][p.X] == '#';
                            }
                            else {
                                return false;
                            }
                        });

                    if (rockStopped) {
                        rockCount++;
                        // Add rock to tower
                        foreach (Point p in rock) {
                            if (p.Y >= tower.Count) {
                                // Add a new row to the tower
                                tower.Add(".......");
                            }
                            if (p.Y >= tower.Count) {
                                // Add a new row to the tower
                                tower.Add(".......");
                            }
                            if (p.Y >= tower.Count) {
                                // Add a new row to the tower
                                tower.Add(".......");
                            }

                            char[] row = tower[p.Y].ToCharArray();
                            row[p.X] = '#';
                            tower[p.Y] = new string(row);
                        }

                        // Switch rocks
                        int rockType = (rock.First().Value + 1) % 5;
                        if (rockType == 0) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                },
                                new() {
                                    X = 5,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                }
                            };
                        }
                        else if (rockType == 1) {
                            rock = new(){
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 1,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 4,
                                    Value = 1,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 4,
                                    Value = 1,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 4,
                                    Value = 1,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 5,
                                    Value = 1,
                                }
                            };
                        }
                        else if (rockType == 2) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 2,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 2,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 3,
                                    Value = 2,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 4,
                                    Value = 2,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 5,
                                    Value = 2,
                                }
                            };
                        }
                        else if (rockType == 3) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 3,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 4,
                                    Value = 3,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 5,
                                    Value = 3,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 6,
                                    Value = 3,
                                },
                            };
                        }
                        else if (rockType == 4) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 4,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 4,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 4,
                                    Value = 4,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 4,
                                    Value = 4,
                                },
                            };
                        }
                    }
                    else {
                        // Move rock downwards
                        rock.ForEach(r => r.Y--);
                    }
                }
                
                if (i > 0 && i % (jets.Count * 5) == 0) {
                    heights.Add(tower.Count);
                    rocksDropped.Add(rockCount);
                }
                
                i++;
            }

            for (int j = heights.Count - 1; j > 0; j--) {
                heights[j] -= heights[j - 1];
            }

            for (int j = rocksDropped.Count - 1; j > 0; j--) {
                rocksDropped[j] -= rocksDropped[j - 1];
            }

            int cycleRockDropOffset = rocksDropped[0];
            int cycleHeightOffset = heights[0];
            int cycleHeight = heights[1] + heights[2];
            int cycleRockDrops = rocksDropped[1] + rocksDropped[2];

            long rockDropsRemaining = (1000000000000 - cycleRockDropOffset) % cycleRockDrops;

            answer += cycleHeightOffset;
            double numberOfCycles = (1000000000000 - cycleRockDropOffset) / cycleRockDrops;
            answer += (cycleHeight * (long)Math.Floor(numberOfCycles));
            
            int heightBefore = tower.Count;

            rockCount = 0;
            while (rockCount < rockDropsRemaining) {
                if (i % 2 == 0) {
                    // Rock moves
                    char movement = jets[(i / 2) % jets.Count];

                    // Check if the rock can move
                    if (movement == '>') {
                        // Check tower and wall
                        bool canMove = true;

                        foreach (Point point in rock) {
                            canMove &= point.X < 6;

                            if (canMove && tower.Count > point.Y) {
                                // We're within the height of the tower
                                canMove &= tower[point.Y][point.X + 1] != '#';
                            }

                            if (!canMove) {
                                break;
                            }
                        }

                        if (canMove) {
                            rock.ForEach(r => r.X++);
                        }
                    }
                    else {
                        // Check tower and wall
                        bool canMove = true;

                        foreach (Point point in rock) {
                            canMove &= point.X > 0;

                            if (canMove && tower.Count > point.Y) {
                                // We're within the height of the tower
                                canMove &= tower[point.Y][point.X - 1] != '#';
                            }

                            if (!canMove) {
                                break;
                            }
                        }

                        if (canMove) {
                            rock.ForEach(r => r.X--);
                        }
                    }
                }
                else {
                    // Rock drops
                    // Check if rock stops
                    bool rockStopped = rock
                        .Any(p => {
                            if (!tower.Any() && p.Y == 0) {
                                return true;
                            }
                            else if (tower.Count > p.Y - 1) {
                                return tower[p.Y - 1][p.X] == '#';
                            }
                            else {
                                return false;
                            }
                        });

                    if (rockStopped) {
                        rockCount++;
                        // Add rock to tower
                        foreach (Point p in rock) {
                            if (p.Y >= tower.Count) {
                                // Add a new row to the tower
                                tower.Add(".......");
                            }
                            if (p.Y >= tower.Count) {
                                // Add a new row to the tower
                                tower.Add(".......");
                            }
                            if (p.Y >= tower.Count) {
                                // Add a new row to the tower
                                tower.Add(".......");
                            }

                            char[] row = tower[p.Y].ToCharArray();
                            row[p.X] = '#';
                            tower[p.Y] = new string(row);
                        }

                        // Switch rocks
                        int rockType = (rock.First().Value + 1) % 5;
                        if (rockType == 0) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                },
                                new() {
                                    X = 5,
                                    Y = tower.Count + 3,
                                    Value = 0,
                                }
                            };
                        }
                        else if (rockType == 1) {
                            rock = new(){
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 1,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 4,
                                    Value = 1,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 4,
                                    Value = 1,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 4,
                                    Value = 1,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 5,
                                    Value = 1,
                                }
                            };
                        }
                        else if (rockType == 2) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 2,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 2,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 3,
                                    Value = 2,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 4,
                                    Value = 2,
                                },
                                new() {
                                    X = 4,
                                    Y = tower.Count + 5,
                                    Value = 2,
                                }
                            };
                        }
                        else if (rockType == 3) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 3,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 4,
                                    Value = 3,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 5,
                                    Value = 3,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 6,
                                    Value = 3,
                                },
                            };
                        }
                        else if (rockType == 4) {
                            rock = new(){
                                new() {
                                    X = 2,
                                    Y = tower.Count + 3,
                                    Value = 4,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 3,
                                    Value = 4,
                                },
                                new() {
                                    X = 2,
                                    Y = tower.Count + 4,
                                    Value = 4,
                                },
                                new() {
                                    X = 3,
                                    Y = tower.Count + 4,
                                    Value = 4,
                                },
                            };
                        }
                    }
                    else {
                        // Move rock downwards
                        rock.ForEach(r => r.Y--);
                    }
                }
                
                i++;
            }

            answer += (tower.Count - heightBefore);

            // 1541449275362 too low

            return answer.ToString();
        }
    }
}