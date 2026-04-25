using TupTrack.UseCases.SensorCoordinator;

namespace TupTrack.UseCases;

public class StartRecordingUC
{
    ISensorCoordinator _sensorCoordinator;
    public StartRecordingUC(ISensorCoordinator sensorCoordinator)
    {
        _sensorCoordinator = sensorCoordinator;
    }

    public void StartRecording() {
        _sensorCoordinator.Start();
    }
}
