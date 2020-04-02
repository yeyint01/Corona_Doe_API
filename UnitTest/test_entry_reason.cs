using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class test_entry_reason
    {
        [TestMethod]
        public void CRUD()
        {
            e.entry_reason obj = new e.entry_reason()
            {
                name_en = "testing",
                name_mm = "testing",
                description = "description"
            };

            //Create
            e.shared.ActionResult insert_result = d.entry_reason.Insert(obj).Result;

            //Read
            obj = d.entry_reason.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.entry_reason> objs = d.entry_reason.Get().Result;

            //Update
            obj.name_en = "testing update";
            e.shared.ActionResult update_result = d.entry_reason.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.entry_reason.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
