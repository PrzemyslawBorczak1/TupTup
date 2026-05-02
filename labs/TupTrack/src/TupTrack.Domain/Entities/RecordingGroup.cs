using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Domain.Entities
{
    public class RecordingGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = null;
    }
}
