namespace AdventOfCode.Services
{
    public class Solution2015_12Service : ISolutionDayService
    {
        public Solution2015_12Service() { }

        public async Task<string> FirstHalf()
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2015_12.txt"));

            int sum = 0;
            List<char> validNumberCharacters = new() { '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string trackedNumber = "";

            foreach (char character in data)
            {
                if (validNumberCharacters.Contains(character))
                {
                    trackedNumber += character;
                }
                else if (trackedNumber != "")
                {
                    sum += Int32.Parse(trackedNumber);
                    trackedNumber = "";
                }
            }

            return $"The sum of all the numbers in the document is {sum}.";
        }

        public async Task<string> SecondHalf()
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2015_12.txt"));

            Stack<ParsedData> stack = new();
            ParsedData currentObject = new();
            List<char> validNumberCharacters = new() { '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string trackedNumber = "";
            string trackedRed = "";

            foreach (char character in data)
            {
                // Track numbers and add them to the existing object when the number is completed
                if (validNumberCharacters.Contains(character))
                {
                    trackedNumber += character;
                }
                else if (trackedNumber != "")
                {
                    if (!currentObject.HasRed)
                    {
                        currentObject.Sum += int.Parse(trackedNumber);
                    }
                    trackedNumber = "";
                }

                // If a new object/array is found, push the current one to the stack and reset the current object
                switch (character)
                {
                    case '{':
                        // If the parent of this object has red, ignore any children
                        stack.Push(currentObject);
                        currentObject = new() { HasRed = currentObject.HasRed };
                        trackedRed = "";
                        break;
                    case '[':
                        // If the parent of this object has red, so do its children
                        stack.Push(currentObject);
                        currentObject = new() { IsArray = true, HasRed = currentObject.HasRed };
                        trackedRed = "";
                        break;
                    case ']':
                    case '}':
                        int sum = currentObject.Sum;
                        currentObject = stack.Pop();
                        currentObject.Sum += sum;
                        trackedRed = "";
                        break;
                    case 'r':
                        trackedRed = "r";
                        break;
                    case 'e':
                        if (trackedRed == "r")
                        {
                            trackedRed = "re";
                        }
                        else
                        {
                            trackedRed = "";
                        }
                        break;
                    case 'd':
                        if (trackedRed == "re")
                        {
                            // We only ignore the sum if red is in an object or has a red parent
                            if (!currentObject.IsArray)
                            {
                                currentObject.HasRed = true;
                                currentObject.Sum = 0;
                            }
                        }
                        trackedRed = "";
                        break;
                    default:
                        trackedRed = "";
                        break;
                }
            }

            return $"The sum of all the numbers in the document after fixing the red double count is {currentObject.Sum}.";
        }
    }

    public class ParsedData
    {
        public bool IsArray { get; set; }
        public int Sum { get; set; }
        public bool HasRed { get; set; }
    }
}
