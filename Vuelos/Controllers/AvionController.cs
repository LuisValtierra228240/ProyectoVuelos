using Vuelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Vuelos.Controllers
{
    public class AvionController : Controller
    {
        // GET: Avion
        public ActionResult Index()
        {
            List<Avion> aviones = Avion.GetAll();
            return View(aviones);
        }

        public ActionResult Registro(int id)
        {
            Avion avion = Avion.GetById(id);
            return View(avion);
        }

        public ActionResult Guardar(int id, string placa, string nombre)
        {
            Avion.Guardar(id, placa, nombre);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            Avion.Eliminar(id);
            return RedirectToAction("Index");
        }

    }
}