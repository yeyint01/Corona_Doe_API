using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;
using System;

namespace UnitTest
{
    public class test_entrypoint_category
    {
        [TestMethod]
        public void CRUD()
        {
            e.entrypoint_category obj = new e.entrypoint_category()
            {
                category_id = 0,
                name_en="Englishe name",
                name_mm="Myanmar name",
                created_by="Admin",
                creation_date= DateTime.Now,
                modified_by= "Admin",
                modified_date=DateTime.Now                
            };

            //Create
            e.shared.ActionResult insert_result = d.entrypoint_category.Insert(obj).Result;

            //Read
            obj = d.entrypoint_category.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.entrypoint_category> objs = d.entrypoint_category.Get().Result;

            //Update
            obj.name_en = "testing update";
            e.shared.ActionResult update_result = d.entrypoint_category.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.entrypoint_category.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
