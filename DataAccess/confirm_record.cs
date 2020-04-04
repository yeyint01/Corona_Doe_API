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
    public class confirm_record
    {
        public static async Task<IEnumerable<e.confirm_record>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.confirm_record>(d.SelectAll<e.confirm_record>());
            }
        }

        public static async Task<e.confirm_record> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.confirm_record>(d.Select<e.confirm_record>(),
                     new { patient_id = id });
            }
        }

        public static async Task<e.confirm_recordResult> Get(e.confirm_recordParam param)
        {            
            using (var db = d.ConnectionFactory())
            {
                var result = new e.confirm_recordResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = @"(patient_name Like'%' + @Name + '%' OR patient_nrc Like '%' + @Name + '%' " +
                                 "OR patient_ph Like'%' + @Name + '%' OR patient_age Like '%' + @Name + '%' OR " +
                                 "gender Like'%' + @Name + '%' OR hometown Like'%' + @Name + '%')";
               
                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM confirm_record
                                    {condition}

                                    SELECT *
                                    FROM confirm_record
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "patient_id"} {( param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Confirm_Records = multi.Read<e.confirm_record>();
                }

                return result;
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.confirm_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.confirm_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.confirm_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.confirm_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.confirm_record>(),
                    new { patient_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
