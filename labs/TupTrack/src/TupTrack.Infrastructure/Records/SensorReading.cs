using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Infrastructure.Records
{
    public class SensorReading
    {
        public Guid Id { get; set; }
        public Guid RecordingId { get; set; }
        public Guid SensorType { get; set; }
        public string SensorName { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }
}
