﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXWebApplication1.Models;

namespace DXWebApplication1.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View(OutlookDataProvider.GetMessages());
        }

        public ActionResult GridViewPartialView() {
            return PartialView("GridViewPartialView", OutlookDataProvider.GetMessages());
        }
    }
}