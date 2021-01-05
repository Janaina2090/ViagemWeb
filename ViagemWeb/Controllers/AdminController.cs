using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViagemWeb.Db;
using ViagemWeb.Models;

namespace ViagemWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult DestinoNovo()
        {
            return View();
        }


        private string GravarFoto(HttpRequestBase Request)
        {
            string nome = Path.GetFileName(Request.Files[0].FileName);
            string pastaVirtual = "~/imagens";
            string pathVirtual = pastaVirtual + "/" + nome;
            string pathFisico = Request.MapPath(pathVirtual);
            Request.Files[0].SaveAs(pathFisico);
            return nome;
        }

        private const string ActionDestinoListagem = "DestinoListagem";

        private ViagemOnDb ObterContext()
        {
            return new ViagemOnDb();
        }

        [HttpPost]
        public ActionResult DestinoNovo(Destino destino)
        {
            //Se alguma validação falhou...
            if (!ModelState.IsValid)
            {
                return View(destino);
            }
            // Foto é obrigatória
            if (Request.Files.Count == 0 ||
            Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("",
                "É necessário enviar uma Foto»");
            return View(destino);
            }
            //Grava
            try
            {
                //Grava a foto e retorna o nome
                destino.Foto = GravarFoto(Request);
                using (var db = ObterContext())
                {
                    db.Destino.Add(destino);
                    db.SaveChanges();
                    return RedirectToAction(ActionDestinoListagem);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(destino);
            }
        }



        public ActionResult DestinoListagem()
        {
            List<Destino> lista = null;
            using (var db = ObterContext())
            {
                lista = db.Destino.ToList();
            }
            return View(lista);
        }

        public ActionResult DestinoAlterar(int id)
        {
            using (var db = ObterContext())
            {
                var destino = db.Destino.Find(id);
                if (destino != null )
                {
                    return View(destino);
                }
                return RedirectToAction(ActionDestinoListagem);
            }
        }

        [HttpPost]
        public ActionResult DestinoAlterar (Destino destino)
        {
            if (ModelState.IsValid)
            {
                using (var db = ObterContext())
                {
                    var destinoOriginal = db.Destino.Find(destino.DestinoId);

                    if (destinoOriginal != null)
                    {
                        destinoOriginal.Nome = destino.Nome;
                        destinoOriginal.Cidade = destino.Cidade;
                        destinoOriginal.Pais = destino.Pais;

                        if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0 )
                        {
                            destinoOriginal.Foto = GravarFoto(Request);
                        }

                        db.SaveChanges();
                        return RedirectToAction(ActionDestinoListagem);

                    }
                }
            }

            return View();
        }

        public ActionResult DestinoExcluir(int id)
        {
            using (var db = ObterContext())
            {
                var destino = db.Destino.Find(id);
                if (destino != null)
                {
                    return View(destino);
                }
            }

            return RedirectToAction(ActionDestinoListagem);
        }
        [HttpPost]
        public ActionResult DestinoExcluir( int id, FormCollection form)
        {
            using (var db = ObterContext())
            {
                var destino = db.Destino.Find(id);
                if (destino != null)
                {
                    db.Destino.Remove(destino);
                    db.SaveChanges();

                }
            }

            return RedirectToAction(ActionDestinoListagem);
        }

                      

   




















        
    }
}
