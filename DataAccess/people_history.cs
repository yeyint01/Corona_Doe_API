using Dapper;
using e = Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using d = DataAccess.shared.DbAccess;
using v = DataAccess.shared.Variables;
using func = DataAccess.shared.Functions;

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

        //public static async Task<e.people_history> Get(string id)
        //{
        //    using (var db = d.ConnectionFactory())
        //    {
        //        return await db.QueryFirstOrDefaultAsync<e.people_history>("SELECT * FROM people_history WHERE ph_or_id=@ph_or_id",
        //             new { ph_or_id = id });
        //    }
        //}

        public static async Task<e.people_history> Get(e.people_historyQueryInfo param)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.people_history>(d.Select<e.people_history>(),
                     new { mid = param.mid, did = param.did, mhash = param.mhash, dhash = param.dhash });
            }
        }

        public static async Task<e.people_historyResult> Get(e.people_historyParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.people_historyResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = @"(mid Like'%' + @Name + '%' OR mhash Like '%' + @Name + '%' OR did Like'%'
                                OR dhash Like'%' + @Name + '%' OR duration Like '%' + @Name + '%' OR source Like'%')";
                
                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM people_history
                                    {condition}

                                    SELECT *
                                    FROM people_history
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "mid"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
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

        //public static async Task<bool> IsExists(e.people_historyQueryInfo param)
        //{          
        //    using (var db = d.ConnectionFactory())
        //    {
        //        int result = await db.QueryFirstOrDefaultAsync<int>("SELECT 1 FROM people_history WHERE mid=@mid AND did=@did AND mhash=@mhash AND dhash=@dhash",
        //                     new { param.mid, param.did, param.mhash, param.dhash });
        //        return result == 1;
        //    }          
        //}

        public static async Task<e.shared.ActionResult> Save(e.people_history obj)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(
                    $@"IF NOT EXISTS (SELECT 1 FROM people_history WHERE mid=@mid AND did=@did AND mhash=@mhash AND dhash=@dhash)
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
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Insert<e.people_history>(), objs);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(e.people_historyQueryInfo param)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync("DELETE FROM people_history WHERE mid=@mid AND did=@did AND mhash=@mhash AND dhash=@dhash",
                    new { mid = param.mid, did = param.did, mhash = param.mhash, dhash = param.dhash });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }
    }
}
