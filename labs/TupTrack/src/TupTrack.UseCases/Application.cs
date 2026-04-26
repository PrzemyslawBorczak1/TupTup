

namespace TupTrack.UseCases
{
    public class Application 
    {
        StartRecordingUC _startRecording;
        public Application(StartRecordingUC sr)
        {
            _startRecording = sr;
        }

        public void StartRecording() => _startRecording.StartRecording();
    }
}
