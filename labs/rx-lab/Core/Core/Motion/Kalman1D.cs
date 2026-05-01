using System.Reactive.Linq;

namespace IndoorLocalization.Trl3.Core.Motion;

public sealed class Kalman1D
{
    public double Omega { get; private set; }
    public double Bias { get; private set; }

    private double p11 = 1, p12 = 0, p21 = 0, p22 = 1;
    private readonly double qOmega = 0.01;
    private readonly double qBias = 0.0001;
    private readonly double r = 0.1;

    public double Update(double measurement)
    {
        // prediction
        p11 += qOmega;
        p22 += qBias;

        // innovation
        var y = measurement - (Omega + Bias);
        var s = p11 + p22 + r;

        var k1 = p11 / s;
        var k2 = p22 / s;

        // update
        Omega += k1 * y;
        Bias  += k2 * y;

        // covariance update
        p11 *= (1 - k1);
        p22 *= (1 - k2);

        return Omega;
    }
}

public static class GyroExtensions
{
    public static IObservable<double> ApplyKalman(
        this IObservable<double> source,
        Kalman1D kalman) => source.Select(kalman.Update);
}