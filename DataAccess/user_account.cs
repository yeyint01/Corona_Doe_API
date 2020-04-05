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
    public class user_account
    {
        public static async Task<IEnumerable<e.user_account>> Get()
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryAsync<e.user_account>(d.SelectAll<e.user_account>());
            }
        }

        public static async Task<e.user_account> Get(int user_id)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.user_account>(d.Select<e.user_account>(),
                     new { user_id });
            }
        }

        public static async Task<e.user_account> Get(string user_name)
        {
            using (var db = d.ConnectionFactory())
            {
                return await db.QueryFirstOrDefaultAsync<e.user_account>("SELECT * FROM user_account WHERE user_name=@user_name",
                            new { user_name });              
            }               
        }

        public static async Task<e.user_accountResult> Get(e.user_accountParam param)
        {
            using (var db = d.ConnectionFactory())
            {
                var result = new e.user_accountResult();
                string condition = "";
                if (!string.IsNullOrWhiteSpace(param.Name))
                    condition = @"(user_name Like'%' + @Name + '%' OR full_name Like '%' + @Name + '%' " +
                                "OR phone_no Like'%' + @Name + '%' OR remark Like '%' + @Name + '%')";

                if (condition.Length > 1)
                    condition = "WHERE " + condition;

                using (var multi = await db.QueryMultipleAsync(
                                    $@"SELECT COUNT(*) 
                                    FROM user_account
                                    {condition}

                                    SELECT *
                                    FROM user_account
                                    {condition}
                                    ORDER BY {param.OrderBy ?? "user_id"} {(param.Order == e.shared.SortOrder.Descending ? "DESC" : "")}
                                    OFFSET {v.RowsInPage * (param.PgNo - 1)} ROWS 
                                    FETCH NEXT {v.RowsInPage} ROWS ONLY", param))
                {
                    result.RCount = await multi.ReadFirstAsync<int>();
                    result.PgCount = func.PageCount(result.RCount);
                    result.User_Accounts = multi.Read<e.user_account>();
                }

                return result;
            }
        }

        public static async Task<bool> IsLoginNameDuplicate(int user_id, string user_name)
        {
            bool flag;
            using (var db = d.ConnectionFactory())
            {
                string result = await db.QueryFirstOrDefaultAsync<string>("SELECT 1 FROM user_account WHERE user_name=@user_name AND user_id<>@user_id",
                new { user_name, user_id });

                flag = result != null;
            }
            return flag;
        }

        public static async Task<e.shared.ActionResult> Insert(e.user_account obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.creation_date = DateTime.Now;
                obj.modified_date = DateTime.Now;

                int id = await db.ExecuteScalarAsync<int>(d.InsertAutoId<e.user_account>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = id };
            }
        }

        public static async Task<e.shared.ActionResult> Update(e.user_account obj)
        {
            using (var db = d.ConnectionFactory())
            {
                obj.modified_date = DateTime.Now;

                await db.ExecuteAsync(d.Update<e.user_account>(), obj);

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> Delete(int user_id)
        {
            using (var db = d.ConnectionFactory())
            {
                await db.ExecuteAsync(d.Delete<e.user_account>(),
                    new { user_id });

                return new e.shared.ActionResult { Status = e.shared.Status.Success };
            }
        }

        public static async Task<e.shared.ActionResult> ChangeLoginName(int user_id, string user_name)
        {           
            using (var db = d.ConnectionFactory())
            {
                int num = await db.ExecuteAsync("UPDATE user_account SET user_name=@user_name WHERE user_id=@user_id",
                           new { user_id, user_name });

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = num };
            }           
        }

        public static async Task<e.shared.ActionResult> ChangePassword(int user_id, string password)
        {          
            using (var db = d.ConnectionFactory())
            {
                int num = await db.ExecuteAsync("UPDATE user_account SET password=@password WHERE user_id=@user_id",
                           new { user_id, password });

                return new e.shared.ActionResult { Status = e.shared.Status.Success, Value = num };
            }           
        }
    }
}
