namespace TupTrack.Domain;

public interface IBarometerReader
{
    double GetPressureInHectopascals();
}

public readonly record struct BarometerReading(double PressureInHectopascals, DateTimeOffset CapturedAtUtc);

public class Class1
{
    public BarometerReading GetBarometerReading(IBarometerReader barometerReader)
    {
        ArgumentNullException.ThrowIfNull(barometerReader);

        var pressureInHectopascals = barometerReader.GetPressureInHectopascals();

        if (pressureInHectopascals <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pressureInHectopascals), "Pressure must be greater than zero.");
        }

        return new BarometerReading(pressureInHectopascals, DateTimeOffset.UtcNow);
    }
}
