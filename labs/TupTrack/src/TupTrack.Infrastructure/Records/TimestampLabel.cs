using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Infrastructure.Records
{
    public class TimestampLabel
    {
        public Guid Id { get; set; }
        public Guid? RecordingId { get; set; }
        public Guid? LabelTypeId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Note { get; set; }
    }
}
