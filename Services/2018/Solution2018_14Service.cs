namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2018/14.txt
    public class Solution2018_14Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 14, example);
            int recipeLimit = int.Parse(lines.First());

            List<int> recipies = new(){3,7};
            int index1 = 0;
            int index2 = 1;

            while(recipies.Count < recipeLimit + 10) {
                int value1 = recipies[index1];
                int value2 = recipies[index2];
                int sum = value1 + value2;
                List<int> newRecipies = sum.ToString().Select(c => int.Parse(c.ToString())).ToList();
                recipies.AddRange(newRecipies);

                index1 = (index1 + value1 + 1) % recipies.Count;
                index2 = (index2 + value2 + 1) % recipies.Count;
            }

            string answer = string.Concat(recipies.TakeLast(10));

            return answer;
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2018, 14, example);
            string scoreDigits = lines.First();

            List<int> recipies = new(){3,7};
            int index1 = 0;
            int index2 = 1;

            bool hasAnswer = false;

            while(!hasAnswer) {
                int value1 = recipies[index1];
                int value2 = recipies[index2];
                int sum = value1 + value2;
                List<int> newRecipies = sum.ToString().Select(c => int.Parse(c.ToString())).ToList();
                recipies.AddRange(newRecipies);

                index1 = (index1 + value1 + 1) % recipies.Count;
                index2 = (index2 + value2 + 1) % recipies.Count;

                hasAnswer = string.Concat(recipies.TakeLast(scoreDigits.Length + 1)).Contains(scoreDigits);
            }

            int answer = string.Concat(recipies).IndexOf(scoreDigits);

            return answer.ToString();
        }
    }
}