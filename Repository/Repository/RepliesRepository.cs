using Database.Models;
using Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class RepliesRepository : RepositoryBase<RespuestasCom, itlatwitterContext>
    {
        public RepliesRepository(itlatwitterContext context) : base(context)
        {

        }
    }
}
