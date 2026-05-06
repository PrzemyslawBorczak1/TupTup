using System;
using System.Collections.Generic;
using System.Text;
using TupTrack.Domain;
using Domain = TupTrack.Domain.Entities;

namespace TupTrack.Infrastructure.Tables
{
    public class TupStateEntity
    {
        [SQLite.PrimaryKey]
        public Guid Id { get;  set; }
        public Guid RecordingId { get;  set; }
        public TupState State { get;  set; }
        public DateTime FromTimestamp { get;  set; }
        public string? Description { get;  set; } = null;

        public TupStateEntity() { }
        public TupStateEntity(Domain.Entities.TupStateEntity tupStateEntity)
        {
            Id = tupStateEntity.Id;
            RecordingId = tupStateEntity.RecordingId;
            State = tupStateEntity.State;
            FromTimestamp = tupStateEntity.FromTimestamp;
            Description = tupStateEntity.Description;
        }

    }
}
