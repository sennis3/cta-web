using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace program.Pages  
{  
    public class LineInfoModel : PageModel  
    {  
				public List<Models.Station> StationList { get; set; }
				public string Input { get; set; }
				public Exception EX { get; set; }
  
        public void OnGet(string input)  
        {  
				  StationList = new List<Models.Station>();
					
					// make input available to web page:
					Input = input;
					
					// clear exception:
					EX = null;
					
					try
					{
						//
						// Do we have an input argument?  If not, there's nothing to do:
						//
						if (input == null)
						{
							//
							// there's no page argument, perhaps user surfed to the page directly?  
							// In this case, nothing to do.
							//
						}
						else  
						{
							// 
							// Lookup movie(s) based on input, which could be id or a partial name:
							// 
							string sql;

						  // lookup station(s) by partial name match:
							input = input.Replace("'", "''");

							sql = string.Format(@"
	SELECT T3.StationID, T3.Name, T3.NumStops, T3.LineID, Lines.Color, T3.Position
FROM (SELECT T2.StationID, T2.Name, T2.NumStops, StationOrder.LineID, StationOrder.Position
    FROM (SELECT T1.StationID, T1.NumStops, Stations.Name
        FROM(SELECT StationID, COUNT(*) AS NumStops
            FROM Stops
            GROUP BY StationID) AS T1
        JOIN Stations ON T1.StationID = Stations.StationID) AS T2
    RIGHT JOIN StationOrder ON T2.StationID = StationOrder.StationID) AS T3
RIGHT JOIN Lines ON T3.LineID = Lines.LineID
WHERE Lines.Color = '{0}'
ORDER BY T3.LineID, T3.Position
	", input);

							DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

							foreach (DataRow row in ds.Tables[0].Rows)
							{
								Models.Station s = new Models.Station();

								s.StationID = Convert.ToInt32(row["StationID"]);
								s.StationName = Convert.ToString(row["Name"]);
                                
                                //NumStops
                                if (row["NumStops"] == System.DBNull.Value)
                                    s.NumStops = 0;
                                else
                                    s.NumStops = Convert.ToInt32(row["NumStops"]);
                                
								StationList.Add(s);
							}
						}//else
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