using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Domain.Entities;

public class Recording
{
    public Guid Id { get; private set; }
    public string? GroupName { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public string? Note { get; private set; } = null;
    public RecordingState State { get; private set; } = RecordingState.Ongoing;



    public Recording(DateTime startTime)
    {
        Id = Guid.NewGuid();
        StartTime = startTime;
    }

}

