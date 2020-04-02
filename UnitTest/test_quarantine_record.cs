using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class test_quarantine_record
    {
        [TestMethod]
        public void CRUD()
        {
            e.quarantine_record obj = new e.quarantine_record()
            {
                station_id = null,
                start_date = DateTime.Now,
                end_date = DateTime.Now,
                person_name = "test name",
                person_nrc = "test nrc",
                person_ph = "test phone",
                person_age = "0",
                person_dob = DateTime.Now,
                gender = "male",
                hometown = "test home town",
                reason_id = null,
                travel_history = "test history",
                residence_address = "residence address",
                current_address = "current address",
                traveled_from = "travel from",
                fever_history = "fever history",
                remark = "test remark",
                checkout_date = DateTime.Now,
                result = "test result",
                checkedout = false
            };

            //Create
            e.shared.ActionResult insert_result = d.quarantine_record.Insert(obj).Result;

            //Read
            obj = d.quarantine_record.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.quarantine_record> objs = d.quarantine_record.Get().Result;

            //Update
            obj.person_name = "testing update";
            e.shared.ActionResult update_result = d.quarantine_record.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.quarantine_record.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
