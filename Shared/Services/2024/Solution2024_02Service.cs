namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2024/02.txt
    public class Solution2024_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 2, example);
            List<List<int>> reports = lines.Select(l => l.Split(" ").ToInts()).ToList();

            int answer = 0;

            foreach (List<int> report in reports)
            {
                if (report[0] == report[1]) {
                    continue;
                }

                bool increasing = report[0] < report[1];

                bool valid = true;

                foreach (int i in report.Count - 1) {
                    if (report[i] == report[i + 1]) {
                        valid = false;
                        break;
                    }

                    bool segmentIncreasing = report[i] < report[i + 1];

                    if (segmentIncreasing != increasing) {
                        valid = false;
                        break;
                    }

                    int diff = Math.Abs(report[i] - report[i + 1]);

                    if (diff > 3) {
                        valid = false;
                        break;
                    }
                }

                if (valid) {
                    answer++;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 2, example);
            List<List<int>> reports = lines.Select(l => l.Split(" ").ToInts()).ToList();

            int answer = 0;

            foreach (List<int> report in reports)
            {
                bool increasing = report[0] < report[1];

                bool valid = true;

                foreach (int i in report.Count - 1) {
                    if (report[i] == report[i + 1]) {
                        valid = false;
                        break;
                    }

                    bool segmentIncreasing = report[i] < report[i + 1];

                    if (segmentIncreasing != increasing) {
                        valid = false;
                        break;
                    }

                    int diff = Math.Abs(report[i] - report[i + 1]);

                    if (diff > 3) {
                        valid = false;
                        break;
                    }
                }

                if (valid) {
                    answer++;
                }
                else {
                    // Try removing at each index until we find a valid answer
                    foreach (int x in report.Count)
                    {
                        List<int> tempReport = report.Where((r, i) => i != x).ToList();
   
                        increasing = tempReport[0] < tempReport[1];
                        valid = true;

                        foreach (int i in tempReport.Count - 1) {
                            if (tempReport[i] == tempReport[i + 1]) {
                                valid = false;
                                break;
                            }

                            bool segmentIncreasing = tempReport[i] < tempReport[i + 1];

                            if (segmentIncreasing != increasing) {
                                valid = false;
                                break;
                            }

                            int diff = Math.Abs(tempReport[i] - tempReport[i + 1]);

                            if (diff > 3) {
                                valid = false;
                                break;
                            }
                        }

                        if (valid)
                        {
                            answer++;
                            break;
                        }
                    }
                }
            }

            return answer.ToString();
        }
    }
}