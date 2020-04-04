using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class test_entry_points
    {
        [TestMethod]
        public void CRUD()
        {
            e.entry_points obj = new e.entry_points()
            {
                name_en = "testing",
                name_mm = "testing",
                location_id = null,
                entrypoint_type_id = null,
                lat = 0,
                lon = 0,
                remark = "remark"
            };

            //Create
            e.shared.ActionResult insert_result = d.entry_points.Insert(obj).Result;

            //Read
            obj = d.entry_points.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.entry_points> objs = d.entry_points.Get().Result;

            //Update
            obj.name_en = "testing update";
            e.shared.ActionResult update_result = d.entry_points.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.entry_points.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
