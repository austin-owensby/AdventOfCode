namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/05.txt
    public class Solution2024_05Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 5, example);

            List<(int,int)> rules = lines.TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split('|')).Select(l => (int.Parse(l[0]),int.Parse(l[1]))).ToList();
            List<List<int>> updates = lines.SkipWhile(l => !string.IsNullOrEmpty(l)).Skip(1).Select(l => l.Split(',').ToInts()).ToList();

            int answer = 0;

            foreach (List<int> update in updates)
            {
                bool followsRules = true;
                foreach ((int, int) rule in rules)
                {
                    int index1 = update.IndexOf(rule.Item1);
                    int index2 = update.IndexOf(rule.Item2);

                    if (index1 != -1 && index2 != -1 && index1 > index2) {
                        followsRules = false;
                        break;
                    }
                }

                if (followsRules)
                {
                    answer += update[(update.Count - 1) / 2];
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 5, example);

            List<(int,int)> rules = lines.TakeWhile(l => !string.IsNullOrEmpty(l)).Select(l => l.Split('|')).Select(l => (int.Parse(l[0]),int.Parse(l[1]))).ToList();
            List<List<int>> updates = lines.SkipWhile(l => !string.IsNullOrEmpty(l)).Skip(1).Select(l => l.Split(',').ToInts()).ToList();

            int answer = 0;


            foreach (List<int> update in updates)
            {
                bool followsRules = true;
                foreach ((int, int) rule in rules)
                {
                    int index1 = update.IndexOf(rule.Item1);
                    int index2 = update.IndexOf(rule.Item2);

                    if (index1 != -1 && index2 != -1 && index1 > index2) {
                        followsRules = false;
                        break;
                    }
                }

                if (!followsRules)
                {
                    // Fix the order
                    do
                    {
                        followsRules = true;
                        foreach ((int, int) rule in rules)
                        {
                            int index1 = update.IndexOf(rule.Item1);
                            int index2 = update.IndexOf(rule.Item2);

                            if (index1 != -1 && index2 != -1 && index1 > index2) {
                                followsRules = false;
                                // Fix the bad rule and try again
                                update[index1] = rule.Item2;
                                update[index2] = rule.Item1;
                                break;
                            }
                        }
                    }
                    while(followsRules == false);

                    answer += update[(update.Count - 1) / 2];
                }
            }

            return answer.ToString();
        }
    }
}