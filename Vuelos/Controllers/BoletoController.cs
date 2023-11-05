using Vuelos.Entidades;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;


namespace Proyecto_vuelos.Controllers
{
    public class BoletoController : Controller
    {
        // GET: Boleto
        public ActionResult Index()
        {
            List<Boleto> boletos = Boleto.GetAll();

            return View(boletos);
        }

        // GET: Boleto/Details/5
        public ActionResult Detalles(int id)
        {
            Boleto boleto = Boleto.GetById(id);
            return View(boleto);
        }

        // GET: Boleto/Registro
        public ActionResult Registro(int id)
        {

            if (id > 0) //Editar
            {
                return View();
            
            } 
            else // Crear nuevo
            {
                dynamic model = new ExpandoObject();
                model.Pasajeros = Pasajero.GetAll();
                model.Vuelos = Vuelo.GetAll();
                return View(model);
            }

        }

        // POST: Boleto/Guardar
        [HttpPost]
        public ActionResult Guardar(FormCollection collection)
        {
            try
            {
                Boleto boleto = new Boleto();
                boleto.Id = 0;
                boleto.Pasajero = Pasajero.GetById(int.Parse(collection["pasajero_id"]));
                int vueloid = int.Parse(collection["vuelo_id"]);
                boleto.Vuelo = Vuelo.GetById(vueloid);

                Boleto.Guardar(boleto);
                

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Eliminar(int id)
        {
            Boleto.Eliminar(id);
            return RedirectToAction("Index", "Boleto");
        }
    }
}
