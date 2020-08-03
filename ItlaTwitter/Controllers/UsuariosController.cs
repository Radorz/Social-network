using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;
using ServiceStack;
using Microsoft.Data.Sql;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using AutoMapper;
using Database.Models;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Repository.Repository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Authorization;

namespace ItlaTwitter.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly PublicacionesRepository _publicacionesRepository;
        private readonly ComentariosRepository _comentariosRepository;
        private readonly RepliesRepository _repliesRepository;

        public UsuariosController(UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment, RepliesRepository repliesRepository
            , PublicacionesRepository publicacionesRepository, ComentariosRepository comentariosRepository)
        {
            _userManager = userManager;
            this.hostEnvironment = hostEnvironment;
            _publicacionesRepository = publicacionesRepository;
            _comentariosRepository = comentariosRepository;
            _repliesRepository = repliesRepository;


        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var userTokens = await _userManager.FindByNameAsync(User.Identity.Name);

            var Vm = await _publicacionesRepository.GetAllpubbyuser(userTokens);

            return View(Vm);
        }
       
        [HttpPost]

        public async Task<IActionResult> reply(HomeViewModel vm)
        {
            if (ModelState.IsValid) { 
           var  user = await _userManager.FindByNameAsync(User.Identity.Name);
            var newereply = new RespuestasCom();
            newereply.Idcomentario = vm.newreply.Idcomentario;
            newereply.Idusuario = user.Id;
            newereply.Contenido = vm.newreply.Contenido.Trim();
               await _repliesRepository.Add(newereply);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public async Task<IActionResult> Tweet(HomeViewModel vms)
        {
            if (ModelState.IsValid)
            {
                await _publicacionesRepository.Tweet(vms, hostEnvironment.WebRootPath);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public async Task<IActionResult> Comentar(HomeViewModel vms)
        {
            if (ModelState.IsValid)
            {
                await _comentariosRepository.Add(vms.nuevoCom);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public async Task<IActionResult> EditarCom(HomeViewModel vms)
        {
            if (ModelState.IsValid)
            {
                var pub = await _publicacionesRepository.GetbyId(vms.ideditpub);
                if (pub != null)
                {
                    pub.Contenido = vms.editconpub;
                   await _publicacionesRepository.Update(pub);

                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public async Task<IActionResult> BorrarPub(HomeViewModel vms)
        {
            if (ModelState.IsValid)
            {
                var pubdelete = await  _publicacionesRepository.GetbyId(vms.ideditpub);

                if (pubdelete != null)
                {
                    await _publicacionesRepository.Delete(pubdelete.Id);
                }

            }
            return RedirectToAction("Index");
        }

    }
}
