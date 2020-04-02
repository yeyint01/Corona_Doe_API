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
    public class regionController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.region>> Get()
        {
            return await d.region.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.region> Get(int id)
        {
            return await d.region.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.region obj)
        {
            try
            {
                e.shared.ActionResult result = await d.region.Insert(obj);
                obj.region_id = int.Parse(result.Value.ToString());
                return Ok(obj);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.region obj)
        {
            try
            {
                e.shared.ActionResult result = await d.region.Update(obj);
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
                var obj = await d.region.Get(id);
                if (obj == null) return NotFound(id);

                e.shared.ActionResult result = await d.region.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}