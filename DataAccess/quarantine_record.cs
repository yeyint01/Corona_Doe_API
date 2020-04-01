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
    public class quarantine_record
    {
        public static async Task<IEnumerable<e.quarantine_record>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.quarantine_record>(d.SelectAll<e.quarantine_record>());
            }
        }

        public static async Task<e.quarantine_record> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.quarantine_record>(d.Select<e.quarantine_record>(),
                     new { quarantine_id = id });
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.quarantine_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Insert<e.quarantine_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.quarantine_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Update<e.quarantine_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.quarantine_record>(),
                    new { quarantine_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
