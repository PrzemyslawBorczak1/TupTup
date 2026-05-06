using System;
using System.Collections.Generic;
using System.Text;
using TupTrack.Domain;

namespace TupTrack.UseCases.DTOs
{
    public class RecordingSummaryDTO
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public RecordingState State { get; set; }
        public DateTime? EndTime { get; set; }
        public string? GroupName { get; set; }

    }
}
