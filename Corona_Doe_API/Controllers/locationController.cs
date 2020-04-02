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
    public class locationController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.location>> Get()
        {
            return await d.location.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.location> Get(int id)
        {
            return await d.location.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.location obj)
        {
            try
            {
                e.shared.ActionResult result = await d.location.Insert(obj);
                obj.location_id = int.Parse(result.Value.ToString());
                return Ok(obj);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.location obj)
        {
            try
            {
                e.shared.ActionResult result = await d.location.Update(obj);
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
                var obj = await d.location.Get(id);
                if (obj == null) return NotFound(id);

                e.shared.ActionResult result = await d.location.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}