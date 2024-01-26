using accountingMAUIBlazor.Models;

namespace accountingMAUIBlazor.Services;

public interface IPoetryStorage
{
    Task InitializeAsync();

    Task SavePoetryAsync(Poetry poetry);

    Task<Poetry> GetPoetryAsync(int id);

    Task<IEnumerable<Poetry>> SearchByNameAsync(string name);
}

