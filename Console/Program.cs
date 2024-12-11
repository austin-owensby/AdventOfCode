// TODO, allow us to hit the same webapi endpoints from the console app
using AdventOfCode.Gateways;
using AdventOfCode.PuzzleHelper;
using AdventOfCode.Services;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

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

AdventOfCodeGateway gateway = new();
SolutionService solutionService = new(serviceProvider.BuildServiceProvider(), gateway);
string result = await solutionService.GetSolution(2015, 1, false, false, false);
Console.WriteLine(result);
