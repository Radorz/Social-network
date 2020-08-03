using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Comentario
    {
        public Comentario()
        {
            RespuestasCom = new HashSet<RespuestasCom>();
        }

        public int Id { get; set; }
        public string Idusuario { get; set; }
        public int? Idpublicacion { get; set; }
        public string Contenido { get; set; }

        public virtual Usuarios IdusuarioNavigation { get; set; }
        public virtual ICollection<RespuestasCom> RespuestasCom { get; set; }
    }
}
