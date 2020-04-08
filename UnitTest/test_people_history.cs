using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;
using System;

namespace UnitTest
{
    [TestClass]
    public class test_people_history
    {
        [TestMethod]
        public void CRUD()
        {
            try
            {
                e.people_history people = new e.people_history()
                {
                    ph_or_id = "099999999",
                    visited_at = DateTime.Now,
                    duration = 0,
                    lat = 0,
                    lon = 0,
                    source_id = "123456"
                };

                //Save
                e.shared.ActionResult save_action = d.people_history.Save(people).Result;

               // e.shared.ActionResult insert_result = d.people_history.Insert(people).Result;

                //Read
                var obj = d.people_history.Get(people.ph_or_id, people.visited_at).Result;

                //Read all
                IEnumerable<e.people_history> objs = d.people_history.Get().Result;
               
                //Delete
                e.shared.ActionResult delete_result = d.people_history.Delete(people.ph_or_id, people.visited_at).Result;

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
