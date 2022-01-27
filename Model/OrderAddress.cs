using System;
using System.Collections.Generic;

namespace AliveStoreTemplate.Model
{
    public partial class OrderAddress
    {
        public int Id { get; set; }
        public int? Uid { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
