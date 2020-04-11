using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using d = DataAccess;
using e = Entity;
using fn = Entity.shared.GlobalClass;

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
      
        [HttpPost("single")]
        public async Task<e.people_history> Get([FromBody] e.people_historyQueryInfo param)
        {
            return await d.people_history.Get(param);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.people_history obj)
        {
            try
            {
                obj.timestamp = fn.GetLocalDateTime(obj.timestamp);
                e.shared.ActionResult result = await d.people_history.Save(obj);
                return Ok(result.Status);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("insertmany")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<e.people_history> objs)
        {
            try
            {
                foreach (var obj in objs)
                    obj.timestamp = fn.GetLocalDateTime(obj.timestamp);

                e.shared.ActionResult result = await d.people_history.InsertMany(objs);

                return Ok(result.Status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.people_history obj)
        {
            try
            {
                obj.timestamp = fn.GetLocalDateTime(obj.timestamp);
                e.shared.ActionResult result = await d.people_history.Save(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] e.people_historyQueryInfo param)
        {
            try
            {
                var data = await d.people_history.Delete(param);
                if (data == null) return NotFound(param);

                e.shared.ActionResult result = await d.people_history.Delete(param);
                return Ok(result.Status);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}