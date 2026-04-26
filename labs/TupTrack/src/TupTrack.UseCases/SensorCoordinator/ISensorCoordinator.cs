using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.UseCases.SensorCoordinator
{
    public interface ISensorCoordinator : IDisposable
    {
        void Start();
        void Stop();

    }
}
