using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
using System.Data;
  
namespace program.Pages  
{  
    public class StationInfoModel : PageModel  
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
	SELECT StationList.StationID, StationList.Name, StationList.AvgDailyRidership, Stop_List.NumStops, Stop_List.HandicapAccessible
FROM (SELECT Stations.StationID, Stations.Name, AVG(DailyTotal) AS AvgDailyRidership
        FROM Stations
        LEFT JOIN Riderships ON Stations.StationID = Riderships.StationID
        LEFT OUTER JOIN Stops ON Stations.StationID = Stops.StationID
        WHERE Stations.Name LIKE '%{0}%'
        GROUP BY Stations.StationID, Stations.Name) AS StationList
LEFT JOIN (SELECT StationID, Count(StationID) AS NumStops, Sum(CAST(ADA AS int)) AS HandicapAccessible
        FROM Stops
        GROUP BY StationID) AS Stop_List
ON StationList.StationID = Stop_List.StationID
ORDER BY StationList.Name ASC
	", input);

							DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

							foreach (DataRow row in ds.Tables[0].Rows)
							{
								Models.Station s = new Models.Station();

								s.StationID = Convert.ToInt32(row["StationID"]);
								s.StationName = Convert.ToString(row["Name"]);

								// avg could be null if there is no ridership data:
								if (row["AvgDailyRidership"] == System.DBNull.Value)
									s.AvgDailyRidership = 0;
								else
									s.AvgDailyRidership = Convert.ToInt32(row["AvgDailyRidership"]);
                                
                                //NumStops
                                if (row["NumStops"] == System.DBNull.Value)
                                    s.NumStops = 0;
                                else
                                    s.NumStops = Convert.ToInt32(row["NumStops"]);
                                
                                //Handicap Accessible
                                if (row["HandicapAccessible"] == System.DBNull.Value)
                                    s.HandicapAccessible = "none";
                                else {
                                    int handicapInt = Convert.ToInt32(row["HandicapAccessible"]);
                                    if (handicapInt == 0)
                                        s.HandicapAccessible = "none";
                                    else if (handicapInt == s.NumStops)
                                        s.HandicapAccessible = "all";
                                    else
                                        s.HandicapAccessible = "some";
                                }
                                
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