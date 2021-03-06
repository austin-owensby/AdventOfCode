using System.Linq;
using System;
using System.IO;

namespace AdventOfCode.Services
{
    public class Solution2015_18Service: ISolutionDayService{
        public Solution2015_18Service(){}

        public string FirstHalf(){
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2015_18.txt"));
            string[] lights = data.Split("\n").SkipLast(1).ToArray();
            string[] nextState = new string[100];
            int numberOfStages = 100;

            for(int i = 0; i < numberOfStages; i++){
                for(int j = 0; j < lights.Length; j++){
                    for(int k = 0; k < lights[j].Length; k++){
                        int liveNeighbors = getNumberOfLiveNeighbors(j, k, lights);
                        if(lights[j][k] == '#'){
                            if(liveNeighbors == 2 || liveNeighbors == 3){
                                nextState[j] += '#';
                            }
                            else{
                                nextState[j] += '.';
                            }
                        }
                        else{
                            if(liveNeighbors == 3){
                                nextState[j] += '#';
                            }
                            else{
                                nextState[j] += '.';
                            }
                        }
                    }
                }

                lights = nextState;
                nextState = new string[100];
            }

            int totalLights = 0;

            foreach(string line in lights){
                foreach(char character in line){
                    if(character == '#'){
                        totalLights++;
                    }
                }
            }

            return $"The number of total lights after {numberOfStages} iterations is {totalLights}";
        }

        public string SecondHalf(){            
            string data =  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Inputs", "2015_18.txt"));
            string[] lights = data.Split("\n").SkipLast(1).ToArray();
            string[] nextState = new string[100];
            int numberOfStages = 100;

            // Set corners to '#' initially
            char[] firstRow = lights[0].ToCharArray();
            firstRow[0] = '#';
            firstRow[firstRow.Length - 1] = '#';
            lights[0] = new string(firstRow);

            char[] lastRow = lights[lights.Length - 1].ToCharArray();
            lastRow[0] = '#';
            lastRow[firstRow.Length - 1] = '#';
            lights[lights.Length - 1] = new string(lastRow);

            for(int i = 0; i < numberOfStages; i++){
                for(int j = 0; j < lights.Length; j++){
                    for(int k = 0; k < lights[j].Length; k++){
                        int liveNeighbors = getNumberOfLiveNeighbors(j, k, lights);

                        if((j == 0 || j == lights.Length - 1) && (k == 0 || k == lights[j].Length - 1)){
                            nextState[j] += '#';
                        }
                        else if(lights[j][k] == '#'){
                            if(liveNeighbors == 2 || liveNeighbors == 3){
                                nextState[j] += '#';
                            }
                            else{
                                nextState[j] += '.';
                            }
                        }
                        else{
                            if(liveNeighbors == 3){
                                nextState[j] += '#';
                            }
                            else{
                                nextState[j] += '.';
                            }
                        }
                    }
                }

                lights = nextState;
                nextState = new string[100];
            }

            int totalLights = 0;

            foreach(string line in lights){
                foreach(char character in line){
                    if(character == '#'){
                        totalLights++;
                    }
                }
            }

            return $"The number of total lights after {numberOfStages} iterations is {totalLights}";
        }

        private int getNumberOfLiveNeighbors(int j, int k, string[] board){
            // Check each of the possible neighbors without going out of the limits of the board
            int neighbors = 0;

            if(j != 0){
                if(k != 0){
                    if(board[j - 1][k - 1] == '#'){
                        neighbors++;
                    }
                }

                if(k != board[j - 1].Length - 1){
                    if(board[j - 1][k + 1] == '#'){
                        neighbors++;
                    }
                }

                if(board[j - 1][k] == '#'){
                    neighbors++;
                }
            }

            if(k != 0){
                if(board[j][k - 1] == '#'){
                    neighbors++;
                }
            }

            if(j != board.Length - 1){
                if(k != board[j + 1].Length - 1){
                    if(board[j + 1][k + 1] == '#'){
                        neighbors++;
                    }
                }

                if(k != 0){
                    if(board[j + 1][k - 1] == '#'){
                        neighbors++;
                    }
                }

                if(board[j + 1][k] == '#'){
                    neighbors++;
                }
            }

            if(k != board[j].Length - 1){
                if(board[j][k + 1] == '#'){
                    neighbors++;
                }
            }

            return neighbors;
        }
    }
}
                        