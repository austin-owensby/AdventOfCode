using System;
using System.IO;

namespace AdventOfCode.Services
{
    public class Solution2017_25Service: ISolutionDayService{
        public Solution2017_25Service(){}

        public string FirstHalf(){
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2017_25.txt"));

            foreach(char character in data){
                
            }

            return $"";
        }

        public string SecondHalf(){            
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2017_25.txt"));

            foreach(char character in data){

            }

            return $"";
        }
    }
}
                        