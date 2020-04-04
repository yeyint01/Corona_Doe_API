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
    public class region
    {
        public static async Task<IEnumerable<e.region>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.region>(d.SelectAll<e.region>());
            }
        }

        public static async Task<e.region> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.region>(d.Select<e.region>(),
                     new { region_id = id });
            }
        }

        public static async Task<e.regionResult> Get(e.regionParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.regionResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = @"(name_en Like'%' + @Name + '%' OR name_mm Like '%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM region
                                    {condition}

                                    SELECT *
                                    FROM region
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "region_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Regions = multi.Read<e.region>();
                }

                return result;
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.region obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.region>(), obj);
                
                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.region obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.region>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.region>(),
                    new { region_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
