using Vuelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_vuelos.Controllers
{
    public class PasajeroController : Controller
    {
        // GET: Pasajero
        public ActionResult Index()
        {
            //if (Session.Count == 0)
            //{
               // return RedirectToAction("Index", "Login");
            //}
            List<Pasajero> pasajeros = Pasajero.GetAll();
            return View(pasajeros);
        }

        public ActionResult Registro(int id)
        {
            //if (Session.Count == 0)
            //{
               // return RedirectToAction("Index", "Login");
            //}
            Pasajero pasajero = Pasajero.GetById(id);
            return View(pasajero);
        }

        public ActionResult Guardar(int id, string nombre, string apellidoPaterno, string apellidoMaterno, string fechaNacimiento)
        {
            //if (Session.Count == 0)
            //{
              //  return RedirectToAction("Index", "Login");
            //}
            Pasajero.Guardar(id, nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            //if (Session.Count == 0)
            //{
                //return RedirectToAction("Index", "Login");
            //}
            Pasajero.Eliminar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Detalles(int id)
        {
            Pasajero pasajero = Pasajero.GetById(id);

            return View(pasajero);
        }
    }
}