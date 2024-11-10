using AdventOfCode.Gateways;

namespace AdventOfCode.Services
{
    public class SolutionService(IServiceProvider serviceProvider, AdventOfCodeGateway adventOfCodeGateway)
    {
        private readonly IServiceProvider serviceProvider = serviceProvider;
        private readonly AdventOfCodeGateway adventOfCodeGateway = adventOfCodeGateway;

        /// <summary>
        /// Execute the specific solution based on the passed in parameters
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <param name="secondHalf"></param>
        /// <param name="send"></param>
        /// <param name="example"></param>
        /// <returns></returns>
        /// <exception cref="SolutionNotFoundException"></exception>
        public async Task<string> GetSolution(int year, int day, bool secondHalf, bool send, bool example)
        {
            ISolutionDayService service = FindSolutionService(year, day);

            // Run the specific solution
            string answer = secondHalf ? service.SecondHalf(example) : service.FirstHalf(example);

            // Optionally submit the answer to AoC
            if (send)
            {
                try
                {
                    string response = await adventOfCodeGateway.SubmitAnswer(year, day, secondHalf, answer);
                    answer = $"Submitted answer: {answer}.\nAdvent of Code response: {response}";
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred while submitting the answer to Advent of Code");
                    answer = $"Submitted answer: {answer}.\nAdvent of Code response: {e.Message}";
                }
            }

            return answer;
        }

        /// <summary>
        /// Fetch the specific service for the specified year and day
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private ISolutionDayService FindSolutionService(int year, int day)
        {
            IEnumerable<ISolutionDayService> services = serviceProvider.GetServices<ISolutionDayService>();

            // Use ':D2' to front pad 0s to single digit days to match the formatting
            string serviceName = $"AdventOfCode.Services.Solution{year}_{day:D2}Service";
            ISolutionDayService? service = services.FirstOrDefault(s => s.GetType().ToString() == serviceName);

            // If the service was not found, throw an exception
            if (service == null)
            {
                throw new SolutionNotFoundException($"No solutions found for day {day}/{year}.");
            }

            return service;
        }
    }
}