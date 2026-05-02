using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Infrastructure.Records
{
    public class LabelType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = null;
    }
}
