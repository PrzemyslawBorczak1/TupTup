using System;
using System.Collections.Generic;
using System.Text;

using TupTrack.Domain;

namespace TupTrack.UseCases.SensorCoordinator
{
    public interface ISensorService : IDisposable
    {
        void Start();
        void Stop();

        bool IsSupported();

        void SetSpeed(SensorSpeed speed);

    }
}
