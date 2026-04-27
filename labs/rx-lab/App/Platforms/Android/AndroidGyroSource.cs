using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Android.Content;
using Android.Hardware;
using IndoorLocalization.Trl3.Core.Motion;

namespace IndoorLocalization.Trl3.Core.Gyro;

public sealed class AndroidGyroSource :
    Java.Lang.Object,
    IGyroSource,
    ISensorEventListener,
    IDisposable
{
    private readonly SensorManager _sensorManager;
    private readonly Sensor? _gyroSensor;

    private readonly Subject<double> _omegaZ = new();

    public IObservable<double> OmegaZ => _omegaZ.AsObservable();

    private bool _started;
    private volatile bool _disposed;

    public AndroidGyroSource(Context context)
    {
        _sensorManager =
            (SensorManager)context.GetSystemService(Context.SensorService)!;

        _gyroSensor =
            _sensorManager.GetDefaultSensor(SensorType.Gyroscope);
    }

    public void Start()
    {
        if (_started)
            return;

        if (_gyroSensor == null)
            throw new InvalidOperationException("Gyroscope not available.");

        _sensorManager.RegisterListener(
            this,
            _gyroSensor,
            SensorDelay.Game); // ~50 Hz

        _started = true;
    }

    public void Stop()
    {
        if (!_started)
            return;

        _sensorManager.UnregisterListener(this);
        _started = false;
    }

    public void OnSensorChanged(SensorEvent? e)
    {
        if (_disposed) return;
        if (e?.Sensor?.Type != SensorType.Gyroscope)
            return;

        // values[2] = omega around Z axis (rad/s)
        var omegaZ = e.Values[2];

        _omegaZ.OnNext(omegaZ);
    }

    public void OnAccuracyChanged(Sensor? sensor, SensorStatus accuracy)
    {
        // optional: możesz reagować na Unreliable
    }

    public new void Dispose()
    {
        _disposed = true;
        Stop();
        _omegaZ.OnCompleted();
        _omegaZ.Dispose();
    }
}
