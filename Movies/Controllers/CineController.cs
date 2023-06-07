using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Controllers
{
    public class CineController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = BL.Cine.GetAll();
            ML.Cine cine = new ML.Cine();

            if (result.Correct)
            {
                cine.Cines = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al mostrar los registros";
            }
            return View(cine);
        }

        [HttpGet]
        public ActionResult Form(int? idCine)
        {
            ML.Zona zona = new ML.Zona();
            ML.Result resultZona = BL.Zona.GetAll();

            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();

            cine.Zona.Zonas = resultZona.Objects;

            if (resultZona.Correct)
            {
                cine.Zona.Zonas = resultZona.Objects;
            }
            if(idCine == null)
            {
                return View(cine);
            }
            else
            {
                ML.Result result = BL.Cine.GetById(idCine.Value);
                if (result.Correct)
                {
                    cine = (ML.Cine)result.Object;

                    cine.Zona.Zonas = resultZona.Objects;

                    return View(cine);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al realzar la consulta del Cine" + result.ErrorMessage;
                    return View("Modal");
                }
            }            
        }

        [HttpPost] //Sin servicios
        public ActionResult Form(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            if (cine.IdCine == 0 || cine.IdCine == null)
            {
                result = BL.Cine.Add(cine);

                if (result.Correct)
                {
                    ViewBag.Message = "Se inserto correctamente el Cine";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al insertar el Cine" + result.ErrorMessage;
                }
            }
            else
            {
                result = BL.Cine.Update(cine);


                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo correctamente el registro de la Cine seleccionado";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el registro de la Cine seleccionado" + result.ErrorMessage;
                }
            }
            return View("Modal");
        }

        [HttpPost]
        public ActionResult Delete(int idCine)
        {
            ML.Cine cine = new ML.Cine();
            cine.IdCine = idCine;
            BL.Cine.Delete(cine);
            ViewBag.Message = "Cine borrado con exito";

            return View("Modal");
        }
    }
}
