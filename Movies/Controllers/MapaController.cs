using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Movies.Controllers
{
    public class MapaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = BL.Cine.GetAll();
            ML.Cine cine = new ML.Cine();
            cine.Estadistica = new ML.Estadistica();

            if (result.Correct)
            {
                cine.Estadistica.Total = result.Objects;
                cine.Cines = result.Objects;
                ML.Estadistica porcentaje = BL.Cine.Porcentaje(cine);
             
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al mostrar los registros";
            }
            return View(cine);
        }
    }
}
