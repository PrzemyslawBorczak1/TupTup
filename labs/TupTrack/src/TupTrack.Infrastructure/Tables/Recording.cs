using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TupTrack.Domain;

namespace TupTrack.Infrastructure.Tables;


public class Recording
{
    [PrimaryKey]
    public Guid Id { get;  set; }
    public Guid? GroupType { get;  set; }
    public DateTime StartTime { get;  set; }
    public DateTime? EndTime { get;  set; }
    public string? Note { get;  set; } 
    public RecordingState State { get;  set; }

}

