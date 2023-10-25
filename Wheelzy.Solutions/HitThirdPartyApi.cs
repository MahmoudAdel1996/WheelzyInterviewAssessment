namespace Wheelzy.Solutions;

public class ResponseDto<T>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Value { get; set; }
}

public static class HitThirdPartyApi
{
    public static async Task Exec()
    {
        const string apiUrl = "https://notrealapi.com/lottery/play";
        const int attempts = 10;
        for (var i = 1; i <= attempts; i++)
        {
            var customerNumber = GenerateRandomNumber();
            Console.WriteLine($"Attempt {i}: Sending customer number {customerNumber}");
            var result = await PlayLottery(apiUrl, customerNumber);
            if (!result.IsSuccess)
            {
                Console.WriteLine($"Error! {result.Message}");
                break;
            }
            if (result.IsSuccess && result.Value == customerNumber)
            {
                Console.WriteLine($"Received result: {result.Value}");
                Console.WriteLine($"Congratulations! You've won!");
                break;
            }
            if (i == attempts)
                Console.WriteLine($"No Luck. Try again later.");
        }
    }

    private static async Task<ResponseDto<int>> PlayLottery(string apiUrl, int customerNumber)
    {
        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("customerNumber", customerNumber.ToString())
        });
        HttpResponseMessage response;
        try
        {
            response = await client.PostAsync(apiUrl, content);

        }
        catch (Exception e)
        {
            return new ResponseDto<int>
            {
                Message = $"Http error: {e.Message}"
            };
        }
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return new ResponseDto<int>
            {
                Message = $"Http error: {response.StatusCode}"
            }; 
        }

        if (int.TryParse(responseContent, out var result))
        {
            return new ResponseDto<int>
            {
                IsSuccess = true,
                Value = result
            };
        }

        return new ResponseDto<int>
        {
            Message = $"Invalid Response from the api => {responseContent}"
        };
    }

    private static int GenerateRandomNumber()
    {
        var rand = new Random();
        return rand.Next(1000, 10000);
    }
}