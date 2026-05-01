namespace IndoorLocalization.Trl3.Core.Motion;

public static class MathExtensions
{
    public static double Variance(this IEnumerable<double> values)
    {
        var data = values.ToArray();
        if (data.Length == 0)
            return 0;

        var mean = data.Average();
        return data
            .Select(x => (x - mean) * (x - mean))
            .Average();
    }
}