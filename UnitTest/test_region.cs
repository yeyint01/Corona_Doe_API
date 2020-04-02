using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class test_region
    {
        [TestMethod]
        public void CRUD()
        {
            e.region obj = new e.region()
            {
                name_en = "testing",
                name_mm = "testing",
            };

            //Create
            e.shared.ActionResult insert_result = d.region.Insert(obj).Result;

            //Read
            obj = d.region.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.region> objs = d.region.Get().Result;

            //Update
            obj.name_en = "testing update";
            e.shared.ActionResult update_result = d.region.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.region.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
