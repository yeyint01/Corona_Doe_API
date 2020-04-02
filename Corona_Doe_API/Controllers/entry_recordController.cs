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
    public class entry_recordController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.entry_record>> Get()
        {
            return await d.entry_record.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.entry_record> Get(int id)
        {
            return await d.entry_record.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.entry_record obj)
        {
            try
            {
                e.shared.ActionResult result = await d.entry_record.Insert(obj);
                obj.id = int.Parse(result.Value.ToString());
                return Ok(obj);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.entry_record obj)
        {
            try
            {
                e.shared.ActionResult result = await d.entry_record.Update(obj);
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
                var obj = await d.entry_record.Get(id);
                if (obj == null) return NotFound(id);

                e.shared.ActionResult result = await d.entry_record.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}