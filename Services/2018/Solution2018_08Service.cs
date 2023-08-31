namespace AdventOfCode.Services
{
    public class Solution2018_08Service : ISolutionDayService
    {
        public Solution2018_08Service() { }

        private int SumMetadata(ref int index, List<int> numbers) {
            int answer = 0;

            int numberOfChildren = numbers[index++];
            int numberOfMetaData = numbers[index++];

            for (int child = 0; child < numberOfChildren; child++) {
                answer += SumMetadata(ref index, numbers);
            }

            for (int metaData = 0; metaData < numberOfMetaData; metaData++) {
                answer += numbers[index++];
            }

            return answer;
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 08, example);
            List<int> numbers = lines.First().Split(" ").ToInts();

            int index = 0;

            int answer = SumMetadata(ref index, numbers);

            return answer.ToString();
        }

        private int SumNode(ref int index, List<int> numbers) {
            int answer = 0;

            int numberOfChildren = numbers[index++];
            int numberOfMetaData = numbers[index++];

            List<int> childSums = new();

            for (int child = 0; child < numberOfChildren; child++) {
                childSums.Add(SumNode(ref index, numbers));
            }

            for (int metaData = 0; metaData < numberOfMetaData; metaData++) {
                int metaDataIndex = numbers[index++];

                if (!childSums.Any()) {
                    answer += metaDataIndex;
                }
                else if (metaDataIndex > 0 && metaDataIndex <= childSums.Count) {
                    answer += childSums[metaDataIndex - 1];
                }
            }

            return answer;
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 08, example);
            List<int> numbers = lines.First().Split(" ").ToInts();

            int index = 0;

            int answer = SumNode(ref index, numbers);

            return answer.ToString();
        }
    }
}