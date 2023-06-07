using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int IdZona { get; set; }
        public decimal Ventas { get; set; }
        public decimal Total { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal PorcentajeZona { get; set; }
        public List<object> Cines { get; set; }
        public ML.Zona Zona { get; set; }
        public ML.Estadistica Estadistica { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
    }
}
