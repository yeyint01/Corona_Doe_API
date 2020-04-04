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

        public static async Task<e.locationResult> Get(e.locationParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.locationResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = @"(name_en Like'%' + @Name + '%' OR name_mm Like '%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM location
                                    {condition}

                                    SELECT *
                                    FROM location
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "location_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Locations = multi.Read<e.location>();
                }

                return result;
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.location obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.location>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.location obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

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
