namespace AdventOfCode.Services
{
    public class Solution2015_21Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "21.txt"));

            string[] lines = data.Split("\n");
            int bossStartHealth = int.Parse(lines[0].Split(" ")[2]);
            int bossDamage = int.Parse(lines[1].Split(" ")[1]);
            int bossArmor = int.Parse(lines[2].Split(" ")[1]);

            List<Item> weapons = new(){
                new(8, 4, 0),
                new(10, 5, 0),
                new(25, 6, 0),
                new(40, 7, 0),
                new(74, 8, 0)
            };

            List<Item> armors = new(){
                new(0, 0, 0),
                new(13, 0, 1),
                new(31, 0, 2),
                new(53, 0, 3),
                new(75, 0, 4),
                new(102, 0, 5)
            };

            List<Item> rings = new(){
                new(0, 0, 0),
                new(0, 0, 0),
                new(25, 1, 0),
                new(50, 2, 0),
                new(100, 3, 0),
                new(20, 0, 1),
                new(40, 0, 2),
                new(80, 0, 3)
            };

            int minCost = int.MaxValue;

            foreach (Item weapon in weapons)
            {
                foreach (Item armor in armors)
                {
                    for (int i = 0; i < rings.Count; i++)
                    {
                        for (int j = 0; j < rings.Count; j++)
                        {
                            if (i == j)
                            {
                                continue;
                            }

                            Item ring1 = rings[i];
                            Item ring2 = rings[j];

                            List<Item> items = new() { weapon, armor, ring1, ring2 };

                            int totalCost = items.Sum(i => i.Cost);
                            int totalArmor = items.Sum(i => i.Armor);
                            int totalDamage = items.Sum(i => i.Damage);
                            int health = 100;
                            int bossHealth = bossStartHealth;

                            bool playersTurn = true;

                            while (health > 0 && bossHealth > 0)
                            {
                                if (playersTurn)
                                {
                                    bossHealth -= Math.Max(1, totalDamage - bossArmor);
                                }
                                else
                                {
                                    health -= Math.Max(1, bossDamage - totalArmor);
                                }

                                playersTurn = !playersTurn;
                            }

                            if (health > 0 && totalCost < minCost)
                            {
                                minCost = totalCost;
                            }
                        }
                    }
                }
            }

            return minCost.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "21.txt"));

            string[] lines = data.Split("\n");
            int bossStartHealth = int.Parse(lines[0].Split(" ")[2]);
            int bossDamage = int.Parse(lines[1].Split(" ")[1]);
            int bossArmor = int.Parse(lines[2].Split(" ")[1]);

            List<Item> weapons = new(){
                new(8, 4, 0),
                new(10, 5, 0),
                new(25, 6, 0),
                new(40, 7, 0),
                new(74, 8, 0)
            };

            List<Item> armors = new(){
                new(0, 0, 0),
                new(13, 0, 1),
                new(31, 0, 2),
                new(53, 0, 3),
                new(75, 0, 4),
                new(102, 0, 5)
            };

            List<Item> rings = new(){
                new(0, 0, 0),
                new(0, 0, 0),
                new(25, 1, 0),
                new(50, 2, 0),
                new(100, 3, 0),
                new(20, 0, 1),
                new(40, 0, 2),
                new(80, 0, 3)
            };

            int maxCost = int.MinValue;

            foreach (Item weapon in weapons)
            {
                foreach (Item armor in armors)
                {
                    for (int i = 0; i < rings.Count; i++)
                    {
                        for (int j = 0; j < rings.Count; j++)
                        {
                            if (i == j)
                            {
                                continue;
                            }

                            Item ring1 = rings[i];
                            Item ring2 = rings[j];

                            List<Item> items = new() { weapon, armor, ring1, ring2 };

                            int totalCost = items.Sum(i => i.Cost);
                            int totalArmor = items.Sum(i => i.Armor);
                            int totalDamage = items.Sum(i => i.Damage);
                            int health = 100;
                            int bossHealth = bossStartHealth;

                            bool playersTurn = true;

                            while (health > 0 && bossHealth > 0)
                            {
                                if (playersTurn)
                                {
                                    bossHealth -= Math.Max(1, totalDamage - bossArmor);
                                }
                                else
                                {
                                    health -= Math.Max(1, bossDamage - totalArmor);
                                }

                                playersTurn = !playersTurn;
                            }

                            if (bossHealth > 0 && totalCost > maxCost)
                            {
                                maxCost = totalCost;
                            }
                        }
                    }
                }
            }

            return maxCost.ToString();
        }
        
        private class Item
        {
            public int Cost { get; set; }
            public int Damage { get; set; }
            public int Armor { get; set; }

            public Item(int cost, int damage, int armor)
            {
                Cost = cost;
                Damage = damage;
                Armor = armor;
            }
        }
    }

}
