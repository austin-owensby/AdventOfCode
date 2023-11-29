namespace AdventOfCode.Services
{
    public class Solution2018_13Service : ISolutionDayService
    {
        private class Cart {
            public int X { get; set; }
            public int Y { get; set; }
            public TurnState State { get; set; }
            public Direction Direction { get; set; }
            public bool Crashed { get; set; }
        }

        private enum Direction {
            Up,
            Right,
            Down,
            Left
        }
        
        private enum TurnState {
            Left,
            Straight,
            Right
        }

        public string FirstHalf(bool example)
        {
            List<List<char>> map = Utility.GetInputLines(2018, 13, example).Select(l => l.ToList()).ToList();

            List<char> cartValues = new(){'^', '>', 'v', '<'};
            List<Cart> carts = new();

            foreach (int y in map.Count) {
                foreach (int x in map[0].Count) {
                    if (cartValues.Contains(map[y][x])) {
                        Cart cart = new() {
                            X = x,
                            Y = y,
                            State = TurnState.Left,
                            Direction = (Direction)cartValues.IndexOf(map[y][x])
                        };

                        carts.Add(cart);

                        if (map[y][x] == '^' || map[y][x] == 'v') {
                            map[y][x] = '|';
                        }
                        else {
                            map[y][x] = '-';
                        }
                    }
                }
            }

            bool crashed = false;
            string answer = string.Empty;

            while (!crashed) {
                carts = carts.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();

                foreach (Cart cart in carts) {
                    char rail = map[cart.Y][cart.X];

                    switch (rail) {
                        case '/':
                            switch (cart.Direction) {
                                case Direction.Up:
                                    cart.Direction = Direction.Right;
                                    break;
                                case Direction.Down:
                                    cart.Direction = Direction.Left;
                                    break;
                                case Direction.Right:
                                    cart.Direction = Direction.Up;
                                    break;
                                case Direction.Left:
                                    cart.Direction = Direction.Down;
                                    break;
                            }
                            break;
                        case '\\':
                            switch (cart.Direction) {
                                case Direction.Up:
                                    cart.Direction = Direction.Left;
                                    break;
                                case Direction.Down:
                                    cart.Direction = Direction.Right;
                                    break;
                                case Direction.Right:
                                    cart.Direction = Direction.Down;
                                    break;
                                case Direction.Left:
                                    cart.Direction = Direction.Up;
                                    break;
                            }
                            break;
                        case '+':
                            switch (cart.State) {
                                case TurnState.Left:
                                    cart.State = TurnState.Straight;
                                    switch (cart.Direction) {
                                        case Direction.Up:
                                            cart.Direction = Direction.Left;
                                            break;
                                        case Direction.Down:
                                            cart.Direction = Direction.Right;
                                            break;
                                        case Direction.Right:
                                            cart.Direction = Direction.Up;
                                            break;
                                        case Direction.Left:
                                            cart.Direction = Direction.Down;
                                            break;
                                    }
                                    break;
                                case TurnState.Straight:
                                    cart.State = TurnState.Right;
                                    break;
                                case TurnState.Right:
                                    cart.State = TurnState.Left;
                                    switch (cart.Direction) {
                                        case Direction.Up:
                                            cart.Direction = Direction.Right;
                                            break;
                                        case Direction.Down:
                                            cart.Direction = Direction.Left;
                                            break;
                                        case Direction.Right:
                                            cart.Direction = Direction.Down;
                                            break;
                                        case Direction.Left:
                                            cart.Direction = Direction.Up;
                                            break;
                                    }
                                    break;
                            }
                            break;
                        default:
                            // Direction doesn't change
                            break;
                    }

                    switch (cart.Direction) {
                        case Direction.Up:
                            cart.Y--;
                            break;
                        case Direction.Down:
                            cart.Y++;
                            break;
                        case Direction.Right:
                            cart.X++;
                            break;
                        case Direction.Left:
                            cart.X--;
                            break;
                    }

                    // Check for a crash
                    if (carts.Count(c => c.X == cart.X && c.Y == cart.Y) > 1) {
                        crashed = true;
                        answer = $"{cart.X},{cart.Y}";
                        break;
                    }
                }
            }

            return answer;
        }

        public string SecondHalf(bool example)
        {
            List<List<char>> map = Utility.GetInputLines(2018, 13, example).Select(l => l.ToList()).ToList();

            List<char> cartValues = new(){'^', '>', 'v', '<'};
            List<Cart> carts = new();

            foreach (int y in map.Count) {
                foreach (int x in map[0].Count) {
                    if (cartValues.Contains(map[y][x])) {
                        Cart cart = new() {
                            X = x,
                            Y = y,
                            State = TurnState.Left,
                            Direction = (Direction)cartValues.IndexOf(map[y][x]),
                            Crashed = false
                        };

                        carts.Add(cart);

                        if (map[y][x] == '^' || map[y][x] == 'v') {
                            map[y][x] = '|';
                        }
                        else {
                            map[y][x] = '-';
                        }
                    }
                }
            }

            string answer = string.Empty;

            while (carts.Count(c => !c.Crashed) != 1) {
                carts = carts.Where(c => !c.Crashed).OrderBy(c => c.Y).ThenBy(c => c.X).ToList();

                foreach (Cart cart in carts) {
                    if (cart.Crashed) {
                        continue;
                    }

                    char rail = map[cart.Y][cart.X];

                    switch (rail) {
                        case '/':
                            switch (cart.Direction) {
                                case Direction.Up:
                                    cart.Direction = Direction.Right;
                                    break;
                                case Direction.Down:
                                    cart.Direction = Direction.Left;
                                    break;
                                case Direction.Right:
                                    cart.Direction = Direction.Up;
                                    break;
                                case Direction.Left:
                                    cart.Direction = Direction.Down;
                                    break;
                            }
                            break;
                        case '\\':
                            switch (cart.Direction) {
                                case Direction.Up:
                                    cart.Direction = Direction.Left;
                                    break;
                                case Direction.Down:
                                    cart.Direction = Direction.Right;
                                    break;
                                case Direction.Right:
                                    cart.Direction = Direction.Down;
                                    break;
                                case Direction.Left:
                                    cart.Direction = Direction.Up;
                                    break;
                            }
                            break;
                        case '+':
                            switch (cart.State) {
                                case TurnState.Left:
                                    cart.State = TurnState.Straight;
                                    switch (cart.Direction) {
                                        case Direction.Up:
                                            cart.Direction = Direction.Left;
                                            break;
                                        case Direction.Down:
                                            cart.Direction = Direction.Right;
                                            break;
                                        case Direction.Right:
                                            cart.Direction = Direction.Up;
                                            break;
                                        case Direction.Left:
                                            cart.Direction = Direction.Down;
                                            break;
                                    }
                                    break;
                                case TurnState.Straight:
                                    cart.State = TurnState.Right;
                                    break;
                                case TurnState.Right:
                                    cart.State = TurnState.Left;
                                    switch (cart.Direction) {
                                        case Direction.Up:
                                            cart.Direction = Direction.Right;
                                            break;
                                        case Direction.Down:
                                            cart.Direction = Direction.Left;
                                            break;
                                        case Direction.Right:
                                            cart.Direction = Direction.Down;
                                            break;
                                        case Direction.Left:
                                            cart.Direction = Direction.Up;
                                            break;
                                    }
                                    break;
                            }
                            break;
                        default:
                            // Direction doesn't change
                            break;
                    }

                    switch (cart.Direction) {
                        case Direction.Up:
                            cart.Y--;
                            break;
                        case Direction.Down:
                            cart.Y++;
                            break;
                        case Direction.Right:
                            cart.X++;
                            break;
                        case Direction.Left:
                            cart.X--;
                            break;
                    }

                    // Check for a crash
                    List<Cart> cartsOnSameSpot = carts.Where(c => !c.Crashed && c.X == cart.X && c.Y == cart.Y).ToList();
                    if (cartsOnSameSpot.Count > 1) {
                        // The current cart crashed with another
                        cartsOnSameSpot.ForEach(c => c.Crashed = true);
                    }
                }
            }

            Cart lastCart = carts.First(c => !c.Crashed);
            answer = $"{lastCart.X},{lastCart.Y}";

            return answer;
        }
    }
}