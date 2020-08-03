using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class PublicacionesViewModel
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public string Idusuario { get; set; }
        public string Imagen { get; set; }

        public string Nombre { get; set; }

        public string Foto { get; set; }

        public string Apellido { get; set; }

        public string Usuario { get; set; }

        public DateTime Fecha { get; set; }


    }
}
