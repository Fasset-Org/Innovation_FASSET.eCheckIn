using System.Web;
using System.Web.Mvc;

namespace FASSET.QRCode_eCheckIn
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
