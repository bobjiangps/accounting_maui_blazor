//using System.Net.Http.Headers;
//using System.Text.Json;
//using accountingMAUIBlazor.Models;

//namespace accountingMAUIBlazor.Services;

//public class AccountService : IAccountService
//{
//    private readonly IHttpClientFactory _httpClientFactory;
//    public IUserService _userService;
//    public BackendService backend = new();
//    private string _token;
//    public Account myAccounts;

//    public AccountService(IHttpClientFactory httpClientFactory, IUserService userService)
//    {
//        _httpClientFactory = httpClientFactory;
//        _userService = userService;
//    }

//    public async Task<Account> GetAccountsAsync()
//	{
//        _token = await _userService.GetTokenAsync();
//        using var httpClient = _httpClientFactory.CreateClient("Accounting");
//        HttpResponseMessage response;
//        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _token);
//        try
//        {
//            response = await httpClient.GetAsync(backend.GetApiByAlias("accounts"));
//            response.EnsureSuccessStatusCode();

//            var result = await response.Content.ReadAsStringAsync();
//            myAccounts = JsonSerializer.Deserialize<Account>(result);
//            return myAccounts;
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine("获取账户出错了: " + e.Message);
//            //这里可以考虑重定向到统一的错误页面
//        }


//    }
//}

