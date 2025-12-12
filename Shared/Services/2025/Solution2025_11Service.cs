namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2025/11.txt
    public class Solution2025_11Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 11, example);

            int answer = 0;

            Dictionary<string, List<string>> graph = lines.Select(x => x.Split(": ")).Select(x => (x[0], x[1].Split(" ").ToList())).ToDictionary();

            Queue<string> queue = [];
            queue.Enqueue("you");

            while (queue.Count > 0)
            {
                string node = queue.Dequeue();

                if (node == "out")
                {
                    answer++;
                    continue;
                }

                List<string> nextNodes = graph[node];

                foreach (string nextNode in nextNodes)
                {
                    queue.Enqueue(nextNode);
                }
            }

            return answer.ToString();
        }

        Dictionary<string, List<string>> graph {get; set;} = [];
        Dictionary<(string, bool , bool), long> validPaths { get; set; } = [];

        private long CheckPath(string node, bool hasDAC, bool hasFFT)
        {
            if (!validPaths.TryGetValue((node, hasDAC, hasFFT), out long value))
            {
                if (node == "out")
                {
                    value = hasDAC && hasFFT ? 1 : 0;
                    validPaths[(node, hasDAC, hasFFT)] = value;
                    return value;
                }
                else if (node == "dac")
                {
                    hasDAC = true;
                }
                else if (node == "fft")
                {
                    hasFFT = true;
                }

                value = 0;

                List<string> nextNodes = graph[node];

                foreach (string nextNode in nextNodes)
                {
                    value += CheckPath(nextNode, hasDAC, hasFFT);
                }

                validPaths[(node, hasDAC, hasFFT)] = value;
                return value;
            }
            else
            {
                return value;
            }
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2025, 11, example);

            graph = lines.Select(x => x.Split(": ")).Select(x => (x[0], x[1].Split(" ").ToList())).ToDictionary();

            long answer = CheckPath("svr", false, false);

            return answer.ToString();
        }
    }
}