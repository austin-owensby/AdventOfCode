using System;
using System.IO;

namespace AdventOfCode.Services
{
    public class Solution2020_22Service: ISolutionDayService{
        public Solution2020_22Service(){}

        public string FirstHalf(){
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2020_22.txt"));

            foreach(char character in data){
                
            }

            return $"";
        }

        public string SecondHalf(){            
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2020_22.txt"));

            foreach(char character in data){

            }

            return $"";
        }
    }
}
                        