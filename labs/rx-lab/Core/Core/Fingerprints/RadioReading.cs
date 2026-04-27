namespace Core.Core.Fingerprints;

public readonly record struct RadioReading(
    RadioType Type,
    string Id,     // BSSID lub MAC beacona
    int Rssi       // dBm
);


public enum SignalBucket
{
    Low,
    Medium,
    High
}
public static class SignalQuantizer
{
    public static SignalBucket ToBucket(int rssi)
    {
        if (rssi > -60)
            return SignalBucket.High;

        if (rssi > -75)
            return SignalBucket.Medium;

        return SignalBucket.Low;
    }
}

public enum RadioType
{
    Wifi,
    Ble
}


public readonly record struct FingerprintToken(
    RadioType Type,
    string Id,
    SignalBucket Bucket);


// Snapshot jako zbiór tokenów
public sealed record Fingerprint(
    IReadOnlySet<FingerprintToken> Tokens,
    DateTime Timestamp
);



public interface IFingerprintBuilder
{
    void AddBatch(
        RadioType type,
        IReadOnlyList<RadioReading> readings);

    Fingerprint Build();

    void Reset();
}

public sealed class FingerprintBuilder : IFingerprintBuilder
{
    private readonly int _minOccurrences;
    private readonly int _minRssi;

    // key: (RadioType, Id)
    private readonly Dictionary<(RadioType Type, string Id), List<int>> _accumulator =
        new();

    public FingerprintBuilder(
        int minOccurrences = 2,
        int minRssi = -85)
    {
        _minOccurrences = minOccurrences;
        _minRssi = minRssi;
    }

    public void AddBatch(
        RadioType type,
        IReadOnlyList<RadioReading> readings)
    {
        foreach (var r in readings)
        {
            if (r.Rssi < _minRssi)
                continue;

            var key = (type, r.Id);

            if (!_accumulator.TryGetValue(key, out var list))
            {
                list = new List<int>();
                _accumulator[key] = list;
            }

            list.Add(r.Rssi);
        }
    }

    public Fingerprint Build()
    {
        var tokens = new HashSet<FingerprintToken>();

        foreach (var kv in _accumulator)
        {
            if (kv.Value.Count < _minOccurrences)
                continue;

            var avgRssi = (int)kv.Value.Average();

            var bucket = SignalQuantizer.ToBucket(avgRssi);

            tokens.Add(new FingerprintToken(
                kv.Key.Type,
                kv.Key.Id,
                bucket));
        }

        return new Fingerprint(
            tokens,
            DateTime.UtcNow);
    }

    public void Reset()
    {
        _accumulator.Clear();
    }
}


public interface IWifiSource
{
    IObservable<IReadOnlyList<RadioReading>> ScanResults { get; }
}

public interface IBleSource
{
    IObservable<IReadOnlyList<RadioReading>> ScanResults { get; }
}