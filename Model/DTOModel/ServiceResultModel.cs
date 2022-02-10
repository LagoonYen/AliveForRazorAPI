using System.Collections.Generic;

namespace AliveStoreTemplate.Model.DTOModel
{
    /// <summary>
    /// Product相關
    /// </summary>
    public class ProductResultModel
    {
        public string Category { get; set; }

        public List<string> SubCategory { get; set; }
    }
}
