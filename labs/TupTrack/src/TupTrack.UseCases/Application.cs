using TupTrack.Domain;

namespace TupTrack.UseCases
{
    public class Application 
    {
        RecordingService _startRecording;
        public Application(RecordingService sr)
        {
            _startRecording = sr;
        }

        public async Task StartRecording(StartRecordingDTO startRecordingDTO) => await _startRecording.StartRecording(startRecordingDTO);
    }
}
