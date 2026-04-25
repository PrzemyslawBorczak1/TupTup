using TupTrack.UseCases.SensorCoordinator;


namespace TupTrack.SensorServices;

public class BarometerService: SensorService<double>, ISensorService
{
    private SensorSpeed _sensorSpeed = SensorSpeed.Fastest;

    private bool isRecording = false;

    public SensorSpeed SensorSpeed
    {
        get => _sensorSpeed;
        set {

            if (_sensorSpeed == value)
                return;

            _sensorSpeed = value;
            if (Barometer.IsMonitoring)
            {
                Barometer.Stop();
                Barometer.Start(_sensorSpeed);
            }
        }

    }
    public BarometerService() : base() { }

    public bool IsSupported() => Barometer.IsSupported;

    public void Start()
    {
        if (isRecording)
            return;

        Clear();

        Barometer.ReadingChanged += Handler;
        Barometer.Start(_sensorSpeed);
        isRecording = true;
    }

    private void Handler(object? sender, BarometerChangedEventArgs arg)
    {
        Add(arg.Reading.PressureInHectopascals);
    }

    public void Stop()
    {
        Barometer.Stop();
        Barometer.ReadingChanged -= Handler;
    }



    public void Dispose()
    {
        Stop();
        Clear();
    }


}
