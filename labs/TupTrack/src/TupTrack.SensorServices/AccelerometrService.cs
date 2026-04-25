using TupTrack.UseCases.SensorCoordinator;

namespace TupTrack.SensorServices
{
    public class AccelerometrService : ISensorService
    {
        public void Log()
        {
            System.Diagnostics.Debug.WriteLine("Accelerometr service started");
        }



        public int GetVal() => 2;
    }
    
}
