
using Domain = TupTrack.Domain;
namespace TupTrack.SensorServices;

public static class Converters
{
    public static SensorSpeed ConvertDomainToServiceSpeed(Domain.SensorSpeed speed) => speed switch
    {
        Domain.SensorSpeed.Default => SensorSpeed.Default,
        Domain.SensorSpeed.Slow => SensorSpeed.UI,
        Domain.SensorSpeed.Medium => SensorSpeed.Game,
        Domain.SensorSpeed.Fast => SensorSpeed.Fastest,
        _ => SensorSpeed.Default
    };
};
