using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class test_confirm_record
    {
        [TestMethod]
        public void Get()
        {
            try
            {
                var all = d.confirm_record.Get();

                var result = d.confirm_record.Get(new e.confirm_recordParam
                {
                    Name = "AA",
                    PgNo = 1
                });

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void CRUD()
        {
            e.confirm_record obj = new e.confirm_record()
            {
                quarantine_id = null,
                station_id = null,
                patient_name = "test name",
                patient_nrc = "test nrc",
                patient_ph = "test phone",
                patient_age = "test age",
                patient_dob = DateTime.Now,
                gender = "male",
                hometown = "test home town",
                reason_id = null,
                travel_history = "travel history",
                residence_address = "resident history",
                current_address = "current address",
                traveled_from = "travel from",
                fever_history = "fever history",
                remark = "Remark",
                result_date = DateTime.Now,
                result = "test result"
            };

            //Create
            e.shared.ActionResult insert_result = d.confirm_record.Insert(obj).Result;

            //Read
            obj = d.confirm_record.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.confirm_record> objs = d.confirm_record.Get().Result;

            //Update
            obj.patient_name = "testing update";
            e.shared.ActionResult update_result = d.confirm_record.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.confirm_record.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
