﻿using Vuelos.Entidades;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Collections;


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

                List<Vuelo> vuelos = Vuelo.GetAll();
                List<Vuelo> vuelosDisponibles = new List<Vuelo>();

                foreach(Vuelo v in vuelos){
                    double porcentaje = (DateTime.Now - v.FechaPartida).TotalSeconds*100/(v.FechaLlegada - v.FechaPartida).TotalSeconds;
                    
                    if (porcentaje <= 0){
                        vuelosDisponibles.Add(v);
                    }

                }

                model.Vuelos = vuelosDisponibles;

                return View(model);
            }

        }

        public ActionResult Imprimir(int id)
        {
            Boleto boleto = Boleto.GetById(id);
            return View(boleto);
        }

        // POST: Boleto/Guardar
        [HttpPost]
        public ActionResult Guardar(IFormCollection collection)
        {
            try
            {
                Boleto boleto = new Boleto();
                boleto.Id = 0;
                boleto.Pasajero = Pasajero.GetById(int.Parse(collection["pasajero_id"]));
                int vueloid = int.Parse(collection["vuelo_id"]);
                boleto.Vuelo = Vuelo.GetById(vueloid);
                double porcentaje = (DateTime.Now - boleto.Vuelo.FechaPartida).TotalSeconds*100/(boleto.Vuelo.FechaLlegada - boleto.Vuelo.FechaPartida).TotalSeconds;

                List<Boleto> Boletos = Boleto.GetByVuelo(vueloid);

                List<int> numeros = new List<int>();

                foreach(Boleto b in Boletos){
                    numeros.Add(b.Asiento);
                }

                for(int i = 1; i<= boleto.Vuelo.Capacidad;i++){
                    if(!numeros.Contains(i)){
                        boleto.Asiento = i;
                        break;
                    }
                }

                dynamic model = new ExpandoObject();
                model.Pasajeros = Pasajero.GetAll();
                model.Vuelos = Vuelo.GetAll();
    
                if (porcentaje > 0){
                    Response.StatusCode = 409;
                    ViewBag.ErrorMessage = "El vuelo ya no está disponible.";

                    return View("Registro", model);
                }
                
                if (Boleto.PasajeroTieneBoletoEnVuelo(boleto.Pasajero.Id, vueloid))
                {
                    Response.StatusCode = 409;
                    ViewBag.ErrorMessage = "El pasajero ya tiene un boleto para este vuelo.";
                    return View("Registro", model);
                }

                if (Vuelo.GetNumeroBoletos(vueloid) >= boleto.Vuelo.Capacidad) {
                    Response.StatusCode = 409;
                    ViewBag.ErrorMessage = "El vuelo se encuentra lleno.";

                    return View("Registro", model);
                }

                boleto.Id = Boleto.Guardar(boleto);
                

                return RedirectToAction("Detalles", new {id = boleto.Id});
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
