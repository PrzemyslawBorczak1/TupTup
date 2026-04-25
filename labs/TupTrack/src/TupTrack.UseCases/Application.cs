using TupTrack.UseCases.Interfaces;


namespace TupTrack.UseCases
{
    public class Application : IApplication
    {
        IStartRecordingUC _startRecording;
        public Application(IStartRecordingUC sr)
        {
            _startRecording = sr;
        }

        public void StartRecording() => _startRecording.StartRecording();
    }
}
