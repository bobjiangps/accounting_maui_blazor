using accountingMAUIBlazor.Models;

namespace accountingMAUIBlazor.Services;

public interface IUserService
{
    Task<String> GetTokenAsync();

}

