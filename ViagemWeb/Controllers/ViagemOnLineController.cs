using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViagemWeb.Db;
using ViagemWeb.Models;

namespace ViagemWeb.Controllers
{
    public class ViagemOnLineController : Controller
    {
        // GET: ViagemOnLine
        public ActionResult Incio()
        {
            return View();
        }

        public ActionResult Destino()
        {
            using (var db = new ViagemOnDb())
            {
                return View(db.Destino.ToArray());

            }
        }
    }
}