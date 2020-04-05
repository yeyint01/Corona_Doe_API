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
    public class featuresController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.features>> Get()
        {
            return await d.features.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.features> Get(int id)
        {
            return await d.features.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.features obj)
        {
            try
            {
                e.shared.ActionResult result = await d.features.Insert(obj);
                obj.feature_id = int.Parse(result.Value.ToString());
                return Ok(obj);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.features obj)
        {
            try
            {
                e.shared.ActionResult result = await d.features.Update(obj);
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
                var obj = await d.features.Get(id);
                if (obj == null) return NotFound(id);

                e.shared.ActionResult result = await d.features.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
