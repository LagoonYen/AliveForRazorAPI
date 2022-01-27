using System;

namespace AliveStoreTemplate.Model.DTOModel
{
    public class AddressUpserConditionModel
    {
        public int Uid { get; set; }
        public string OrderName { get; set; }
        public string OrderPhoneNumber { get; set; }
        public string OrderCity { get; set; }
        public string OrderTown { get; set; }
        public string OrderAddress { get; set; }
        public DateTime DateTime { get; set; }
    }
}
