using AdventOfCode.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdventOfCode.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller : ControllerBase
    {
        private readonly ISolutionService solutionService;

        public Controller(ISolutionService solutionService)
        {
            this.solutionService = solutionService;
        }

        [HttpGet]
        public IActionResult GetSolution([FromQuery, BindRequired]int year = 2015, [FromQuery, BindRequired]int day = 1, bool secondHalf = false){
            try{
                return Ok(solutionService.GetSolution(year, day, secondHalf));
            }
            catch(SolutionNotFoundException e){
                return NotFound(e.Message);
            }
        }
    }
}