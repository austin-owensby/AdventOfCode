namespace AdventOfCode.Services
{
    public class Solution2015_15Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "15.txt"));

            string[] lines = data.Split("\n");
            lines = lines.SkipLast(1).ToArray();

            List<Ingredient> ingredients = new();

            foreach (string line in lines)
            {
                string[] words = line.Split(" ");

                Ingredient ingredient = new()
                {
                    Capacity = int.Parse(words[2].Split(',')[0]),
                    Durability = int.Parse(words[4].Split(',')[0]),
                    Flavor = int.Parse(words[6].Split(',')[0]),
                    Texture = int.Parse(words[8].Split(',')[0]),
                    Calories = int.Parse(words[10])
                };

                ingredients.Add(ingredient);
            }

            int highestScore = 0;

            // Iterate through each of the possibilities of the 3 ingredient totals
            // The 4th ingredient will always be 100 - the sum of the first 3
            for (int i = 0; i <= 100; i++)
            {
                for (int j = 0; j <= 100 - i; j++)
                {
                    for (int k = 0; k <= 100 - i - j; k++)
                    {
                        int l = 100 - i - j - k;

                        int capactity = Math.Max(0, (i * ingredients[0].Capacity) + (j * ingredients[1].Capacity) + (k * ingredients[2].Capacity) + (l * ingredients[3].Capacity));
                        int durability = Math.Max(0, (i * ingredients[0].Durability) + (j * ingredients[1].Durability) + (k * ingredients[2].Durability) + (l * ingredients[3].Durability));
                        int flavor = Math.Max(0, (i * ingredients[0].Flavor) + (j * ingredients[1].Flavor) + (k * ingredients[2].Flavor) + (l * ingredients[3].Flavor));
                        int texture = Math.Max(0, (i * ingredients[0].Texture) + (j * ingredients[1].Texture) + (k * ingredients[2].Texture) + (l * ingredients[3].Texture));

                        int score = capactity * durability * flavor * texture;

                        if (score > highestScore)
                        {
                            highestScore = score;
                        }
                    }
                }
            }

            return highestScore.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "15.txt"));

            string[] lines = data.Split("\n");
            lines = lines.SkipLast(1).ToArray();

            List<Ingredient> ingredients = new();

            foreach (string line in lines)
            {
                string[] words = line.Split(" ");

                Ingredient ingredient = new()
                {
                    Capacity = int.Parse(words[2].Split(',')[0]),
                    Durability = int.Parse(words[4].Split(',')[0]),
                    Flavor = int.Parse(words[6].Split(',')[0]),
                    Texture = int.Parse(words[8].Split(',')[0]),
                    Calories = int.Parse(words[10])
                };

                ingredients.Add(ingredient);
            }

            int highestScore = 0;

            // Iterate through each of the possibilities of the 3 ingredient totals
            // The 4th ingredient will always be 100 - the sum of the first 3
            for (int i = 0; i <= 100; i++)
            {
                for (int j = 0; j <= 100 - i; j++)
                {
                    for (int k = 0; k <= 100 - i - j; k++)
                    {
                        int l = 100 - i - j - k;

                        int capactity = Math.Max(0, (i * ingredients[0].Capacity) + (j * ingredients[1].Capacity) + (k * ingredients[2].Capacity) + (l * ingredients[3].Capacity));
                        int durability = Math.Max(0, (i * ingredients[0].Durability) + (j * ingredients[1].Durability) + (k * ingredients[2].Durability) + (l * ingredients[3].Durability));
                        int flavor = Math.Max(0, (i * ingredients[0].Flavor) + (j * ingredients[1].Flavor) + (k * ingredients[2].Flavor) + (l * ingredients[3].Flavor));
                        int texture = Math.Max(0, (i * ingredients[0].Texture) + (j * ingredients[1].Texture) + (k * ingredients[2].Texture) + (l * ingredients[3].Texture));
                        int calories = Math.Max(0, (i * ingredients[0].Calories) + (j * ingredients[1].Calories) + (k * ingredients[2].Calories) + (l * ingredients[3].Calories));

                        int score = capactity * durability * flavor * texture;

                        if (calories == 500 && score > highestScore)
                        {
                            highestScore = score;
                        }
                    }
                }
            }

            return highestScore.ToString();
        }

        private class Ingredient
        {
            public int Capacity { get; set; }
            public int Durability { get; set; }
            public int Flavor { get; set; }
            public int Texture { get; set; }
            public int Calories { get; set; }
        }
    }
}
