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
    public class entry_record
    {
        public static async Task<IEnumerable<e.entry_record>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.entry_record>(d.SelectAll<e.entry_record>());
            }
        }

        public static async Task<e.entry_record> Get(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.entry_record>(d.Select<e.entry_record>(),
                     new { id = id });
            }
        }

        public static async Task<e.entry_recordResult> Get(e.entry_recordParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.entry_recordResult();
                string condition = "";

                if (!string.IsNullOrWhiteSpace(param.Name) && param.IsMobile)
                    condition = "(person_name Like'%' + @Name + '%' OR person_age Like '%' + @Name + '%')";
                else if(!string.IsNullOrWhiteSpace(param.Name) && !param.IsMobile)
                    condition = @"(person_name Like'%' + @Name + '%' OR person_age Like '%' + @Name + '%'
                                OR current_address Like'%' + @Name + '%' OR travel_history Like '%' + @Name + '%' 
                                OR remark Like'%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM entry_record
                                    {condition}

                                    SELECT *
                                    FROM entry_record
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.Entry_Records = multi.Read<e.entry_record>();
                }

                return result;
            }
        }

        public static async Task<e.entry_record> GetLastRecord()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.entry_record>(
                    @"SELECT * FROM entry_record
                    WHERE entry_record.id = (
                    SELECT TOP 1 id FROM entry_record 
                    ORDER BY entry_record.id DESC);");
            }
        }

        public static async Task<e.shared.ActionResult> Insert(e.entry_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.entry_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.entry_record obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.entry_record>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.entry_record>(),
                    new { id = id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
