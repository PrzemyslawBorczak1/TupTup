using TupTrack.Domain.Entities;
using TupTrack.UseCases.DTOs;
using TupTrack.UseCases.Repositories;
using TupTrack.UseCases.SensorCoordinator;


namespace TupTrack.UseCases.Handlers;

public class StartRecordingHandler // TODO  inital room    no room exception handling
{
    ISensorCoordinator _sensorCoordinator;
    IRecordingRepository _recordingRepository;
    public StartRecordingHandler(ISensorCoordinator sensorCoordinator, IRecordingRepository recordingRepository)
    {
        _sensorCoordinator = sensorCoordinator;
        _recordingRepository = recordingRepository;
    }

    public async Task<Guid> StartRecording(StartRecordingDTO startRecordingDTO)
    {

        try
        {
            if (string.IsNullOrEmpty(startRecordingDTO.Room))
            {
                //throw new ArgumentException("Room name cannot be null or empty.");
            }

            //var initialRoom = await _recordingRepository.GetRoomAsync(startRecordingDTO.Room);



            var recording = new Recording(startRecordingDTO.StartTime);
            var firstTupStateEntity = new TupStateEntity(recording.Id, startRecordingDTO.FirstTupState, startRecordingDTO.StartTime);

            var roomTimestamp = new RoomTimestamp(initialRoom.Name, recording.Id, startRecordingDTO.StartTime);

            await _recordingRepository.AddInitialRecording(recording, firstTupStateEntity, roomTimestamp);

            try
            {
                _sensorCoordinator.SetSpeed(startRecordingDTO.SensorSpeed);
                _sensorCoordinator.Start();
            }
            catch (Exception ex)
            {
                // TODO handle sensor coordinator start failure, 
            }

            return recording.Id;
        }
        catch (Exception ex) // TODO dlete later
        {
            return Guid.Empty;
        }
    }


}
