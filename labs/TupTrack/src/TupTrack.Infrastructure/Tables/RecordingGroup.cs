using System;
using System.Collections.Generic;
using System.Text;


namespace TupTrack.Infrastructure.Tables
{
    public class RecordingGroup
    {
        [SQLite.PrimaryKey]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = null;
    }
}
