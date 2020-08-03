using AutoMapper;
using Database.Models;
using EmailHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace Repository.Repository
{
   public class UsuarioRepository : RepositoryBase<Usuarios, itlatwitterContext >
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Iemailsender _iemailsender;
        private readonly IMapper _mapper;

        public UsuarioRepository(itlatwitterContext context, Iemailsender iemailsender, UserManager<IdentityUser> userManager, IMapper mapper) : base(context)
        {
            _userManager = userManager;
            _iemailsender = iemailsender;
            this._mapper = mapper;

        }
        public async Task<Usuarios> GetbyIdString(string id)
        {
            return await _context.Set<Usuarios>().FindAsync(id);
        }
        public async Task<Usuarios> GetbyName(string Usuario)
        {
          
            return await _context.Set<Usuarios>().FirstOrDefaultAsync(c =>
             c.Usuario == Usuario.Trim());
        }
        public async Task<Usuarios> Register(RegisterViewModel vm, string root)
        {
            
           
                vm.Telefono = string.Format("{0}-{1}", vm.indicetel, vm.Telefono).Trim();
                vm.Contraseña = PasswordEncryption(vm.Contraseña).Trim();
                var usuarioentity = _mapper.Map<Usuarios>(vm);
                string uniqueName = null;
                if (vm.photo != null)
                {

                    var folderPath = Path.Combine(root, "img/profile");
                    uniqueName = Guid.NewGuid().ToString() + "_" + vm.photo.FileName;
                    var filepath = Path.Combine(folderPath, uniqueName);

                    if (filepath != null)
                    {

                        vm.photo.CopyTo(new FileStream(filepath, mode: FileMode.Create));
                    }
                
                usuarioentity.Foto = uniqueName;
                await Add(usuarioentity);
            }
            return (usuarioentity);

        }

        public async Task<string> Changepassword(RecuperarViewModel vm)
        {


            var user = await GetbyName(vm.usuario);

            var useridentity = await _userManager.FindByNameAsync(vm.usuario);
            Random rnd = new Random();
            int nuevacontraseña = rnd.Next(100000, 999999);
            var token = await _userManager.GeneratePasswordResetTokenAsync(useridentity);
            await _userManager.ResetPasswordAsync(useridentity, token, nuevacontraseña.ToString());
            user.Contraseña = PasswordEncryption(Convert.ToString(nuevacontraseña));
            await Update(user);

            return nuevacontraseña.ToString();

        }
            private string PasswordEncryption(string password)
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
