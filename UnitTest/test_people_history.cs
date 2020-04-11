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
                    mid = "123456",
                    mhash = "mobile hash",
                    did = "789456",
                    dhash = "device hash",
                    contact = "09789456123",
                    contacttype = "Friend",
                    duration = 14,
                    source = "source",
                    eventname = "event",
                    lat = 0,
                    lng = 0,
                    location = "Mandalay",
                    timestamp = DateTime.Now,
                    remark = "Testing"
                };                

                var param = new e.people_historyQueryInfo
                {
                    mid = "",
                    did = "0",
                    dhash = "device id",
                    mhash = "mobile di"
                };

                //Save
                 e.shared.ActionResult save_action = d.people_history.Save(people).Result;                

                //Read
                 var obj = d.people_history.Get(param).Result;

                // Paging
                var result = d.people_history.Get(new e.people_historyParam
                {
                    PgNo = 1

                }).Result;

                //Read all
                 IEnumerable<e.people_history> objs = d.people_history.Get().Result;

                //Delete
                 e.shared.ActionResult delete_result = d.people_history.Delete(param).Result;

                 Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
