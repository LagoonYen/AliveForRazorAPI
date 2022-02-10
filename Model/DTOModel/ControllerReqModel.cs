using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AliveStoreTemplate.Model.DTOModel
{
    /// <summary>
    /// Member相關
    /// </summary>
    public class LoginReqModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件帳號")]
        public string Account { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "{0}必需擁有至少{1}個字元,最多{2}個字元", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }
    }

    public class PatchMemberInfoReqModel
    {
        [EmailAddress]
        [Display(Name = "電子郵件帳號")]
        public string Account { get; set; }

        [Display(Name = "UID")]
        public int UID { get; set; }

        [Display(Name = "暱稱")]
        public string NickName { get; set; }

        [Display(Name = "電話號碼")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "信箱")]
        public string Email { get; set; }
    }

    /// <summary>
    /// Order相關
    /// </summary>
    public class ToOrderReqModel
    {
        public int Uid { get; set; }
        public class OrderDetail
        {
            public int ProductId { get; set; }
            public int ProductNum { get; set; }
            public int ProductPrice { get; set; }
        }
        public string OrderName { get; set; }
        public string OrderPhoneNumber { get; set; }
        public string OrderCity { get; set; }
        public string OrderTown { get; set; }
        public string OrderAddress { get; set; }
        public string Remark { get; set; }
    }

    public class OrderDetailReqModel
    {
        public int OrderId { get; set; }
    }

    public class OrderDetailResponseModel
    {
        public string OrderNumber { get; set; }
        public int Uid { get; set; }
        public string Recipient { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientCity { get; set; }
        public string RecipientTown { get; set; }
        public string RecipientAddress { get; set; }
        public string Remark { get; set; }
        public int? PayPrice { get; set; }
        public byte? IsPay { get; set; }
        public int? PayTime { get; set; }
        public byte? IsShip { get; set; }
        public int? ShipTime { get; set; }
        public byte? IsReceipt { get; set; }
        public int? ReceiptTime { get; set; }
        public string ShipNumber { get; set; }
        public byte? Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public List<OrderProduct> Products { get; set; }
    }

    /// <summary>
    /// Product相關
    /// </summary>
    public class ProductListReqModel
    {
        public string Category { get; set; }
        public string Subcategory { get; set; }
    }

    public class ProductInfoReqModel
    {
        public int product_id { get; set; }
    }

    /// <summary>
    /// Shopcar相關
    /// </summary>
    public class AddToCartReqModel
    {
        [Required]
        public int Uid { get; set; }

        [Required]
        public int product_id { get; set; }

        [Required]
        public int num { get; set; }
    }

    public class DelFromCartReqModel
    {
        [Required]
        public int uid { get; set; }

        [Required]
        public int product_id { get; set; }
    }

    public class UpsertCartReqModel
    {
        [Required]
        public int uid { get; set; }

        [Required]
        public int product_id { get; set; }

        [Required]
        public int num { get; set; }

        public DateTime UpdateTime { get; set; }
    }

    public class UIDReqModel
    {
        public int UID { get; set; }
    }

    public class ProductReqModel
    {
        [Required]
        public string Category { get; set; }
        [Required]
        public string Subcategory { get; set; }
        [Required]
        public string CardName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Inventory { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile CardImg { get; set; }
    }
}
