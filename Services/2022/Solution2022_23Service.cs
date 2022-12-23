namespace AdventOfCode.Services
{
    public class Elf {
        public int X {get; set;}
        public int Y {get; set;}
        public int? SuggestedX {get; set;}
        public int? SuggestedY {get; set;}
    }

    public class Solution2022_23Service : ISolutionDayService
    {
        public Solution2022_23Service() { }

        private enum Direction {
            North,
            South,
            West,
            East
        }

        public async Task<string> FirstHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2022_23.txt")).ToList();

            List<Elf> elves = new();

            for (int y = 0; y < lines.Count; y++) {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++) {
                    if (line[x] == '#') {
                        elves.Add(new(){
                            X = x,
                            Y = y,
                        });
                    }
                }
            }

            Direction direction = Direction.North;

            // Check answer after 10 rounds
            for (int round = 0; round < 10; round++) {
                foreach (Elf elf in elves) {
                    // Reset old suggestion
                    elf.SuggestedX = null;
                    elf.SuggestedY = null;

                    bool canMoveNorth = !elves.Any(e => !(e.X == elf.X && e.Y == elf.Y) && Math.Abs(e.X - elf.X) <= 1 && e.Y == elf.Y - 1);
                    bool canMoveSouth = !elves.Any(e => !(e.X == elf.X && e.Y == elf.Y) && Math.Abs(e.X - elf.X) <= 1 && e.Y == elf.Y + 1);
                    bool canMoveWest = !elves.Any(e => !(e.X == elf.X && e.Y == elf.Y) && Math.Abs(e.Y - elf.Y) <= 1 && e.X == elf.X - 1);
                    bool canMoveEast = !elves.Any(e => !(e.X == elf.X && e.Y == elf.Y) && Math.Abs(e.Y - elf.Y) <= 1 && e.X == elf.X + 1);

                    // Don't make a suggestion if there's no adjacent elves
                    if (!(canMoveNorth && canMoveSouth && canMoveEast && canMoveWest)) {
                        // Make a suggestion based on what's available
                        switch (direction) {
                            case Direction.North:
                                // Check N, S, W, E
                                if (canMoveNorth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y - 1;
                                }
                                else if (canMoveSouth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y + 1;
                                }
                                else if (canMoveWest) {
                                    elf.SuggestedX = elf.X - 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveEast) {
                                    elf.SuggestedX = elf.X + 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                break;
                            case Direction.South:
                                // Check S, W, E, N
                                if (canMoveSouth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y + 1;
                                }
                                else if (canMoveWest) {
                                    elf.SuggestedX = elf.X - 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveEast) {
                                    elf.SuggestedX = elf.X + 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveNorth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y - 1;
                                }
                                break;
                            case Direction.West:
                                // Check W, E, N, S
                                if (canMoveWest) {
                                    elf.SuggestedX = elf.X - 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveEast) {
                                    elf.SuggestedX = elf.X + 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveNorth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y - 1;
                                }
                                else if (canMoveSouth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y + 1;
                                }
                                break;
                            case Direction.East:
                                // Check E, N, S, W
                                if (canMoveEast) {
                                    elf.SuggestedX = elf.X + 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveNorth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y - 1;
                                }
                                else if (canMoveSouth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y + 1;
                                }
                                else if (canMoveWest) {
                                    elf.SuggestedX = elf.X - 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                break;
                        }
                    }
                }

                // Move elves if possible
                List<Elf> moveableElves = elves.Where(e => e.SuggestedX != null && e.SuggestedY != null && !elves.Any(elf => !(e.X == elf.X && e.Y == elf.Y) && e.SuggestedX == elf.SuggestedX && e.SuggestedY == elf.SuggestedY)).ToList();

                foreach (Elf elf in moveableElves) {
                    elf.X = (int)elf.SuggestedX;
                    elf.Y = (int)elf.SuggestedY;
                }

                // Update priority direction
                direction = (Direction)Utility.Mod((int)direction + 1, 4);
            }

            int minX = elves.Min(e => e.X);
            int minY = elves.Min(e => e.Y);
            int maxX = elves.Max(e => e.X);
            int maxY = elves.Max(e => e.Y);

            // Get the number of empty spaces in the bounding rectangle
            int answer = (maxX - minX + 1)*(maxY - minY + 1) - elves.Count;

            return await Utility.SubmitAnswer(2022, 23, false, answer, send);
        }

        public async Task<string> SecondHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2022_23.txt")).ToList();

            List<Elf> elves = new();

            for (int y = 0; y < lines.Count; y++) {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++) {
                    if (line[x] == '#') {
                        elves.Add(new(){
                            X = x,
                            Y = y,
                        });
                    }
                }
            }

            Direction direction = Direction.North;

            int answer = 0;
            bool elvesMoving = true;

            // Check answer after 10 rounds
            while (elvesMoving) {
                answer++;
                
                foreach (Elf elf in elves) {
                    // Reset old suggestion
                    elf.SuggestedX = null;
                    elf.SuggestedY = null;

                    bool canMoveNorth = !elves.Any(e => !(e.X == elf.X && e.Y == elf.Y) && Math.Abs(e.X - elf.X) <= 1 && e.Y == elf.Y - 1);
                    bool canMoveSouth = !elves.Any(e => !(e.X == elf.X && e.Y == elf.Y) && Math.Abs(e.X - elf.X) <= 1 && e.Y == elf.Y + 1);
                    bool canMoveWest = !elves.Any(e => !(e.X == elf.X && e.Y == elf.Y) && Math.Abs(e.Y - elf.Y) <= 1 && e.X == elf.X - 1);
                    bool canMoveEast = !elves.Any(e => !(e.X == elf.X && e.Y == elf.Y) && Math.Abs(e.Y - elf.Y) <= 1 && e.X == elf.X + 1);

                    // Don't make a suggestion if there's no adjacent elves
                    if (!(canMoveNorth && canMoveSouth && canMoveEast && canMoveWest)) {
                        // Make a suggestion based on what's available
                        switch (direction) {
                            case Direction.North:
                                // Check N, S, W, E
                                if (canMoveNorth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y - 1;
                                }
                                else if (canMoveSouth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y + 1;
                                }
                                else if (canMoveWest) {
                                    elf.SuggestedX = elf.X - 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveEast) {
                                    elf.SuggestedX = elf.X + 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                break;
                            case Direction.South:
                                // Check S, W, E, N
                                if (canMoveSouth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y + 1;
                                }
                                else if (canMoveWest) {
                                    elf.SuggestedX = elf.X - 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveEast) {
                                    elf.SuggestedX = elf.X + 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveNorth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y - 1;
                                }
                                break;
                            case Direction.West:
                                // Check W, E, N, S
                                if (canMoveWest) {
                                    elf.SuggestedX = elf.X - 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveEast) {
                                    elf.SuggestedX = elf.X + 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveNorth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y - 1;
                                }
                                else if (canMoveSouth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y + 1;
                                }
                                break;
                            case Direction.East:
                                // Check E, N, S, W
                                if (canMoveEast) {
                                    elf.SuggestedX = elf.X + 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                else if (canMoveNorth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y - 1;
                                }
                                else if (canMoveSouth) {
                                    elf.SuggestedX = elf.X;
                                    elf.SuggestedY = elf.Y + 1;
                                }
                                else if (canMoveWest) {
                                    elf.SuggestedX = elf.X - 1;
                                    elf.SuggestedY = elf.Y;
                                }
                                break;
                        }
                    }
                }

                // Move elves if possible
                List<Elf> moveableElves = elves.Where(e => e.SuggestedX != null && e.SuggestedY != null && !elves.Any(elf => !(e.X == elf.X && e.Y == elf.Y) && e.SuggestedX == elf.SuggestedX && e.SuggestedY == elf.SuggestedY)).ToList();

                if (!moveableElves.Any()) {
                    elvesMoving = false;
                }

                foreach (Elf elf in moveableElves) {
                    elf.X = (int)elf.SuggestedX;
                    elf.Y = (int)elf.SuggestedY;
                }

                // Update priority direction
                direction = (Direction)Utility.Mod((int)direction + 1, 4);
            }

            return await Utility.SubmitAnswer(2022, 23, true, answer, send);
        }
    }
}