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
    public class people_historyController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.people_history>> Get()
        {
            return await d.people_history.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.people_history> Get(string id)
        {
            return await d.people_history.Get(id);
        }

        [HttpPost("single")]
        public async Task<e.people_history> Get([FromBody] e.people_historyQueryInfo obj)
        {
            return await d.people_history.Get(obj.ph_or_id, obj.visited_at);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.people_history obj)
        {
            try
            {
                e.shared.ActionResult result = await d.people_history.Save(obj);
                return Ok(result.Status);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("insertMany")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<e.people_history> obj)
        {
            try
            {
                e.shared.ActionResult result = await d.people_history.InsertMany(obj);
                return Ok(result.Status);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] e.people_historyQueryInfo obj)
        {
            try
            {
                var data = await d.people_history.Delete(obj.ph_or_id, obj.visited_at);
                if (data == null) return NotFound(obj.ph_or_id);

                e.shared.ActionResult result = await d.people_history.Delete(obj.ph_or_id, obj.visited_at);
                return Ok(result.Status);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}