using AliveStoreTemplate.Model.DTOModel;
using AliveStoreTemplate.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace AliveStoreTemplate.Pages
{
    public class productCreateModel : PageModel
    {
        private readonly ProductService _productService;
        private IHostingEnvironment _environment;

        public productCreateModel(ProductService productService, IHostingEnvironment environment)
        {
            _productService = productService;
            _environment = environment;
        }

        public void OnGet()
        {
            
        }

        [BindProperty]
        public ProductReqModel productReq { get; set; }

        public IActionResult OnPost()
        {
            var result = _productService.InsertProduct(productReq);
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            //var file = Path.Combine(_environment.ContentRootPath, "img", ImgUrl.FileName);

            //using (var fileStream = new FileStream(file, FileMode.Create))
            //{
            //    ImgUrl.CopyTo(fileStream);
            //}
            return RedirectToPage("./productIndex");
        }
    }
}
