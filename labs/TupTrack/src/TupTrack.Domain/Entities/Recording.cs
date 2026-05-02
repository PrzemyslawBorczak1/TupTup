using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Domain.Entities;

public class Recording
{
    public Guid Id { get; private set; }
    public Guid? GroupType { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public string? Note { get; private set; } = null;




    public static Recording Create(DateTime startTime) => new Recording()
    {
        Id = Guid.NewGuid(),
        StartTime = startTime
    };

}

