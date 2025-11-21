using Microsoft.OpenApi;
using System.Text.Json.Nodes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AdventOfCode.Controllers
{
    /// <summary>
    /// Configures parameter filters
    /// </summary>
    public class ParameterFilter : IParameterFilter
    {
        /// <summary>
        /// Applies parameter filters to the API calls in Swagger
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="context"></param>
        public void Apply(IOpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter?.Name == null || parameter.Schema == null)
                return;

            // Ensure the schema Enum collection is initialized. In some cases
            // Microsoft.OpenApi leaves the Enum property null; it's a read-only
            // property so we initialize the private backing field via
            // reflection when necessary.
            if (parameter.Schema.Enum == null)
            {
                var schemaType = parameter.Schema.GetType();

                // Try to find a backing field that represents the Enum collection.
                var fields = schemaType.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                // Prefer fields with 'enum' in the name and compatible generic IList<JsonNode> type
                var enumField = fields.FirstOrDefault(f => f.Name.ToLower().Contains("enum")
                    && f.FieldType.IsGenericType
                    && f.FieldType.GetGenericTypeDefinition() == typeof(IList<>)
                    && f.FieldType.GetGenericArguments()[0] == typeof(JsonNode));

                // Fallback: first field whose type is IList<JsonNode>
                if (enumField == null)
                {
                    enumField = fields.FirstOrDefault(f => f.FieldType.IsGenericType
                        && f.FieldType.GetGenericTypeDefinition() == typeof(IList<>)
                        && f.FieldType.GetGenericArguments()[0] == typeof(JsonNode));
                }

                if (enumField != null)
                {
                    enumField.SetValue(parameter.Schema, new List<JsonNode>());
                }
            }

            // Ensure that the input day is a valid value (1 - 25)
            // This does not check if the currently selected year has that date
            if (parameter.Name.Equals("day", StringComparison.InvariantCultureIgnoreCase))
            {
                var days = Enumerable.Range(1, Globals.NUMBER_OF_PUZZLES);
                // Enum is a read-only collection; clear and add values
                parameter.Schema.Enum!.Clear();
                foreach (var d in days)
                {
                    parameter.Schema.Enum.Add(JsonValue.Create(d.ToString()));
                }
            }

            // Ensure that the input year is a valid value (2015 - this year)
            if (parameter.Name.Equals("year", StringComparison.InvariantCultureIgnoreCase))
            {
                DateTime now = DateTime.UtcNow.AddHours(Globals.SERVER_UTC_OFFSET);
                var years = Enumerable.Range(Globals.START_YEAR, now.Year - Globals.START_YEAR + 1);
                parameter.Schema.Enum!.Clear();
                foreach (var y in years)
                {
                    parameter.Schema.Enum.Add(JsonValue.Create(y.ToString()));
                }
            }
        }
    }
}