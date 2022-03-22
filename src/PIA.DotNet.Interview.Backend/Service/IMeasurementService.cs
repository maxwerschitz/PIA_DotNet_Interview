using PIA.DotNet.Interview.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIA.DotNet.Interview.Backend.Service
{
    public interface IMeasurementService
    {
        public System.Threading.Tasks.Task SetupData();
        public Task<List<Measurement>> GetMeasurementDataList();
        public Task<bool> SaveMeasurement(Measurement measurement);
        public Task<Measurement> GetMeasurement(string id);
        public Task<bool> DeleteMeasurement(string id);
    }
}
