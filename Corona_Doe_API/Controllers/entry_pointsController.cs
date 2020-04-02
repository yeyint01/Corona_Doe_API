using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using d = DataAccess;
using e = Entity;
namespace Corona_Doe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class entry_pointsController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.entry_points>> Get()
        {
            return await d.entry_points.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.entry_points> Get(int id)
        {
            return await d.entry_points.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.entry_points obj)
        {
            try
            {
                e.shared.ActionResult result = await d.entry_points.Insert(obj);
                if (result.Status == e.shared.Status.Success)
                {
                    obj.entrypoint_id = int.Parse(result.Value.ToString());
                    return Ok(obj);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.entry_points obj)
        {
            try
            {
                await d.entry_points.Update(obj);

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
                await d.entry_points.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}