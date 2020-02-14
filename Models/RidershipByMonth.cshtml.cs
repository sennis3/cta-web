using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
  
namespace program.Pages  
{  
    public class RidershipByMonthModel : PageModel  
    {  
        public List<string> Months { get; set; }
        public List<int> NumRiders { get; set; }
        public Exception EX { get; set; }
  
        public void OnGet()  
        {
          Months = new List<string>();
          NumRiders = new List<int>();
          
          EX = null;
          
          Months.Add("Jan");
          Months.Add("Feb");
          Months.Add("Mar");
          Months.Add("Apr");
          Months.Add("May");
          Months.Add("Jun");
          Months.Add("Jul");
          Months.Add("Aug");
          Months.Add("Sep");
          Months.Add("Oct");
          Months.Add("Nov");
          Months.Add("Dec");
          
          try
          {
            string sql = string.Format(@"
SELECT MONTH(TheDate) AS TheMonth, Sum(DailyTotal) AS NumRiders
FROM Riderships
GROUP BY Month(TheDate)
ORDER BY Month(TheDate);
");
          
            DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
              int numriders = Convert.ToInt32(row["NumRiders"]);

              NumRiders.Add(numriders);
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