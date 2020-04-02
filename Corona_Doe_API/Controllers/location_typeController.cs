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
    public class location_typeController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.location_type>> Get()
        {
            return await d.location_type.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.location_type> Get(int id)
        {
            return await d.location_type.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.location_type obj)
        {
            try
            {
                e.shared.ActionResult result = await d.location_type.Insert(obj);
                obj.locationtype_id = int.Parse(result.Value.ToString());
                return Ok(obj);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.location_type obj)
        {
            try
            {
                e.shared.ActionResult result = await d.location_type.Update(obj);
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
                var obj = await d.location_type.Get(id);
                if (obj == null) return NotFound(id);

                e.shared.ActionResult result = await d.location_type.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}