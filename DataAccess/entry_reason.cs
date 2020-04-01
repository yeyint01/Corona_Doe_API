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
    public class entry_reason
    {
        public static async Task<IEnumerable<e.entry_reason>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.entry_reason>(d.SelectAll<e.entry_reason>());
            }
        }

        public static async Task<e.entry_reason> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.entry_reason>(d.Select<e.entry_reason>(),
                     new { reason_id = id });
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.entry_reason obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Insert<e.entry_reason>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.entry_reason obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Update<e.entry_reason>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.entry_reason>(),
                    new { reason_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
