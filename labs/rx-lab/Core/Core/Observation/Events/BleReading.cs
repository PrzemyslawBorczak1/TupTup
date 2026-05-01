namespace Core.Core.Observation.Events;

public sealed record BleReading(
    string BeaconHash,
    int Rssi
);