using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Movies.Controllers
{
    public class FavoriteController : Controller
    {
        public ActionResult GetAll()
        {
            Models.Pelicula pelicula = new Models.Pelicula();
            pelicula.Peliculas = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.themoviedb.org/3/");
                var responseTask = client.GetAsync("account/19729015/favorite/movies?api_key=b5fbd729af526d11f05ecbc2d4eda15c&session_id=24098887a0003c0488c5a2b112e5ef79ecdb7b23");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic json = JObject.Parse(readTask.Result.ToString());

                    foreach (var res in json.results)
                    {
                        Models.Pelicula peliList = new Models.Pelicula();
                        peliList.idPelicula = res.id;
                        peliList.Portada = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + res.poster_path;
                        peliList.Nombre = res.original_title;
                        peliList.Sinopsis = res.overview;

                        pelicula.Peliculas.Add(peliList);
                    }
                }
            }
            return View(pelicula);
        }

        public ActionResult Delete(ML.Favorito favorito)
        {
            favorito.media_type = "movie";
            favorito.favorite = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responseTask = client.PostAsJsonAsync<ML.Favorito>("account/19729015/favorite?api_key=b5fbd729af526d11f05ecbc2d4eda15c&session_id=24098887a0003c0488c5a2b112e5ef79ecdb7b23", favorito);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se a eliminado de favoritos";
                    return View("Modal");
                }
            }
            return View("GetAll");
        }
    }
}
