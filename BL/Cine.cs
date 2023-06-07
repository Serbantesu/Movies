using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    var query = context.Cines.FromSqlRaw("CineGetAll").ToList();
                    result.Objects = new List<object>();

                    foreach (var res in query)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.Zona = new ML.Zona();

                        cine.IdCine = res.IdCine;
                        cine.Nombre = res.Nombre;
                        cine.Direccion = res.Direccion;

                        cine.Zona.IdZona = (int)res.IdZona;
                        cine.Zona.Nombre = res.NombreZona;

                        cine.Ventas = (decimal)res.Ventas;
                        cine.Latitud = (float)res.Latitud;
                        cine.Longitud = (float)res.Longitud;
                        result.Objects.Add(cine);
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

        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Nombre}', '{cine.Direccion}', {cine.Zona.IdZona}, {cine.Ventas}, {cine.Latitud}, {cine.Longitud}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al insertar el resgistro" + ex;
            }

            return result;
        }

        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.Nombre}', '{cine.Direccion}', {cine.Zona.IdZona}, {cine.Ventas}, {cine.Latitud}, {cine.Longitud} ");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al actualizar el registro seleccionado" + ex;
            }
            return result;
        }

        public static ML.Result Delete(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineDelete {cine.IdCine}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al eliminar el resgistro seleccionado" + ex;
            }
            return result;
        }

        public static ML.Result GetById(int idCine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetById {idCine}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.Zona = new ML.Zona();

                        cine.IdCine = query.IdCine;
                        cine.Nombre = query.Nombre;
                        cine.Direccion = query.Direccion;


                        cine.Zona.IdZona = (int)query.IdZona;
                        cine.Zona.Nombre = query.NombreZona;

                        cine.Ventas = (decimal)query.Ventas;
                        cine.Latitud = (float)query.Latitud;
                        cine.Longitud = (float)query.Longitud;
                        result.Object = cine;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al Obtener el registro seleccionado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al mostrar el registro seleccionado" + ex;
            }
            return result;
        }

        public static ML.Estadistica Porcentaje(ML.Cine cine)
        {
            ML.Result res = new ML.Result();
            ML.Estadistica result = new ML.Estadistica();
            cine.Total = 0;
            cine.PorcentajeZona = 0;

            foreach (ML.Cine total in cine.Estadistica.Total)
            {
                cine.Total = cine.Total + total.Ventas;
            }
            res.Objects = new List<object>();            
            foreach (ML.Cine porcentaje in cine.Estadistica.Total)
            {                
                if (porcentaje.Zona.IdZona == 1 || porcentaje.Zona.IdZona == 2 || porcentaje.Zona.IdZona == 3 || porcentaje.Zona.IdZona == 4 || porcentaje.Zona.IdZona == 5)
                {                             
                    porcentaje.Porcentaje = (porcentaje.Ventas / cine.Total) * 100;
                    cine.PorcentajeZona = cine.PorcentajeZona + porcentaje.Porcentaje;                                       
                }                
                res.Objects.Add(porcentaje);
            }

            return result;
        }
    }
}
