using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BL
{    
    public class Usuario
    {
        //private IConfiguration _configuration;
        //private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        //public Usuario(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        //{
        //    _configuration = configuration;
        //    _hostingEnvironment = hostingEnvironment;
        //}

        public static ML.Result GetByUsername(string Email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"GetByUserName @Email", new SqlParameter("Email", Email)).AsEnumerable().FirstOrDefault();
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
                    if(query > 0)
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

        public static ML.Result PassUpdate(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JcervantesCinesContext context = new DL.JcervantesCinesContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"PasswordUpdate '{usuario.Email}', @Password", new SqlParameter("Password", usuario.Password));
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrio un errro al actualizar la contraseña" + ex;
            }
            return result;
        }

        //public static ML.Result SendEmail(ML.Usuario usuario)
        //{
        //    ML.Result result = BL.Usuario.GetByUsername(usuario.Email);
        //    usuario = (ML.Usuario)result.Object;

        //    string emailOrigen = _configuration["EmailOrigen"];
        //    string appPassword = _configuration["AppPassword"];
        //    string urlLogin = _configuration["UrlLogin"];
        //    string template = _configuration["HTMLTemplate"];

        //    if (email == usuario.Email)
        //    {
        //        MailMessage mailMessage = new MailMessage(usuario.Email, email, "Recuperar Contraseña", "<p>Correo para recuperar la Contarseña</p>");
        //        mailMessage.IsBodyHtml = true;
        //        string contenidoHTML = System.IO.File.ReadAllText(template);
        //        mailMessage.Body = contenidoHTML;

        //        string url = urlLogin + HttpUtility.UrlEncode(email);
        //        mailMessage.Body = mailMessage.Body.Replace("{Url}", url);

        //        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        //        smtpClient.EnableSsl = true;
        //        smtpClient.UseDefaultCredentials = false;
        //        smtpClient.Port = 587;
        //        smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, appPassword);

        //        smtpClient.Send(mailMessage);
        //        smtpClient.Dispose();

        //    }
        //    return result;
        //}

    }
}
