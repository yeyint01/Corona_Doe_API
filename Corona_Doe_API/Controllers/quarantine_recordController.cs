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
    public class quarantine_recordController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<e.quarantine_record>> Get()
        {
            return await d.quarantine_record.Get();
        }

        [HttpGet("{id}")]
        public async Task<e.quarantine_record> Get(int id)
        {
            return await d.quarantine_record.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] e.quarantine_record obj)
        {
            try
            {
                obj.start_date = fn.GetLocalDateTime(obj.start_date);
                obj.end_date = fn.GetLocalDateTime(obj.end_date);
                obj.person_dob = fn.GetLocalDateTime(obj.person_dob);
                obj.checkout_date = fn.GetLocalDateTime(obj.checkout_date);
                obj.lab_testing_date = fn.GetLocalDateTime(obj.lab_testing_date);
                obj.result_date = fn.GetLocalDateTime(obj.result_date);

                e.shared.ActionResult result = await d.quarantine_record.Insert(obj);
                obj.quarantine_id = int.Parse(result.Value.ToString());
                return Ok(obj);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] e.quarantine_record obj)
        {
            try
            {
                obj.start_date = fn.GetLocalDateTime(obj.start_date);
                obj.end_date = fn.GetLocalDateTime(obj.end_date);
                obj.person_dob = fn.GetLocalDateTime(obj.person_dob);
                obj.checkout_date = fn.GetLocalDateTime(obj.checkout_date);
                obj.lab_testing_date = fn.GetLocalDateTime(obj.lab_testing_date);
                obj.result_date = fn.GetLocalDateTime(obj.result_date);

                e.shared.ActionResult result = await d.quarantine_record.Update(obj);
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
                var obj = await d.quarantine_record.Get(id);
                if (obj == null) return NotFound(id);

                e.shared.ActionResult result = await d.quarantine_record.Delete(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}