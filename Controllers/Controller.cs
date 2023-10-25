﻿using AdventOfCode.Services;
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
        /// Runs a specific day's solution, and optionally posts the answer to Advent of Code and returns the result.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <param name="secondHalf"></param>
        /// <param name="send">Submit the result to Advent of Code</param>
        /// <param name="example">Use an example file instead of the regular input, you must add the example at `Inputs/YYYY/DD_example.txt`</param>
        /// <response code="200">The result of running the solution. If submitting the solution, also returns the response from Advent of Code.</response>
        [HttpGet("run-solution")]
        public async Task<ActionResult<string>> GetSolution([FromQuery, BindRequired] int year = Globals.START_YEAR, [FromQuery, BindRequired] int day = 1, bool secondHalf = false, bool send = false, bool example = false)
        {
            if (send && example) {
                return BadRequest("You're attempting to submit your answer to AOC while using an example input, this is likely a mistake.");
            }

            if (day == 25 && secondHalf) {
                return NotFound("There is no problem for Day 25 part 2, solve all other problems to get the last star.");
            }

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
        /// Creates missing service files.
        /// </summary>
        /// <remarks>
        /// Useful when a new year has started to preemptively generate the service files for the calendar year before the advent starts.
        /// The program is idempotent (You can run this multiple times as it will only add files if they are needed.)
        /// </remarks> 
        /// <response code="200">A string describing the updated solution folders/files.</response>
        [HttpPost("puzzle-helper")]
        public async Task<string> RunPuzzleHelper()
        {
            return await puzzleHelperService.Run();
        }

        /// <summary>
        /// Imports the input from Advent of Code for a specific day.
        /// </summary>
        /// <remarks>
        /// The program is idempotent (You can run this multiple times as it will only add files if they are needed.)
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