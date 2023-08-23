namespace AdventOfCode.Services
{
    public class Solution2022_12Service : ISolutionDayService
    {
        public Solution2022_12Service() { }

        private class Node
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Altitude { get; set; }
            public int DistanceTraveled { get; set; }
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2022, 12, example);

            string startLine = lines.First(line => line.Contains("S"));
            string endLine = lines.First(line => line.Contains("E"));

            Node start = new()
            {
                X = lines.IndexOf(startLine),
                Y = startLine.IndexOf('S'),
                Altitude = 'a',
                DistanceTraveled = 0,
            };

            Node end = new()
            {
                X = lines.IndexOf(endLine),
                Y = endLine.IndexOf('E'),
                Altitude = 'z'
            };

            List<List<Node>> paths = new(){
                new(){
                    start
                }
            };

            List<Node> visitedNodes = new();
            Queue<Node> queue = new();
            queue.Enqueue(start);

            while (queue.Any())
            {
                Node currentNode = queue.Dequeue();
                visitedNodes.Add(currentNode);

                // Get all paths that have this next node as their last node
                // There should only be 1 match because all other paths would be equal or greater distance and should have been removed
                List<Node>? pathWithNextNode = paths.FirstOrDefault(p => p.Last().X == currentNode.X && p.Last().Y == currentNode.Y);

                if (pathWithNextNode != null)
                {
                    List<Node> possibleNodes = new();

                    // Check for possible steps
                    if (currentNode.X > 0)
                    {
                        possibleNodes.Add(new Node()
                        {
                            X = currentNode.X - 1,
                            Y = currentNode.Y,
                            Altitude = lines[currentNode.X - 1][currentNode.Y],
                            DistanceTraveled = pathWithNextNode.Count + 1
                        });
                    }

                    if (currentNode.X + 1 < lines.Count)
                    {
                        possibleNodes.Add(new Node()
                        {
                            X = currentNode.X + 1,
                            Y = currentNode.Y,
                            Altitude = lines[currentNode.X + 1][currentNode.Y],
                            DistanceTraveled = pathWithNextNode.Count + 1
                        });
                    }

                    if (currentNode.Y > 0)
                    {
                        possibleNodes.Add(new Node()
                        {
                            X = currentNode.X,
                            Y = currentNode.Y - 1,
                            Altitude = lines[currentNode.X][currentNode.Y - 1],
                            DistanceTraveled = pathWithNextNode.Count + 1
                        });
                    }

                    if (currentNode.Y + 1 < lines.First().Length)
                    {
                        possibleNodes.Add(new Node()
                        {
                            X = currentNode.X,
                            Y = currentNode.Y + 1,
                            Altitude = lines[currentNode.X][currentNode.Y + 1],
                            DistanceTraveled = pathWithNextNode.Count + 1
                        });
                    }

                    List<Node> validatedNodes = new();

                    foreach (Node possibleNode in possibleNodes)
                    {
                        bool startNode = possibleNode.X == start.X && possibleNode.Y == start.Y;
                        bool endNode = possibleNode.X == end.X && possibleNode.Y == end.Y;

                        // We can step to the end or if there is at most a single increase in altitude
                        bool canStep = ((possibleNode.Altitude <= currentNode.Altitude + 1 && !endNode) || (endNode && currentNode.Altitude >= 'y')) && !startNode;

                        if (canStep)
                        {
                            bool nodeInPath = pathWithNextNode.Any(node => node.X == possibleNode.X && node.Y == possibleNode.Y);

                            if (!nodeInPath)
                            {
                                // Check if the node is already in the queue 
                                bool nodeInQueue = queue.Any(node => node.X == possibleNode.X && node.Y == possibleNode.Y);
                                bool nodeVisited = visitedNodes.Any(node => node.X == possibleNode.X && node.Y == possibleNode.Y);

                                if (!endNode && !nodeVisited && !nodeInQueue)
                                {
                                    // Add the node to the queue if it's not the end, already visited, or currently in the queue
                                    queue.Enqueue(possibleNode);
                                }

                                validatedNodes.Add(possibleNode);
                            }
                        }
                    }

                    // Update the existing path with the new node
                    for (int i = validatedNodes.Count - 1; i >= 0; i--)
                    {
                        Node possibleNode = validatedNodes[i];

                        // Add the first node to the existing path
                        if (i == 0)
                        {
                            pathWithNextNode.Add(possibleNode);
                        }
                        else
                        {
                            // Create a copy of the existing path to add new nodes to it
                            List<Node> newPath = pathWithNextNode.Select(node => new Node()
                            {
                                X = node.X,
                                Y = node.Y,
                                Altitude = node.Altitude,
                                DistanceTraveled = node.DistanceTraveled
                            }).ToList();
                            newPath.Add(possibleNode);
                            paths.Add(newPath);
                        }
                    }
                }
            }

            // Subtract 1 for the initial step
            int answer = paths.Where(path => path.Last().X == end.X && path.Last().Y == end.Y).Min(path => path.Count) - 1;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {

            List<string> lines = Utility.GetInputLines(2022, 12, example);

            string endLine = lines.First(line => line.Contains("E"));

            Node end = new()
            {
                X = lines.IndexOf(endLine),
                Y = endLine.IndexOf('E'),
                Altitude = 'z'
            };

            List<Node> startNodes = new();

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                // Due to the nature of the input, only 'a's on the first row is valid
                char character = line[0];

                if (character is 'a' or 'S')
                {
                    startNodes.Add(new()
                    {
                        X = i,
                        Y = 0,
                        Altitude = 'a'
                    });

                    if (character == 'S')
                    {
                        character = 'a';
                    }
                }
            }

            int answer = int.MaxValue;

            foreach (Node start in startNodes)
            {
                List<List<Node>> paths = new(){
                    new(){
                        start
                    }
                };

                List<Node> visitedNodes = new();
                Queue<Node> queue = new();
                queue.Enqueue(start);

                while (queue.Any())
                {
                    Node currentNode = queue.Dequeue();
                    visitedNodes.Add(currentNode);

                    // Get all paths that have this next node as their last node
                    // There should only be 1 match because all other paths would be equal or greater distance and should have been removed
                    List<Node>? pathWithNextNode = paths.FirstOrDefault(p => p.Last().X == currentNode.X && p.Last().Y == currentNode.Y);

                    if (pathWithNextNode != null)
                    {
                        List<Node> possibleNodes = new();

                        // Check for possible steps
                        if (currentNode.X > 0)
                        {
                            possibleNodes.Add(new Node()
                            {
                                X = currentNode.X - 1,
                                Y = currentNode.Y,
                                Altitude = lines[currentNode.X - 1][currentNode.Y],
                                DistanceTraveled = pathWithNextNode.Count + 1
                            });
                        }

                        if (currentNode.X + 1 < lines.Count)
                        {
                            possibleNodes.Add(new Node()
                            {
                                X = currentNode.X + 1,
                                Y = currentNode.Y,
                                Altitude = lines[currentNode.X + 1][currentNode.Y],
                                DistanceTraveled = pathWithNextNode.Count + 1
                            });
                        }

                        if (currentNode.Y > 0)
                        {
                            possibleNodes.Add(new Node()
                            {
                                X = currentNode.X,
                                Y = currentNode.Y - 1,
                                Altitude = lines[currentNode.X][currentNode.Y - 1],
                                DistanceTraveled = pathWithNextNode.Count + 1
                            });
                        }

                        if (currentNode.Y + 1 < lines.First().Length)
                        {
                            possibleNodes.Add(new Node()
                            {
                                X = currentNode.X,
                                Y = currentNode.Y + 1,
                                Altitude = lines[currentNode.X][currentNode.Y + 1],
                                DistanceTraveled = pathWithNextNode.Count + 1
                            });
                        }

                        List<Node> validatedNodes = new();

                        foreach (Node possibleNode in possibleNodes)
                        {
                            bool startNode = possibleNode.X == start.X && possibleNode.Y == start.Y;
                            bool endNode = possibleNode.X == end.X && possibleNode.Y == end.Y;

                            // We can step to the end or if there is at most a single increase in altitude
                            bool canStep = ((possibleNode.Altitude <= currentNode.Altitude + 1 && !endNode) || (endNode && currentNode.Altitude >= 'y')) && !startNode;

                            if (canStep)
                            {
                                bool nodeInPath = pathWithNextNode.Any(node => node.X == possibleNode.X && node.Y == possibleNode.Y);

                                if (!nodeInPath)
                                {
                                    // Check if the node is already in the queue 
                                    bool nodeInQueue = queue.Any(node => node.X == possibleNode.X && node.Y == possibleNode.Y);
                                    bool nodeVisited = visitedNodes.Any(node => node.X == possibleNode.X && node.Y == possibleNode.Y);

                                    if (!endNode && !nodeVisited && !nodeInQueue)
                                    {
                                        // Add the node to the queue if it's not the end, already visited, or currently in the queue
                                        queue.Enqueue(possibleNode);
                                    }

                                    validatedNodes.Add(possibleNode);
                                }
                            }
                        }

                        // Update the existing path with the new node
                        for (int i = validatedNodes.Count - 1; i >= 0; i--)
                        {
                            Node possibleNode = validatedNodes[i];

                            // Add the first node to the existing path
                            if (i == 0)
                            {
                                pathWithNextNode.Add(possibleNode);
                            }
                            else
                            {
                                // Create a copy of the existing path to add new nodes to it
                                List<Node> newPath = pathWithNextNode.Select(node => new Node()
                                {
                                    X = node.X,
                                    Y = node.Y,
                                    Altitude = node.Altitude,
                                    DistanceTraveled = node.DistanceTraveled
                                }).ToList();
                                newPath.Add(possibleNode);
                                paths.Add(newPath);
                            }
                        }
                    }
                }

                // Subtract 1 for the initial step
                List<List<Node>> endPaths = paths.Where(path => path.Last().X == end.X && path.Last().Y == end.Y).ToList();
                int loopAnswer = endPaths.Any() ? (endPaths.Min(path => path.Count) - 1) : int.MaxValue;
                answer = Math.Min(loopAnswer, answer);
            }

            return answer.ToString();
        }
    }
}