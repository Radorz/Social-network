using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Publicaciones
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public string Idusuario { get; set; }
        public DateTime? Fecha { get; set; }
        public string Imagen { get; set; }

        public virtual Usuarios IdusuarioNavigation { get; set; }
    }
}
