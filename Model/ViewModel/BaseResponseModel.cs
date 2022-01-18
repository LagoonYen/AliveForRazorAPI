using System.Collections.Generic;
using System.Net;

namespace AliveStoreTemplate.Model.ViewModel
{
    public class BaseResponseModel  //Db更新用
    {
        //public int RowCount { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }  //回傳的Http狀態碼
    }

    public class BaseQueryModel<T> //Query資料用
    {
        public IEnumerable<T> Results { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }  //回傳的Http狀態碼
    }
}
