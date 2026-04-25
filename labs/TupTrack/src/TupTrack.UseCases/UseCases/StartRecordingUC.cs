using TupTrack.UseCases.SensorCoordinator;
using TupTrack.UseCases.Interfaces;

namespace TupTrack.UseCases;

public class StartRecordingUC : IStartRecordingUC
{
    ISensorCoordinator _sensorCoordinator;
    public StartRecordingUC(ISensorCoordinator sensorCoordinator)
    {
        _sensorCoordinator = sensorCoordinator;
    }

    public void StartRecording() {
        _sensorCoordinator.Check();
        }
}
