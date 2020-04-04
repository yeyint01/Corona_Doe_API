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
    public class quarantine_record
    {
        public static async Task<IEnumerable<e.quarantine_record>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.quarantine_record>(d.SelectAll<e.quarantine_record>());
            }
        }

        public static async Task<e.quarantine_record> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.quarantine_record>(d.Select<e.quarantine_record>(),
                     new { quarantine_id = id });
            }
        }
        
        public static async Task<e.quarantine_recordResult> Get(e.quarantine_recordParam param)
        {
            //e.quarantine_recordResult quarantineResult;
            using (var db = d.ConnectionFactory())
            {
                var result = new e.quarantine_recordResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = @"(person_name Like'%' + @Name + '%' OR person_nrc Like '%' + @Name + '%' " +
                                 "OR person_ph Like'%' + @Name + '%' OR person_age Like '%' + @Name + '%' OR " +
                                 "gender Like'%' + @Name + '%' OR hometown Like'%' + @Name + '%')";

                //if (condition.EndsWith(" AND "))
                //    condition = condition.Remove(condition.LastIndexOf(" AND "));
                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM quarantine_record
                                    {condition}

                                    SELECT *
                                    FROM quarantine_record
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "quarantine_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY",param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Quarantine_Records = multi.Read<e.quarantine_record>();
                }

                return result;
            }            
        }

        public static async Task<e.shared.ActionResult> Insert(e.quarantine_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.quarantine_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.quarantine_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.quarantine_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.quarantine_record>(),
                    new { quarantine_id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
