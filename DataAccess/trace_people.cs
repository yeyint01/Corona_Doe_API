using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using d = DataAccess.shared.DbAccess;
using e = Entity;
using v = DataAccess.shared.Variables;

namespace DataAccess
{
	public class trace_people
    {
        public static async Task<e.trace_result> GetTraceData(e.trace_param param)
        {
            using (var db = d.ConnectionFactory())
            {
				var rotues = await GetRoutes(param);

				var contactPeople = await db.QueryAsync<e.contact_person>(
					$@"WITH tbl AS
					(
						SELECT * 
						FROM people_history
						WHERE timestamp BETWEEN @start_date AND @end_date
					)
					SELECT mhash, count, timestamp AS last_contact, lat, lng
					FROM
					(
						SELECT *, MAX(r) OVER (PARTITION BY mhash) AS count
						FROM
						(
							SELECT *, ROW_NUMBER() OVER (PARTITION BY mhash ORDER BY timestamp DESC) AS r
							FROM
							(
								SELECT t2.mhash, t2.lat, t2.lng, t1.timestamp, dbo.GetDistance(t1.lat, t1.lng, t2.lat, t2.lng) AS distance
								FROM
								(
									SELECT mhash, lat, lng, timestamp,
										DATEADD(MI, -{v.ContactTime}, timestamp) AS start_time,
										DATEADD(MI, {v.ContactTime}, timestamp) AS end_time
									FROM tbl
									WHERE mhash=@mhash
								) t1,
								(
									SELECT mhash, lat, lng, timestamp
									FROM tbl
									WHERE mhash<>@mhash
								) t2
								WHERE t2.timestamp BETWEEN t1.start_time AND t1.end_time
							) t3
							WHERE distance < 3
						) t4
					) t5
					WHERE r=1",
					param);

				return new e.trace_result
				{
					target_routes = rotues,
					contact_people = contactPeople
				};
            }
        }

		public static async Task<IEnumerable<e.target_route>> GetRoutes(e.trace_param param)
		{
			using (var db = d.ConnectionFactory())
			{
				return await db.QueryAsync<e.target_route>(
					@"SELECT lat, lng
					FROM people_history
					WHERE mhash=@mhash AND timestamp BETWEEN @start_date AND @end_date
					ORDER BY timestamp",
					param);
			}
		}
    }
}
