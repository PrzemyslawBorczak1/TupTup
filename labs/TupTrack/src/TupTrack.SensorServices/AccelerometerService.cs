using TupTrack.UseCases.SensorCoordinator;

namespace TupTrack.SensorServices
{
    public class AccelerometerService : SensorService<System.Numerics.Vector3>, ISensorService
    {

        private SensorSpeed _sensorSpeed = SensorSpeed.Fastest;
        private bool isRecording = false;
        public SensorSpeed SensorSpeed
        {
            get => _sensorSpeed;
            set
            {

                if (_sensorSpeed == value)
                    return;

                _sensorSpeed = value;
                if (Accelerometer.IsMonitoring)
                {
                    Accelerometer.Stop();
                    Accelerometer.Start(_sensorSpeed);
                }
            }

        }
        public AccelerometerService() : base() { }
        public bool IsSupported() => Accelerometer.IsSupported;

        public void Start()
        {

            if (isRecording)
                return;

            Clear();

            Accelerometer.ReadingChanged += Handler;
            Accelerometer.Start(_sensorSpeed);

            isRecording = true;
        }
        private void Handler(object? sender, AccelerometerChangedEventArgs arg)
        {
            Add(arg.Reading.Acceleration);
        }

        public void Stop()
        {

            if (!isRecording)
                return;

            Accelerometer.Stop();
            Accelerometer.ReadingChanged -= Handler;
            isRecording = false;
        }

        public void Dispose()
        {
            Stop();
            Clear();
        }

    }
    
}
