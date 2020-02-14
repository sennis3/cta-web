using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace program.Pages  
{  
    public class TopTenStationsModel : PageModel  
    {  
				public List<Models.Station> StationList { get; set; }
				public Exception EX { get; set; }
  
        public void OnGet()  
        {  
				  StationList = new List<Models.Station>();
					
					
					// clear exception:
					EX = null;
					
					try
					{
							string sql;

							sql = string.Format(@"
	SELECT T1.StationID, Stations.Name, T1.NumRiders
    FROM (SELECT TOP 10 StationID, SUM(DailyTotal) AS NumRiders
        FROM Riderships
        GROUP BY StationID
        ORDER BY NumRiders DESC) AS T1
    LEFT JOIN Stations ON T1.StationID = Stations.StationID
	");

							DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

							foreach (DataRow row in ds.Tables[0].Rows)
							{
								Models.Station s = new Models.Station();

								s.StationID = Convert.ToInt32(row["StationID"]);
								s.StationName = Convert.ToString(row["Name"]);
                                
                                s.NumRiders = Convert.ToInt32(row["NumRiders"]);
                                
								StationList.Add(s);
							}
					}
					catch(Exception ex)
					{
					  EX = ex;
					}
					finally
					{
					  // nothing at the moment
				  }
				}
			
    }//class  
}//namespace