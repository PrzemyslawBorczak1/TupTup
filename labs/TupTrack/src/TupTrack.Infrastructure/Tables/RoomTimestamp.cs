using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Infrastructure.Tables
{
    public class RoomTimestamp
    {
        [SQLite.PrimaryKey] 
        public Guid Id { get; set; }
        
        public Guid RoomId { get; set; }
        public Guid RecordingId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Description { get; set; } = null;

    }
}
