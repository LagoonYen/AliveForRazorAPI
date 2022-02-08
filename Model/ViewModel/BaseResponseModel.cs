using System.Collections.Generic;
using System.Net;

namespace AliveStoreTemplate.Model.ViewModel
{
    public class BaseResponseModel  //Db更新資料用
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }  //回傳的Http狀態碼
    }

    public class BaseQueryModel<T> //Query詢問資料用
    {
        public IEnumerable<T> Results { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }  //回傳的Http狀態碼
    }

    public class ResponseModel<T>
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T ResponseData { get; set; }

        public ResponseModel(HttpStatusCode? responseCode, T responseData)
        {
            this.ResponseCode = (int)responseCode;
            this.ResponseMessage = responseCode.ToString();
            this.ResponseData = responseData;
        }
    }
}
