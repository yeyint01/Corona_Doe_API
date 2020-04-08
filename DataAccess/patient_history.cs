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
    public class patient_history
    {
        public static async Task<IEnumerable<e.patient_history>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.patient_history>(d.SelectAll<e.patient_history>());
            }
        }

        public static async Task<e.patient_history> Get(long id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.patient_history>(d.Select<e.patient_history>(),
                     new { id });
            }
        }
        public static async Task<e.featuresResult> Get(e.featuresParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.featuresResult();
                string condition = "";

                if (!string.IsNullOrWhiteSpace(param.Name) && param.IsMobile)
                    condition = "(name Like'%' + @Name + '%' OR place Like '%' + @Name + '%')";
                else if (!string.IsNullOrWhiteSpace(param.Name) && !param.IsMobile)
                    condition = @"(name Like'%' + @Name + '%' OR place Like '%' + @Name + '%'
                                OR comments Like'%' + @Name + '%' OR staytimes Like '%' + @Name + '%')";                

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM patient_history
                                    {condition}

                                    SELECT *
                                    FROM patient_history
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Features = multi.Read<e.patient_history>();
                }

                return result;
            }
        }

        public static async Task<IEnumerable<string>> GetName()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<string>(@"SELECT DISTINCT(name) FROM patient_history
                               WHERE patient_history.name IS NOT NULL AND LEN(patient_history.name) > 0");                    
            }
        }

        public static async Task<IEnumerable<string>> GetPlace()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<string>(@"SELECT DISTINCT(place) FROM patient_history 
                            WHERE patient_history.place IS NOT NULL AND LEN(patient_history.place) > 0");
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.patient_history obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                long id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.patient_history>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.patient_history obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.patient_history>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(long id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.patient_history>(),
                    new { id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
