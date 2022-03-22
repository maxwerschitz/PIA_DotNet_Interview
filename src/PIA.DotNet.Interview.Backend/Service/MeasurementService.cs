using PIA.DotNet.Interview.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace PIA.DotNet.Interview.Backend.Service
{
    public class MeasurementService : IMeasurementService
    {
        private ObjectCache _cache;

        public MeasurementService()
        {
            _cache = System.Runtime.Caching.MemoryCache.Default;
        }

        public async Task<bool> DeleteMeasurement(string id)
        {
            var result = _cache.Remove(id);

            return result != null ? await System.Threading.Tasks.Task.FromResult<Boolean>(true) : await System.Threading.Tasks.Task.FromResult<Boolean>(false);
        }

        public async Task<Measurement> GetMeasurement(string id)
        {
            var result = _cache.Get(Guid.Parse(id).ToString());

            return result != null ? 
                await System.Threading.Tasks.Task.FromResult<Measurement>((Measurement)result) : 
                await System.Threading.Tasks.Task.FromResult<Measurement>((Measurement)result);
        }

        public async Task<List<Measurement>> GetMeasurementDataList()
        {
            var result = _cache.Where(x => x.Value != null).ToList();
            var resultList = new List<Measurement>();
            foreach (var item in result)
            {
                var measurementItem = item.Value as Measurement;

                resultList.Add(new Measurement()
                {
                    Id = measurementItem.Id,
                    Name = measurementItem.Name,
                    Value = measurementItem.Value
                });
            }

            return result != null ?
                await System.Threading.Tasks.Task.FromResult<List<Measurement>>(resultList) :
                await System.Threading.Tasks.Task.FromResult<List<Measurement>>(resultList);
        }

        public async Task<bool> SaveMeasurement(Measurement measurement)
        {
            return await System.Threading.Tasks.Task.FromResult<bool>(_cache.Add(measurement.Id.ToString(), measurement, null));
        }

        public System.Threading.Tasks.Task SetupData()
        {
            var rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                var measurement = new Measurement();
                measurement.Id = Guid.NewGuid();
                measurement.Name = "Messwert_" + i;
                measurement.Value = rnd.Next();

                _cache.Add(measurement.Id.ToString(), measurement, null);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
