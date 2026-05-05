using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Domain.Entities
{
    public class RoomTimestamp
    {
        public Guid Id { get; set; }

        public string RoomName { get; set; }
        public Guid RecordingId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Description { get; set; } = null;


        public RoomTimestamp(string roomName, Guid recordingId, DateTime timestamp, string? description = null)
        {
            Id = Guid.NewGuid();
            RoomName = roomName;
            RecordingId = recordingId;
            Timestamp = timestamp;
            Description = description;
        }
    }
}
