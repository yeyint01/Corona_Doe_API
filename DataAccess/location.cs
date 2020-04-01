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
    public class location
    {
        public static async Task<IEnumerable<e.location>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.location>(d.SelectAll<e.location>());
            }
        }

        public static async Task<e.location> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.location>(d.Select<e.location>(),
                     new { location_id = id });
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.location obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Insert<e.location>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.location obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Update<e.location>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.location>(),
                    new { location_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
