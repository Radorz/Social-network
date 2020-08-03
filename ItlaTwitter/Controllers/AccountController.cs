using AutoMapper;
using EmailHandler;
using ViewModels.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Repository.Repository;

namespace ItlaTwitter.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UsuarioRepository _userRepository;
        private readonly Iemailsender _iemailsender;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public AccountController(UsuarioRepository userRepository, Iemailsender iemailsender, IMapper mapper,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _iemailsender = iemailsender;
            this._mapper = mapper;
            this.hostEnvironment = hostEnvironment;

        }
        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {


                return RedirectToAction("Index", "Usuarios");
            }
            ViewBag.mostrar = "none";


            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Index(LoginViewModel vm)
        {

            ViewBag.mostrar = "none";
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(vm.usuario, vm.Contraseña, false, false);

                if (resultado.IsLockedOut)
                {

                    ModelState.AddModelError("Error", "Esta cuenta ha sido Inactivada");
                    return View(vm);
                }
                if (resultado.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(vm.usuario);
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ViewBag.mostrar = "block";
                        return View(vm);


                    }
                    else { 
                    return RedirectToAction("Index", "Usuarios");
                    }

                }
                ModelState.AddModelError("UserOrPasswordInvalid", "El usuario o contraseña es invalido");
            }
            return View(vm);
                   }


        [HttpGet]

        public IActionResult Registrar()
        {
            if (User.Identity.IsAuthenticated)
            {


                return RedirectToAction("Index", "Usuarios");
            }
            ViewBag.mostrar = "none";


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegisterViewModel vm)
        {
            ViewBag.mostrar = "none";

            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = new IdentityUser { UserName = vm.usuario, Email = vm.Correo };
            await _userManager.CreateAsync(user, vm.Contraseña);
            vm.Telefono = string.Format("{0}-{1}", vm.indicetel, vm.Telefono).Trim();
            vm.Contraseña = PasswordEncryption(vm.Contraseña).Trim();
            var usuarioentity = _mapper.Map<Usuarios>(vm);
            string uniqueName = null;
            if (vm.photo != null)
            {

                var folderPath = Path.Combine(hostEnvironment.WebRootPath, "img/profile");
                uniqueName = Guid.NewGuid().ToString() + "_" + vm.photo.FileName;
                var filepath = Path.Combine(folderPath, uniqueName);

                if (filepath != null)
                {

                    vm.photo.CopyTo(new FileStream(filepath, mode: FileMode.Create));
                }
               
                
            }
            usuarioentity.Id = user.Id;

            usuarioentity.Foto = uniqueName;
            await _userRepository.Add(usuarioentity);

            string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            string confirmationLink = Url.Action("Activador", "Account", new { id = user.Id, token = confirmationToken }, protocol: HttpContext.Request.Scheme);
            var message = new Message(new string[] { vm.Correo }, "Activacion de Cuenta", "¡Bienvenido a Itla Twitter!, \n " + usuarioentity.Nombre + " " + usuarioentity.Apellido + " \n Gracias por ingresar a la familia ITLA, esperamos que disfrutes de esta aplicacion y puedas sacarle todo el potencial que tiene escondio \n Este es el link para activar la cuenta del Usuario " + usuarioentity.Usuario + ":" + " \n" + confirmationLink);
            await _iemailsender.SendMailAsync(message);
            ViewBag.mostrar = "block";

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Activador(string id, string token)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var Usuario = await _userRepository.GetbyIdString(id);
            var useridentity = await _userManager.FindByIdAsync(id);
            if (useridentity == null)
            {
                return NotFound();
            }
            Usuario.Estado = "Activo".Trim();
            await _userManager.ConfirmEmailAsync(useridentity, token);
            await _userRepository.Update(Usuario);
            
            


            return  View();
        }
        [HttpGet]
        public IActionResult Recuperar()
        {
            if (User.Identity.IsAuthenticated)
            {


                return RedirectToAction("Index", "Usuarios");
            }

            ViewBag.mostrar = "none";

            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Recuperar(RecuperarViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userRepository.GetbyName(vm.usuario);
            
            var useridentity = await _userManager.FindByNameAsync(vm.usuario);
            Random rnd = new Random();
            int nuevacontraseña = rnd.Next(100000, 999999);
            var token = await _userManager.GeneratePasswordResetTokenAsync(useridentity);
            await _userManager.ResetPasswordAsync(useridentity, token, nuevacontraseña.ToString());
            user.Contraseña = PasswordEncryption(Convert.ToString( nuevacontraseña));
            await _userRepository.Update(user);
            var message = new Message(new string[] {user.Correo}, "Recuperacion de Cuenta", "Hemos sidos notificados que que has perdido tu contraseña para acceder a ITLA Twiiter \nEsta es tu nueva Contraseña" + user.Usuario+ ":\n" + nuevacontraseña); ;
            await _iemailsender.SendMailAsync(message);
            
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }
        public IActionResult Redirect()
        {
            
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public async Task <IActionResult> VerifyUser(string usuario)
        {
            var user = await _userRepository.GetbyName(usuario );
            if (user != null)
            {
                return Json($"El Usuaurio {usuario} ya esta en uso.");
            }

            return Json(true);
        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ReturnUser(string usuario)
        {
            var user = await _userRepository.GetbyName(usuario);
            if (user != null)
            {
                return Json(true);

            }

            return Json($"Este Usuario {usuario} no existe");
        }
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index", "Usuarios");

        }
        private string PasswordEncryption (string password)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {

                byte[] bytes = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (Byte t in bytes)
                {

                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }

        }


    }

}
