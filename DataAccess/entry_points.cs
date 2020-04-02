using System;
using System.Collections.Generic;
using d = DataAccess.shared.DbAccess;
using v = DataAccess.shared.Variables;
using e = Entity;
using System.Threading.Tasks;
using Dapper;

namespace DataAccess
{
    public class entry_points
    {
        public static async Task<IEnumerable<e.entry_points>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.entry_points>(d.SelectAll<e.entry_points>());
            }
        }

        public static async Task<e.entry_points> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.entry_points>(d.Select<e.entry_points>(),
                     new { entrypoint_id = id });
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.entry_points obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.entry_points>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.entry_points obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.entry_points>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.entry_points>(),
                    new { entrypoint_id = id});

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
