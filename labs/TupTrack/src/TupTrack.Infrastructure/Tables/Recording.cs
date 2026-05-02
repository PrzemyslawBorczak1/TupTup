using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TupTrack.Infrastructure.Tables;


public class Recording
{
    [PrimaryKey]
    public Guid Id { get;  set; }
    public Guid? GroupType { get;  set; }
    public DateTime StartTime { get;  set; }
    public DateTime? EndTime { get;  set; }
    public string? Note { get;  set; } = null;

}

