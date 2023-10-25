namespace AdventOfCode.Services
{
    public class Solution2022_07Service : ISolutionDayService
    {
                private class Directory
        {
            public long Size { get; set; }
            public string Name { get; set; } = string.Empty;
            public List<DirFile> Files { get; set; } = new();
            public string ParentName { get; set; } = string.Empty;
        }

        private class DirFile
        {
            public string Name { get; set; } = string.Empty;
            public long Size { get; set; }
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 07, example);

            List<Directory> directories = new(){
                new() {
                    Name = "/"
                }
            };

            long answer = 0;

            Directory currentDirectory = directories.Single();

            int i = 0;

            while (i < lines.Count())
            {
                string line = lines[i];

                if (line.Contains("$ cd"))
                {
                    // Navigating to a new directory
                    string newDirectoryName = line.QuickRegex(@"\$ cd (.+)").Single();

                    if (newDirectoryName == "..")
                    {
                        if (currentDirectory.Name != "/")
                        {
                            // If we're already at the top, don't do anything
                            currentDirectory = directories.Single(d => d.Name == currentDirectory.ParentName);
                        }
                    }
                    else if (newDirectoryName == "/")
                    {
                        currentDirectory = directories.Single(d => d.Name == "/");
                    }
                    else
                    {
                        string parent = currentDirectory.Name == "/" ? "" : currentDirectory.Name;
                        currentDirectory = directories.Single(d => d.Name == $"{parent}/{newDirectoryName}");
                    }

                    i++;
                }
                else if (line.Contains("$ ls"))
                {
                    // List out the children of the current directory
                    i++;
                    line = lines[i];

                    while (!line.StartsWith("$"))
                    {
                        if (line.Contains("dir "))
                        {
                            // Directory
                            string parent = currentDirectory.Name == "/" ? "" : currentDirectory.Name;
                            string newDirectoryName = line.QuickRegex(@"dir (.+)").Single();
                            string dirName = $"{parent}/{newDirectoryName}";

                            if (!directories.Any(c => c.Name == dirName))
                            {
                                // Add the new dir
                                Directory newDir = new()
                                {
                                    Name = dirName,
                                    ParentName = currentDirectory.Name
                                };

                                directories.Add(newDir);
                            }
                        }
                        else
                        {
                            // File
                            string fileName = line.QuickRegex(@"(.+) (.+)")[1];

                            if (!currentDirectory.Files.Any(c => c.Name == fileName))
                            {
                                long size = int.Parse(line.QuickRegex(@"(.+) (.+)")[0]);

                                currentDirectory.Files.Add(new()
                                {
                                    Name = fileName,
                                    Size = size
                                });

                                // Increase the parent's size
                                Directory? parent = directories.Single(d => d.Name == currentDirectory.Name);

                                while (parent != null)
                                {
                                    parent.Size += size;
                                    parent = directories.FirstOrDefault(p => p.Name == parent.ParentName);
                                }
                            }
                        }

                        i++;

                        if (i < lines.Count)
                        {
                            line = lines[i];
                        }
                        else
                        {
                            // Ended input with ls
                            break;
                        }
                    }
                }
            }

            answer = directories.Sum(d => d.Size <= 100000 ? d.Size : 0);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 07, example);

            List<Directory> directories = new(){
                new() {
                    Name = "/"
                }
            };

            long answer = 0;

            Directory currentDirectory = directories.Single();

            int i = 0;

            while (i < lines.Count())
            {
                string line = lines[i];

                if (line.Contains("$ cd"))
                {
                    // Navigating to a new directory
                    string newDirectoryName = line.QuickRegex(@"\$ cd (.+)").Single();

                    if (newDirectoryName == "..")
                    {
                        if (currentDirectory.Name != "/")
                        {
                            // If we're already at the top, don't do anything
                            currentDirectory = directories.Single(d => d.Name == currentDirectory.ParentName);
                        }
                    }
                    else if (newDirectoryName == "/")
                    {
                        currentDirectory = directories.Single(d => d.Name == "/");
                    }
                    else
                    {
                        string parent = currentDirectory.Name == "/" ? "" : currentDirectory.Name;
                        currentDirectory = directories.Single(d => d.Name == $"{parent}/{newDirectoryName}");
                    }

                    i++;
                }
                else if (line.Contains("$ ls"))
                {
                    // List out the children of the current directory
                    i++;
                    line = lines[i];

                    while (!line.StartsWith("$"))
                    {
                        if (line.Contains("dir "))
                        {
                            // Directory
                            string parent = currentDirectory.Name == "/" ? "" : currentDirectory.Name;
                            string newDirectoryName = line.QuickRegex(@"dir (.+)").Single();
                            string dirName = $"{parent}/{newDirectoryName}";

                            if (!directories.Any(c => c.Name == dirName))
                            {
                                // Add the new dir
                                Directory newDir = new()
                                {
                                    Name = dirName,
                                    ParentName = currentDirectory.Name
                                };

                                directories.Add(newDir);
                            }
                        }
                        else
                        {
                            // File
                            string fileName = line.QuickRegex(@"(.+) (.+)")[1];

                            if (!currentDirectory.Files.Any(c => c.Name == fileName))
                            {
                                long size = int.Parse(line.QuickRegex(@"(.+) (.+)")[0]);

                                currentDirectory.Files.Add(new()
                                {
                                    Name = fileName,
                                    Size = size
                                });

                                // Increase the parent's size
                                Directory? parent = directories.Single(d => d.Name == currentDirectory.Name);

                                while (parent != null)
                                {
                                    parent.Size += size;
                                    parent = directories.FirstOrDefault(p => p.Name == parent.ParentName);
                                }
                            }
                        }

                        i++;

                        if (i < lines.Count)
                        {
                            line = lines[i];
                        }
                        else
                        {
                            // Ended input with ls
                            break;
                        }
                    }
                }
            }

            Directory topDir = directories.First(d => d.Name == "/");

            long totalSpace = 70000000;

            long unusedSpace = totalSpace - topDir.Size;

            long minDirSizeToDelete = 30000000 - unusedSpace;

            answer = directories.Where(d => d.Size >= minDirSizeToDelete).Select(d => d.Size).OrderBy(d => d).First();

            return answer.ToString();
        }
    }
}