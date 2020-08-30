using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_MvcOA_WebApp.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }
		public ActionResult SearchContent()
		{
			if (string.IsNullOrEmpty(Request["btnSearch"]))
			{

			}
			else
			{

			}
			return View();
		}
    }
}
