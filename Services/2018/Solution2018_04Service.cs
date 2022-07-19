using System.Text.RegularExpressions;

namespace AdventOfCode.Services
{
    public class Solution2018_04Service: ISolutionDayService{
        public Solution2018_04Service(){}

        public string FirstHalf(){
            List<string> data =  File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2018_04.txt")).OrderBy(s => s).ToList();

            Dictionary<int, List<TimeOnly>> guardWakingMinutes = new();

            int? currentGuard = null;
            DateTime? sleepTime = null;

            foreach(string line in data) {
                if (line.EndsWith("begins shift")) {
                    string pattern = @"\[(.*)\] Guard #(\d*) begins shift";
                    Match match = Regex.Match(line, pattern);

                    string dateString = match.Groups[1].Value;
                    DateTime eventTime = DateTime.Parse(dateString);

                    string gaurdString = match.Groups[2].Value;
                    int guard = int.Parse(gaurdString);

                    if (currentGuard != null) {
                        if (!guardWakingMinutes.ContainsKey(currentGuard.Value)) {
                            guardWakingMinutes.Add(currentGuard.Value, new());
                        }

                        if (sleepTime.HasValue) {
                            // Update the current guard's sleep schedule since they just got woken up by the next guard
                            // Add each minute that passed to the list
                            for(DateTime time = sleepTime.Value; time < eventTime; time.AddMinutes(1)) {
                                guardWakingMinutes[currentGuard.Value].Add(TimeOnly.FromDateTime(time));
                            }
                        }
                    }

                    currentGuard = guard;
                    sleepTime = null;
                }
                else if (line.EndsWith("wakes up")) {
                    string pattern = @"\[(.*)\] wakes up";
                    Match match = Regex.Match(line, pattern);

                    string dateString = match.Groups[1].Value;
                    DateTime eventTime = DateTime.Parse(dateString);

                    if (currentGuard != null) {
                        if (!guardWakingMinutes.ContainsKey(currentGuard.Value)) {
                            guardWakingMinutes.Add(currentGuard.Value, new());
                        }

                        // Update the current guard's sleep schedule since they just got woken up by the next guard
                        // Add each minute that passed to the list
                        for(DateTime time = sleepTime.Value; time < eventTime; time = time.AddMinutes(1)) {
                            guardWakingMinutes[currentGuard.Value].Add(TimeOnly.FromDateTime(time));
                        }
                    }

                    sleepTime = null;
                }
                else if (line.EndsWith("falls asleep")){
                    string pattern = @"\[(.*)\] falls asleep";
                    Match match = Regex.Match(line, pattern);

                    string dateString = match.Groups[1].Value;
                    DateTime eventTime = DateTime.Parse(dateString);

                    sleepTime = eventTime;
                }
                else {
                    throw new Exception($"Unable to parse line {line}");
                }
            }

            KeyValuePair<int, List<TimeOnly>> mostAsleepGuard = guardWakingMinutes.MaxBy(g => g.Value.Count);

            int guardId = mostAsleepGuard.Key;

            TimeOnly mostFrequentTime = mostAsleepGuard.Value.MaxBy(v => mostAsleepGuard.Value.Count(c => c == v));

            return $"The most frequent guard multiplied by his most frequent minute asleep is {guardId * mostFrequentTime.Minute}.";
        }

        public string SecondHalf(){
            List<string> data =  File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2018_04.txt")).OrderBy(s => s).ToList();

            Dictionary<int, List<TimeOnly>> guardWakingMinutes = new();

            int? currentGuard = null;
            DateTime? sleepTime = null;

            foreach(string line in data) {
                if (line.EndsWith("begins shift")) {
                    string pattern = @"\[(.*)\] Guard #(\d*) begins shift";
                    Match match = Regex.Match(line, pattern);

                    string dateString = match.Groups[1].Value;
                    DateTime eventTime = DateTime.Parse(dateString);

                    string gaurdString = match.Groups[2].Value;
                    int guard = int.Parse(gaurdString);

                    if (currentGuard != null) {
                        if (!guardWakingMinutes.ContainsKey(currentGuard.Value)) {
                            guardWakingMinutes.Add(currentGuard.Value, new());
                        }

                        if (sleepTime.HasValue) {
                            // Update the current guard's sleep schedule since they just got woken up by the next guard
                            // Add each minute that passed to the list
                            for(DateTime time = sleepTime.Value; time < eventTime; time.AddMinutes(1)) {
                                guardWakingMinutes[currentGuard.Value].Add(TimeOnly.FromDateTime(time));
                            }
                        }
                    }

                    currentGuard = guard;
                    sleepTime = null;
                }
                else if (line.EndsWith("wakes up")) {
                    string pattern = @"\[(.*)\] wakes up";
                    Match match = Regex.Match(line, pattern);

                    string dateString = match.Groups[1].Value;
                    DateTime eventTime = DateTime.Parse(dateString);

                    if (currentGuard != null) {
                        if (!guardWakingMinutes.ContainsKey(currentGuard.Value)) {
                            guardWakingMinutes.Add(currentGuard.Value, new());
                        }

                        // Update the current guard's sleep schedule since they just got woken up by the next guard
                        // Add each minute that passed to the list
                        for(DateTime time = sleepTime.Value; time < eventTime; time = time.AddMinutes(1)) {
                            guardWakingMinutes[currentGuard.Value].Add(TimeOnly.FromDateTime(time));
                        }
                    }

                    sleepTime = null;
                }
                else if (line.EndsWith("falls asleep")){
                    string pattern = @"\[(.*)\] falls asleep";
                    Match match = Regex.Match(line, pattern);

                    string dateString = match.Groups[1].Value;
                    DateTime eventTime = DateTime.Parse(dateString);

                    sleepTime = eventTime;
                }
                else {
                    throw new Exception($"Unable to parse line {line}");
                }
            }

            int mostFrequentGuard = 0;
            TimeOnly mostFrequentMinute = new();
            int mostFrequentMinuteCount = 0;

            foreach (int guardId in guardWakingMinutes.Where(g => g.Value.Any()).Select(g => g.Key)) {
                TimeOnly mostFrequentTime = guardWakingMinutes[guardId].MaxBy(v => guardWakingMinutes[guardId].Count(g => g == v));
                int count = guardWakingMinutes[guardId].Count(g => g == mostFrequentTime);

                if (count > mostFrequentMinuteCount) {
                    mostFrequentMinuteCount = count;
                    mostFrequentMinute = mostFrequentTime;
                    mostFrequentGuard = guardId;
                }
            }

            return $"The most frequent guard multiplied by his most frequent minute asleep is {mostFrequentGuard * mostFrequentMinute.Minute}.";
        }
    }
}
                        