using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FASSET.QRCode_eCheckIn.Controllers
{
    public class GeoLocationController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<ActionResult> GetGeolocation()
        {
            var response = await client.GetStringAsync("http://ip-api.com/json");
            return Content(response, "application/json");
        }
    }
}