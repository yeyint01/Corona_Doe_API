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

        public static async Task<e.location_typeResult> Get(e.location_typeParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.location_typeResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = @"(name_en Like'%' + @Name + '%' OR name_mm Like '%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM location_type
                                    {condition}

                                    SELECT *
                                    FROM location_type
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "locationtype_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY",param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.location_Types = multi.Read<e.location_type>();
                }

                return result;
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
