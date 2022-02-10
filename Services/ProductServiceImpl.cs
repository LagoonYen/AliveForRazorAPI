using AliveStoreTemplate.Model;
using AliveStoreTemplate.Model.ViewModel;
using AliveStoreTemplate.Repositories;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AliveStoreTemplate.Model.ReqModel;
using AliveStoreTemplate.Model.DTOModel;
using System.IO;

namespace AliveStoreTemplate.Services
{
    public class ProductServiceImpl : ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductServiceImpl(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public BaseQueryModel<ProductList> SearchProduct(ProductListReqModel Req)
        {
            try
            {
                //取得Category及SubCategory分類
                var category = Req.Category;
                var subCategory =  Req.Subcategory;

                var baseQueryModel = _productRepository.SearchProduct(category, subCategory);
                return new BaseQueryModel<ProductList>
                {
                    Results = baseQueryModel.Results,
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK
                    
                };
            }
            catch(Exception ex)
            {
                return new BaseQueryModel<ProductList>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public BaseQueryModel<ProductResultModel> Product_CategoryList()
        {
            try
            {
                string category = "";
                string subCategory = "";

                //先取出所有卡片的資料
                var product_list = _productRepository.SearchProduct(category, subCategory).Results;
                var productViewModel = product_list.Select(x => new 
                {
                    Category = x.Category,
                    SubCategory = x.Subcategory
                }).GroupBy(x => x.Category).Select(x => new ProductResultModel
                {
                    Category = x.Key,
                    SubCategory = x.Select(x => x.SubCategory).Distinct().ToList()
                });
            return new BaseQueryModel<ProductResultModel>
                {
                    Results = productViewModel,
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseQueryModel<ProductResultModel>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        //取單卡資訊
        public BaseQueryModel<ProductList> Product_Info(int id)
        {
            try 
            {
                var baseQueryModel = _productRepository.Product_Info(id);
                return new BaseQueryModel<ProductList>
                {
                    Results = baseQueryModel.Results,
                    Message = String.Empty,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseQueryModel<ProductList>
                {
                    Results = null,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public BaseResponseModel PatchProductAllInfo(ProductList product)
        {
            try
            {
                product.UpdateTime = DateTime.Now;
                var baseResponseModel = _productRepository.PatchProduct(product);
                return new BaseResponseModel
                {
                    Message = baseResponseModel.Message,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public BaseResponseModel InsertProduct(ProductReqModel Req)
        {
            try
            {
                //取出所有的卡片清單
                var category = "";
                var subcategory = "";
                var cardList = _productRepository.SearchProduct(category, subcategory).Results;
                var NewcardPathInDb = "";
                if (Req.CardImg != null)
                {
                    Req.ImgUrl = Req.CardImg.FileName;
                    //比對是否有重複的圖片名稱
                    var fileName = Req.CardImg.FileName;
                    foreach (var item in cardList)
                    {
                        var dbfileName = item.ImgUrl.Split("/").TakeLast(1).FirstOrDefault();
                        fileName = (fileName == dbfileName) ? "card" + DateTime.Now.ToString("yyyyMMddHHmm") : fileName;
                    }

                    //建造儲存路徑
                    var fileExtension = Req.CardImg.FileName.Split(".").TakeLast(1).FirstOrDefault();
                    NewcardPathInDb = $"img/{fileName}.{fileExtension}";

                    //感謝Kevin指引 [該死的] 路徑要加在哪邊www
                    using (var stream = new FileStream($"./wwwroot/" + NewcardPathInDb, FileMode.Create))
                    {
                        Req.CardImg.CopyTo(stream);
                    }
                }

                ProductList product = new ProductList
                {
                    CardName = Req.CardName,
                    Category = Req.Category,
                    Subcategory = Req.Subcategory,
                    Description = Req.Description,
                    Price = Req.Price,
                    Inventory = Req.Inventory,
                    ImgUrl = NewcardPathInDb,
                    RealseTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };
                var baseResponseModel = _productRepository.InsertProduct(product);
                return new BaseResponseModel
                {
                    Message = baseResponseModel.Message,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }

        public BaseResponseModel DeleteProduct(int productId)
        {
            try
            {
                var baseResponseModel = _productRepository.DeleteProduct(productId);
                return new BaseResponseModel
                {
                    Message = baseResponseModel.Message,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
    }
}
