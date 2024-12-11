using AdventOfCode.Gateways;
using AdventOfCode.PuzzleHelper;
using AdventOfCode.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Console.Controllers
{
    public class Controller {
        private readonly AdventOfCodeGateway gateway = new();

        /// <summary>
        /// Runs a specific day's solution, and optionally posts the answer to Advent of Code and returns the result.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <param name="secondHalf"></param>
        /// <param name="send">Submit the result to Advent of Code</param>
        /// <param name="example">Use an example file instead of the regular input, you must add the example at `Inputs/YYYY/DD_example.txt`</param>
        public async Task GetSolution(int year = Globals.START_YEAR, int day = 1, bool secondHalf = false, bool send = false, bool example = false) {
            if (send && example)
            {
                System.Console.WriteLine("You're attempting to submit your answer to AOC while using an example input, this is likely a mistake.");
            }

            if (day == 25 && secondHalf)
            {
                System.Console.WriteLine("There is no problem for Day 25 part 2, solve all other problems to get the last star.");
            }
            
            SolutionService solutionService = SetupSolutionService();

            string result = await solutionService.GetSolution(year, day, secondHalf, send, example);
            System.Console.WriteLine(result);
        }

        /// <summary>
        /// Imports the input from Advent of Code for a specific day.
        /// </summary>
        /// <remarks>
        /// The program is idempotent (You can run this multiple times as it will only add a file if it is needed.)
        /// </remarks>
        /// <param name="year"></param>
        /// <param name="day"></param>
        public async Task ImportInputFile(int year = Globals.START_YEAR, int day = 1) {
            PuzzleHelperService puzzleHelperService = new(gateway);
            await puzzleHelperService.ImportInputFile(year, day);
        }

        /// <summary>
        /// Creates missing daily solution service files.
        /// </summary>
        /// <remarks>
        /// Useful when a new year has started to preemptively generate the service files for the calendar year before the advent starts.
        /// The program is idempotent (You can run this multiple times as it will only add files if they are needed.)
        /// 
        /// You'll likely only need to use this once per year and only if either your source code has gotten out of sync from the `main` branch or I haven't kept it up to date.
        /// </remarks> 
        public async Task GenerateMissingSolutionServiceFiles() {
            PuzzleHelperService puzzleHelperService = new(gateway);
            await puzzleHelperService.GenerateMissingSolutionServiceFiles();
        }

        private SolutionService SetupSolutionService() {
            // Setup access to each daily solution service
            ServiceCollection serviceProvider = new();

            #region Setup Daily Solution Services
            // Get a list of assembly types for the whole app
            IEnumerable<Type> assemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes());

            // Get only the types for the classes that inherit from the ISolutionDayService
            IEnumerable<Type> solutionDayServiceTypes = assemblyTypes.Where(x => !x.IsInterface && x.GetInterface(nameof(ISolutionDayService)) != null);

            // Register each Solution Day Service class
            foreach (Type solutionDayServiceType in solutionDayServiceTypes)
            {
                // This is not null because of the filter a few lines above
                Type interfaceType = solutionDayServiceType.GetInterface(nameof(ISolutionDayService))!;

                serviceProvider.AddTransient(interfaceType, solutionDayServiceType);
            }
            #endregion

            SolutionService solutionService = new(serviceProvider.BuildServiceProvider(), gateway);

            return solutionService;
        }
    }
}