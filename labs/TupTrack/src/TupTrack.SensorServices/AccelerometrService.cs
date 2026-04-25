using TupTrack.UseCases.SensorCoordinator;

namespace TupTrack.SensorServices
{
    public class AccelerometrService : SensorService<decimal>, ISensorService
    {
        public AccelerometrService() : base() { }
        public bool IsSupported()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
    
}
