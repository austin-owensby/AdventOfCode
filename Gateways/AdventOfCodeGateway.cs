using HtmlAgilityPack;

public class AdventOfCodeGateway
{
    private HttpClient? client;
    private readonly int throttleInMinutes = 3;
    private DateTimeOffset? lastCall = null;

    /// <summary>
    /// For a given year and day, get the user's puzzle input
    /// </summary>
    /// <param name="year"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public async Task<string> ImportInput(int year, int day)
    {
        ThrottleCall();

        HttpRequestMessage message = new(HttpMethod.Get, $"/{year}/day/{day}/input");

        if (client == null)
        {
            try
            {
                InitializeClient();
            }
            catch
            {
                return "Unable to read Cookie.txt. Make sure that it exists in the PuzzleHelper folder. See the ReadMe for more.";
            }
        }

        HttpResponseMessage result = await client!.SendAsync(message);
        string response = await GetSuccessfulResponseContent(result);

        return response;
    }

    /// <summary>
    /// Send the user's answer to the specific question
    /// </summary>
    /// <param name="year"></param>
    /// <param name="day"></param>
    /// <param name="secondHalf"></param>
    /// <param name="answer"></param>
    /// <returns></returns>
    public async Task<string> SubmitAnswer(int year, int day, bool secondHalf, string answer)
    {
        ThrottleCall();

        Dictionary<string, string> data = new()
        {
            { "level", secondHalf ? "2" : "1"},
            { "answer", answer }
        };

        HttpContent request = new FormUrlEncodedContent(data);

        if (client == null)
        {
            try
            {
                InitializeClient();
            }
            catch
            {
                return "Unable to read Cookie.txt. Make sure that it exists in the PuzzleHelper folder. See the ReadMe for more.";
            }
        }

        HttpResponseMessage result = await client!.PostAsync($"/{year}/day/{day}/answer", request);

        string response = await GetSuccessfulResponseContent(result);

        if (response.Contains("please identify yourself"))
        {
            // We tried to submit an answer, but our token has expired
            response = "Your cookie has expired, view the ReadMe for instructions on how to update it.";
        }
        else
        {
            try
            {
                // Find article component
                HtmlDocument doc = new();
                doc.LoadHtml(response);
                HtmlNode article = doc.DocumentNode.SelectSingleNode("//article/p");
                response = article.InnerHtml;
            }
            catch (Exception)
            {
                Console.WriteLine("Error parsing html response.");
            }
        }

        return response;
    }

    /// <summary>
    /// Ensure that the response was successful and return the parsed response if it was
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static async Task<string> GetSuccessfulResponseContent(HttpResponseMessage result)
    {
        result.EnsureSuccessStatusCode();
        return await result.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Tracks the last API call and prevents another call from being made until after the configured limit
    /// </summary>
    private void ThrottleCall()
    {
        // If someone is running the project for the first time that's 400 calls
        if (lastCall != null && (DateTimeOffset.Now < lastCall.Value.AddMinutes(throttleInMinutes)))
        {
            throw new Exception($"Unable to make another API call to AOC Server because we are attempting to throttle calls according to their specifications (See more in the ReadMe). Please try again after {lastCall.Value.AddMinutes(throttleInMinutes)}.");
        }
        else
        {
            lastCall = DateTimeOffset.Now;
        }
    }

    /// <summary>
    /// Initialize the Http Client using the user's cookie
    /// </summary>
    private void InitializeClient()
    {
        // We're waiting to do this until the last moment in case someone want's to use the code base without setting up the cookie
        client = new HttpClient
        {
            BaseAddress = new Uri("https://adventofcode.com")
        };

        client.DefaultRequestHeaders.UserAgent.ParseAdd($".NET 8.0 (+via https://github.com/austin-owensby/AdventOfCode by austin_owensby@hotmail.com)");

        try
        {
            string cookie = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "PuzzleHelper/Cookie.txt"));
            client.DefaultRequestHeaders.Add("Cookie", cookie);
        }
        catch (Exception)
        {
            throw new Exception("Unable to read Cookie.txt. Make sure that it exists in the PuzzleHelper folder. See the ReadMe for more.");
        }
    }
}