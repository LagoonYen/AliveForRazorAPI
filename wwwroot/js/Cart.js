var RootPath = "";

function DelCartItem(cId, fId, successFunc, errorFunc) {
	var $res = null;
	$.ajax({
		type: "POST",
		url: RootPath + "/orderAgent/cartdelitem",
		//contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'text',
		data: "fid=" + fId + "&cid=" + cId,
		success: function (data) {
			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}
	});
	return $res;
}

function GetAllCart(fanId, successFunc, errorFunc) {
	var $res = null;
	$.ajax({
		type: "POST",
		url: RootPath + "/orderAgent/cartallitems",
		//contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'json',
		data: "fid=" + fanId,
		success: function (data) {
			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}
	});
	return $res;
}

function AddCart(obj, cId, successFunc, errorFunc) {
	var $res = '';
	$.ajax({
		type: "POST",
		url: RootPath + "/orderAgent/cartadditem",
		//contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'text',
		data: "val=" + encodeURIComponent(JSON.stringify(obj)) + "&cid=" + cId,
		success: function (data) {

			if (data.indexOf("error") !== -1) {
				if (errorFunc != null)
					errorFunc();
				return;
			}

			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}
	});
	return $res;
}

function CartCount(fanId, successFunc, errorFunc) {
	var $res = '';
	$.ajax({
		type: "POST",
		url: RootPath + "/orderAgent/cartcount",
		//contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'text',
		data: "fid=" + fanId,
		success: function (data) {
			if (data.indexOf("error") !== -1) {
				if (errorFunc != null)
					errorFunc();
				return;
			}
			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}
	});
	return $res;
}

function SetCartQvt(spec, qvt, successFunc, errorFunc) {
	var $res = null;
	$.ajax({
		type: "POST",
		url: RootPath + "/orderAgent/cartsetitem",
		//contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'text',
		data: "spec=" + spec + "&qvt=" + qvt,
		success: function (data) {
			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}
	});
	return $res;
}

//function SetCartShipType(ship, successFunc, errorFunc) {
//    var $res = null;
//    $.ajax({
//        type: "POST",
//        url: RootPath + "/orderAgent/cartsetshiptype",
//        //contentType: "application/json; charset=utf-8",
//        async: false,
//        cache: false,
//        dataType: 'text',
//        data: "st=" + ship,
//        success: function (data) {
//            if (data.hasOwnProperty("d")) {
//                $res = data.d;
//                if (successFunc != null)
//                    successFunc(data.d);
//            }
//            else {
//                $res = data;
//                if (successFunc != null)
//                    successFunc(data);
//            }
//        },
//        error: function () {
//            if (errorFunc != null)
//                errorFunc();
//        }
//    });
//    return $res;
//}

//--------------------------------------------------------------------------------
// Generate By No2AjaxGenerator
// No2don is no warranty for this document works correctly.
// If you have any questions.You can  feel free mail to no2don@gmail.com
//--------------------------------------------------------------------------------


//-----------------------------Object Models---------------------------------//

// -- ProdCates
var ProdCates = new Array();

// -- ProdCate
function ProdCate() {
	/// <summary>ProdCate</summary>
	ProdCate.prototype.Id = '';
	ProdCate.prototype.Name = '';
	ProdCate.prototype.CreateDate = '';
	ProdCate.prototype.SortIndex = '';
}

// -- ProdSpec
function ProdSpec() {
	/// <summary>ProdSpec</summary>
	ProdSpec.prototype.ProdId = '';
	ProdSpec.prototype.Id = '';
	ProdSpec.prototype.Name = '';
	ProdSpec.prototype.Quantity = '';
	ProdSpec.prototype.ItemNO = '';
	ProdSpec.prototype.CreateDate = '';
	ProdSpec.prototype.LastModifyDate = '';
}

// -- Order
function Order() {
	/// <summary>Order</summary>
	Order.prototype.OrderId = '';
	Order.prototype.UserId = '';
	Order.prototype.BuyerName = '';
	Order.prototype.Buyer = '';
	Order.prototype.Receiver = '';
	Order.prototype.PaymentType = '';
	Order.prototype.PaymentInfo = '';
	Order.prototype.PayMemo = '';
	Order.prototype.OrderStatus = '';
	Order.prototype.PayStatus = '';
	Order.prototype.ShipStatus = '';
	Order.prototype.Invoice = '';
	Order.prototype.ShipInfo = '';
	Order.prototype.CouponId = '';
	Order.prototype.OrderDate = '';
	Order.prototype.PayDate = '';
	Order.prototype.ShipDate = '';
	Order.prototype.CancelDate = '';
	Order.prototype.CompleteDate = '';
	Order.prototype.LastModifyDate = '';
	Order.prototype.ProdPrice = '';
	Order.prototype.CouponValue = '';
	Order.prototype.TotalPrice = '';
	Order.prototype.ShipFee = '';
	Order.prototype.OrderDetails = new Array();
	Order.prototype.Memo = '';
	Order.prototype.OrderLogs = new Array();
}

// -- OrderProfile
function OrderProfile() {
	/// <summary>OrderProfile</summary>
	OrderProfile.prototype.Name = '';
	OrderProfile.prototype.Mobile = '';
	OrderProfile.prototype.City = '';
	OrderProfile.prototype.Villages = '';
	OrderProfile.prototype.Address = '';
	OrderProfile.prototype.Memo = '';
}

