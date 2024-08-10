namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2015/19.txt
    public class Solution2015_19Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "19.txt"));

            string[] parsedData = data.Split("\n\n");
            string molecule = parsedData[1].Split("\n")[0];
            string[] replacements = parsedData[0].Split("\n");
            HashSet<string> distinctMolecules = new();

            foreach (string replacement in replacements)
            {
                string source = replacement.Split(" => ")[0];
                string destination = replacement.Split(" => ")[1];

                string[] splits = molecule.Split(source);

                foreach (int i in splits.Length - 1)
                {
                    string newMolecule = "";

                    foreach (int j in i)
                    {
                        newMolecule += $"{splits[j]}{source}";
                    }

                    newMolecule += $"{splits[i]}{destination}";

                    for (int j = i + 1; j < splits.Length; j++)
                    {
                        newMolecule += splits[j];

                        if (j != splits.Length - 1)
                        {
                            newMolecule += source;
                        }
                    }

                    _ = distinctMolecules.Add(newMolecule);
                }
            }

            return distinctMolecules.Count.ToString();
        }

        public string SecondHalf(bool example)
        {
            string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Inputs", "2015", "19.txt"));

            string[] parsedData = data.Split("\n\n");
            string molecule = parsedData[1].Split("\n")[0];
            Random rnd = new(); // There is probably a better way to do this, but this is my compromise between brute force and an actual solution
            string[] replacements = parsedData[0].Split("\n").OrderBy(p => rnd.Next()).ToArray();
            HashSet<string> distinctMolecules = new();

            int replacementCount = 0;

            while (molecule != "e")
            {
                bool matchFound = false;

                foreach (string replacement in replacements)
                {
                    string source = replacement.Split(" => ")[0];
                    string destination = replacement.Split(" => ")[1];

                    int position = molecule.IndexOf(destination);

                    if (position != -1)
                    {
                        molecule = $"{molecule[..position]}{source}{molecule[(position + destination.Length)..]}";
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    throw new Exception("Nothing found, try again.");
                }

                replacementCount++;
            }

            return replacementCount.ToString();
        }
    }
}
