using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZalidaneSite.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("GeneralError");
        }
        
        public ViewResult NotFound(HandleErrorInfo exception)
        {
            Response.StatusCode = 200;
            return View("NotFound", exception);
        }

        public ViewResult Forbidden(HandleErrorInfo exception)
        {
            Response.StatusCode = 200;
            return View("Forbidden", exception);
        }

        public ViewResult InternalServer(HandleErrorInfo exception)
        {
            Response.StatusCode = 200;
            return View("InternalServer", exception);
        }

        public ViewResult GeneralError()
        {
            Response.StatusCode = 200;
            return View("GeneralError");
        }
    }
}