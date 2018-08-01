using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QRCoder;

namespace Mohejj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private static IConfiguration _configuration;

        public QRCodeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{custId}")]
        public IActionResult GenerateQrCode(string custId)
        {
            string url = "http://www.google.com";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            // Saving Image to server
            Response.ContentType = "image/jpeg";
            string folderpath = Url.Content("~/") + "QrCodes";
            string fileName = "QRCode_" + custId + ".jpg";

            qrCodeImage.Save(string.Format("~/QrCode/" + custId + ".jpg", "image /jpeg"));

            return Ok();
        }
    }
}