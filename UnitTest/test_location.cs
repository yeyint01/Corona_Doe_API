using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class test_location
    {
        [TestMethod]
        public void CRUD()
        {
            e.location obj = new e.location()
            {
                name_en = "testing",
                name_mm = "testing",               
                locationtype_id = null
            };

            //Create
            e.shared.ActionResult insert_result = d.location.Insert(obj).Result;

            //Read
            obj = d.location.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.location> objs = d.location.Get().Result;

            //Update
            obj.name_en = "testing update";
            e.shared.ActionResult update_result = d.location.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.location.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
