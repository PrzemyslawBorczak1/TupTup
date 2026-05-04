using TupTrack.UseCases.SensorCoordinator;

using TupTrack.Domain.Entities;
using TupTrack.UseCases.DTOs;
using TupTrack.UseCases.Repositories;


namespace TupTrack.UseCases.Handlers;

public class StartRecordingHandler // TODO  inital room   
{
    ISensorCoordinator _sensorCoordinator;
    IRecordingRepository _appDatabase;
    public StartRecordingHandler(ISensorCoordinator sensorCoordinator, IRecordingRepository recordingRepository)
    {
        _sensorCoordinator = sensorCoordinator;
        _appDatabase = recordingRepository;
    }

    public async Task<Guid> StartRecording(StartRecordingDTO startRecordingDTO) {
        var recording = Recording.Create(startRecordingDTO.StartTime);
        var firstTupStateEntity = TupStateEntity.Create(recording.Id, startRecordingDTO.FirstTupState, startRecordingDTO.StartTime);


        await _appDatabase.AddRecording(recording);
        await _appDatabase.AddTupState(firstTupStateEntity);

        _sensorCoordinator.SetSpeed(startRecordingDTO.SensorSpeed);
        _sensorCoordinator.Start();

        return recording.Id;
    }


}
