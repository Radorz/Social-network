using AutoMapper;
using ViewModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Migrations;
using Repository.Repository;

namespace ItlaTwitter.Controllers
{
    [Authorize]
    public class AmigosController : Controller
    {
       

            private readonly itlatwitterContext _context;
            private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PublicacionesRepository _publicacionesRepository;
        private readonly AmigosRepository _amigosRepository;
        private readonly ComentariosRepository _comentariosRepository;
        private readonly RepliesRepository _repliesRepository;



        public AmigosController(itlatwitterContext context, IMapper mapper, UserManager<IdentityUser> userManager, RepliesRepository repliesRepository,
            PublicacionesRepository publicacionesRepository, AmigosRepository amigosRepository, ComentariosRepository comentariosRepository)
            {
                _context = context;
            this._mapper = mapper;
            _userManager = userManager;
            _publicacionesRepository = publicacionesRepository;
            _amigosRepository = amigosRepository;
            _comentariosRepository = comentariosRepository;
            _repliesRepository = repliesRepository;


        }
        [HttpGet]

        public async Task<IActionResult> Index()
        { 
            var userTokens = await _userManager.FindByNameAsync(User.Identity.Name);
         var vm=   await _publicacionesRepository.GetAllpubFirends(userTokens);

            return View(vm);
        }
        [HttpPost]

        public async Task<IActionResult> reply(AmigosViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var newereply = new RespuestasCom();
                newereply.Idcomentario = vm.newreply.Idcomentario;
                newereply.Idusuario = user.Id;
                newereply.Contenido = vm.newreply.Contenido.Trim();
                await _repliesRepository.Add(newereply);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> AñadirA(AmigosViewModel vms)
        {
            if (ModelState.IsValid)
            {
                var añadir = await _userManager.FindByNameAsync(vms.Añadiruser.Trim());

                var añadiramigo = new Amigos();
                añadiramigo.Idenvia = vms.idusuario;
                añadiramigo.Idrecibe = añadir.Id;

                await _amigosRepository.Add(añadiramigo);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> BorrarAmix(AmigosViewModel vms)
        {
            if (ModelState.IsValid)
            {
                var Borrar = await _context.Amigos.FirstOrDefaultAsync(a => a.Idenvia == vms.idusuario && a.Idrecibe == vms.BorrarAmix);

                if (Borrar != null)
                {
                  await  _amigosRepository.Deletefriend(Borrar);
                }

            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public async Task<IActionResult> Comentar(AmigosViewModel vms)
        {
            if (ModelState.IsValid)
            {
                await _comentariosRepository.Add(vms.nuevoCom);
            }
            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ReturnUser(string Añadiruser)
        {
            var userTokens = await _userManager.FindByNameAsync(User.Identity.Name);

            var user = _context.Usuarios.FirstOrDefault(c =>
               c.Usuario == Añadiruser);
           
            if (user != null)
            {
                if (userTokens.UserName == Añadiruser.Trim())
                {
                    return Json($"No puedes agregarte a ti mismo");

                }
              var revisaramigo= _context.Amigos.FirstOrDefault(a => a.Idenvia == userTokens.Id && a.Idrecibe == user.Id);
                if (revisaramigo != null)
                {

                    return Json($"Ya tienes añadido este Usuario {Añadiruser}");

                }
                else {

                    return Json(true);


                }

            }
            
            return Json($"Este Usuario {Añadiruser} no existe");
        }


    }
}
   

