namespace Movies.Models
{
    public class Pelicula
    {
        public int idPelicula { get; set; }
        public string Portada { get; set; }
        public string Nombre { get; set; }
        public string Sinopsis { get; set; }

        public string media_type { get; set; }       
        public bool favorite { get; set; }
        public List<object> Peliculas { get; set; }
    }
}
