namespace AdventOfCode.Services
{
    public class Solution2022_13Service : ISolutionDayService
    {
        public Solution2022_13Service() { }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 13, example);

            List<List<string>> packets = lines.ChunkByExclusive(string.IsNullOrEmpty);

            int answer = 0;

            for (int i = 1; i <= packets.Count; i++)
            {
                List<string> packetPair = packets[i - 1];

                if (ComparePackets(packetPair[0], packetPair[1]) == -1)
                {
                    answer += i;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 13, example);
            lines = lines.Where(line => !string.IsNullOrEmpty(line)).ToList();
            lines.Add("[[2]]");
            lines.Add("[[6]]");

            lines.Sort(ComparePackets);

            int answer = (lines.IndexOf("[[2]]") + 1) * (lines.IndexOf("[[6]]") + 1);

            return answer.ToString();
        }

        private int ComparePackets(string packet1, string packet2)
        {
            List<string> packet1Elements = new();
            List<string> packet2Elements = new();

            int bracketLevel = packet1.StartsWith('[') ? 0 : 1;
            string currentSignal = string.Empty;
            foreach (char signal in packet1)
            {
                if (signal == ']')
                {
                    bracketLevel--;
                }

                if (bracketLevel > 0)
                {
                    if (signal == ',' && bracketLevel == 1)
                    {
                        packet1Elements.Add(currentSignal);
                        currentSignal = string.Empty;
                    }
                    else
                    {
                        currentSignal += signal;
                    }
                }

                if (signal == '[')
                {
                    bracketLevel++;
                }
            }

            if (!string.IsNullOrEmpty(currentSignal))
            {
                packet1Elements.Add(currentSignal);
            }

            bracketLevel = packet1.StartsWith('[') ? 0 : 1;
            currentSignal = string.Empty;
            foreach (char signal in packet2)
            {
                if (signal == ']')
                {
                    bracketLevel--;
                }

                if (bracketLevel > 0)
                {
                    if (signal == ',' && bracketLevel == 1)
                    {
                        packet2Elements.Add(currentSignal);
                        currentSignal = string.Empty;
                    }
                    else
                    {
                        currentSignal += signal;
                    }
                }

                if (signal == '[')
                {
                    bracketLevel++;
                }
            }

            if (!string.IsNullOrEmpty(currentSignal))
            {
                packet2Elements.Add(currentSignal);
            }

            int result = 0;

            for (int i = 0; i < Math.Max(packet1Elements.Count, packet2Elements.Count); i++)
            {
                if (packet1Elements.Count - 1 < i && packet1Elements.Count != packet2Elements.Count)
                {
                    // Packet 1 has run out first
                    result = -1;
                    break;
                }
                else if (packet2Elements.Count - 1 < i && packet1Elements.Count != packet2Elements.Count)
                {
                    // Packet 2 has run out first
                    result = 1;
                    break;
                }

                string element1 = packet1Elements[i];
                string element2 = packet2Elements[i];

                bool element1IsNumber = int.TryParse(element1, out int number1);
                bool element2IsNumber = int.TryParse(element2, out int number2);

                // Both elements are numberic, compare the numbers directly
                if (element1IsNumber && element2IsNumber)
                {
                    if (number1 < number2)
                    {
                        result = -1;
                    }
                    else if (number1 > number2)
                    {
                        result = 1;
                    }
                }

                // One element is a number, the other is a list. Convert the number to a list
                else
                {
                    result = element1IsNumber != element2IsNumber
                        ? element1IsNumber ? ComparePackets($"[{element1}]", element2) : ComparePackets(element1, $"[{element2}]")
                        : ComparePackets(element1, element2);
                }

                if (result != 0)
                {
                    break;
                }
            }

            return result;
        }
    }
}