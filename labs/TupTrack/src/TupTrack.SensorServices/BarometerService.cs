using TupTrack.UseCases.SensorCoordinator;


namespace TupTrack.SensorServices;

public class BarometerService : ISensorService
{

    public void Log()
    {
        System.Diagnostics.Debug.WriteLine("Barometer service started");
    }


    public int GetVal() => 1;
}
