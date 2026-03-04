using accountingMAUIBlazor.Models;

namespace accountingMAUIBlazor.Services;

public interface IAccountService
{
    Task<Account> GetAccountsAsync();
}

