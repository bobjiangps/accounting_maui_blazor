namespace accountingMAUIBlazor.Services;

public class BackendService : IBackendService
{
	public const string baseAddress = "http://127.0.0.1:8000/external/api/";
    UriBuilder uriBuilder = new (baseAddress);
    public Dictionary<string, string> apiList = new()
    {
        { "login", "login/" },
        { "logout", "logout/" }
    };

    public string GetApiByAlias(string alias)
	{
        var endpoint = apiList[alias];
        return endpoint;
    }

    public string GetApiByName(string name)
    {
        return string.Concat(uriBuilder, name);
    }
}
