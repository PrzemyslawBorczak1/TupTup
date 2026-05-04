using TupTrack.UseCases.SensorCoordinator;


namespace TupTrack.SensorServices;

public class BarometerService: SensorService<double>, ISensorService
{
    private SensorSpeed _sensorSpeed = SensorSpeed.Fastest;

    private bool isRecording = false;

    
    public BarometerService() : base() { }

    public void SetSpeed(Domain.SensorSpeed speed)
    {
        var value = Converters.ConvertDomainToServiceSpeed(speed);

        if (_sensorSpeed == value)
            return;

        _sensorSpeed = value;
        if (Barometer.IsMonitoring)
        {
            Barometer.Stop();
            Barometer.Start(_sensorSpeed);
        }

    }

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
        if (!isRecording)
            return;

        Barometer.Stop();
        Barometer.ReadingChanged -= Handler;
        isRecording = false;
    }



    public void Dispose()
    {
        Stop();
        Clear();
    }


}
