using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Domain.Entities
{
    public class Room
    {
        public string Name { get; set; }
        public string? Description { get; set; } = null;

        public Room(string name, string? description)
        {
            Name = name;
            Description = description;
        }
    }
}
