using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Quiz.DTO.UserManagement;
using Quiz.UI.Models;
using Quiz.UI.ServicesClient;
using Serilog;
using VnPayLib;

namespace Quiz.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginServiceClient _service;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(ILoginServiceClient service, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var profile = await _service.GetMyProfile(userId);
            ViewBag.Profile = profile.ResultObj;
            ViewBag.url = await UrlPayment(2, "test");
            return View();
        }
        public async Task<IActionResult> PayComplete()
        {
            ViewBag.Success =  await VnPayReturn();
            return View();
        }

        public async Task<string> UrlPayment(int TypePaymentVn, string orderCode)
        {
            //Get Config Info
            string vnp_Returnurl = _configuration.GetSection("VnPay:vnp_Returnurl").Value; //URL nhan ket qua tra ve 
            string vnp_Url = _configuration.GetSection("VnPay:vnp_Url").Value;  //URL thanh toan cua VNPAY 
            string vnp_TmnCode = _configuration.GetSection("VnPay:vnp_TmnCode").Value; ; //Ma website
            string vnp_HashSecret = _configuration.GetSection("VnPay:vnp_HashSecret").Value;  //Chuoi bi mat
            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                return "Vui lòng cấu hình các tham số: vnp_TmnCode,vnp_HashSecret trong file web.config";
            }
            //Get payment input
            OrderInfo order = new OrderInfo();
            order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.Amount = 10000; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
            order.CreatedDate = DateTime.Now;
            //Save order to db

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVn == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVn == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVn == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            vnpay.AddRequestData("vnp_IpAddr", ip);
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            Log.Information("VNPAY URL: {0}", paymentUrl);
            return paymentUrl;
        }
        //http://localhost:13000/Account/PayComplete?vnp_Amount=5000000&vnp_BankCode=NCB&vnp_BankTranNo=VNP14178510&vnp_CardType=ATM&vnp_OrderInfo=Thanh+toan+don+hang%3A638354807959186304&vnp_PayDate=20231113140047&vnp_ResponseCode=00&vnp_TmnCode=OW156YUW&vnp_TransactionNo=14178510&vnp_TransactionStatus=00&vnp_TxnRef=638354807959186304&vnp_SecureHash=dfdef9d3dc8921672b1b7642a1c6c6aef415899397eab1b95b52e0d103838ca5e02e122c2d6ef429786164047f654962cc986d96d12dc655c27324abf3a37b16
        public async Task<string> VnPayReturn()
        {
            if (!string.IsNullOrEmpty(Request.QueryString.Value))
            //if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = _configuration.GetSection("VnPay:vnp_HashSecret").Value; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                var array = vnpayData.Value.Split('&');
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string item in array)
                {
                    var newitem = item.Replace('?', ' ');
                    newitem = newitem.Trim();
                    //get all querystring data
                    if (!string.IsNullOrEmpty(newitem) && newitem.StartsWith("vnp_"))
                    {
                        var responesData = newitem.Split('=');
                        vnpay.AddResponseData(responesData[0], responesData[1]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");

                String vnp_SecureHash = vnpay.GetResponseData("vnp_SecureHash");
                String TerminalID = vnpay.GetResponseData("vnp_TmnCode");
                //String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                //String TerminalID = Request.QueryString["vnp_TmnCode"];

                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = vnpay.GetResponseData("vnp_BankCode");
                var request = new AddMoneyRequest()
                {
                    Money = vnp_Amount,
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
                var updateMoney = await _service.UpdateMoney(request);
                //String bankCode = Request.QueryString["vnp_BankCode"];
                //RHQNVCBZRYLKYUCRTUPCADKPQWUWREUA
                //
                //bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                //if (checkSignature)
                //{
                //    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                //    {
                //        //Thanh toan thanh cong
                //        Log.Information("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                //        return $"Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ {vnp_Amount}";
                //    }
                //    else
                //    {
                //        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                //        Log.Information("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                //        return "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                //    }
                //    //displayTmnCode.InnerText = "Mã Website (Terminal ID):" + TerminalID;
                //    //displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                //    //displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                //    //displayAmount.InnerText = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                //    //displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
                //}
                //else
                //{
                //    //Log.Information("Invalid signature, InputData={0}", Request.);
                //    return "Có lỗi xảy ra trong quá trình xử lý";
                //}
                return $"Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ {vnp_Amount}";
            }
            return "Có lỗi xảy ra trong quá trình xử lý";
        }
    }
}
