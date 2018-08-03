using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Mohejj.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public QRCodeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("{custId}")]
        public async Task<IActionResult> GenerateQrCode(int custId)
        {
            string url = "http://mohejj.azurewebsites.net/api/Payment/Pay/" + custId + "/30/true";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            //Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));

            // Saving Image to server
            string wwwrootPath = _hostingEnvironment.WebRootPath;
            string fileName = "QRCode_" + custId + ".jpg";
            string path = wwwrootPath + @"\" + fileName;
            FileInfo file = new FileInfo(Path.Combine(wwwrootPath, fileName));

            if (file.Exists)
            {
                file.Delete();
                //file = new FileInfo(Path.Combine(wwwrootPath, fileName));
            }
            else
            {
                qrCodeImage.Save(string.Format(path, "image /jpeg"));

                var memory = new MemoryStream();
                using (var stream = new FileStream(string.Format(path), FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
            }
            return Content(string.Format(path, "application/json"));

        }
    }
}