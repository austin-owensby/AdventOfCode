namespace AdventOfCode.Services
{
    public class Solution2022_20Service : ISolutionDayService
    {
        public Solution2022_20Service() { }

        private class FileValue {
            public long Value {get; set;}
            public int OriginalIndex {get; set;}
        }

        public string FirstHalf()
        {
            List<int> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2022_20.txt")).ToInts();

            List<FileValue> fileValues = new();

            for (int i = 0; i < lines.Count; i++)
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

        public string SecondHalf()
        {
            List<long> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2022_20.txt")).ToLongs();

            List<FileValue> fileValues = new();

            for (int i = 0; i < lines.Count; i++)
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

            for (int i = 0; i < 10; i++) {
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