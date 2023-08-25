public class PuzzleHelperService
{
    private readonly AdventOfCodeGateway adventOfCodeGateway;

    public PuzzleHelperService(AdventOfCodeGateway adventOfCodeGateway) {
        this.adventOfCodeGateway = adventOfCodeGateway;
    }

    /// <summary>
    /// Generates solution files.
    /// </summary>
    /// <returns></returns>
    public async Task<string> Run()
    {
        string output = string.Empty;

        Tuple<int, int> latestResults = GetLatestYearAndDate();
        int latestPuzzleYear = latestResults.Item1;
        int latestPuzzleDay = latestResults.Item2;

        bool update = false;

        // Create a folder for each year that is missing one
        DateTime now = DateTime.UtcNow.AddHours(Globals.SERVER_UTC_OFFSET);
        for (int year = Globals.START_YEAR; year <= now.Year; year++)
        {
            string yearFolderPath = Path.Combine(Environment.CurrentDirectory, $"Services/{year}");

            if (!Directory.Exists(yearFolderPath))
            {
                Directory.CreateDirectory(yearFolderPath);
                Console.WriteLine($"Created folder for {year}.");
                output += $"Created folder for {year}.\n";
                update = true;
            }

            // Create/update files for each day that is missing one
            for (int day = 1; day <= Globals.CRISTMAS_DATE; day++)
            {
                string dayFilePath = Path.Combine(yearFolderPath, $"Solution{year}_{day:D2}Service.cs");

                if (!File.Exists(dayFilePath))
                {
                    // Initialize the new service file
                    using StreamWriter serviceFile = new(dayFilePath);

                    await serviceFile.WriteAsync($$"""
        namespace AdventOfCode.Services
        {
            public class Solution{{year}}_{{day:D2}}Service : ISolutionDayService
            {
                public Solution{{year}}_{{day:D2}}Service() { }

                public string FirstHalf(bool example)
                {
                    List<string> lines = Utility.GetInputLines({{year}},{{day}}, example);

                    int answer = 0;

                    foreach (string line in lines) {

                    }

                    return answer.ToString();
                }

                public string SecondHalf(bool example)
                {
                    {{(
                        day == Globals.CRISTMAS_DATE ?
                        """
                        return "There is no problem for Day 25 part 2, solve all other problems to get the last star.";
                        """ :
                        $$"""
                        List<string> lines = Utility.GetInputLines({{year}},{{day}}, example);

                                    int answer = 0;

                                    foreach (string line in lines) {

                                    }

                                    return answer.ToString();
                        """
                    )}}
                }
            }
        }
        """);

                    Console.WriteLine($"Created solution file for Year: {year}, Day: {day}.");
                    output += $"Created solution file for Year: {year}, Day: {day}.\n";
                    update = true;
                }
            }
        }

        if (!update)
        {
            Console.WriteLine("No updates applied.");
            output += "No updates applied.\n";
        }

        return output;
    }
    
    /// <summary>
    /// A streamlined version of the puzzle helper that imports just the day's input file.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public async Task<string> RunDaily(int year, int day)
    {
        string output = string.Empty;

        Tuple<int, int> latestResults = GetLatestYearAndDate();
        int latestPuzzleYear = latestResults.Item1;
        int latestPuzzleDay = latestResults.Item2;

        if (latestPuzzleYear < year || latestPuzzleYear == year && latestPuzzleDay < day) {
            Console.WriteLine("No updates applied.");
            output += "No updates applied.\n";
        }
        else {
            bool update = await WriteInputFile(year, day);

            if (update) {
                output = $"Created input file for Year: {year}, Day: {day}.";
            }
            else
            {
                Console.WriteLine("No updates applied.");
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
        
        string yearFolderPath = Path.Combine(Environment.CurrentDirectory, $"Inputs/{year}");

        if (!Directory.Exists(yearFolderPath))
        {
            Directory.CreateDirectory(yearFolderPath);
        }

        string inputFilePath = Path.Combine(Environment.CurrentDirectory, $"Inputs/{year}/{day:D2}.txt");

        if (!File.Exists(inputFilePath))
        {
            string response;
            try
            {
                response = await adventOfCodeGateway.ImportInput(year, day);
            }
            catch (Exception) {
                Console.WriteLine("An error occured while getting the puzzle input from Advent of Code");
                throw;
            }

            using StreamWriter inputFile = new(inputFilePath);
            await inputFile.WriteAsync(response);

            Console.WriteLine($"Created input file for Year: {year}, Day: {day}.");
            update = true;
        }

        return update;
    }

    /// <summary>
    /// Based on today's date, calculate the latest AOC year and day available
    /// </summary>
    /// <returns></returns>
    private Tuple<int, int> GetLatestYearAndDate() {
        DateTime now = DateTime.UtcNow.AddHours(Globals.SERVER_UTC_OFFSET);
        int latestPuzzleYear, latestPuzzleDay;

        // If we're in December, then the latest available puzzle is today
        if (now.Month == Globals.DECEMBER)
        {
            latestPuzzleYear = now.Year;

            // If it's December 26th-31st the latest day is the 25th
            latestPuzzleDay = Math.Min(now.Day, Globals.CRISTMAS_DATE);
        }
        else
        {
            // Otherwise the latest puzzle is from the end of the previous event
            latestPuzzleYear = now.Year - 1;
            latestPuzzleDay = Globals.CRISTMAS_DATE;
        }

        return Tuple.Create(latestPuzzleYear, latestPuzzleDay);
    }
}
