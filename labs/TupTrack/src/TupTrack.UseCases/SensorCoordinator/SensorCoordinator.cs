using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TupTrack.UseCases.SensorCoordinator
{
    public class SensorCoordinator : ISensorCoordinator
    {
        private readonly List<ISensorService> _services;

        public SensorCoordinator(IEnumerable<ISensorService> services)
        {
            _services = services.Where(s => s.IsSupported()).ToList();
        }
      

        public void Start() { 
            foreach(var service in _services)
                service.Start();
            
        }

        public void Stop() { 
            foreach(var service in _services)
                service.Stop();
            
        }

        public void Dispose()
        {
            foreach(var service in _services)
            {
                service.Dispose();
            }
        }
    }
}
