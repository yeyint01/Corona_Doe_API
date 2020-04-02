using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class test_location_type
    {
        [TestMethod]
        public void CRUD()
        {
            e.location_type obj = new e.location_type()
            {
                name_en = "testing",
                name_mm = "testing"                
            };

            //Create
            e.shared.ActionResult insert_result = d.location_type.Insert(obj).Result;

            //Read
            obj = d.location_type.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.location_type> objs = d.location_type.Get().Result;

            //Update
            obj.name_en = "testing update";
            e.shared.ActionResult update_result = d.location_type.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.location_type.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
