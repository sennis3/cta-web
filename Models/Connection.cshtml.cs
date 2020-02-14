using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;  
  
namespace program.Pages  
{  
    public class ConnectionModel : PageModel  
    {  
        public string Status { get; set; }  
  
        public void OnGet()  
        {  
           if (DataAccessTier.DB.TestConnection())
					   Status = "Database connection status:  good";
					 else
					   Status = "Database connection status:  unreachable";
        }  
    }  
}  