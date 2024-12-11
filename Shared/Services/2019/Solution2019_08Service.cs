namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2019/08.txt
    public class Solution2019_08Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 08, example);
            List<int> digits = lines.First().ToInts();

            int width = 25;
            int height = 6;
            int layerSize = width * height;
            int layers = digits.Count / layerSize;

            int answer = 0;
            int minZerosOnLayer = int.MaxValue;

            foreach (int layer in layers)
            {
                List<int> layerDigits = digits.Skip(layerSize * layer).Take(layerSize).ToList();
                int zerosOnLayer = layerDigits.Count(d => d == 0);

                if (zerosOnLayer < minZerosOnLayer) {
                    answer = layerDigits.Count(d => d == 1) * layerDigits.Count(d => d == 2);
                    minZerosOnLayer = zerosOnLayer;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 08, example);
            List<int> digits = lines.First().ToInts();

            int width = 25;
            int height = 6;
            int layerSize = width * height;
            int layers = digits.Count / layerSize;

            List<int> display = Enumerable.Repeat(2, layerSize).ToList();

            foreach (int layer in layers)
            {
                List<int> layerDigits = digits.Skip(layerSize * layer).Take(layerSize).ToList();
                foreach (int i in layerSize) {
                    if (display[i] == 2) {
                        display[i] = layerDigits[i];
                    }
                }
            }
            
            string answer = Utility.ParseASCIILetters(string.Join("",display), height, '0', '1');

            return answer;
        }
    }
}