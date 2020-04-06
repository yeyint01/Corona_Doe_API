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
    public class features
    {
        public static async Task<IEnumerable<e.features>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.features>(d.SelectAll<e.features>());
            }
        }

        public static async Task<e.features> Get(long id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.features>(d.Select<e.features>(),
                     new { feature_id = id });
            }
        }
        public static async Task<e.featuresResult> Get(e.featuresParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.featuresResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = @"(name Like'%' + @Name + '%' " +
                               "OR remark Like'%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM features
                                    {condition}

                                    SELECT *
                                    FROM features
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "feature_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Features = multi.Read<e.features>();
                }

                return result;
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.features obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                long id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.features>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.features obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.features>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(long id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.features>(),
                    new { feature_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
