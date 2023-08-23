using HtmlAgilityPack;

public class AdventOfCodeGateway
{
    private readonly HttpClient client;
    private readonly int throttleInMinutes = 3;
    private DateTimeOffset? lastCall = null;

    public AdventOfCodeGateway(HttpClient client) {
        this.client = client;
    }

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

        HttpResponseMessage result = await client.SendAsync(message);
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

        HttpResponseMessage result = await client.PostAsync($"/{year}/day/{day}/answer", request);

        string response = await GetSuccessfulResponseContent(result);

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

        return response;
    }

    /// <summary>
    /// Ensure that the response was successful and return the parsed response if it was
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private async Task<string> GetSuccessfulResponseContent(HttpResponseMessage result) {
        result.EnsureSuccessStatusCode();
        return await result.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Tracks the last API call and prevents another call from being made until after the configured limit
    /// </summary>
    private void ThrottleCall() {
        // If someone is running the project for the first time that's 400 calls
        if (lastCall != null && (DateTimeOffset.Now < lastCall.Value.AddMinutes(throttleInMinutes))) {
            throw new Exception($"Unable to make another API call to AOC Server to grab your input because we are attempting to throttle calls according to their specifications (See more in the ReadMe). Please try again after {lastCall.Value.AddMinutes(throttleInMinutes)}.");
        }
        else {
            lastCall = DateTimeOffset.Now;
        }
    }
}