// -- Invoice
function Invoice() {
	/// <summary>Invoice</summary>
	Invoice.prototype.InvoiceType = '';
	Invoice.prototype.Title = '';
	Invoice.prototype.InvoiceCode = '';
}

// -- OrderDetails
var OrderDetails = new Array();

// -- OrderDetail
function OrderDetail() {
	/// <summary>OrderDetail</summary>
	OrderDetail.prototype.Id = '';
	OrderDetail.prototype.OrderId = '';
	OrderDetail.prototype.ProdId = '';
	OrderDetail.prototype.ProdSpecId = '';
	OrderDetail.prototype.ItemNO = '';
	OrderDetail.prototype.ProdName = '';
	OrderDetail.prototype.ProdSpecName = '';
	OrderDetail.prototype.MainImage = '';
	OrderDetail.prototype.Qvt = '';
	OrderDetail.prototype.Price = '';
	OrderDetail.prototype.ReturnQvt = '';
	OrderDetail.prototype.LastReturnDate = '';
}

// -- OrderLogs
var OrderLogs = new Array();

// -- OrderLog
function OrderLog() {
	/// <summary>OrderLog</summary>
	OrderLog.prototype.Id = '';
	OrderLog.prototype.OrderId = '';
	OrderLog.prototype.Editor = '';
	OrderLog.prototype.OrderAction = '';
	OrderLog.prototype.Context = '';
	OrderLog.prototype.CreateDate = '';
}



//-----------------------------Methods---------------------------------//

// -- GetProdCatesByFatherId
function GetProdCatesByFatherId(token, fathercateId, successFunc, errorFunc) {
	/// <summary></summary>
	/// <param name="token" type="string">token</param>
	/// <param name="fathercateId" type="string">fathercateId</param>
	/// <param name="successFunc" type="function">Success Function</param>
	/// <param name="errorFunc" type="function">Error Function</param>
	/// <returns type="ProdCates">GetProdCatesByFatherIdResult as ProdCates</returns>
	var $res = new Array();
	$.ajax({
		type: "POST",
		url: "/AJAXService.asmx/GetProdCatesByFatherId",
		contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'json',
		data: "{token:" + JSON.stringify(token) + ",fathercateId:" + JSON.stringify(fathercateId) + "}",
		success: function (data) {
			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}


	});
	return $res;
}

// -- GetFatherCateByChildId
function GetFatherCateByChildId(token, childcateId, successFunc, errorFunc) {
	/// <summary></summary>
	/// <param name="token" type="string">token</param>
	/// <param name="childcateId" type="string">childcateId</param>
	/// <param name="successFunc" type="function">Success Function</param>
	/// <param name="errorFunc" type="function">Error Function</param>
	/// <returns type="ProdCate">GetFatherCateByChildIdResult as ProdCate</returns>
	var $res = new ProdCate();
	$.ajax({
		type: "POST",
		url: "/AJAXService.asmx/GetFatherCateByChildId",
		contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'json',
		data: "{token:" + JSON.stringify(token) + ",childcateId:" + JSON.stringify(childcateId) + "}",
		success: function (data) {
			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}


	});
	return $res;
}

// -- GetProdSpecModel
function GetProdSpecModel(successFunc, errorFunc) {
	/// <summary></summary>
	/// <param name="successFunc" type="function">Success Function</param>
	/// <param name="errorFunc" type="function">Error Function</param>
	/// <returns type="ProdSpec">GetProdSpecModelResult as ProdSpec</returns>
	var $res = new ProdSpec();
	$.ajax({
		type: "POST",
		url: "/AJAXService.asmx/GetProdSpecModel",
		contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'json',
		data: '',
		success: function (data) {
			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}


	});
	return $res;
}

// -- GetProdSpecById
function GetProdSpecById(specId, successFunc, errorFunc) {
	/// <summary></summary>
	/// <param name="specId" type="string">specId</param>
	/// <param name="successFunc" type="function">Success Function</param>
	/// <param name="errorFunc" type="function">Error Function</param>
	/// <returns type="ProdSpec">GetProdSpecByIdResult as ProdSpec</returns>
	var $res = new ProdSpec();
	$.ajax({
		type: "POST",
		url: "/AJAXService.asmx/GetProdSpecById",
		contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'json',
		data: "{specId:" + JSON.stringify(specId) + "}",
		success: function (data) {
			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}


	});
	return $res;
}

// -- GetOrderInfo
function GetOrderInfo(successFunc, errorFunc) {
	/// <summary></summary>
	/// <param name="successFunc" type="function">Success Function</param>
	/// <param name="errorFunc" type="function">Error Function</param>
	/// <returns type="Order">GetOrderInfoResult as Order</returns>
	var $res = new Order();
	$.ajax({
		type: "POST",
		url: "/AJAXService.asmx/GetOrderInfo",
		contentType: "application/json; charset=utf-8",
		async: false,
		cache: false,
		dataType: 'json',
		data: '',
		success: function (data) {
			if (data.hasOwnProperty("d")) {
				$res = data.d;
				if (successFunc != null)
					successFunc(data.d);
			}
			else {
				$res = data;
				if (successFunc != null)
					successFunc(data);
			}
		},
		error: function () {
			if (errorFunc != null)
				errorFunc();
		}


	});
	return $res;
}

