using System.Web;
using System.Web.Mvc;

namespace Lab_04_JosueHigueros_PabloAlvarado
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
