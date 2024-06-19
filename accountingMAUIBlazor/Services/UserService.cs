using System.Text.Json;
using System.Text;
using accountingMAUIBlazor.Models;

namespace accountingMAUIBlazor.Services;

public class UserService : IUserService
{
    public bool IsLoggedIn { get; set; } = false;
    private string _token = string.Empty;

    public async Task<String> GetTokenAsync() //remember to add user parameter
    {
        if (!string.IsNullOrWhiteSpace(_token) || IsLoggedIn)
        {
            Console.WriteLine("no need to request api, return existent token");
            return _token;
        }

        using var httpClient = new HttpClient();
        HttpResponseMessage response;
        try
        {
            var loginData = new {
                username = "Bo",
                password = "Bo"
            };
            var loginContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            response =
                await httpClient.PostAsync("http://127.0.0.1:8000/external/api/login/", loginContent);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error when request api: " + e.Message);
            return _token;
        }

        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine("result: " + result);
        try
        {
            _token = JsonSerializer.Deserialize<Token>(result)?.token ?? throw new JsonException();
            IsLoggedIn = true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error when handle json: " + e.Message);
            return _token;

        }

        return _token;
    }
}
