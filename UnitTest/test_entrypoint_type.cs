using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class test_entrypoint_type
    {
        [TestMethod]
        public void CRUD()
        {
            e.entrypoint_type obj = new e.entrypoint_type()
            {
                name_en = "testing",
                name_mm = "testing",                
                remark = "remark"
            };

            //Create
            e.shared.ActionResult insert_result = d.entrypoint_type.Insert(obj).Result;

            //Read
            obj = d.entrypoint_type.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.entrypoint_type> objs = d.entrypoint_type.Get().Result;

            //Update
            obj.name_en = "testing update";
            e.shared.ActionResult update_result = d.entrypoint_type.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.entrypoint_type.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
