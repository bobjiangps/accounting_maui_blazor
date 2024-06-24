namespace accountingMAUIBlazor.Services;

public interface IUserService
{
    Task<String> GetTokenAsync(string userName, string password);

    Task RevokeTokenAsync();

    bool CheckLoggedStatus();

    string CheckLoginName();

}
