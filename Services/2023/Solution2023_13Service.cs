namespace AdventOfCode.Services
{
    public class Solution2023_13Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 13, example);
            List<List<List<char>>> groups = lines.ChunkByExclusive(line => line.Length == 0).Select(line => line.Select(x => x.ToList()).ToList()).ToList();

            int answer = 0;

            foreach (List<List<char>> group in groups)
            {
                // Check vertical reflection
                bool reflectionFound = false;

                // Test each reflection point
                for (int i = 1; i <= group.First().Count - 1; i++) {
                    // Calculate the length of the reflection
                    int reflectionLength = Math.Min(i, group.First().Count - i);

                    // Check each step of the reflection
                    bool isReflection = true;
                    foreach (int x in reflectionLength) {
                        foreach (int y in group.Count) {
                            if (group[y][i + x] != group[y][i - x - 1]) {
                                isReflection = false;
                                break;
                            }
                        }

                        if (!isReflection) {
                            break;
                        }
                    }

                    if (isReflection) {
                        reflectionFound = true;
                        answer += i;
                        break;
                    }
                }

                // Check horizontal reflection
                if (!reflectionFound) {
                    // Test each reflection point
                    for (int i = 1; i <= group.Count - 1; i++) {
                        // Calculate the length of the reflection
                        int reflectionLength = Math.Min(i, group.Count - i);

                        // Check each step of the reflection
                        bool isReflection = true;
                        foreach (int y in reflectionLength) {
                            foreach (int x in group.First().Count) {
                                if (group[i + y][x] != group[i - y - 1][x]) {
                                    isReflection = false;
                                    break;
                                }
                            }

                            if (!isReflection) {
                                break;
                            }
                        }

                        if (isReflection) {
                            reflectionFound = true;
                            answer += 100 * i;
                            break;
                        }
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 13, example);
            List<List<List<char>>> groups = lines.ChunkByExclusive(line => line.Length == 0).Select(line => line.Select(x => x.ToList()).ToList()).ToList();

            int answer = 0;

            List<(bool, int)> currentReflections = [];

            foreach (List<List<char>> group in groups)
            {
                // Check vertical reflection
                bool reflectionFound = false;

                // Test each reflection point
                for (int i = 1; i <= group.First().Count - 1; i++) {
                    // Calculate the length of the reflection
                    int reflectionLength = Math.Min(i, group.First().Count - i);

                    // Check each step of the reflection
                    bool isReflection = true;
                    foreach (int x in reflectionLength) {
                        foreach (int y in group.Count) {
                            if (group[y][i + x] != group[y][i - x - 1]) {
                                isReflection = false;
                                break;
                            }
                        }

                        if (!isReflection) {
                            break;
                        }
                    }

                    if (isReflection) {
                        reflectionFound = true;
                        currentReflections.Add((true, i));
                        break;
                    }
                }

                // Check horizontal reflection
                if (!reflectionFound) {
                    // Test each reflection point
                    for (int i = 1; i <= group.Count - 1; i++) {
                        // Calculate the length of the reflection
                        int reflectionLength = Math.Min(i, group.Count - i);

                        // Check each step of the reflection
                        bool isReflection = true;
                        foreach (int y in reflectionLength) {
                            foreach (int x in group.First().Count) {
                                if (group[i + y][x] != group[i - y - 1][x]) {
                                    isReflection = false;
                                    break;
                                }
                            }

                            if (!isReflection) {
                                break;
                            }
                        }

                        if (isReflection) {
                            reflectionFound = true;
                            currentReflections.Add((false, i));
                            break;
                        }
                    }
                }
            }

            int groupIndex = 0;
            foreach (List<List<char>> group in groups)
            {
                bool stopLooping = false;
                // Loop over each char and change 1 value
                foreach (int a in group.Count) {
                    foreach (int b in group[a].Count) {
                        char originalValue = group[a][b];

                        group[a][b] = originalValue == '#' ? '.' : '#';

                        // Check vertical reflection
                        bool reflectionFound = false;

                        // Test each reflection point
                        for (int i = 1; i <= group.First().Count - 1; i++) {
                            // Calculate the length of the reflection
                            int reflectionLength = Math.Min(i, group.First().Count - i);

                            // Check each step of the reflection
                            bool isReflection = true;
                            foreach (int x in reflectionLength) {
                                foreach (int y in group.Count) {
                                    if (group[y][i + x] != group[y][i - x - 1]) {
                                        isReflection = false;
                                        break;
                                    }
                                }

                                if (!isReflection) {
                                    break;
                                }
                            }

                            if (isReflection) {
                                if (!(currentReflections[groupIndex].Item1 && currentReflections[groupIndex].Item2 == i)) {
                                    reflectionFound = true;
                                    answer += i;
                                    break;
                                }
                            }
                        }

                        // Check horizontal reflection
                        if (!reflectionFound) {
                            // Test each reflection point
                            for (int i = 1; i <= group.Count - 1; i++) {
                                // Calculate the length of the reflection
                                int reflectionLength = Math.Min(i, group.Count - i);

                                // Check each step of the reflection
                                bool isReflection = true;
                                foreach (int y in reflectionLength) {
                                    foreach (int x in group.First().Count) {
                                        if (group[i + y][x] != group[i - y - 1][x]) {
                                            isReflection = false;
                                            break;
                                        }
                                    }

                                    if (!isReflection) {
                                        break;
                                    }
                                }

                                if (isReflection) {
                                    if (!(!currentReflections[groupIndex].Item1 && currentReflections[groupIndex].Item2 == i)) {
                                        reflectionFound = true;
                                        answer += 100 * i;
                                        break;
                                    }
                                }
                            }
                        }

                        if (reflectionFound) {
                            stopLooping = true;
                            break;
                        }

                        group[a][b] = originalValue;
                    }

                    if (stopLooping) {
                        break;
                    }
                }
                groupIndex++;
            }

            return answer.ToString();
        }
    }
}