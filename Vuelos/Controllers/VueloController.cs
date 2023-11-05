using Vuelos.Entidades;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_vuelos.Controllers
{
    public class VueloController : Controller
    {
        // GET: Vuelo
        public ActionResult Index()
        {
            //if (Session.Count == 0)
            //{
                //return RedirectToAction("Index", "Login");
            //}
            List<Vuelo> vuelos = Vuelo.GetAll();
            return View(vuelos);
        }

        public ActionResult Registro(int id)
        {
            //if (Session.Count == 0)
            //{
                //return RedirectToAction("Index", "Login");
            //}
            dynamic model = new ExpandoObject();
            model.Vuelo = Vuelo.GetById(id);
            model.aviones = Avion.GetAll();
            return View(model);
        }

        public ActionResult Guardar(int id, string origen, string destino, int idAvion, string capacidad, string fecha)
        {
            //if (Session.Count == 0)
            //{
                //return RedirectToAction("Index", "Login");
            //}
            //Vuelo.Guardar(id, origen, destino, idAvion, capacidad, DateTime.Parse(fecha));
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            //if (Session.Count == 0)
            //{
               // return RedirectToAction("Index", "Login");
            //}
            Vuelo.Eliminar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Pasajeros(int id)
        {
           // if (Session.Count == 0)
            //{
                //return RedirectToAction("Index", "Login");
           // }
            List<Pasajero> pasajeros = Vuelo.GetPasajeros(id).pasajeros;

            return View("/Views/Pasajero/Index.cshtml", pasajeros);
        }
    }
}