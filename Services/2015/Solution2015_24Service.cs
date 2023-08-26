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
                List<List<int>> candidateGroups = new();

                // This should be 1 shorter than the group length we're checking
                IEnumerable<IEnumerable<int>> indexesList = Enumerable.Range(0, totalPackages).GetCombinations(packageCount - 1);

                foreach (IEnumerable<int> indexes in indexesList) {
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
                            // This should be 1 shorter than the group length we're checking
                            IEnumerable<IEnumerable<int>> indexesListInner = Enumerable.Range(0, totalPackagesInner).GetCombinations(packageCountInner - 1);

                            foreach (IEnumerable<int> indexesInner in indexesListInner) {
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
                                    return answer.ToString();
                                }
                            }
                        }
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2015, 24, example);

            List<int> packages = lines.ToInts();
            int totalPackages = packages.Count;

            // If we evenly distribute the packages between 4 groups, this is the weight of a single group
            int groupWeight = packages.Sum() / 4;

            // The minimum number of packages it could be can be found by the minimum number of the largest packages added together to get us above the limit
            int minPackagesNeeded = Enumerable.Range(1, totalPackages).MinBy(x => packages.TakeLast(x).Sum() < groupWeight);
            // The maximum number of package would be if it was split 4 ways rounded down for the smallest group since anything greater would not be the smallest group
            int maxPackagesNeeded = totalPackages / 4;

            long answer = 0;

            // Loop between the min and max number of smallest group size
            for (int packageCount = minPackagesNeeded; packageCount <= maxPackagesNeeded; packageCount++) {
                List<List<int>> candidateGroups = new();

                // This should be 1 shorter than the group length we're checking
                IEnumerable<IEnumerable<int>> indexesList = Enumerable.Range(0, totalPackages).GetCombinations(packageCount - 1);

                foreach (IEnumerable<int> indexes in indexesList) {
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
                        // The maximum number of package would be if it was split 3 ways rounded down for the smallest group since anything greater would not be the smallest group
                        int maxPackagesNeededInner = totalPackagesInner / 3;

                        // Loop between the min and max number of smallest group size
                        for (int packageCountInner = minPackagesNeededInner; packageCountInner <= maxPackagesNeededInner; packageCountInner++) {
                            List<List<int>> candidateGroupsInner = new();

                            // This should be 1 shorter than the group length we're checking
                            IEnumerable<IEnumerable<int>> indexesInnerList = Enumerable.Range(0, totalPackagesInner).GetCombinations(packageCountInner - 1);

                            foreach (IEnumerable<int> indexesInner in indexesInnerList) {
                                // Get the package group for the current indexes
                                List<int> packageGroup = packagesInner.Where((package, index) => indexesInner.Contains(index)).ToList();

                                int sum = packageGroup.Sum();

                                // GroupWeight - sum gives us the weight that the last package should be
                                // Check if it exists
                                int missingIndex = packagesInner.IndexOf(groupWeight - sum);

                                // We only care if the missing package's index is after our largest (last) index to prevent duplicates
                                // We also want to make sure that the missing package is not a duplicate within the group
                                if (indexesInner.Last() < missingIndex && !indexesInner.Contains(missingIndex)) {
                                    packageGroup.Add(groupWeight - sum);
                                    candidateGroupsInner.Add(packageGroup);
                                }
                            }
                            
                            if (candidateGroupsInner.Any()) {
                                // Order by quantum entanglement size (product of all elements)
                                candidateGroupsInner = candidateGroupsInner.OrderBy(g => g.Select(g => (long)g).Aggregate((a, b) => a * b)).ToList();

                                // Loop over each candidate to see if we can devide the remaining packages into 2 even groups
                                foreach (List<int> candidateGroupInner in candidateGroupsInner) {
                                    // Filter out the packages in the candidate group
                                    List<int> packagesInner2 = packagesInner.Where(p => !candidateGroupInner.Contains(p)).ToList();
                                    int totalPackagesInner2 = packagesInner2.Count;
                                    // The minimum number of packages it could be can be found by the minimum number of the largest packages added together to get us above the limit
                                    int minPackagesNeededInner2 = Enumerable.Range(1, totalPackagesInner2).MinBy(x => packagesInner2.TakeLast(x).Sum() < groupWeight);
                                    // The maximum number of package would be if it was split 2 ways rounded down for the smallest group since anything greater would not be the smallest group
                                    int maxPackagesNeededInner2 = totalPackagesInner2 / 2;

                                    // Loop between the min and max number of smallest group size
                                    for (int packageCountInner2 = minPackagesNeededInner2; packageCountInner2 <= maxPackagesNeededInner2; packageCountInner2++) {
                                        // This should be 1 shorter than the group length we're checking
                                        IEnumerable<IEnumerable<int>> indexesInnerList2 = Enumerable.Range(0, totalPackagesInner2).GetCombinations(packageCountInner2 - 1);

                                        foreach (IEnumerable<int> indexesInner2 in indexesInnerList2) {
                                            // Get the package group for the current indexes
                                            List<int> packageGroup = packagesInner2.Where((package, index) => indexesInner2.Contains(index)).ToList();

                                            int sum = packageGroup.Sum();

                                            // GroupWeight - sum gives us the weight that the last package should be
                                            // Check if it exists
                                            int missingIndex = packagesInner2.IndexOf(groupWeight - sum);

                                            // We've found a solution, calculate the answer and stop looping
                                            if (missingIndex != -1 && !indexesInner2.Contains(missingIndex)) {
                                                // Answer is the QE of the candiate group (user original candidate for the answer instead of the inner)
                                                answer = candidateGroup.Select(g => (long)g).Aggregate((a, b) => a * b);
                                                return answer.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return answer.ToString();
        }
    }
}