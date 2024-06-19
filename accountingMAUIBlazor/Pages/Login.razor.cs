using accountingMAUIBlazor.Services;

namespace accountingMAUIBlazor.Pages;

public partial class Login
{
    //private readonly IUserService _userService;
    private string token;
    private int click_count = 0;

    public async void LoginWithoutInput()
    {
        Console.WriteLine("start to login...");
        click_count += 1;
        token = await _userService.GetTokenAsync();
        Console.WriteLine($"Token is: {token}");
        //NavigationManager.NavigateTo("/account");
    }
}

