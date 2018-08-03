using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mohejj.Models;
using Newtonsoft.Json;
using Nexmo.Api;

namespace Mohejj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private static IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _accountSid;
        private readonly string _authToken;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        public PaymentController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _accountSid = _configuration["AppSettings:accountSid"];
            _authToken = _configuration["AppSettings:authToken"];
        }

        /// <summary>
        /// Payment Method
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="amount"></param>
        /// <param name="verified"></param>
        /// <returns></returns>
        [HttpPost("[action]/{custId}/{amount}/{verified}")]
        public IActionResult Pay(int custId, int amount, bool verified)
        {
            string wwwrootPath = _hostingEnvironment.WebRootPath;
            string path = string.Format(wwwrootPath + @"\Data\customers.json");

            //using (StreamReader r = new StreamReader(string.Format(wwwrootPath + @"\Data\customers.json")))
            //{
            //    string json = r.ReadToEnd();
            //    List<Customer> items = JsonConvert.DeserializeObject<List<Customer>>(json);
            //    var customer = items.First(x => x.Id == custId);
            //    customer.Credit -= amount;
            //    var jsonobj = JsonConvert.SerializeObject(items);
            //    FileInfo file = new FileInfo(path);

            //    if (file.Exists)
            //    {
            //        file.Delete();
            //    }

            //    System.IO.File.WriteAllText(path, jsonobj);
            //}

            // Send Message
            var client = new Client(new Nexmo.Api.Request.Credentials
            {
                ApiKey = _accountSid,
                ApiSecret = _authToken
            });

            client.SMS.Send(new SMS.SMSRequest
            {
                from = "Acme Inc",
                to = "+966541700674",
                text = "Payment Done Successfully!!"
            });
            return Ok("success");
        }
    }
}