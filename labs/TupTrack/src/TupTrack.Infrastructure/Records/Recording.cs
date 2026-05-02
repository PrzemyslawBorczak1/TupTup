using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Infrastructure.Records
{
    public class Recording
    {
        public Guid Id { get; set; }
        public Guid? GroupType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Note { get; set; } = null;
    }
}
