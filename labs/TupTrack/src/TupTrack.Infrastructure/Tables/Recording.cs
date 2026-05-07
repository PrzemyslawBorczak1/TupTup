using SQLite;

using TupTrack.Domain;

namespace TupTrack.Infrastructure.Tables;


public class Recording
{
    [PrimaryKey]
    public Guid Id { get;  set; }
    public string? GroupName { get;  set; }
    public DateTime StartTime { get;  set; }
    public DateTime? EndTime { get;  set; }
    public string? Note { get;  set; } 
    public RecordingState State { get;  set; }

    public Recording() { }
    public Recording(Domain.Entities.Recording recording)
    {
        Id = recording.Id;
        GroupName = recording.GroupName;
        StartTime = recording.StartTime;
        EndTime = recording.EndTime;
        Note = recording.Note;
        State = recording.State;
    }



}
