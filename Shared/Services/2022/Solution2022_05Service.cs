namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2022/05.txt
    public class Solution2022_05Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 05, example);

            List<Stack<string>> crates = lines
                .TakeWhile(l => !string.IsNullOrWhiteSpace(l)) // Only get the part of the file with the initial crate configuration
                .SkipLast(1) // Skip the last line with the numbers
                .QuickRegex(@".(.).\s.(.).\s.(.).\s.(.).\s.(.).\s.(.).\s.(.).\s.(.).\s.(.).") // Parse the labels of the crates
                .Pivot() // Pivot the 2D list of parsed crates
                .Select(l => new Stack<string>(l.Where(x => !string.IsNullOrWhiteSpace(x)))) // Filter out empty spaces and add the list to the stack
                .ToList();

            // Start parsing after the empty line
            List<string> instructions = lines.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1).ToList();

            foreach (string line in instructions)
            {
                List<int> nums = line.QuickRegex(@"move (\d+) from (\d+) to (\d+)").ToInts();

                foreach (int i in nums[0])
                {
                    string box = crates[nums[1] - 1].Pop();
                    crates[nums[2] - 1].Push(box);
                }
            }

            string answer = new(crates.Select(c => c.Pop()[0]).ToArray());

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 05, example);

            List<Stack<string>> crates = lines
                .TakeWhile(l => !string.IsNullOrWhiteSpace(l)) // Only get the part of the file with the initial crate configuration
                .SkipLast(1) // Skip the last line with the numbers
                .QuickRegex(@".(.).\s.(.).\s.(.).\s.(.).\s.(.).\s.(.).\s.(.).\s.(.).\s.(.).") // Parse the labels of the crates
                .Pivot() // Pivot the 2D list of parsed crates
                .Select(l => new Stack<string>(l.Where(x => !string.IsNullOrWhiteSpace(x)))) // Filter out empty spaces and add the list to the stack
                .ToList();

            // Start parsing after the empty line
            List<string> instructions = lines.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1).ToList();

            foreach (string line in instructions)
            {
                List<int> nums = line.QuickRegex(@"move (\d+) from (\d+) to (\d+)").ToInts();

                List<string> moved = new();

                foreach (int i in nums[0])
                {
                    string box = crates[nums[1] - 1].Pop();
                    moved.Add(box);
                }

                moved.Reverse();

                foreach (string box in moved)
                {
                    crates[nums[2] - 1].Push(box);
                }
            }

            string answer = new(crates.Select(c => c.Pop()[0]).ToArray());

            return answer.ToString();
        }
    }
}