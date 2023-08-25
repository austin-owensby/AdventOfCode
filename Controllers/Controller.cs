using AdventOfCode.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdventOfCode.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller : ControllerBase
    {
        private readonly SolutionService solutionService;
        private readonly PuzzleHelperService puzzleHelperService;

        public Controller(SolutionService solutionService, PuzzleHelperService puzzleHelperService)
        {
            this.solutionService = solutionService;
            this.puzzleHelperService = puzzleHelperService;
        }

        /// <summary>
        /// Run a specific solution.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <param name="secondHalf"></param>
        /// <param name="send">Submit the result to Advent of Code</param>
        /// <param name="example">Use the example text file instead of the problem's.</param>
        /// <response code="200">The result of running the solution. If submitting the solution, also returns the response from Advent of Code.</response>
        [HttpGet("run-solution")]
        public async Task<ActionResult<string>> GetSolution([FromQuery, BindRequired] int year = Globals.START_YEAR, [FromQuery, BindRequired] int day = 1, bool secondHalf = false, bool send = false, bool example = false)
        {
            try
            {
                return await solutionService.GetSolution(year, day, secondHalf, send, example);
            }
            catch (SolutionNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Add missing service folders and files.
        /// </summary>
        /// <remarks>
        /// Useful when a new year has started to preemptively generate the service files for the calendar year before the advent starts.
        /// </remarks> 
        /// <response code="200">A string describing the updated solution folders/files.</response>
        [HttpPost("puzzle-helper")]
        public async Task<string> RunPuzzleHelper()
        {
            return await puzzleHelperService.Run();
        }

        /// <summary>
        /// For a specific puzzle, import the missing input file.
        /// </summary>
        /// <remarks>
        /// This is useful when a puzzle first opens and you want to quickly download it's input.
        /// </remarks>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <response code="200">A message on what was updated.</response>
        [HttpPost("puzzle-helper-daily")]
        public async Task<string> RunPuzzleHelper([FromQuery, BindRequired] int year = Globals.START_YEAR, [FromQuery, BindRequired] int day = 1)
        {
            return await puzzleHelperService.RunDaily(year, day);
        }
    }
}