//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using ProyectoColProfesionales.Recursos;
//using ProyectoColProfesionales.Servicios.Contrato;
//using System.Security.Claims;

//namespace ProyectoColProfesionales.Controllers
//{
//    public class UserController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using ProyectoColProfesionales.Recursos;
using ProyectoColProfesionales.Servicios.Contrato;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ProyectoColProfesionales.Models.DB1;

namespace ProyectoColProfesionales.Controllers
{

    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly DBColProfessionalContext _context;
        //private readonly IPasswordHasher<User> _passwordHasher;


        public UserController(IUserService userService, DBColProfessionalContext context)
        {
            _userService = userService;
            _context = context;
            // _passwordHasher = passwordHasher;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewData["Mensaje"] = "Ingrese su usuario o contraseña";
                return View();
            }
            User user_encontrado = await _userService.GetUser(username, Utilidades.EncriptarClave(password));
            if (user_encontrado == null)
            {
                ViewData["Mensaje"] = "Usuario NO encontrado";
                return View();
            }
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user_encontrado.UserName)
            };


            claims.Add(new Claim(ClaimTypes.Role, user_encontrado.Role));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                //IsPersistent = false,
            };
            //await HttpContext.SignInAsync(
            //    CookieAuthenticationDefaults.AuthenticationScheme,
            //    new ClaimsPrincipal(claimsIdentity),
            //    properties
            //    );
            await HttpContext.SignInAsync(principal, properties);

            return RedirectToAction("Index", "Home");
        }
        // Método para cerrar sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Cerrar sesión

            // Redireccionar al usuario a la página de inicio o a donde prefieras
            return RedirectToAction("Login", "User");
        }
    }
}
