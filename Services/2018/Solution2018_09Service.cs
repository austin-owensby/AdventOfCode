namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2018/09.txt
    public class Solution2018_09Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 09, example);

            List<int> values = lines.First().QuickRegex(@"(\d+) players; last marble is worth (\d+) points").ToInts();

            int players = values.First();
            int lastMarble = values.Last();

            List<int> marbles = [0];
            List<int> scores = Enumerable.Repeat(0, players).ToList();

            int currentPlayer = 0;

            LinkedList<int> list = new();
            LinkedListNode<int> current = list.AddFirst(0);

            for (int currentMarble = 1; currentMarble <= lastMarble; currentMarble++)
            {
                if (currentMarble % 23 == 0) {
                    scores[currentPlayer] += currentMarble;

                    7.ForEach(() => { current = current.Previous ?? list.Last!;});

                    scores[currentPlayer] += current.Value;
                    current = current.Next ?? list.First!;
                    list.Remove(current.Previous ?? list.Last!);
                }
                else {
                    current = current.Next ?? list.First!;
                    current = list.AddAfter(current, currentMarble);
                }

                currentPlayer = (currentPlayer + 1) % players;
            }

            int answer = scores.Max();

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 09, example);

            List<int> values = lines.First().QuickRegex(@"(\d+) players; last marble is worth (\d+) points").ToInts();

            int players = values.First();
            int lastMarble = values.Last() * 100;

            List<int> marbles = [0];
            List<long> scores = Enumerable.Repeat((long)0, players).ToList();

            int currentPlayer = 0;

            LinkedList<long> list = new();
            LinkedListNode<long> current = list.AddFirst(0);

            for (int currentMarble = 1; currentMarble <= lastMarble; currentMarble++)
            {
                if (currentMarble % 23 == 0) {
                    scores[currentPlayer] += currentMarble;

                    7.ForEach(() => { current = current.Previous ?? list.Last!;});

                    scores[currentPlayer] += current.Value;
                    current = current.Next ?? list.First!;
                    list.Remove(current.Previous ?? list.Last!);
                }
                else {
                    current = current.Next ?? list.First!;
                    current = list.AddAfter(current, currentMarble);
                }

                currentPlayer = (currentPlayer + 1) % players;
            }

            long answer = scores.Max();

            return answer.ToString();
        }
    }
}