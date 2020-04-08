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
    public class quarantine_station
    {
        public static async Task<IEnumerable<e.quarantine_station>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.quarantine_station>(d.SelectAll<e.quarantine_station>());
            }
        }

        public static async Task<e.quarantine_station> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.quarantine_station>(d.Select<e.quarantine_station>(),
                     new { station_id = id });
            }
        }

        public static async Task<e.quarantine_stationResult> Get(e.quarantine_stationParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.quarantine_stationResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = "(name_en Like'%' + @Name + '%' OR name_mm Like '%' + @Name + '%' OR description Like'%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM quarantine_station
                                    {condition}

                                    SELECT *
                                    FROM quarantine_station
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "station_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Quarantine_Stations = multi.Read<e.quarantine_station>();
                }

                return result;
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.quarantine_station obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.quarantine_station>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.quarantine_station obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.quarantine_station>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.quarantine_station>(),
                    new { station_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
