using System;
using System.IO;

namespace AdventOfCode.Services
{
    public class Solution2016_16Service: ISolutionDayService{
        public Solution2016_16Service(){}

        public string FirstHalf(){
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2016_16.txt"));

            foreach(char character in data){
                
            }

            return $"";
        }

        public string SecondHalf(){            
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2016_16.txt"));

            foreach(char character in data){

            }

            return $"";
        }
    }
}
                        