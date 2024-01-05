using GenerateQRCode.Models;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace GenerateQRCode.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult CreateQRCode()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateQRCode(QRCodeModel qRCode)
        {
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(qRCode.QRCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            ViewBag.QrCodeUri = QrUri;
            string fileName = "QRCode_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            TempData["QrCodeDownloadLink"] = "data:image/png;base64," + Convert.ToBase64String(BitmapArray);
            TempData["QrCodeFileName"] = fileName;

            return View();
        }
    }

    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}