using Vuelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_vuelos.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Request.Method.ToString() == "POST")
            {
                string correo = Request.Form["correo"];
                string contra = Request.Form["contra"];

                if (!(correo.Length > 0 || contra.Length > 0)) //Se comprueba que se recibieron todos los campos
                {
                    Usuario u = new Usuario();
                    u.Correo = correo;
                    u.Contra = contra;


                    Usuario datosUsuario = Usuario.validarUsuario(u);
                    if (datosUsuario != null) // ValidarUsuario retorna el objecto usuario con todos los datos o null en caso de no encontrarlo 
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //Response.Write("<script>document.onload=alert('Creadenciales incorrectas!')</script>");
                    }
                } else
                {
                    //Response.Write("<script>document.onload=alert('Rellena todos los campos!')</script>");
                }
            }

            //if (Session.Count > 0)
            //{
                //return RedirectToAction("Index", "Home");
            //}
            return View();
        }
    }
}