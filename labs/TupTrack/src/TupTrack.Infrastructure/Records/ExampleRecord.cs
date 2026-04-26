
using SQLite;

namespace TupTrack.Infrastructure.Records
{
    public class ExampleRecord
    {
        [PrimaryKey]
        public Guid Id { get; set; }
    }
}
