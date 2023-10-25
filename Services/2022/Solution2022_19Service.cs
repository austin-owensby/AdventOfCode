namespace AdventOfCode.Services
{
    public class Solution2022_19Service : ISolutionDayService
    {
                private class Blueprint {
            public int Id {get; set;}
            public int OreCost {get; set;}
            public int ClayOreCost {get; set;}
            public int ObsidianOreCost {get; set;}
            public int ObsidianClayCost {get; set;}
            public int GeodeOreCost {get; set;}
            public int GeodeObsidianCost {get; set;}
        }

        private int GetGeodesProduced(Blueprint blueprint, int time, int ore = 0, int clay = 0, int obsidian = 0, int geodes = 0, int oreRobots = 1, int clayRobots = 0, int obsidianRobots = 0, int geodeRobots = 0) {
            // Calculate resources produced this turn
            int newOre = ore + oreRobots;
            int newClay = clay + clayRobots;
            int newObsidian = obsidian + obsidianRobots;
            int newGeodes = geodes + geodeRobots;

            if (time > 1) {
                List<int> options = new();

                int turnsToGeode = (int)Math.Ceiling(Math.Max((blueprint.GeodeOreCost - ore)/(double)oreRobots, (obsidianRobots == 0 ? int.MaxValue : (blueprint.GeodeObsidianCost - obsidian)/(double)obsidianRobots)));
                turnsToGeode = Math.Max(turnsToGeode, 0);

                // 1) Produce geode robot
                if (time - turnsToGeode > 1) {
                    // Try saving for a geode robot
                    options.Add(GetGeodesProduced(blueprint, time - 1 - turnsToGeode, newOre + oreRobots * turnsToGeode - blueprint.GeodeOreCost, newClay + clayRobots * turnsToGeode, newObsidian + obsidianRobots * turnsToGeode - blueprint.GeodeObsidianCost, newGeodes + geodeRobots * turnsToGeode, oreRobots, clayRobots, obsidianRobots, geodeRobots + 1));
                }
                
                if (turnsToGeode != 0) {
                    // 2) Produce obsidian robot (If needed)
                    if (obsidianRobots < blueprint.GeodeObsidianCost) {
                        int turnsToObsidian = (int)Math.Ceiling(Math.Max((blueprint.ObsidianOreCost - ore)/(double)oreRobots, (clayRobots == 0 ? int.MaxValue : (blueprint.ObsidianClayCost - clay)/(double)clayRobots)));
                        turnsToObsidian = Math.Max(turnsToObsidian, 0);

                        if (time - turnsToObsidian > 1) {
                            // Try saving for a obsidian robot
                            options.Add(GetGeodesProduced(blueprint, time - 1 - turnsToObsidian, newOre + oreRobots * turnsToObsidian - blueprint.ObsidianOreCost, newClay + clayRobots * turnsToObsidian - blueprint.ObsidianClayCost, newObsidian + obsidianRobots * turnsToObsidian, newGeodes + geodeRobots * turnsToObsidian, oreRobots, clayRobots, obsidianRobots + 1, geodeRobots));
                        }
                    }

                    // 3) Produce clay robot (If needed)
                    if (clayRobots < blueprint.ObsidianClayCost) {
                        int turnsToClay = (int)Math.Ceiling(Math.Max((blueprint.ClayOreCost - ore)/(double)oreRobots, 0));

                        if (time - turnsToClay > 1) {
                            // Try saving for a clay robot
                            options.Add(GetGeodesProduced(blueprint, time - 1 - turnsToClay, newOre + oreRobots * turnsToClay - blueprint.ClayOreCost, newClay + clayRobots * turnsToClay, newObsidian + obsidianRobots * turnsToClay, newGeodes + geodeRobots * turnsToClay, oreRobots, clayRobots + 1, obsidianRobots, geodeRobots));
                        }
                    }

                    // 4) Produce ore robot (If needed)
                    List<int> oreCosts = new(){blueprint.GeodeOreCost, blueprint.ObsidianOreCost, blueprint.ClayOreCost, blueprint.OreCost};
                    if (oreRobots < oreCosts.Max()) {
                        int turnsToOre = (int)Math.Ceiling(Math.Max((blueprint.OreCost - ore)/(double)oreRobots, 0));

                        if (time - turnsToOre > 1) {
                            // Try saving for an ore robot
                            options.Add(GetGeodesProduced(blueprint, time - 1 - turnsToOre, newOre + oreRobots * turnsToOre - blueprint.OreCost, newClay + clayRobots * turnsToOre, newObsidian + obsidianRobots * turnsToOre, newGeodes + geodeRobots * turnsToOre, oreRobots + 1, clayRobots, obsidianRobots, geodeRobots));
                        }
                    }
                }

                // Increase geodes based on options
                if (options.Any()) {
                    newGeodes = options.Max();
                }
                else {
                    // We can't build any more robots, find the rest of our geode count
                    newGeodes += (time - 1) * geodeRobots;
                }
            }
            
            return newGeodes;
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 19, example);

            List<Blueprint> blueprints = lines.QuickRegex(@"Blueprint (\d+): Each ore robot costs (\d+) ore\. Each clay robot costs (\d+) ore\. Each obsidian robot costs (\d+) ore and (\d+) clay\. Each geode robot costs (\d+) ore and (\d+) obsidian\.")
                .ToInts()
                .Select(b => new Blueprint(){
                    Id = b[0],
                    OreCost = b[1],
                    ClayOreCost = b[2],
                    ObsidianOreCost = b[3],
                    ObsidianClayCost = b[4],
                    GeodeOreCost = b[5],
                    GeodeObsidianCost = b[6],
                }).ToList();

            int answer = blueprints.Sum(blueprint => blueprint.Id * GetGeodesProduced(blueprint, 24));

            return answer.ToString();
        } 

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 19, example);

            List<Blueprint> blueprints = lines.Take(3).QuickRegex(@"Blueprint (\d+): Each ore robot costs (\d+) ore\. Each clay robot costs (\d+) ore\. Each obsidian robot costs (\d+) ore and (\d+) clay\. Each geode robot costs (\d+) ore and (\d+) obsidian\.")
                .ToInts()
                .Select(b => new Blueprint(){
                    Id = b[0],
                    OreCost = b[1],
                    ClayOreCost = b[2],
                    ObsidianOreCost = b[3],
                    ObsidianClayCost = b[4],
                    GeodeOreCost = b[5],
                    GeodeObsidianCost = b[6],
                }).ToList();

            int answer = 1;

            foreach (Blueprint blueprint in blueprints) {
                answer *= GetGeodesProduced(blueprint, 32);
            }

            return answer.ToString();
        }
    }
}