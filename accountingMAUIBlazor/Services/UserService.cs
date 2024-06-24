using System.Text.Json;
using System.Text;
using accountingMAUIBlazor.Models;

namespace accountingMAUIBlazor.Services;

public class UserService : IUserService
{
    //public bool IsLoggedIn { get; set; } = false;
    //public string loginName;
    //private string _token = string.Empty;

    public bool IsLoggedIn;
    public string loginName;
    private string _token;

    public bool CheckLoggedStatus()
    {
        IsLoggedIn = Preferences.Get("IsLoggedIn", false);
        return IsLoggedIn;
    }

    public string CheckLoginName()
    {
        loginName = Preferences.Get("loginName", null);
        return loginName;
    }

    public async Task<String> GetTokenAsync(string userName, string passWord)
    {
        _token = Preferences.Get("UserToken", String.Empty);
        if (!string.IsNullOrWhiteSpace(_token) || IsLoggedIn)
        {
            Console.WriteLine("no need to request api, return existent token");
            return _token;
        }

        using var httpClient = new HttpClient();
        HttpResponseMessage response;
        try
        {
            var loginData = new
            {
                username = userName,
                password = passWord
            };
            var loginContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            response =
                await httpClient.PostAsync("http://127.0.0.1:8000/external/api/login/", loginContent);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Console.WriteLine("登录出错了: " + e.Message);
            return _token;
        }

        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine("result: " + result);
        try
        {
            _token = JsonSerializer.Deserialize<Token>(result)?.token ?? throw new JsonException();
            Preferences.Set("UserToken", _token);
            loginName = JsonSerializer.Deserialize<Token>(result)?.username ?? throw new JsonException();
            Preferences.Set("loginName", loginName);
            IsLoggedIn = true;
            Preferences.Set("IsLoggedIn", IsLoggedIn);
        }
        catch (Exception e)
        {
            Console.WriteLine("处理json出错了: " + e.Message);
            return _token;
        }

        return _token;
    }

    public async Task RevokeTokenAsync()
    {
        _token = Preferences.Get("UserToken", String.Empty);
        if (!string.IsNullOrWhiteSpace(_token))
        {
            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {_token}");
                HttpResponseMessage response;
                response = await httpClient.PostAsync("http://127.0.0.1:8000/external/api/logout/", null);
                //response.EnsureSuccessStatusCode();
                var statusCode = (int)response.StatusCode;
                int[] availableCode = new[] { 200, 401 };
                if (availableCode.Contains(statusCode))
                {
                    _token = null;
                    Preferences.Set("UserToken", _token);
                    loginName = null;
                    Preferences.Set("loginName", loginName);
                    IsLoggedIn = false;
                    Preferences.Set("IsLoggedIn", IsLoggedIn);
                }
                else
                {
                    throw new Exception("未知的返回，退出登录失败");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("退出登录出错了: " + e.Message);
            }

        }
    }
}
