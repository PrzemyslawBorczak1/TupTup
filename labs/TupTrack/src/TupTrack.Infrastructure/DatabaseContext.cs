using SQLite;

namespace TupTrack.Infrastructure;

public class DatabaseContext
{
    private bool _initialized = false;

    private SQLiteAsyncConnection _connection;
    public SQLiteAsyncConnection Connection
    {
        get => _connection;
        set => _connection = value;
    }

    public DatabaseContext(string path)
    {
        _connection = new SQLiteAsyncConnection(path);
    }

    public async Task InitAsync()
    {
        if (_initialized)
            return;


        await _connection.CreateTableAsync<Tables.Recording>();
        await _connection.CreateTableAsync<Tables.TupStateEntity>();
        await _connection.CreateTableAsync<Tables.RecordingGroup>();
        await _connection.CreateTableAsync<Tables.SensorType>();
        await _connection.CreateTableAsync<Tables.SensorReading>();
        await _connection.CreateTableAsync<Tables.Room>();
        await _connection.CreateTableAsync<Tables.RoomTimestamp>();

        await SeedDatabase();

        _initialized = true;
    }

    private async Task SeedDatabase()
    {
        var groups = await _connection.Table<Tables.RecordingGroup>().ToListAsync();
        if (groups.Count == 0)
        {
            await _connection.InsertAllAsync(new List<Tables.RecordingGroup>()
            {
                new Tables.RecordingGroup() { Name = "Group 1", Description = "Description for Group 1" },
                new Tables.RecordingGroup() { Name = "Group 2", Description = "Description for Group 2" },
                new Tables.RecordingGroup() { Name = "Group 3", Description = "Description for Group 3" },
            });
        }

        var rooms = await _connection.Table<Tables.Room>().ToListAsync();
        if (rooms.Count == 0)
        {
            await _connection.InsertAllAsync(new List<Tables.Room>()
            {
                new Tables.Room() { Name = "Room 1", Description = "Description for Room 1" },
                new Tables.Room() { Name = "Room 2", Description = "Description for Room 2" },
                new Tables.Room() { Name = "Room 3", Description = "Description for Room 3" },
            });
        }

    }

    private async Task DropAllTablesAsync()
    {
        await _connection.DropTableAsync<Tables.Recording>();
        await _connection.DropTableAsync<Tables.TupStateEntity>();
        await _connection.DropTableAsync<Tables.RecordingGroup>();
        await _connection.DropTableAsync<Tables.SensorType>();
        await _connection.DropTableAsync<Tables.SensorReading>();
        await _connection.DropTableAsync<Tables.Room>();
        await _connection.DropTableAsync<Tables.RoomTimestamp>();
    }

}

