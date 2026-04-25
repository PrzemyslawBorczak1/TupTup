using System;
using System.Collections.Generic;
using System.Text;

namespace TupTrack.UseCases.SensorCoordinator
{
    public class SensorCoordinator : ISensorCoordinator
    {
        IEnumerable<ISensorService> _services;

        public SensorCoordinator(IEnumerable<ISensorService> services)
        {
            _services = services;

        }
        public void Check()
        {
            foreach(var s in _services)
            {
               
            }
        }

        public void Start() { throw new NotImplementedException(); }
        public void Stop() { throw new NotImplementedException(); }
        public void Clear() { throw new NotImplementedException(); }
    }
}
