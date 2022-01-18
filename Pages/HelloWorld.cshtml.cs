using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AliveStoreTemplate.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AliveStoreTemplate.Pages
{
    public class HelloWorldModel : BasePageModel
    {
        public string Result { get; set; }

        public void OnGet()
        {
        }
    }
}
