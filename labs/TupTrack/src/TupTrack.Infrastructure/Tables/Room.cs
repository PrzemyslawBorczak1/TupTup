using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Infrastructure.Tables
{
    public class Room
    {
        [SQLite.PrimaryKey]
        public string Name { get; set; }
        public string? Description { get; set; } = null;
    }
}
