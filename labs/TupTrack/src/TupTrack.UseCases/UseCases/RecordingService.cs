using TupTrack.UseCases.SensorCoordinator;
using TupTrack.Infrastructure;
using TupTrack.Domain;

namespace TupTrack.UseCases;

public class RecordingService
{
    ISensorCoordinator _sensorCoordinator;
    AppDatabase _appDatabase;
    public RecordingService(ISensorCoordinator sensorCoordinator, AppDatabase appDatabase)
    {
        _sensorCoordinator = sensorCoordinator;
        _appDatabase = appDatabase;
    }

    public async Task StartRecording(StartRecordingDTO startRecordingDTO) {
        await _appDatabase.AddStartRecordingAsync(startRecordingDTO);
        _sensorCoordinator.Start();
    }


}
