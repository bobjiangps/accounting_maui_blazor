using SQLite;

namespace accountingMAUIBlazor.Models;

public class Poetry
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Content { get; set; }
}

