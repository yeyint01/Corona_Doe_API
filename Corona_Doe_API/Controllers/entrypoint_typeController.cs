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
    public class entrypoint_typeController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.entrypoint_type>> Get()
        {
            return await d.entrypoint_type.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.entrypoint_type> Get(int id)
        {
            return await d.entrypoint_type.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.entrypoint_type obj)
        {
            try
            {
                e.shared.ActionResult result = await d.entrypoint_type.Insert(obj);
                obj.entrypoint_type_id = int.Parse(result.Value.ToString());
                return Ok(obj);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.entrypoint_type obj)
        {
            try
            {
                e.shared.ActionResult result = await d.entrypoint_type.Update(obj);
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
                var obj = await d.entrypoint_type.Get(id);
                if (obj == null) return NotFound(id);

                e.shared.ActionResult result = await d.entrypoint_type.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}