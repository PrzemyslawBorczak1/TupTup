

namespace TupTrack.Infrastructure.Tables
{
    public class Room
    {
        [SQLite.PrimaryKey]
        public string Name { get; set; }
        public string? Description { get; set; } = null;

        public Room() { }

        public Room(Domain.Entities.Room room)
        {
            Name = room.Name;
            Description = room.Description;
        }
    }
}
