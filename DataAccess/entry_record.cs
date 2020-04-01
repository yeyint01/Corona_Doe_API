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
    public class entry_record
    {
        public static async Task<IEnumerable<e.entry_record>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.entry_record>(d.SelectAll<e.entry_record>());
            }
        }

        public static async Task<e.entry_record> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.entry_record>(d.Select<e.entry_record>(),
                     new { id = id });
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.entry_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Insert<e.entry_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.entry_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Update<e.entry_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.entry_record>(),
                    new { id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
