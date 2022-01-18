using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace AliveStoreTemplate.Pages
{
    public class IndexModel : PageModel
    {
        public string Result { get; set; }
        public IndexModel()
        {
        }

        public void OnGet()
        {
            Response.Redirect("/home");
        }
    }
}
