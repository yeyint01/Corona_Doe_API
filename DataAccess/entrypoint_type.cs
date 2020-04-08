using Dapper;
using System;
using e = Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using d = DataAccess.shared.DbAccess;
using v = DataAccess.shared.Variables;
using func = DataAccess.shared.Functions;

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

        public static async Task<e.entrypoint_typeResult> Get(e.entrypoint_typeParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.entrypoint_typeResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = "(name_en Like'%' + @Name + '%' OR name_mm Like '%' + @Name + '%' OR remark Like'%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM entrypoint_type
                                    {condition}

                                    SELECT *
                                    FROM entrypoint_type
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "entrypoint_type_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Entrypoint_Types = multi.Read<e.entrypoint_type>();
                }

                return result;
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
