using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaTwitter.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly PublicacionesRepository _PublicacionesRepository;
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AmigosRepository _AmigosRepository;

        public ApiController(PublicacionesRepository PublicacionesRepository, UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signInManager, AmigosRepository AmigosRepository
            )
        {
            _PublicacionesRepository = PublicacionesRepository;
            _usermanager = usermanager;
            _signInManager = signInManager;
            _AmigosRepository = AmigosRepository;

        }
        //public async Task<ActionResult<List<PublicacionesDTO>>> publicaciones(string username )
        //{
        //    try { 
        //    var user = await _usermanager.FindByNameAsync(username);
        //    if (user != null) {
        //            var list = await _PublicacionesRepository.GetAllpubbyuserApi(user);

        //            if (list.Count >0 ) { 
        //                return list;
        //            }
        //            else
        //            {
        //                return NoContent();
        //            }
        //        }
        //    else
        //    {
        //        return NotFound();
        //    }
        //    }
        //    catch
        //    {

        //        return StatusCode(500);
        //    }

        //}
        [HttpGet]
        public async Task<ActionResult<List<userinfoDTO>>> Amigos(string username)
        {
            try
            {

                var user = await _usermanager.FindByNameAsync(username);
                if (user != null)
                {
                    var list = await _PublicacionesRepository.GetAllFriendsuserApi(user);
                    if (list.Count > 0)
                    {
                        return list;
                    }
                    else
                    {
                        return NoContent();
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {

                return StatusCode(500);
            }

        }
        //[HttpGet]
        //public async Task<ActionResult<PublicacionesDTO>> PopularPub(string username)
        //{
        //    try
        //    {

        //        var user = await _usermanager.FindByNameAsync(username);
        //        if (user != null)
        //        {
        //            var list = await _PublicacionesRepository.PopularPub(user);
        //            if (list != null)
        //            {
        //                return list;
        //            }
        //            else
        //            {
        //                return NoContent();
        //            }

        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch
        //    {

        //        return StatusCode(500);
        //    }

        //}
        //[HttpPost]
        //public async Task<ActionResult> Publicar(NewPubDTO vm)
        //{
        //    try
        //    {
        //            var user = await _usermanager.FindByNameAsync(vm.User);

        //        if (user != null)
        //        {
        //            var resul = _signInManager.CheckPasswordSignInAsync(user, vm.Contraseña, false);

        //            if (resul.Result.Succeeded)
        //            {
        //                vm.iduser = user.Id;
        //                await _PublicacionesRepository.TweetApi(vm);
        //                return NoContent();
        //            }
        //            else
        //            {
        //                return StatusCode(500);


        //            }
        //        }
        //        else
        //        {
        //            return StatusCode(500);



        //        }

        //    }
        //    catch
        //    {

        //        return StatusCode(500);
        //    }


        //}

        [HttpPost]
        public async Task<ActionResult> Addfriend(addfriendDTO vm)
        {
            try
            {
                var user = await _usermanager.FindByNameAsync(vm.User);

                if (user != null)
                {
                    var resul = _signInManager.CheckPasswordSignInAsync(user, vm.Contraseña, false);

                    if (resul.Result.Succeeded)
                    {
                        var friend = await _usermanager.FindByNameAsync(vm.Friend);

                        await _AmigosRepository.Addcustom(user.Id, friend.Id);
                        return NoContent();
                    }
                    else
                    {
                        return StatusCode(500);


                    }
                }
                else
                {
                    return StatusCode(500);



                }

            }
            catch
            {

                return StatusCode(500);
            }


        }
    }
}
