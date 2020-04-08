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
    public class entrypoint_category
    {
        public static async Task<IEnumerable<e.entrypoint_category>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.entrypoint_category>(d.SelectAll<e.entrypoint_category>());
            }
        }

        public static async Task<e.entrypoint_category> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.entrypoint_category>(d.Select<e.entrypoint_category>(),
                     new { category_id = id });
            }
        }

        public static async Task<e.entrypoint_categoryResult> Get(e.entrypoint_categoryParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.entrypoint_categoryResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = "(name_en Like'%' + @Name + '%' OR name_mm Like '%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM entrypoint_category
                                    {condition}

                                    SELECT *
                                    FROM entrypoint_category
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "category_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Entrypoint_Categories = multi.Read<e.entrypoint_category>();
                }

                return result;
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.entrypoint_category obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.entrypoint_category>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.entrypoint_category obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.entrypoint_category>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.entrypoint_category>(),
                    new { category_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
