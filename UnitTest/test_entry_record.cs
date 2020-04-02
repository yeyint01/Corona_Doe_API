using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;
using System;

namespace UnitTest
{
    [TestClass]
    public class test_entry_record
    {
        [TestMethod]
        public void CRUD()
        {
            e.entry_record obj = new e.entry_record()
            {
                entrypoint_id = null,
                entrance_date = DateTime.Now,
                location_id = null,
                person_name = "test person",
                person_nrc = "test nrc",
                person_ph = "test phone",
                person_age = "1",
                person_dob = DateTime.Now,
                gender = "male",
                hometown = "Yangon",
                reason_id = null,
                travel_history = "test history",
                residence_address = "test address",
                current_address = "current address",
                traveled_from = "test from",
                fever_history = "test fever history",
                remark = "test remark"
            };

            //Create
            e.shared.ActionResult insert_result = d.entry_record.Insert(obj).Result;

            //Read
            obj = d.entry_record.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.entry_record> objs = d.entry_record.Get().Result;

            //Update
            obj.fever_history = "testing update";
            e.shared.ActionResult update_result = d.entry_record.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.entry_record.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
