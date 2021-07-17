using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Services
{
    public class Solution2015_14Service: ISolutionDayService{
        public Solution2015_14Service(){}

        public string FirstHalf(){
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2015_14.txt"));

            string[] lines = data.Split("\n");
            lines = lines.SkipLast(1).ToArray();

            int raceLength = 2503;

            int maxDistance = 0;

            foreach(string line in lines){
                string[] words = line.Split(" ");
                
                int speed = int.Parse(words[3]);
                int flightLength = int.Parse(words[6]);
                int restLength = int.Parse(words[13]);

                int flightLeft = flightLength;
                int restLeft = 0;
                bool flying = true;
                int distance = 0;

                for(int i = 0; i < raceLength; i++){
                    if(flying){
                        flightLeft--;
                        distance += speed;

                        if(flightLeft == 0){
                            flying = false;
                            restLeft = restLength;
                        }
                    }
                    else{
                        restLeft--;

                        if(restLeft == 0){
                            flying = true;
                            flightLeft = flightLength;
                        }
                    }
                }

                if(distance > maxDistance){
                    maxDistance = distance;
                }
            }

            return $"The max distance a reindeer flew after {raceLength} seconds was {maxDistance}.";
        }

        public string SecondHalf(){            
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2015_14.txt"));

            string[] lines = data.Split("\n");
            lines = lines.SkipLast(1).ToArray();

            int raceLength = 2503;

            List<List<int>> distances = new();

            foreach(string line in lines){
                List<int> distanceSet = new();

                string[] words = line.Split(" ");
                
                int speed = int.Parse(words[3]);
                int flightLength = int.Parse(words[6]);
                int restLength = int.Parse(words[13]);

                int flightLeft = flightLength;
                int restLeft = 0;
                bool flying = true;
                int distance = 0;

                for(int i = 0; i < raceLength; i++){
                    if(flying){
                        flightLeft--;
                        distance += speed;

                        if(flightLeft == 0){
                            flying = false;
                            restLeft = restLength;
                        }
                    }
                    else{
                        restLeft--;

                        if(restLeft == 0){
                            flying = true;
                            flightLeft = flightLength;
                        }
                    }

                    distanceSet.Add(distance);
                }

                distances.Add(distanceSet);
            }

            // Calculate the points each reindeer gained
            List<int> points = new(new int[lines.Count()]);



            for(int i = 0; i < raceLength; i++){
                int max = distances.Select(d => d[i]).Max();

                for(int j = 0; j < distances.Count; j++){
                    if(distances[j][i] == max){
                        points[j]++;
                    }
                }
            }

            int maxPoints = points.Max();

            return $"The max points a reindeer gained after {raceLength} seconds was {maxPoints}.";
        }
    }
}
                        