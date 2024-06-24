using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace accountingMAUIBlazor.Pages;

public partial class Login
{
    public bool isLoggedIn;
    public string loginName;
    private string token;
    public string formMessage;
    public string errorMessage;
    public UserLoginViewModel LoginModel = new();

    protected override void OnInitialized()
    {
        isLoggedIn = _userService.CheckLoggedStatus();
        if (isLoggedIn)
        {
            loginName = _userService.CheckLoginName();
        }
    }

    public async Task LoginWithInput(EditContext context)
    {
        Console.WriteLine("start to login...");
        token = await _userService.GetTokenAsync(LoginModel.UserName, LoginModel.Password);
        Console.WriteLine($"Token is: {token}");
        if (!string.IsNullOrWhiteSpace(token))
        {
            formMessage = "登录成功, 即将跳转";
            await Task.Delay(500);
            NavigationManager.NavigateTo("/accounts");
        }
        else
        {
            errorMessage = "登录失败";
        }
    }

    public async Task SignOut()
    {
        Console.WriteLine("准备退出登录...");
        await _userService.RevokeTokenAsync();
        isLoggedIn = _userService.CheckLoggedStatus();
        Console.WriteLine("退出登录完毕...");
    }
}

public class UserLoginViewModel
{
    [Required(ErrorMessage = "用户名不能为空")]
    [StringLength(16, ErrorMessage = "用户名不应超出16个字符长度")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "密码不能为空")]
    public string Password { get; set; }
}
