using System;
using Dapper;
using e = Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using d = DataAccess.shared.DbAccess;
using v = DataAccess.shared.Variables;
using func = DataAccess.shared.Functions;
using System.Linq;

namespace DataAccess
{
    public class people_history
    {
        public static async Task<IEnumerable<e.people_history>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.people_history>(d.SelectAll<e.people_history>());
            }
        }

        public static async Task<e.people_history> Get(string id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.people_history>(d.Select<e.people_history>(),
                     new { ph_or_id = id });
            }
        }

        public static async Task<e.people_history> Get(string id, DateTime visited_at)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.people_history>(d.Select<e.people_history>(),
                     new { ph_or_id = id, visited_at });
            }
        }

        public static async Task<e.people_historyResult> Get(e.people_historyParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.people_historyResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name) && param.IsMobile)
                    condition = "(ph_or_id Like'%' + @Name + '%')";
                else if (!string.IsNullOrWhiteSpace(param.Name) && !param.IsMobile)
                    condition = @"(ph_or_id Like'%' + @Name + '%' OR duration Like '%' + @Name + '%' OR source_id Like'%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM people_history
                                    {condition}

                                    SELECT *
                                    FROM people_history
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "ph_or_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.People_Histories = multi.Read<e.people_history>();
                }

                return result;
            }
        }

        public static async Task<bool> IsExists(string id, DateTime visited_at)
        {          
            using (var db = d.ConnectionFactory())
            {
                int result = await db.QueryFirstOrDefaultAsync<int>("SELECT 1 FROM people_history WHERE ph_or_id=@id AND visited_at=@visited_at",
                             new { id, visited_at });
                return result == 1;
            }          
        }

        public static async Task<e.shared.ActionResult> Save(e.people_history obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(
                    $@"IF NOT EXISTS (SELECT 1 FROM people_history WHERE ph_or_id=@ph_or_id AND visited_at=@visited_at)
                        BEGIN
                            {d.Insert<e.people_history>()}
                        END
                    ELSE
                        BEGIN
                            {d.Update<e.people_history>()}
                        END", 
                    obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> InsertMany(IEnumerable<e.people_history> objs)
        {
            if (objs == null || objs.Count() < 0)
                return new e.shared.ActionResult { Status = e.shared.Status.Fail, Value = objs, Msg = "Data is empty" };

            using (var db = d.ConnectionFactory())
            {
                foreach (var obj in objs)
                {
                    await db.ExecuteAsync(
                        $@"IF NOT EXISTS (SELECT 1 FROM people_history WHERE ph_or_id=@ph_or_id AND visited_at=@visited_at)
                        BEGIN
                            {d.Insert<e.people_history>()}
                        END",
                    obj);
                }
             
                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(string id, DateTime visited_at)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync("DELETE FROM people_history WHERE ph_or_id=@id AND visited_at=@visited_at",
                    new { id, visited_at });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
