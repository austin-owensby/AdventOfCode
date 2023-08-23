using AdventOfCode.Controllers;
using AdventOfCode.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdventOfCode", Version = "v1" });
    c.ParameterFilter<ParameterFilter>();

    var filePath = Path.Combine(AppContext.BaseDirectory, "AdventOfCode.xml");
     c.IncludeXmlComments(filePath);
});

// Add the gateway as singleton since almost all API calls use it and it sets up a client that we'd like to keep configured
builder.Services.AddSingleton<AdventOfCodeGateway>()
    .AddHttpClient(string.Empty, c => {
        c.BaseAddress = new Uri("https://adventofcode.com");
        // Don't modify this User Agent, it should match the repo making the request and not the user making the request
        c.DefaultRequestHeaders.UserAgent.ParseAdd($".NET 7.0 (+via https://github.com/austin-owensby/AdventOfCode by austin_owensby@hotmail.com)");
        
        try {
            string cookie = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "PuzzleHelper/Cookie.txt"));
            c.DefaultRequestHeaders.Add("Cookie", cookie);
        }
        catch (Exception) {
            throw new Exception("Unable to read Cookie.txt. Make sure that it exists in the PuzzleHelper folder. See the ReadMe for more.");
        }
    });

// Adding all services as Transient because on each request, we should only call the service once.
// We could use Singleton for preformance improvement on succesive calls,
//    but because we want to avoid spamming the AoC server, we'll assume that this performance is negligible.
builder.Services.AddTransient<SolutionService>();
builder.Services.AddTransient<PuzzleHelperService>();

// This is a collapsable region so that I can hide my shameful, unintuitive, lazy, "clever", and unreadable code :'(
#region Here be dragons!
// Here be dragons! ( Especially lazy dragons ;) )
// Get a list of assembly types for the whole app
Type[] assemblyTypes = Assembly.GetAssembly(typeof(Program))?.GetTypes() ?? new Type[]{};

// Get only the types for the classes that inherit from the ISolutionDayService
IEnumerable<Type> solutionDayServiceTypes = assemblyTypes.Where(x => !x.IsInterface && x.GetInterface(nameof(ISolutionDayService)) != null);

// Register each Solution Day Service class
foreach (Type solutionDayServiceType in solutionDayServiceTypes)
{
    // This is not null because of the filter a few lines above
    Type interfaceType = solutionDayServiceType.GetInterface(nameof(ISolutionDayService))!;
    
    builder.Services.AddTransient(interfaceType, solutionDayServiceType);
}
#endregion

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();