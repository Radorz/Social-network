using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class RespuestasCom
    {
        public int Id { get; set; }
        public string Idusuario { get; set; }
        public int? Idcomentario { get; set; }
        public string Contenido { get; set; }

        public virtual Comentario IdcomentarioNavigation { get; set; }
        public virtual Usuarios IdusuarioNavigation { get; set; }
    }
}
