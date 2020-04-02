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
    public class location_type
    {
        public static async Task<IEnumerable<e.location_type>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.location_type>(d.SelectAll<e.location_type>());
            }
        }

        public static async Task<e.location_type> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.location_type>(d.Select<e.location_type>(),
                     new { locationtype_id = id });
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.location_type obj)
        {
            using (var db = d.ConnectionFactory())
            {
                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.location_type>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.location_type obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Update<e.location_type>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.location_type>(),
                    new { locationtype_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
