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
    public class entrypoint_type
    {
        public static async Task<IEnumerable<e.entrypoint_type>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.entrypoint_type>(d.SelectAll<e.entrypoint_type>());
            }
        }

        public static async Task<e.entrypoint_type> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.entrypoint_type>(d.Select<e.entrypoint_type>(),
                     new { entrypoint_type_id = id });
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.entrypoint_type obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.entrypoint_type>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.entrypoint_type obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.entrypoint_type>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.entrypoint_type>(),
                    new { entrypoint_type_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
