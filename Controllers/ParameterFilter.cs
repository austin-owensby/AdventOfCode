using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AdventOfCode.Controllers
{
    public class ParameterFilter : IParameterFilter
    {
        public ParameterFilter() { }

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            // Ensure that the input day is a valid value (1 - 25)
            // This does not check if the currently selected year has that date
            if (parameter.Name.Equals("day", StringComparison.InvariantCultureIgnoreCase))
            {
                List<int> daysInDecember = Enumerable.Range(1, Globals.CRISTMAS_DATE).ToList();
                parameter.Schema.Enum = daysInDecember.Select(d => new OpenApiString(d.ToString())).ToList<IOpenApiAny>();
            }

            // Ensure that the input year is a valid value (2015 - this year)
            if (parameter.Name.Equals("year", StringComparison.InvariantCultureIgnoreCase))
            {
                DateTime now = DateTime.UtcNow.AddHours(Globals.SERVER_UTC_OFFSET);

                List<int> daysInDecember = Enumerable.Range(Globals.START_YEAR, now.Year - Globals.START_YEAR + 1).ToList();
                parameter.Schema.Enum = daysInDecember.Select(d => new OpenApiString(d.ToString())).ToList<IOpenApiAny>();
            }
        }
    }
}