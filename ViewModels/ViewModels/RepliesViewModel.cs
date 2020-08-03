using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class RepliesViewModel
    {
        public int Id { get; set; }
        public string Idusuario { get; set; }
        public string Nameuser { get; set; }

        public string Photouser { get; set; }

        public int Idcomentario { get; set; }
        public string Contenido { get; set; }

    }
}
