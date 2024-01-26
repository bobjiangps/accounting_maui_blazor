using accountingMAUIBlazor.Models;
using SQLite;

namespace accountingMAUIBlazor.Services
{
	public class PoetryStorage: IPoetryStorage
	{
        public const string DBName = "poetrydb.sqlite";

        public static readonly string PoetryDBPath = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData
                ), DBName
            );

        private SQLiteAsyncConnection _connection;

        private SQLiteAsyncConnection Connection =>
            _connection ??= new SQLiteAsyncConnection(PoetryDBPath);

        public async Task InitializeAsync()
        {
            await Connection.CreateTableAsync<Poetry>();
        }

        //LINQ
        public async Task<Poetry> GetPoetryAsync(int id)
        {
            return await Connection.Table<Poetry>()
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
        }

        public async Task SavePoetryAsync(Poetry poetry)
        {
            await Connection.InsertAsync(poetry);
        }

        public async Task<IEnumerable<Poetry>> SearchByNameAsync(string name)
        {
            return await Connection.Table<Poetry>()
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }
    }
}

