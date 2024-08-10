namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2022/09.txt
    public class Solution2022_09Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 09, example);

            int answer = 0;

            List<Point> pointsVisited = new();

            Point headCoordinate = new()
            {
                X = 0,
                Y = 0
            };

            Point tailCoordinate = new()
            {
                X = 0,
                Y = 0
            };

            pointsVisited.Add(new()
            {
                X = 0,
                Y = 0
            });

            foreach (string line in lines)
            {
                List<string> instruction = line.QuickRegex(@"(.) (\d+)");

                string direction = instruction[0];
                int amount = int.Parse(instruction[1]);

                foreach (int i in amount)
                {
                    if (direction == "U")
                    {
                        headCoordinate.Y++;
                    }
                    else if (direction == "D")
                    {
                        headCoordinate.Y--;
                    }
                    else if (direction == "R")
                    {
                        headCoordinate.X++;
                    }
                    else if (direction == "L")
                    {
                        headCoordinate.X--;
                    }

                    int yDiff = headCoordinate.Y - tailCoordinate.Y;
                    int xDiff = headCoordinate.X - tailCoordinate.X;

                    if (yDiff == 2 || (yDiff == 1 && Math.Abs(xDiff) == 2))
                    {
                        // Tail needs to move up
                        tailCoordinate.Y++;
                    }
                    else if (yDiff == -2 || (yDiff == -1 && Math.Abs(xDiff) == 2))
                    {
                        // Tail needs to move down
                        tailCoordinate.Y--;
                    }

                    if (xDiff == 2 || (xDiff == 1 && Math.Abs(yDiff) == 2))
                    {
                        // Tail needs to move right
                        tailCoordinate.X++;
                    }
                    else if (xDiff == -2 || (xDiff == -1 && Math.Abs(yDiff) == 2))
                    {
                        // Tail needs to move left
                        tailCoordinate.X--;
                    }

                    pointsVisited.Add(new()
                    {
                        X = tailCoordinate.X,
                        Y = tailCoordinate.Y
                    });
                }
            }

            answer = pointsVisited.DistinctBy(p => $"{p.X} {p.Y}").Count();

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 09, example);

            int answer = 0;

            List<Point> pointsVisited = new();

            List<Point> rope = new(){
                new(){
                    X = 0,
                    Y = 0
                },
                new(){
                    X = 0,
                    Y = 0
                },
                new(){
                    X = 0,
                    Y = 0
                },
                new(){
                    X = 0,
                    Y = 0
                },
                new(){
                    X = 0,
                    Y = 0
                },
                new(){
                    X = 0,
                    Y = 0
                },
                new(){
                    X = 0,
                    Y = 0
                },
                new(){
                    X = 0,
                    Y = 0
                },
                new(){
                    X = 0,
                    Y = 0
                },
                new(){
                    X = 0,
                    Y = 0
                },
            };

            pointsVisited.Add(new()
            {
                X = 0,
                Y = 0
            });

            foreach (string line in lines)
            {
                List<string> instruction = line.QuickRegex(@"(.) (\d+)");

                string direction = instruction[0];
                int amount = int.Parse(instruction[1]);

                // Move the head of the rope
                Point headCoordinate = rope.First();

                foreach (int i in amount)
                {
                    if (direction == "U")
                    {
                        headCoordinate.Y++;
                    }
                    else if (direction == "D")
                    {
                        headCoordinate.Y--;
                    }
                    else if (direction == "R")
                    {
                        headCoordinate.X++;
                    }
                    else if (direction == "L")
                    {
                        headCoordinate.X--;
                    }

                    // Move the rest of the rope
                    for (int j = 1; j < rope.Count; j++)
                    {
                        Point head = rope[j - 1];
                        Point tail = rope[j];

                        int yDiff = head.Y - tail.Y;
                        int xDiff = head.X - tail.X;

                        if (yDiff == 2 || (yDiff == 1 && Math.Abs(xDiff) == 2))
                        {
                            // Tail needs to move up
                            tail.Y++;
                        }
                        else if (yDiff == -2 || (yDiff == -1 && Math.Abs(xDiff) == 2))
                        {
                            // Tail needs to move down
                            tail.Y--;
                        }

                        if (xDiff == 2 || (xDiff == 1 && Math.Abs(yDiff) == 2))
                        {
                            // Tail needs to move right
                            tail.X++;
                        }
                        else if (xDiff == -2 || (xDiff == -1 && Math.Abs(yDiff) == 2))
                        {
                            // Tail needs to move left
                            tail.X--;
                        }
                    }

                    Point tailCoordinate = rope.Last();

                    pointsVisited.Add(new()
                    {
                        X = tailCoordinate.X,
                        Y = tailCoordinate.Y
                    });
                }
            }

            answer = pointsVisited.DistinctBy(p => $"{p.X} {p.Y}").Count();

            return answer.ToString();
        }
    }
}