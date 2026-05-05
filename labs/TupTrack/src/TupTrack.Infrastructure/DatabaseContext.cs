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

        _initialized = true;
    }


}

