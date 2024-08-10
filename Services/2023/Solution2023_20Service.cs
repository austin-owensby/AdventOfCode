namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2023/20.txt
    public class Solution2023_20Service : ISolutionDayService
    {
        private class Module {
            public string Name { get; set; } = string.Empty;
            public ModuleType Type { get; set; }
            public List<string> OutputModules { get; set; } = [];
            public bool State { get; set; }
            public Dictionary<string, bool> Memory { get; set; } = [];

            public static Module Create(List<string> parts) {
                Module module = new(){
                    Name = parts[1],
                    OutputModules = parts[2].Split(", ").ToList()
                };

                switch (parts[0]) {
                    case "%":
                        module.Type = ModuleType.FlipFlop;
                        break;
                    case "&":
                        module.Type = ModuleType.Conjunction;
                        break;
                    default:
                        module.Type = ModuleType.Broadcaster;
                        module.Name = "broadcaster";
                        break;
                }

                if (parts.First() == "%") {
                    module.Name = parts[1];
                }

                return module;
            }

            public enum ModuleType {
                FlipFlop,
                Conjunction,
                Broadcaster
            }
        }

        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 20, example);

            // Parse the list of modules
            Dictionary<string, Module> modules = lines.Select(line => Module.Create(line.QuickRegex(@"(&|%|b)(\w+) -> (.+)"))).ToDictionary(m => m.Name, m => m);

            // Initialize the memory of the conjunction modules
            List<string> conjunctionModules = modules.Where(m => m.Value.Type == Module.ModuleType.Conjunction).Select(m => m.Key).ToList();
            foreach (string conjunctionModule in conjunctionModules) {
                Module module = modules[conjunctionModule];
                module.Memory = modules.Where(m => m.Value.OutputModules.Contains(conjunctionModule)).Select(m => m.Key).ToDictionary(m => m, m => false);
            }

            int lowSignals = 0;
            int highSignals = 0;

            // Send the broadcast signal 1000 times
            foreach (int i in 1000) {
                Module currentModule = modules["broadcaster"];
                List<Tuple<string, bool>> modulesSignalsIn = currentModule.OutputModules.Select(m => new Tuple<string, bool>($"{currentModule.Name}->{m}", false)).ToList();
                List<Tuple<string, bool>> moduleSignalsOut = [];

                // Console.WriteLine("button -low-> broadcaster");
                lowSignals++; // Button press sends a low signal to the broadcaster module

                bool signalSent;

                // Keep looping while signals are updating
                do {
                    signalSent = false;
                    moduleSignalsOut = [];

                    foreach (Tuple<string, bool> moduleSignalIn in modulesSignalsIn) {
                        string[] parts = moduleSignalIn.Item1.Split("->");
                        string fromModule = parts[0];
                        string toModule = parts[1];
                    
                        // Console.WriteLine($"{fromModule} -{(moduleSignalIn.Item2 ? "high" : "low")}-> {toModule}");
                        if (moduleSignalIn.Item2) {
                            highSignals++;
                        }
                        else {
                            lowSignals++;
                        }
                        
                        // The signal could be going out to a module that doesn't exist
                        if (modules.TryGetValue(toModule, out currentModule!)) {
                            if (currentModule.Type == Module.ModuleType.FlipFlop) {
                                if (moduleSignalIn.Item2 == false) {
                                    // Flip flops only care about low signals
                                    currentModule.State = !currentModule.State;

                                    signalSent = true;
                                    moduleSignalsOut.AddRange(currentModule.OutputModules.Select(m => new Tuple<string, bool>($"{currentModule.Name}->{m}", currentModule.State)));
                                }
                            }
                            else {
                                // Conjunction module
                                currentModule.Memory[fromModule] = moduleSignalIn.Item2;
                                currentModule.State = !currentModule.Memory.All(m => m.Value);

                                signalSent = true;
                                moduleSignalsOut.AddRange(currentModule.OutputModules.Select(m => new Tuple<string, bool>($"{currentModule.Name}->{m}", currentModule.State)));
                            }
                        }
                    }

                    modulesSignalsIn = moduleSignalsOut;
                } while (signalSent);
            }

            int answer = lowSignals * highSignals;

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2023, 20, example);

            // Parse the list of modules
            Dictionary<string, Module> modules = lines.Select(line => Module.Create(line.QuickRegex(@"(&|%|b)(\w+) -> (.+)"))).ToDictionary(m => m.Name, m => m);

            // Initialize the memory of the conjunction modules
            List<string> conjunctionModules = modules.Where(m => m.Value.Type == Module.ModuleType.Conjunction).Select(m => m.Key).ToList();
            foreach (string conjunctionModule in conjunctionModules) {
                Module module = modules[conjunctionModule];
                module.Memory = modules.Where(m => m.Value.OutputModules.Contains(conjunctionModule)).Select(m => m.Key).ToDictionary(m => m, m => false);
            }

            long answer = 0;
            int index = 0;
            bool lookingForAnswer = true;

            // TODO, this is specific to my input. There's probably a way to detect this besides directly observing your specific input
            List<long> vbHistory = [];
            List<long> kvHistory = [];
            List<long> vmHistory = [];
            List<long> klHistory = [];

            // Keep looping until we find an answer
            while (lookingForAnswer) {
                answer++;
                Module currentModule = modules["broadcaster"];
                List<Tuple<string, bool>> moduleSignalsIn = currentModule.OutputModules.Select(m => new Tuple<string, bool>($"{currentModule.Name}->{m}", false)).ToList();
                List<Tuple<string, bool>> moduleSignalsOut = [];

                // Console.WriteLine("button -low-> broadcaster");

                bool signalSent;

                // Keep looping while signals are updating
                do {
                    index++;
                    signalSent = false;
                    moduleSignalsOut = [];

                    foreach (Tuple<string, bool> moduleSignalIn in moduleSignalsIn) {
                        string[] parts = moduleSignalIn.Item1.Split("->");
                        string fromModule = parts[0];
                        string toModule = parts[1];
                    
                        // Console.WriteLine($"{fromModule} -{(moduleSignalIn.Item2 ? "high" : "low")}-> {toModule}");
                        
                        // The signal could be going out to a module that doesn't exist
                        if (modules.TryGetValue(toModule, out currentModule!)) {
                            if (currentModule.Type == Module.ModuleType.FlipFlop) {
                                if (moduleSignalIn.Item2 == false) {
                                    // Flip flops only care about low signals
                                    currentModule.State = !currentModule.State;

                                    signalSent = true;
                                    moduleSignalsOut.AddRange(currentModule.OutputModules.Select(m => new Tuple<string, bool>($"{currentModule.Name}->{m}", currentModule.State)));
                                }
                            }
                            else {
                                // Conjunction module
                                currentModule.Memory[fromModule] = moduleSignalIn.Item2;
                                currentModule.State = !currentModule.Memory.All(m => m.Value);

                                signalSent = true;
                                moduleSignalsOut.AddRange(currentModule.OutputModules.Select(m => new Tuple<string, bool>($"{currentModule.Name}->{m}", currentModule.State)));
                            }
                        }
                    }

                    // Check if we've found the answer (low signal sent to module rx)
                    if (moduleSignalsOut.Any(m => m.Item1 == "rx" && m.Item2 == false)) {
                        lookingForAnswer = false;
                        break;
                    }

                    moduleSignalsIn = moduleSignalsOut.DistinctBy(m => $"{m.Item1} {m.Item2}").ToList();

                    if (modules["vb"].State && vbHistory.Count == 0) {
                        vbHistory.Add(answer);
                    }
                    if (modules["kv"].State && kvHistory.Count == 0) {
                        kvHistory.Add(answer);
                    }
                    if (modules["vm"].State && vmHistory.Count == 0) {
                        vmHistory.Add(answer);
                    }
                    if (modules["kl"].State && klHistory.Count == 0) {
                        klHistory.Add(answer);
                    }
                } while (signalSent);

                if (vbHistory.Count != 0 && kvHistory.Count != 0 && vmHistory.Count != 0 && klHistory.Count != 0) {
                    answer = Utility.LCM(vbHistory.First(), kvHistory.First());
                    answer = Utility.LCM(answer, vmHistory.First());
                    answer = Utility.LCM(answer, klHistory.First());
                    lookingForAnswer = false;
                }
            }

            return answer.ToString();
        }
    }
}