namespace Core.Core.Observation;

// RawEventType defines observable physical facts only.
// Any spatial or semantic meaning is derived in later phases.
public enum RawEventType
{
    /// <summary>
    /// Physical motion has started (energy above threshold).
    /// No direction, no axis, no spatial meaning.
    /// </summary>
    MotionStart,

    /// <summary>
    /// Physical motion has ended (energy below threshold).
    /// Duration is derived later.
    /// </summary>
    MotionEnd,

    /// <summary>
    /// Change in orientation (heading delta).
    /// Interpreted later as turn / alignment.
    /// </summary>
    OrientationChange,

    /// <summary>
    /// Change in atmospheric pressure.
    /// Interpreted later as vertical movement.
    /// </summary>
    PressureChange,

    /// <summary>
    /// Snapshot of BLE radio environment.
    /// Used later for fingerprinting and context.
    /// </summary>
    BleScan
}