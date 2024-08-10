namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2015/20.txt
    public class Solution2015_20Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "20.txt"));

            int goalNumber = int.Parse(data.Split("\n")[0]);
            int foundHouse = 0;

            for (int currentHouse = 1; currentHouse <= goalNumber / 10; currentHouse++)
            {
                int sumOfFactors = 0;

                for (int i = 1; i <= Math.Sqrt(currentHouse); i++)
                {
                    if (currentHouse % i == 0)
                    {
                        sumOfFactors += i;

                        if (currentHouse / i != i)
                        {
                            sumOfFactors += currentHouse / i;
                        }
                    }
                }

                if (sumOfFactors >= goalNumber / 10)
                {
                    foundHouse = currentHouse;
                    break;
                }
            }

            return foundHouse.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "20.txt"));

            int goalNumber = int.Parse(data.Split("\n")[0]);
            int foundHouse = 0;

            Dictionary<int, int> factors = new();

            for (int currentHouse = 1; currentHouse <= goalNumber / 11; currentHouse++)
            {
                int sumOfFactors = 0;

                for (int i = 1; i <= Math.Sqrt(currentHouse); i++)
                {
                    if (currentHouse % i == 0)
                    {
                        if (factors.ContainsKey(i))
                        {
                            if (factors[i] < 50)
                            {
                                factors[i] = 1 + factors[i];
                                sumOfFactors += i;
                            }
                        }
                        else
                        {
                            factors[i] = 1;
                            sumOfFactors += i;
                        }

                        if (currentHouse / i != i)
                        {
                            if (factors.ContainsKey(currentHouse / i))
                            {
                                if (factors[currentHouse / i] < 50)
                                {
                                    factors[currentHouse / i] = 1 + factors[currentHouse / i];
                                    sumOfFactors += currentHouse / i;
                                }
                            }
                            else
                            {
                                factors[currentHouse / i] = 1;
                                sumOfFactors += currentHouse / i;
                            }
                        }
                    }
                }

                if (sumOfFactors >= goalNumber / 11)
                {
                    foundHouse = currentHouse;
                    break;
                }
            }

            return foundHouse.ToString();
        }
    }
}
