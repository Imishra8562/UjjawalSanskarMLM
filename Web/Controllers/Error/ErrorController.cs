using BusinessLayer;
using BusinessLayer.Interface;
using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error404()
        {
            return View("Error");
        }
    }
}