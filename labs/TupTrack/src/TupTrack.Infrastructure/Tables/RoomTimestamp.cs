using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Infrastructure.Tables
{
    public class RoomTimestamp
    {
        [SQLite.PrimaryKey]
        public Guid Id { get; set; }

        public string RoomName { get; set; }
        public Guid RecordingId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Description { get; set; } = null;

        public RoomTimestamp() { }

        public RoomTimestamp(Domain.Entities.RoomTimestamp roomTimestamp)
        {
            Id = roomTimestamp.Id;
            RoomName = roomTimestamp.RoomName;
            RecordingId = roomTimestamp.RecordingId;
            Timestamp = roomTimestamp.Timestamp;
            Description = roomTimestamp.Description;
        }

    }
}
