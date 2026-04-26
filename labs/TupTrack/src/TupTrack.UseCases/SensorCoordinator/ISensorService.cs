using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.UseCases.SensorCoordinator
{
    public interface ISensorService : IDisposable
    {
        void Start();
        void Stop();

        bool IsSupported();

    }
}
