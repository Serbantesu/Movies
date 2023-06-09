using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetByUsername(string userName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"GetByUserName {userName}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;
                        usuario.Email = query.Email;

                        result.Object = usuario;
                        result.Correct = true; 
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al Obtener el Registro";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "No se encuentra el registro" + ex;
            }

            return result;
        }

        public static ML.Result UsuarioAdd(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.UserName}', '{usuario.Email}', @Password", new SqlParameter("Password", usuario.Password));
                    if(query != 0)
                    {
                        result.Correct = true; 
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un error al agregar al Usuario" + ex;
            }

            return result;
        }

    }
}
