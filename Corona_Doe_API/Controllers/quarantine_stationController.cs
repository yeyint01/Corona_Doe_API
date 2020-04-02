using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using d = DataAccess;
using e = Entity;

namespace Corona_Doe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class quarantine_stationController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.quarantine_station>> Get()
        {
            return await d.quarantine_station.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.quarantine_station> Get(int id)
        {
            return await d.quarantine_station.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.quarantine_station obj)
        {
            try
            {
                e.shared.ActionResult result = await d.quarantine_station.Insert(obj);
                obj.station_id = int.Parse(result.Value.ToString());
                return Ok(obj);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.quarantine_station obj)
        {
            try
            {
                e.shared.ActionResult result = await d.quarantine_station.Update(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var obj = await d.quarantine_station.Get(id);
                if (obj == null) return NotFound(id);

                e.shared.ActionResult result = await d.quarantine_station.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}