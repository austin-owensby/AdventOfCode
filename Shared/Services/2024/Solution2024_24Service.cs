namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/24.txt
    public class Solution2024_24Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 24, example);
            Dictionary<string, int> wires = lines.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.Split(": ")).ToDictionary(x => x[0], x => int.Parse(x[1]));
            List<(string in1, string op, string in2, string output)> connections = lines.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1).QuickRegex("(.+) (.+) (.+) -> (.+)").Select(x => (x[0], x[1], x[2], x[3])).ToList();

            List<(string in1, string op, string in2, string output)> connectionsToSolve = connections.ToList();

            while (connectionsToSolve.Count > 0) {
                List<(string in1, string op, string in2, string output)> newConnectionsToSolve = [];
                foreach ((string in1, string op, string in2, string output) in connectionsToSolve) {
                    if (wires.TryGetValue(in1, out int value1)) {
                        if (wires.TryGetValue(in2, out int value2)) {
                            wires[output] = op switch
                            {
                                "AND" => value1 & value2,
                                "XOR" => value1 ^ value2,
                                "OR" => value1 | value2,
                                _ => throw new NotImplementedException()
                            };
                        }
                        else {
                            newConnectionsToSolve.Add((in1, op, in2, output));
                        }
                    }
                    else {
                        newConnectionsToSolve.Add((in1, op, in2, output));
                    }
                }
                connectionsToSolve = newConnectionsToSolve;
            }

            string binaryValue = string.Join("", wires.Where(w => w.Key.StartsWith('z')).OrderByDescending(x => x.Key).Select(x => x.Value));
            long answer = Convert.ToInt64(binaryValue, 2);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 24, example);
            Dictionary<string, int> wires = lines.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.Split(": ")).ToDictionary(x => x[0], x => int.Parse(x[1]));
            List<(string in1, string op, string in2, string output)> connections = lines.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1).QuickRegex("(.+) (.+) (.+) -> (.+)").Select(x => (x[0], x[1], x[2], x[3])).ToList();

            string? cin = null;

            List<string> badWires = [];

            int bits = wires.Count / 2;
            for (int i = 0; i < bits; i++)
            {
                // Look for the A AND B connection
                string aANDb = connections.First(c => c.op == "AND" && (c.in1 == $"x{i:D2}" && c.in2 == $"y{i:D2}" || c.in1 == $"y{i:D2}" && c.in2 == $"x{i:D2}")).output;

                // Look for the A XOR B connection
                string aXORb = connections.First(c => c.op == "XOR" && (c.in1 == $"x{i:D2}" && c.in2 == $"y{i:D2}" || c.in1 == $"y{i:D2}" && c.in2 == $"x{i:D2}")).output;

                // For the first bit, we don't have a carry in signal, treat as a half adder
                if (i == 0) {
                    if (aXORb != "z00") {
                        badWires.Add("z00");
                    }

                    cin = aANDb;
                    continue;
                }

                // Sum bit should be carry in XOR a
                if (!string.IsNullOrEmpty(cin)) {
                    string? sum = connections.FirstOrDefault(c => c.op == "XOR" && (c.in1 == aXORb && c.in2 == cin || c.in1 == cin && c.in2 == aXORb)).output;

                    if (string.IsNullOrEmpty(sum)) {
                        badWires.Add(aXORb);
                        var replacementCon = connections.First(c => c.op == "XOR" && (c.in1 == cin || c.in2 == cin));
                        string replacement = replacementCon.in1 == cin ? replacementCon.in2 : replacementCon.in1;
                        badWires.Add(replacement);

                        // Do the swap
                        int index1 = connections.FindIndex(c => c.output == replacement);
                        int index2 = connections.FindIndex(c => c.output == aXORb);
                        var con1 = connections[index1];
                        var con2 = connections[index2];
                        con1.output = aXORb;
                        con2.output = replacement;
                        connections[index1] = con1;
                        connections[index2] = con2;
                        // restart our search with the fixed value
                        i = -1;
                        continue;
                    }
                    else if (sum != $"z{i:D2}") {
                        badWires.Add(sum);
                        badWires.Add($"z{i:D2}");

                        // Do the swap
                        int index1 = connections.FindIndex(c => c.output == sum);
                        int index2 = connections.FindIndex(c => c.output == $"z{i:D2}");
                        var con1 = connections[index1];
                        var con2 = connections[index2];
                        con1.output = $"z{i:D2}";
                        con2.output = sum;
                        connections[index1] = con1;
                        connections[index2] = con2;
                        // restart our search with the fixed value
                        i = -1;
                        continue;
                    }

                    // Intermediate value
                    // These is guaranteed because we already validated all inputs
                    string aXORbANDc = connections.First(c => c.op == "AND" && (c.in1 == aXORb && c.in2 == cin || c.in1 == cin && c.in2 == aXORb)).output;

                    string cout = connections.FirstOrDefault(c => c.op == "OR" && (c.in1 == aXORbANDc && c.in2 == aANDb || c.in1 == aANDb && c.in2 == aXORbANDc)).output;

                    // Last carry out should be the most significant bit of z
                    if (i == wires.Count / 2 - 1) {
                        if (cout != $"z{bits}") {
                            badWires.Add($"x{bits}");
                            badWires.Add(cout);
                        }
                    }

                    cin = cout;
                }

                if (badWires.Distinct().Count() == 8) {
                    break;
                }
            }

            string answer = string.Join(",", badWires.Order());

            return answer;
        }
    }
}