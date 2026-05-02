using System;
using System.Collections.Generic;
using System.Text;
using TupTrack.Domain;

namespace TupTrack.Infrastructure.Records
{
    public class TupStateEntity
    {
        public Guid Id { get; set; }
        public Guid RecordingId { get; set; }
        public TupState State { get; set; }
        public DateTime FromTimestamp { get; set; }
        public string? Description { get; set; } = null;

    }
}
