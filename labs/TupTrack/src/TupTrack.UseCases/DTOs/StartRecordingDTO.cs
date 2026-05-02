using System;
using System.Collections.Generic;
using System.Text;
using TupTrack.Domain;

namespace TupTrack.UseCases.DTOs
{
    public class StartRecordingDTO
    {
        public DateTime StartTime { get; set; }
        public TupState FirstTupState { get; set; }
    }
}
