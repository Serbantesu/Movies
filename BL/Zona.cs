using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    var query = context.Zonas.FromSqlRaw("ZonaGetAll").ToList();
                    result.Objects = new List<object>();

                    foreach (var res in query)
                    {
                        ML.Zona zona = new ML.Zona();
                        zona.IdZona = res.IdZona;
                        zona.Nombre = res.Nombre;

                        result.Objects.Add(zona);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al realizar la consulta" + ex;
            }

            return result;
        }

    }
}
