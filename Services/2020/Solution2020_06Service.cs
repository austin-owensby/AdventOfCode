using System;
using System.IO;

namespace AdventOfCode.Services
{
    public class Solution2020_06Service: ISolutionDayService{
        public Solution2020_06Service(){}

        public string FirstHalf(){
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2020_06.txt"));

            foreach(char character in data){
                
            }

            return $"";
        }

        public string SecondHalf(){            
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2020_06.txt"));

            foreach(char character in data){

            }

            return $"";
        }
    }
}
                        