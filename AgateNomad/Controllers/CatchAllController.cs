using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgateNomad.Controllers
{
    public class CatchAllController : Controller
    {
        //
        // GET: /CatchAll/

        public ActionResult Index()
        {
            return View();
        }

    }
}
