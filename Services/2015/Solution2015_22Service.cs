namespace AdventOfCode.Services
{
    public class Solution2015_22Service : ISolutionDayService
    {
        public Solution2015_22Service() { }

        private int bestSoFar {get; set;} = 0;

        private int GetMinManaUsed(int playerHealth, int bossHealth, int bossDamage, int armor, int mana, int poisonTimer, int rechargeTimer, int shieldTimer, bool playerTurn, bool hardMode = false) {
            int newPlayerHealth = playerHealth;
            int newBossHealth = bossHealth;
            int newArmor = armor;
            int newMana = mana;
            int newPoisonTimer = poisonTimer;
            int newRechargeTimer = rechargeTimer;
            int newShieldTimer = shieldTimer;

            List<int> options = new();

            // Handle effects
            if (hardMode) {
                newPlayerHealth--;
            }

            if (newPlayerHealth > 0) {
                if (newRechargeTimer > 0) {
                    newMana += 101;
                    newRechargeTimer--;
                }

                if (newPoisonTimer > 0) {
                    newBossHealth -= 3;
                    newPoisonTimer--;
                }

                if (newShieldTimer > 0) {
                    newShieldTimer--;

                    if (newShieldTimer == 0) {
                        newArmor = 0;
                    }
                }

                if (newBossHealth <= 0) {
                    options.Add(0);
                }
                else {
                    if (playerTurn) {
                        // Magic missle
                        if (newMana >= 53 && newBossHealth - 4 <= 0) {
                            // If me can immediately kill the boss with the cheapest option, go for it without testing anything else
                            options.Add(53);
                        }
                        else {
                            // Magic missle
                            if (newMana >= 53) {
                                options.Add(53 + GetMinManaUsed(newPlayerHealth, newBossHealth - 4, bossDamage, newArmor, newMana - 53, newPoisonTimer, newRechargeTimer, newShieldTimer, !playerTurn, hardMode));
                            }

                            // Drain
                            if (newMana >= 73) {
                                if (newBossHealth - 2 <= 0) {
                                    options.Add(73);
                                }
                                else {
                                    options.Add(73 + GetMinManaUsed(newPlayerHealth + 2, newBossHealth - 2, bossDamage, newArmor, newMana - 73, newPoisonTimer, newRechargeTimer, newShieldTimer, !playerTurn, hardMode));
                                }
                            }

                            // Shield
                            if (newMana >= 113 && newShieldTimer == 0) {
                                options.Add(113 + GetMinManaUsed(newPlayerHealth, newBossHealth, bossDamage, 7, newMana - 113, newPoisonTimer, newRechargeTimer, 6, !playerTurn, hardMode));
                            }

                            // Poison
                            if (newMana >= 173 && newPoisonTimer == 0) {
                                options.Add(173 + GetMinManaUsed(newPlayerHealth, newBossHealth, bossDamage, newArmor, newMana - 173, 6, newRechargeTimer, newShieldTimer, !playerTurn, hardMode));
                            }

                            // Recharge
                            if (newMana >= 229 && newRechargeTimer == 0) {
                                options.Add(229 + GetMinManaUsed(newPlayerHealth, newBossHealth, bossDamage, newArmor, newMana - 229, newPoisonTimer, 5, newShieldTimer, !playerTurn, hardMode));
                            }
                        }
                    }
                    else {
                        newPlayerHealth -= (bossDamage - newArmor);

                        if (newPlayerHealth > 0) {
                            options.Add(GetMinManaUsed(newPlayerHealth, newBossHealth, bossDamage, newArmor, newMana, newPoisonTimer, newRechargeTimer, newShieldTimer, !playerTurn, hardMode));
                        }
                    }
                }
            }

            // /2 to prevent overflowing
            return options.Any() ? options.Min() : (int.MaxValue / 2);
        } 

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2015, 22, example);

            int bossHitPoints = lines.First().QuickRegex(@"Hit Points: (\d+)").ToInts().First();
            int bossDamage = lines.Last().QuickRegex(@"Damage: (\d+)").ToInts().First();
            
            int playerHitPoints = 50;
            int playerMana = 500;

            int answer = GetMinManaUsed(playerHitPoints, bossHitPoints, bossDamage, 0, playerMana, 0, 0, 0, true);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2015, 22, example);

            int bossHitPoints = lines.First().QuickRegex(@"Hit Points: (\d+)").ToInts().First();
            int bossDamage = lines.Last().QuickRegex(@"Damage: (\d+)").ToInts().First();
            
            int playerHitPoints = 50;
            int playerMana = 500;

            int answer = GetMinManaUsed(playerHitPoints, bossHitPoints, bossDamage, 0, playerMana, 0, 0, 0, true, true);

            return answer.ToString();
        }
    }
}