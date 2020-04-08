using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class patient_history
    {
        [TestMethod]
        public void CRUD()
        {
            e.patient_history obj = new e.patient_history()
            {
                type = "testing",
                coordinatesx = 34.8001090000001f,
                coordinatesy = 32.008471f,
                name = "testing name",
                place = "testing place",
                comments = "testing comments",
                pointx = 32.008471f,
                pointy = 34.800109f,
                fromtime = 1584691200000,
                totime = 1584709200000,
                sourceoid = 1,
                staytimes = "10:00 - 15:00",
                remark = "remark"
            };

            //Create
            e.shared.ActionResult insert_result = d.patient_history.Insert(obj).Result;

            //Read
            obj = d.patient_history.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.patient_history> objs = d.patient_history.Get().Result;

            //Update
            obj.name = "testing update";
            e.shared.ActionResult update_result = d.patient_history.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.patient_history.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
