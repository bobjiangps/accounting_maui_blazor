using System.Text.Json;
using System.Text;
using accountingMAUIBlazor.Models;
using System.Net.Http.Headers;

namespace accountingMAUIBlazor.Services;

public class UserService : IUserService
{
    //public bool IsLoggedIn { get; set; } = false;
    //public string loginName;
    //private string _token = string.Empty;

    public bool isLoggedIn;
    public string loginName;
    private string _token;
    private readonly IHttpClientFactory _httpClientFactory;
    public BackendService backend = new();

    public UserService(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    public bool CheckLoggedStatus()
    {
        isLoggedIn = Preferences.Get("IsLoggedIn", false);
        return isLoggedIn;
    }

    public string CheckLoginName()
    {
        loginName = Preferences.Get("loginName", null);
        return loginName;
    }

    public async Task<String> GetTokenAsync(string userName, string passWord)
    {
        _token = Preferences.Get("UserToken", String.Empty);
        if (!string.IsNullOrWhiteSpace(_token) || isLoggedIn)
        {
            Console.WriteLine("no need to request api, return existent token");
            return _token;
        }

        //using var httpClient = new HttpClient();
        using var httpClient = _httpClientFactory.CreateClient("Accounting");
        HttpResponseMessage response;
        try
        {
            var loginData = new
            {
                username = userName,
                password = passWord
            };
            var loginContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            //response = await httpClient.PostAsync("http://127.0.0.1:8000/external/api/login/", loginContent);
            //response = await httpClient.PostAsync("login/", loginContent);
            response = await httpClient.PostAsync(backend.GetApiByAlias("login"), loginContent);
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
            isLoggedIn = true;
            Preferences.Set("IsLoggedIn", isLoggedIn);
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
                //using var httpClient = new HttpClient();
                using var httpClient = _httpClientFactory.CreateClient("Accounting");
                //httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {_token}");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _token);
                HttpResponseMessage response;
                //response = await httpClient.PostAsync("http://127.0.0.1:8000/external/api/logout/", null);
                //response = await httpClient.PostAsync("logout/", null);
                response = await httpClient.PostAsync(backend.GetApiByAlias("logout"), null);
                var statusCode = (int)response.StatusCode;
                int[] availableCode = new[] { 200, 401 };
                if (availableCode.Contains(statusCode))
                {
                    _token = null;
                    Preferences.Set("UserToken", _token);
                    loginName = null;
                    Preferences.Set("loginName", loginName);
                    isLoggedIn = false;
                    Preferences.Set("IsLoggedIn", isLoggedIn);
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
