
using SQLite;
using TupTrack.Infrastructure.Records;


namespace TupTrack.Infrastructure;

public class AppDatabase
{
    private SQLiteAsyncConnection _connection;
    private bool _initialized = false;

    public AppDatabase(string path)
    {
        _connection = new SQLiteAsyncConnection(path);
    }

    private async Task InitAsync()
    {
        if (_initialized)
            return;

        await _connection.CreateTableAsync<ExampleRecord>();

        _initialized = true;
    }

    public async Task Save(ExampleRecord e)
    {
        await InitAsync();
        await _connection.InsertAsync(e);
    }

    public async Task<List<ExampleRecord>> Get()
    {
        await InitAsync();
        return await _connection.Table<ExampleRecord>().ToListAsync();
    }
}

