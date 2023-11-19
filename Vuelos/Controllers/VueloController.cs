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

        // GET: Vuelo/Details/5
        public ActionResult Detalles(int id)
        {

            dynamic model = new ExpandoObject();

            model.Vuelo = Vuelo.GetById(id);

            model.porcentaje = (DateTime.Now - model.Vuelo.FechaPartida).TotalSeconds*100/(model.Vuelo.FechaLlegada - model.Vuelo.FechaPartida).TotalSeconds;

            List<Boleto> Boletos = Boleto.GetByVuelo(id);

            List<int> numeros = new List<int>();

            foreach(Boleto b in Boletos){
                numeros.Add(b.Asiento);
            }

            model.Boletos = numeros;

            if(model.porcentaje < 0){
                model.porcentaje = 0;
            } else if(model.porcentaje > 100){
                model.porcentaje = 100;
            }

            return View(model);
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

        public ActionResult Guardar(int id, string origen, string destino, int idAvion, string capacidad, string fechaPartida, string fechaLlegada)
        {
            //if (Session.Count == 0)
            //{
                //return RedirectToAction("Index", "Login");
            //}
            Vuelo.Guardar(id, origen, destino, idAvion, capacidad, DateTime.Parse(fechaPartida), DateTime.Parse(fechaLlegada));
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

        public ActionResult ComprarBoleto(int id)
        {
            dynamic model = new ExpandoObject();
            model.Pasajeros = Pasajero.GetAll();

            List<Vuelo> vuelos = new List<Vuelo>
            {
                Vuelo.GetById(id)
            };

            model.Vuelos = vuelos;

            return View("/Views/Boleto/Registro.cshtml", model);
        }

        public ActionResult VerBoletos(int id)
        {
            List<Boleto> boletos = Boleto.GetByVuelo(id);

            return View("/Views/Boleto/Index.cshtml", boletos);
        }
    }
}