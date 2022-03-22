using Microsoft.AspNetCore.Mvc;
using PIA.DotNet.Interview.Backend.Service;
using PIA.DotNet.Interview.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIA.DotNet.Interview.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementValuesController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementValuesController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        // GET: api/GetMeasurements/
        [HttpGet("[action]")]
        public async Task<List<Measurement>> GetMeasurements()
        {
            return await _measurementService.GetMeasurementDataList();
        }

        // GET api/<GetMeasurement>/eaa8ece6-f020-4880-9651-6e0e8ac7bb4e
        [HttpGet("[action]/{id}")]
        public async Task<Measurement> GetMeasurement(string id)
        {
            return await _measurementService.GetMeasurement(id);
        }

        // POST api/SaveMeasurement
        [HttpPost("[action]")]
        public async Task<bool> SaveMeasurement([FromBody] Measurement value)
        {
            return await _measurementService.SaveMeasurement(value);
        }

        // POST api/Setup
        [HttpPost("[action]")]
        public async System.Threading.Tasks.Task Setup()
        {
            await _measurementService.SetupData();
        }

        // POST api/DeleteMeasurement
        [HttpPut("[action]")]
        public async Task<bool> DeleteMeasurement(string id)
        {
            return await _measurementService.DeleteMeasurement(id);
        }
    }
}
