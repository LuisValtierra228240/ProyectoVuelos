using Vuelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_vuelos.Controllers
{
    public class RegistroController : Controller
    {
        // GET: Registro
        public ActionResult Index()
        {
            if (Request.Method.ToString() == "POST")
            {
                string nombre = Request.Form["nombre_completo"];
                string correo = Request.Form["correo"];
                string contra = Request.Form["contra"];

                if ( !(nombre.Length < 0 || correo.Length < 0 || contra.Length < 0)) //Se comprueba que se recibieron todos los campos
                {
                    Usuario u = new Usuario();
                    u.Id = 0;
                    u.Correo = correo;
                    u.Nombre = nombre;
                    u.Contra = contra;

                    if (!Usuario.existe(correo))
                    {
                        if (Usuario.Guardar(u) != -1)
                        {
                            //Response.Write("Guardado correctamente");
                            return RedirectToAction("Index", "Login");
                        }
                        else //En caso de que haya error al guardar
                        {
                            //Response.Write("<script>document.onload=alert('Error al guardar!')</script>");
                        }
                    } else
                    {
                        //Response.Write("<script>document.onload=alert('Ya hay un usuario registrado con ese correo!')</script>");

                    }
                }
                else //En caso de que haya un campo vacio
                {
                    //Response.Write("<script>document.onload=alert('Rellena todos lo campos!')</script>");
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