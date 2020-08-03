using Database.Models;
using Microsoft.EntityFrameworkCore;
using Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace Repository.Repository
{
    public class AmigosRepository : RepositoryBase<Amigos, itlatwitterContext>
    {
        public AmigosRepository(itlatwitterContext context) : base(context)
        {

        }
        public async Task<bool> Deletefriend(Amigos amg)
        {
            _context.Amigos.Remove(amg);
            await _context.SaveChangesAsync();
            return  true;
        }
        public async Task<Amigos> findfriend(AmigosViewModel amg)
        {
            var Borrar = await _context.Amigos.FirstOrDefaultAsync(a => a.Idenvia == amg.idusuario && a.Idrecibe == amg.BorrarAmix);

            return Borrar;
        }
        public async Task<Amigos> Addcustom(string envia, string recibe)
        {
            var amigo = new Amigos();
            amigo.Idenvia = envia;
            amigo.Idenvia = recibe;

            _context.Set<Amigos>().Add(amigo);
            await _context.SaveChangesAsync();
            return amigo;
        }
    }
}
