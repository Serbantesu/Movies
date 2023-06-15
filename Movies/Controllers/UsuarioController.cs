using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;

namespace Movies.Controllers
{
    public class UsuarioController : Controller
    {
        private IConfiguration _configuration;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ML.Usuario usuario, string password)
        {
            //Crear una instancia del algoritmo de hash bcrypt
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[20], 10000, HashAlgorithmName.SHA256);
            //Obtener el hash de la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            if (usuario.Nombre != null)
            {
                usuario.Password = passwordHash;
                ML.Result result = BL.Usuario.UsuarioAdd(usuario);

                ViewBag.Message = "Te has registrado correctamente";
                return View("Modal");
            }
            else
            {
                ML.Result result = BL.Usuario.GetByUsername(usuario.Email);
                usuario = (ML.Usuario)result.Object;

                if (usuario.Password.SequenceEqual(passwordHash))
                {
                    return RedirectToAction("GetAll", "Popular");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult CambiarPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CambiarPassword(string email)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Email = email;
            ML.Result result = BL.Usuario.GetByUsername(email);            
            usuario = (ML.Usuario)result.Object;

            string emailOrigen = _configuration["EmailOrigen"];
            string appPassword = _configuration["AppPassword"];
            string urlLogin = _configuration["UrlLogin"];
            string template = _configuration["HTMLTemplate"];

            if (email == usuario.Email)
            {
                MailMessage mailMessage = new MailMessage(usuario.Email, email, "Recuperar Contraseña","<p>Correo para recuperar la Contarseña</p>");
                mailMessage.IsBodyHtml = true;
                string contenidoHTML = System.IO.File.ReadAllText(template);
                mailMessage.Body = contenidoHTML;

                string url = urlLogin + HttpUtility.UrlEncode(email);
                mailMessage.Body = mailMessage.Body.Replace("{Url}", url);

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");               
                smtpClient.EnableSsl = true;                
                smtpClient.UseDefaultCredentials = false;                
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, appPassword);

                smtpClient.Send(mailMessage);
                smtpClient.Dispose();

                ViewBag.Modal = "show";
                ViewBag.Message = "Se ha enviado el correo de recuperacion a tu correo electronico";
            }
            return View("Modal");
        }

        [HttpGet]
        public ActionResult NewPassword(string email)
        {                   
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(ML.Usuario usuario, string password)
        {
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[20], 10000, HashAlgorithmName.SHA256);
            //Obtener el hash de la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            usuario.Password = passwordHash;
            ML.Result result = BL.Usuario.PassUpdate(usuario);

            if (result.Correct)
            {
                ViewBag.Message = "La contraseña se a actualizado correctamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un eror al actualizar la contarseña" + result.ErrorMessage;
            }
            return View("Modal");
        }
    }
}

