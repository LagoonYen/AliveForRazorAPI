using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AliveStoreTemplate.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AliveStoreTemplate.Pages
{
    public class categoryModel : PageModel
    {

        [BindProperty]
        public List<ProductList> ProductList { get; set; }


        private IHostingEnvironment Environment;

        public categoryModel(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public void OnGet()
        {

            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "img/"));
            var fileNames = new List<string>();
            foreach (string file in filePaths)
                fileNames.Add(Path.GetFileName(file));
            ProductList = new List<ProductList>();

            for (int i = 0; i < 20; i++)
            {
                var product = new ProductList();
                product.CardName = "Name" + i;
                product.Price = new Random().Next(100, 300);
                product.Inventory = new Random().Next(10, 30);
                product.Description = "Desc" + i;
                product.ImgUrl = "./img/" + fileNames[new Random().Next(0, 61)];
                ProductList.Add(product);
            }

        }
    }
}