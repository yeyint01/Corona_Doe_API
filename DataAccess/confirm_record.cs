using System;
using System.Collections.Generic;
using System.Text;
using d = DataAccess.shared.DbAccess;
using v = DataAccess.shared.Variables;
using e = Entity;
using System.Threading.Tasks;
using Dapper;


namespace DataAccess
{
    public class confirm_record
    {
        public static async Task<IEnumerable<e.confirm_record>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.confirm_record>(d.SelectAll<e.confirm_record>());
            }
        }

        public static async Task<e.confirm_record> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.confirm_record>(d.Select<e.confirm_record>(),
                     new { patient_id = id });
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.confirm_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.confirm_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.confirm_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.confirm_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.confirm_record>(),
                    new { patient_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
