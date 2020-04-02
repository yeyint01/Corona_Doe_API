using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class test_quarantine_station
    {
        [TestMethod]
        public void CRUD()
        {
            e.quarantine_station obj = new e.quarantine_station()
            {
                name_en = "testing",
                name_mm = "testing",
                description = "",
                location_id = null,
                capacity = 1,
                discontinued = false,
                discontinued_date = DateTime.Now
            };

            //Create
            e.shared.ActionResult insert_result = d.quarantine_station.Insert(obj).Result;

            //Read
            obj = d.quarantine_station.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.quarantine_station> objs = d.quarantine_station.Get().Result;

            //Update
            obj.name_en = "testing update";
            e.shared.ActionResult update_result = d.quarantine_station.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.quarantine_station.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
