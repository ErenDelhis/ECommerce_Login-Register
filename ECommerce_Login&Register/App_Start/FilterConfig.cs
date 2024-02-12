using System.Web;
using System.Web.Mvc;

namespace ECommerce_Login_Register
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
