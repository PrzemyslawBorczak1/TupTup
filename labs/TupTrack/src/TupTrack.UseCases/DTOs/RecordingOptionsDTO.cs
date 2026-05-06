using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.UseCases.DTOs
{
    public class RecordingOptionsDTO
    {
        public required List<string> Rooms { get; set; }
        public required List<string> Groups { get; set; }
    }
}
