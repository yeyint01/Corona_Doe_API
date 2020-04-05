using Microsoft.VisualStudio.TestTools.UnitTesting;
using e = Entity;
using d = DataAccess;
using System.Collections.Generic;
using System;

namespace UnitTest
{
    public class test_user_account
    {
        [TestMethod]
        public void CRUD()
        {
            e.user_account obj = new e.user_account()
            {
                user_id = 0,
                user_name = "owner",
                full_name = "itstar",
                password = "AQAAAAEAACcQAAAAEP3BlQw/Ei+Vh6yf8ZEOzDTCN4iUf2QIaR1AW0zm3Y26Bl7eK3kj+H75yqdsaE+vFA==",
                phone_no = "098888888",
                entrypoint_user = true,
                dashboard_user = false,
                active = true,
                point_admin_right = true,
                station_id = 1,
                quarantine_user = false,
                created_by = "Admin",
                creation_date = DateTime.Now,
                entrypoint_id = 1,
                modified_by = "owner",
                modified_date = DateTime.Now,
                remark = "testing"
            };

            //Create
            e.shared.ActionResult insert_result = d.user_account.Insert(obj).Result;

            //Read
            obj = d.user_account.Get(int.Parse(insert_result.Value.ToString())).Result;

            //Read all
            IEnumerable<e.user_account> objs = d.user_account.Get().Result;

            //Update
            obj.user_name = "testing update";
            e.shared.ActionResult update_result = d.user_account.Update(obj).Result;

            //Delete
            e.shared.ActionResult delete_result = d.user_account.Delete(int.Parse(insert_result.Value.ToString())).Result;
        }
    }
}
