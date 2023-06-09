using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Movies.Controllers
{
    public class UsuarioController : Controller
    {
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

            if(usuario.Nombre != null)
            {
                usuario.Password = passwordHash;
                ML.Result result = BL.Usuario.UsuarioAdd(usuario);
                return View();
            }
            else
            {
                ML.Result result = BL.Usuario.GetByUsername(usuario.UserName);
                usuario = (ML.Usuario)result.Object;

                if (usuario.Password.SequenceEqual(passwordHash))
                {
                    return RedirectToAction("GetAll", "Popular");
                }
            }
            return View();
        }
    }
    }

