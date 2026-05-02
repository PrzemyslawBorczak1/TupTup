using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.Domain
{
    public class StartRecordingDTO
    {
        public DateTime StartTime { get; set; }
        public TupState FirstTupState { get; set; }
    }
}
