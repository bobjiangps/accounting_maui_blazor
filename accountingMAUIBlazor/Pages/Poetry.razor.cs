namespace accountingMAUIBlazor.Pages;

public partial class Poetry
{
	private async Task InitializeAsync()
	{
		await ips.InitializeAsync();

    }

    private string _name;

    private string _content;

    private string _query;

    private IEnumerable<Models.Poetry> _poetries = new List<Models.Poetry>();

    private async Task SaveAsync()
    {
        //var poetry = new Models.Poetry();
        //poetry.Name = _name;
        //poetry.Content = _content;
        var poetry = new Models.Poetry
        {
            Name = _name,
            Content = _content
        };
        await ips.SavePoetryAsync(poetry);
    }

    private async Task SearchAsync()
    {
        _poetries =  await ips.SearchByNameAsync(_query);

    }
}
