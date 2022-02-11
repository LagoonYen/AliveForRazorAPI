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
        public BaseQueryModel<ProductList> GetProductInfo(int id)
        {
            try 
            {
                var baseQueryModel = _productRepository.GetProductInfo(id);
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

        //更改資料
        public BaseResponseModel PatchProductAllInfo(ProductReqModel productReqModel)
        {
            try
            {
                //先抓舊的ImgUrl
                var fileName = productReqModel.ImgUrl;

                if (
                    //productReqModel.ImgUrl != null 
                    productReqModel.CardImg.FileName != null)
                    //&& string.Empty is var fileName
                    //&& productReqModel.ImgUrl.Split("/").TakeLast(1).FirstOrDefault() != productReqModel.CardImg.FileName)
                {
                    var fileExtension = productReqModel.CardImg.FileName.Split(".").TakeLast(1).FirstOrDefault();
                    fileName = !string.IsNullOrWhiteSpace(fileName) ? fileName :$"img/{Guid.NewGuid().ToString()}.{fileExtension}";
                    using (var stream = new FileStream($"./wwwroot/" + fileName, FileMode.Create))
                    {
                        productReqModel.CardImg.CopyTo(stream);
                    }
                }

                ProductList productList = new ProductList()
                {
                    Id = productReqModel.Id,
                    Category = productReqModel.Category,
                    Subcategory = productReqModel.Subcategory,
                    CardName = productReqModel.CardName,
                    Description = productReqModel.Description,
                    Price = productReqModel.Price,
                    Inventory = productReqModel.Inventory,
                    ImgUrl = fileName,
                    UpdateTime = DateTime.Now
                };

                var baseResponseModel = _productRepository.PatchProduct(productList);
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
                    //var fileName = Req.CardImg.FileName;
                    var fileName = Guid.NewGuid().ToString();
                    foreach (var item in cardList)
                    {
                        var dbfileName = item.ImgUrl.Split(".").TakeLast(1).FirstOrDefault();
                        fileName = (fileName == dbfileName) ? Guid.NewGuid().ToString() : fileName;
                    }

                    //建造儲存路徑
                    var fileExtension = Req.CardImg.FileName.Split(".").TakeLast(1).FirstOrDefault();
                    NewcardPathInDb = $"img/{fileName}.{fileExtension}";

                    //感謝Kevin指引 [走丟的] 路徑要加在哪邊www
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

        public BaseResponseModel DeleteProduct(int productId, string ImgUrl)
        {
            try
            {
                if(ImgUrl != null)
                {
                    var path = $"./wwwroot/" + ImgUrl;
                    File.Delete(path);
                }
                
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
