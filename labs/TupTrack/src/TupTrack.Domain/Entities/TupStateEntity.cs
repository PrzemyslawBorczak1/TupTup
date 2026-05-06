using System;
using System.Collections.Generic;
using System.Text;
using TupTrack.Domain;

namespace TupTrack.Domain.Entities
{
    public class TupStateEntity
    {
        public Guid Id { get; private set; }
        public Guid RecordingId { get; private set; }
        public TupState State { get; private set; }
        public DateTime FromTimestamp { get; private set; }
        public string? Description { get; private set; } = null;


        public TupStateEntity(Guid recordingId, TupState state, DateTime fromTimeStamp)
        {
            Id = Guid.NewGuid();
            RecordingId = recordingId;
            State = state;
            FromTimestamp = fromTimeStamp;
        }
    }
}
