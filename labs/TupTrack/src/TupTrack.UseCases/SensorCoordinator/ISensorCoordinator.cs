using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.UseCases.SensorCoordinator
{
    public interface ISensorCoordinator
    {
        void Start();
        void Stop();
        void Clear();


        void Check();
    }
}
