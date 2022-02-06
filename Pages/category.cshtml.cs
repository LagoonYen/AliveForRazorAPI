using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AliveStoreTemplate.Pages
{
    public class categoryModel : PageModel
    {
        [Obsolete]
        private IHostingEnvironment Environment;
        private readonly ProductService _productService;

        [Obsolete]
        public categoryModel(IHostingEnvironment _environment, ProductService productService)
        {
            Environment = _environment;
            _productService = productService;
        }

        [BindProperty]
        public List<ProductList> CardList { get; set; }

        public void OnGet(string category, string subCategory)
        {
            ProductListReqModel Req = new ProductListReqModel();
            if (category != null)
            {
                Req.Category = category;
                Req.SubCategory = "";
                if(subCategory != null)
                {
                    Req.SubCategory = subCategory;
                }
            }
            
            var result = _productService.SearchProduct(Req);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (result.Results != null)
                {
                    CardList = result.Results.ToList();
                    return;
                };
            }
            ViewData["Message"] = string.Format("Category Error");

            //string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "img/"));
            //var fileNames = new List<string>();
            //foreach (string file in filePaths)
            //    fileNames.Add(Path.GetFileName(file));
            //ProductList = new List<ProductList>();

            //for (int i = 0; i < 20; i++)
            //{
            //    var product = new ProductList();
            //    product.CardName = "Name" + i;
            //    product.Price = new Random().Next(100, 300);
            //    product.Inventory = new Random().Next(10, 30);
            //    product.Description = "Desc" + i;
            //    product.ImgUrl = "./img/" + fileNames[new Random().Next(0, 61)];
            //    ProductList.Add(product);
            //}
        }
    }
}