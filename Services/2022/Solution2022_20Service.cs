namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2022/20.txt
    public class Solution2022_20Service : ISolutionDayService
    {
                private class FileValue {
            public long Value {get; set;}
            public int OriginalIndex {get; set;}
        }

        public string FirstHalf(bool example)
        {
            List<int> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2022", "20.txt")).ToInts();

            List<FileValue> fileValues = new();

            foreach (int i in lines.Count)
            {
                fileValues.Add(new(){
                    Value = lines[i],
                    OriginalIndex = i
                });
            }

            List<FileValue> copy = fileValues.Select(l => new FileValue(){
                Value = l.Value,
                OriginalIndex = l.OriginalIndex
            }).ToList();

            foreach (FileValue value in fileValues) {
                int currentIndex = copy.FindIndex(c => c.OriginalIndex == value.OriginalIndex);

                copy.RemoveAt(currentIndex);

                int newIndex = (int)Utility.Mod(currentIndex + value.Value, fileValues.Count - 1);

                copy.Insert(newIndex, value);
            }

            int zeroIndex = copy.FindIndex(v => v.Value == 0);

            long answer = copy[(zeroIndex + 1000) % copy.Count].Value;
            answer += copy[(zeroIndex + 2000) % copy.Count].Value;
            answer += copy[(zeroIndex + 3000) % copy.Count].Value;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<long> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2022", "20.txt")).ToLongs();

            List<FileValue> fileValues = new();

            foreach (int i in lines.Count)
            {
                fileValues.Add(new(){
                    Value = lines[i] * 811589153,
                    OriginalIndex = i
                });
            }

            List<FileValue> copy = fileValues.Select(l => new FileValue(){
                Value = l.Value,
                OriginalIndex = l.OriginalIndex
            }).ToList();

            foreach (int i in 10) {
                foreach (FileValue value in fileValues) {
                    int currentIndex = copy.FindIndex(c => c.OriginalIndex == value.OriginalIndex);

                    copy.RemoveAt(currentIndex);

                    int newIndex = (int)Utility.Mod(currentIndex + value.Value, fileValues.Count - 1);

                    copy.Insert(newIndex, value);
                }
            }

            int zeroIndex = copy.FindIndex(v => v.Value == 0);

            long answer = copy[(zeroIndex + 1000) % copy.Count].Value;
            answer += copy[(zeroIndex + 2000) % copy.Count].Value;
            answer += copy[(zeroIndex + 3000) % copy.Count].Value;

            return answer.ToString();
        }
    }
}