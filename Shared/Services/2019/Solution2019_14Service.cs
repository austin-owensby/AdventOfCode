namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/2019/14.txt
    public class Solution2019_14Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 14, example);

            // Parse reactions
            Dictionary<string, (int amount, List<(string chemical, int amount)>)> reactions = [];

            foreach (string line in lines)
            {
                string[] parts = line.Split(" => ");
                string[] chemicals = parts[0].Split(", ");
                List<(string chemical, int amount)> parsedChemicals = [];

                foreach (string chemical in chemicals)
                {
                    string[] chemicalParts = chemical.Split(" ");
                    int chemicalAmount = int.Parse(chemicalParts[0]);
                    string chemicalName = chemicalParts[1];
                    parsedChemicals.Add((chemicalName, chemicalAmount));
                }

                string[] resultParts = parts[1].Split(" ");
                int amount = int.Parse(resultParts[0]);
                string name = resultParts[1];

                reactions[name] = (amount, parsedChemicals);
            }

            Dictionary<string, int> needs = [];
            needs["FUEL"] = 1;

            Dictionary<string, int> remainders = [];

            int answer = 0;

            // Keep looping while we have a need
            while (needs.Count > 0)
            {
                // Check the next need on our list
                (string chemical, int amountNeeded) = needs.First();
                needs.Remove(chemical);
                int remainderAmount = remainders.GetValueOrDefault(chemical);

                // Remove the used amount from the remainder
                remainders[chemical] = Math.Max(remainderAmount - amountNeeded, 0);

                // Check if we already have our need in our remainder
                if (remainderAmount > amountNeeded)
                {
                    continue;
                }

                // Subtract any remainder we already have
                amountNeeded -= remainderAmount;

                (int resultAmount, List<(string, int)> requirements) = reactions[chemical];

                int reactionsRequired = (int)Math.Ceiling((double)amountNeeded / resultAmount);
                int totalResult = reactionsRequired * resultAmount;

                // If we generate more than we need from this reaction, save the remainder for later reactions
                if (totalResult > amountNeeded)
                {
                    remainders[chemical] += totalResult - amountNeeded;
                }

                // Add requirements of the reaction to our list of needs
                foreach ((string requirement, int requirementAmount) in requirements)
                {
                    int totalRequirementAmount = requirementAmount * reactionsRequired;

                    if (requirement == "ORE")
                    {
                        answer += totalRequirementAmount;
                    }
                    else
                    {
                        needs[requirement] = needs.GetValueOrDefault(requirement) + totalRequirementAmount;
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2019, 14, example);

            // Parse reactions
            Dictionary<string, (int amount, List<(string chemical, int amount)>)> reactions = [];

            foreach (string line in lines)
            {
                string[] parts = line.Split(" => ");
                string[] chemicals = parts[0].Split(", ");
                List<(string chemical, int amount)> parsedChemicals = [];

                foreach (string chemical in chemicals)
                {
                    string[] chemicalParts = chemical.Split(" ");
                    int chemicalAmount = int.Parse(chemicalParts[0]);
                    string chemicalName = chemicalParts[1];
                    parsedChemicals.Add((chemicalName, chemicalAmount));
                }

                string[] resultParts = parts[1].Split(" ");
                int amount = int.Parse(resultParts[0]);
                string name = resultParts[1];

                reactions[name] = (amount, parsedChemicals);
            }

            Dictionary<string, int> remainders = [];
            Dictionary<string, int> needs = [];

            long oreUsed = 0;
            long oreTarget = 1_000_000_000_000;
            long answer = 0;

            List<string> cycles = [];

            // Keep looping until we've used all of our ore
            while (oreUsed < oreTarget)
            {
                needs["FUEL"] = 1;

                // Keep looping while we have a need
                while (needs.Count > 0)
                {
                    // Check the next need on our list
                    (string chemical, int amountNeeded) = needs.First();
                    needs.Remove(chemical);
                    int remainderAmount = remainders.GetValueOrDefault(chemical);

                    // Remove the used amount from the remainder
                    remainders[chemical] = Math.Max(remainderAmount - amountNeeded, 0);

                    // Check if we already have our need in our remainder
                    if (remainderAmount > amountNeeded)
                    {
                        continue;
                    }

                    // Subtract any remainder we already have
                    amountNeeded -= remainderAmount;

                    (int resultAmount, List<(string, int)> requirements) = reactions[chemical];

                    int reactionsRequired = (int)Math.Ceiling((double)amountNeeded / resultAmount);
                    int totalResult = reactionsRequired * resultAmount;

                    // If we generate more than we need from this reaction, save the remainder for later reactions
                    if (totalResult > amountNeeded)
                    {
                        remainders[chemical] += totalResult - amountNeeded;
                    }

                    // Add requirements of the reaction to our list of needs
                    foreach ((string requirement, int requirementAmount) in requirements)
                    {
                        int totalRequirementAmount = requirementAmount * reactionsRequired;

                        if (requirement == "ORE")
                        {
                            oreUsed += totalRequirementAmount;
                        }
                        else
                        {
                            needs[requirement] = needs.GetValueOrDefault(requirement) + totalRequirementAmount;
                        }
                    }
                }

                // Increase the amount of fuel produced
                answer++;

                // Check if we've reset back to having 0 remainders
                bool reset = remainders.All(r => r.Value == 0);

                if (reset)
                {
                    // Fast forward a whole number of cycles while still under the goal
                    long cyclesNeeded = oreTarget / oreUsed;
                    oreUsed *= cyclesNeeded;
                    answer *= cyclesNeeded;
                    // After this we need to get the last few bits of fuel before another cycle repeat
                }
            }

            // Remove the extra fuel from the cycle that used the last of the ore
            answer--;

            return answer.ToString();
        }
    }
}