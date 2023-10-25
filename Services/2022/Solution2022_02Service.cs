namespace AdventOfCode.Services
{
    public class Solution2022_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 02, example);

            int score = 0;

            foreach (string line in lines)
            {
                char firstLetter = line[2];
                char secondLetter = line[0];

                if (firstLetter == 'X')
                {
                    score++;

                    if (secondLetter == 'C')
                    {
                        score += 6;
                    }
                    else if (secondLetter == 'A')
                    {
                        score += 3;
                    }
                }

                if (firstLetter == 'Y')
                {
                    score += 2;

                    if (secondLetter == 'A')
                    {
                        score += 6;
                    }
                    else if (secondLetter == 'B')
                    {
                        score += 3;
                    }
                }

                if (firstLetter == 'Z')
                {
                    score += 3;

                    if (secondLetter == 'B')
                    {
                        score += 6;
                    }
                    else if (secondLetter == 'C')
                    {
                        score += 3;
                    }
                }
            }

            return score.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 02, example);
            int score = 0;

            foreach (string line in lines)
            {
                char firstLetter = line[0];
                char secondLetter = line[2];

                if (secondLetter == 'Y')
                {
                    score += 3;
                }
                else if (secondLetter == 'Z')
                {
                    score += 6;
                }

                if (firstLetter == 'A')
                {
                    if (secondLetter == 'Y')
                    {
                        score++; // Tie with rock
                    }
                    else if (secondLetter == 'Z')
                    {
                        score += 2; // Win with paper
                    }
                    else
                    {
                        score += 3; // Lose with scissors
                    }
                }

                if (firstLetter == 'B')
                {
                    if (secondLetter == 'Y')
                    {
                        score += 2; // Tie with paper
                    }
                    else if (secondLetter == 'Z')
                    {
                        score += 3;
                    }
                    else
                    {
                        score++;
                    }
                }

                if (firstLetter == 'C')
                {
                    if (secondLetter == 'Y')
                    {
                        score += 3; // Tie with scissors
                    }
                    else if (secondLetter == 'Z')
                    {
                        score++;
                    }
                    else
                    {
                        score += 2;
                    }
                }
            }

            return score.ToString();
        }
    }
}