using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using d = DataAccess;
using e = Entity;
using fn = DataAccess.shared.Functions;

namespace Corona_Doe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class featuresController : ControllerBase
    {
        [HttpGet("{rnd}")]
        public async Task<object> Get(string rnd)
        {
            var fts = await d.features.Get();

            return new
            { 
                type = "FeatureCollection",
                properties = new { exceededTransferLimit = true },
                features = fts.Select(f => new
                { 
                    type = f.type,
                    id = f.feature_id,
                    geometry = new
                    {
                        type = f.geometrytype,
                        coordinates = new[] { f.coordinatesx, f.coordinatesy },
                    },
                    properties = new
                    { 
                        OBJECTID = f.feature_id,
                        Name = f.name,
                        Place = f.place,
                        Comments = f.comments,
                        POINT_X = f.pointx,
                        POINT_Y = f.pointy,
                        fromTime = f.fromtime,
                        toTime = f.totime,
                        sourceOID = f.sourceoid,
                        stayTimes = fn.GetStayTimeString(f.fromtime, f.totime)
                    }
                })
            };
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
