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

        [HttpPost("puzzle-helper")]
        public async Task<string> RunPuzzleHelper()
        {
            return await puzzleHelperService.Run();
        }

        [HttpPost("puzzle-helper-daily")]
        public async Task<string> RunPuzzleHelper([FromQuery, BindRequired] int year = Globals.START_YEAR, [FromQuery, BindRequired] int day = 1)
        {
            return await puzzleHelperService.RunDaily(year, day);
        }

        [HttpPost("init")]
        public async Task InitializeRepo()
        {
            await puzzleHelperService.InitializeRepo();
        }
    }
}