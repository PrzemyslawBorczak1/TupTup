
using TupTrack.Domain;
namespace TupTrack.UseCases.SensorCoordinator
{
    public interface ISensorCoordinator : IDisposable
    {
        void Start();
        void Stop();
        void SetSpeed(SensorSpeed speed);

    }
}
