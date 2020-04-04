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

        public static async Task<e.entry_reasonResult> Get(e.entry_reasonParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.entry_reasonResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = @"(name_en Like'%' + @Name + '%' OR name_mm Like '%' + @Name + '%' OR description Like'%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM entry_reason
                                    {condition}

                                    SELECT *
                                    FROM entry_reason
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "reason_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Entry_Reasons = multi.Read<e.entry_reason>();
                }

                return result;
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.entry_reason obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.entry_reason>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.entry_reason obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

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
