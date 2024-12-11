﻿using AdventOfCode.Gateways;

namespace AdventOfCode.PuzzleHelper
{
    public class PuzzleHelperService(AdventOfCodeGateway adventOfCodeGateway)
    {
        private readonly AdventOfCodeGateway adventOfCodeGateway = adventOfCodeGateway;

        /// <summary>
        /// Generates solution files.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GenerateMissingSolutionServiceFiles()
        {
            string output = string.Empty;

            bool update = false;

            string directoryPath = Directory.GetParent(Environment.CurrentDirectory)!.FullName;

            // Create a folder for each year that is missing one
            DateTime now = DateTime.UtcNow.AddHours(Globals.SERVER_UTC_OFFSET);
            for (int year = Globals.START_YEAR; year <= now.Year; year++)
            {
                string yearFolderPath = Path.Combine(directoryPath, "Shared", "Services", year.ToString());

                if (!Directory.Exists(yearFolderPath))
                {
                    Directory.CreateDirectory(yearFolderPath);
                    System.Console.WriteLine($"Created folder for {year}.");
                    output += $"Created folder for {year}.\n";
                    update = true;
                }

                // Create/update files for each day that is missing one
                for (int day = 1; day <= Globals.NUMBER_OF_PUZZLES; day++)
                {
                    string dayFilePath = Path.Combine(yearFolderPath, $"Solution{year}_{day:D2}Service.cs");

                    if (!File.Exists(dayFilePath))
                    {
                        // Initialize the new service file
                        using StreamWriter serviceFile = new(dayFilePath);

                        await serviceFile.WriteAsync($$"""
            namespace AdventOfCode.Services
            {
                // (ctrl/command + click) the link to open the input file
                // file://./../../../Inputs/{{year}}/{{day:D2}}.txt
                public class Solution{{year}}_{{day:D2}}Service : ISolutionDayService
                {
                    public string FirstHalf(bool example)
                    {
                        List<string> lines = Utility.GetInputLines({{year}}, {{day}}, example);

                        int answer = 0;

                        foreach (string line in lines)
                        {

                        }

                        return answer.ToString();
                    }

                    public string SecondHalf(bool example)
                    {
                        {{(
                            day == Globals.NUMBER_OF_PUZZLES ?
                            """
                            return "There is no problem for Day 25 part 2, solve all other problems to get the last star.";
                            """ :
                            $$"""
                            List<string> lines = Utility.GetInputLines({{year}}, {{day}}, example);

                                        int answer = 0;

                                        foreach (string line in lines)
                                        {

                                        }

                                        return answer.ToString();
                            """
                        )}}
                    }
                }
            }
            """);

                        System.Console.WriteLine($"Created solution file for Year: {year}, Day: {day}.");
                        output += $"Created solution file for Year: {year}, Day: {day}.\n";
                        update = true;
                    }
                }
            }

            if (!update)
            {
                System.Console.WriteLine("No updates applied.");
                output += "No updates applied.\n";
            }

            return output;
        }

        /// <summary>
        /// Imports the day's input file.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public async Task<string> ImportInputFile(int year, int day)
        {
            string output = string.Empty;

            Tuple<int, int> latestResults = GetLatestYearAndDate();
            int latestPuzzleYear = latestResults.Item1;
            int latestPuzzleDay = latestResults.Item2;

            if (latestPuzzleYear < year || latestPuzzleYear == year && latestPuzzleDay < day)
            {
                System.Console.WriteLine("No updates applied.");
                output += "No updates applied.\n";
            }
            else
            {
                bool update = await WriteInputFile(year, day);

                if (update)
                {
                    output = $"Created input file for Year: {year}, Day: {day}.";
                }
                else
                {
                    System.Console.WriteLine("No updates applied.");
                    output += "No updates applied.\n ";
                }
            }

            return output;
        }

        /// <summary>
        /// Fetch and write the input file if it doesn't exist
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private async Task<bool> WriteInputFile(int year, int day)
        {
            bool update = false;

            string directoryPath = Directory.GetParent(Environment.CurrentDirectory)!.FullName;
            string yearFolderPath = Path.Combine(directoryPath, $"Inputs/{year}");

            if (!Directory.Exists(yearFolderPath))
            {
                Directory.CreateDirectory(yearFolderPath);
            }

            string inputFilePath = Path.Combine(directoryPath, "Inputs", year.ToString(), $"{day:D2}.txt");

            if (!File.Exists(inputFilePath))
            {
                string response;
                try
                {
                    response = await adventOfCodeGateway.ImportInput(year, day);
                }
                catch (Exception)
                {
                    System.Console.WriteLine("An error occurred while getting the puzzle input from Advent of Code");
                    throw;
                }

                using StreamWriter inputFile = new(inputFilePath);
                await inputFile.WriteAsync(response);

                System.Console.WriteLine($"Created input file for Year: {year}, Day: {day}.");
                update = true;
            }

            return update;
        }

        /// <summary>
        /// Based on today's date, calculate the latest AOC year and day available
        /// </summary>
        /// <returns></returns>
        private static Tuple<int, int> GetLatestYearAndDate()
        {
            DateTime now = DateTime.UtcNow.AddHours(Globals.SERVER_UTC_OFFSET);
            int latestPuzzleYear, latestPuzzleDay;

            // If we're in the event month, then the latest available puzzle is today
            if (now.Month == Globals.EVENT_MONTH)
            {
                latestPuzzleYear = now.Year;

                // If it's event month, but after the final puzzle, default to the final puzzle
                latestPuzzleDay = Math.Min(now.Day, Globals.NUMBER_OF_PUZZLES);
            }
            else
            {
                // Otherwise the latest puzzle is from the end of the previous event
                latestPuzzleYear = now.Year - 1;
                latestPuzzleDay = Globals.NUMBER_OF_PUZZLES;
            }

            return Tuple.Create(latestPuzzleYear, latestPuzzleDay);
        }
    }
}