namespace accountingMAUIBlazor.Services;

public interface IBackendService
{
    string GetApiByAlias(string alias);
    string GetApiByName(string name);
}

