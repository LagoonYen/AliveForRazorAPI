using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;

namespace AliveStoreTemplate.Common
{
    public interface CodeValidator
    {
        /// <summary>
        /// 驗證
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool Validate(string code);

        /// <summary>
        /// 產生驗證碼
        /// </summary>
        /// <returns></returns>
        string Generate();
    }

    public class CodeValidatorImpl : CodeValidator
    {
        private const string KEY = "ValidationCode";
        private HttpContext _httpContext { get; set; }

        // 注入 IHttpContextAccessor ，因為我們要使用HttpContext取得Session
        public CodeValidatorImpl(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string Generate()
        {
            string code = CreateRandomWord(5);

            // session只能儲存byte[]，將字串轉為byte[]
            byte[] codeBytes = Encoding.ASCII.GetBytes(code);

            _httpContext.Session.Set(KEY, codeBytes);
            return code;
        }

        public bool Validate(string code)
        {
            bool isOk = false;
            byte[] codeBytes = null;

            if (_httpContext.Session.TryGetValue(KEY, out codeBytes))
            {
                // 從Session取出來的byte[] 轉成字串
                string serverCode = Encoding.ASCII.GetString(codeBytes);

                // 忽略大小寫比對
                if (serverCode.Equals(code, StringComparison.InvariantCultureIgnoreCase))
                {
                    isOk = true;
                }
            }

            // 無論成功失敗，都清除Session。(依情境，非必要)
            _httpContext.Session.Remove(KEY);
            return isOk;
        }

        /// <summary>
        /// 產生隨機字串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string CreateRandomWord(int length = 5)
        {
            string code = "";
            var letters = "ABCDEFGHJKMPQRSTUVWXYZ23456789abcdefghjkmpqrstuvwxyz".ToArray();

            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = r.Next(0, letters.Length);
                code += letters[index];
            }
            return code;
        }
    }
}
