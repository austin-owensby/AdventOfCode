namespace AdventOfCode.Services
{
    public class Solution2015_24Service : ISolutionDayService
    {
        public Solution2015_24Service() { }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2015, 24, example);

            List<int> packages = lines.ToInts();
            int totalPackages = packages.Count;

            // If we evenly distribute the packages between 3 groups, this is the weight of a single group
            int groupWeight = packages.Sum() / 3;

            // The minimum number of packages it could be can be found by the minimum number of the largest packages added together to get us above the limit
            int minPackagesNeeded = Enumerable.Range(1, totalPackages).MinBy(x => packages.TakeLast(x).Sum() < groupWeight);
            // The maximum number of package would be if it was split 3 ways rounded down for the smallest group since anything greater would not be the smallest group
            int maxPackagesNeeded = totalPackages / 3;

            long answer = 0;

            // Loop between the min and max number of smallest group size
            for (int packageCount = minPackagesNeeded; packageCount <= maxPackagesNeeded; packageCount++) {
                // Initialize list of indexes to check 0, 1, 2, 3
                // This should be 1 shorter than the group length we're checking
                List<int> indexes = Enumerable.Range(0, packageCount - 1).ToList();
                List<List<int>> candidateGroups = new();

                // Stop once the first index reaches it's largest value
                // Ex. 26, 27, 28, 29
                while (indexes.First() < totalPackages - packageCount + 2) {
                    // Get the package group for the current indexes
                    List<int> packageGroup = packages.Where((package, index) => indexes.Contains(index)).ToList();

                    int sum = packageGroup.Sum();

                    // GroupWeight - sum gives us the weight that the last package should be
                    // Check if it exists
                    int missingIndex = packages.IndexOf(groupWeight - sum);

                    // We only care if the missing package's index is after our largest (last) index to prevent duplicates
                    // We also want to make sure that the missing package is not a duplicate within the group
                    if (indexes.Last() < missingIndex && !indexes.Contains(missingIndex)) {
                        packageGroup.Add(groupWeight - sum);
                        candidateGroups.Add(packageGroup);
                    }

                    // Move on to the next loop
                    indexes[packageCount - 2]++; // Increase the last index
                    // Loop from the last index to the first
                    for (int i = packageCount - 2; i >= 0; i--) {
                        // Check if we're at the max value for this index
                        // Ex. For the last index, that's 29, then 28 etc.
                        if (indexes[i] == (totalPackages - packageCount + 2 + i) && i != 0) {
                            // Since we're at the max for this index, increase the previous index
                            // Also set the current index to 1 more than the previous index to avoid duplicates
                            indexes[i] = ++indexes[i - 1] + 1;

                            for (int j = i + 1; j < packageCount - 1; j++) {
                                if (indexes[j] == (totalPackages - packageCount + 2 + j) ) {
                                    indexes[j] = indexes[j - 1] + 1;
                                }
                                else {
                                    break;
                                }
                            }
                        }
                    }
                }

                if (candidateGroups.Any()) {
                    // Order by quantum entanglement size (product of all elements)
                    candidateGroups = candidateGroups.OrderBy(g => g.Select(g => (long)g).Aggregate((a, b) => a * b)).ToList();

                    // Loop over each candidate to see if we can devide the remaining packages into 2 even groups
                    foreach (List<int> candidateGroup in candidateGroups) {
                        // Filter out the packages in the candidate group
                        List<int> packagesInner = packages.Where(p => !candidateGroup.Contains(p)).ToList();
                        int totalPackagesInner = packagesInner.Count;
                        // The minimum number of packages it could be can be found by the minimum number of the largest packages added together to get us above the limit
                        int minPackagesNeededInner = Enumerable.Range(1, totalPackagesInner).MinBy(x => packagesInner.TakeLast(x).Sum() < groupWeight);
                        // The maximum number of package would be if it was split 2 ways rounded down for the smallest group since anything greater would not be the smallest group
                        int maxPackagesNeededInner = totalPackagesInner / 2;

                        // Loop between the min and max number of smallest group size
                        for (int packageCountInner = minPackagesNeededInner; packageCountInner <= maxPackagesNeededInner; packageCountInner++) {
                            // Initialize list of indexes to check 0, 1, 2, 3
                            // This should be 1 shorter than the group length we're checking
                            List<int> indexesInner = Enumerable.Range(0, packageCountInner - 1).ToList();

                            // Stop once the first index reaches it's largest value
                            // Ex. 26, 27, 28, 29
                            while (indexesInner.First() < totalPackagesInner - packageCountInner + 2) {
                                // Get the package group for the current indexes
                                List<int> packageGroup = packagesInner.Where((package, index) => indexesInner.Contains(index)).ToList();

                                int sum = packageGroup.Sum();

                                // GroupWeight - sum gives us the weight that the last package should be
                                // Check if it exists
                                int missingIndex = packagesInner.IndexOf(groupWeight - sum);

                                // We've found a solution, calculate the answer and stop looping
                                if (missingIndex != -1 && !indexesInner.Contains(missingIndex)) {
                                    // Answer is the QE of the candiate group
                                    answer = candidateGroup.Select(g => (long)g).Aggregate((a, b) => a * b);
                                    packageGroup.Add(packagesInner[missingIndex]);
                                    break;
                                }

                                // Move on to the next loop
                                indexesInner[packageCountInner - 2]++; // Increase the last index
                                // Loop from the last index to the first
                                for (int i = packageCountInner - 2; i >= 0; i--) {
                                    // Check if we're at the max value for this index
                                    // Ex. For the last index, that's 29, then 28 etc.
                                    if (indexesInner[i] == (totalPackagesInner - packageCountInner + 2 + i) && i != 0) {
                                        // Since we're at the max for this index, increase the previous index
                                        // Also set the current index to 1 more than the previous index to avoid duplicates
                                        indexesInner[i] = ++indexesInner[i - 1] + 1;

                                        for (int j = i + 1; j < packageCountInner - 1; j++) {
                                            if (indexesInner[j] == (totalPackagesInner - packageCountInner + 2 + j) ) {
                                                indexesInner[j] = indexesInner[j - 1] + 1;
                                            }
                                            else {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        
                            // If we found an answer, then this is the best answer because we've sorted on QE, stop looping
                            if (answer > 0) {
                                break;
                            }
                        }
                    
                        // If we found the answer, stop looping
                        if (answer > 0) {
                            break;
                        }
                    }
                }
            
                // If we found the answer, stop looping
                if (answer > 0) {
                    break;
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2015, 24, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }
    }
}