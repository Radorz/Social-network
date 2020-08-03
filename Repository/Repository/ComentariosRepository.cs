using Database.Models;
using Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
   public class ComentariosRepository : RepositoryBase<Comentario, itlatwitterContext>
    {
        public ComentariosRepository(itlatwitterContext context) : base(context)
        {

        }
    }
}
