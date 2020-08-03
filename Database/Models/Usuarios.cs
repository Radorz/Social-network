using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            AmigosIdenviaNavigation = new HashSet<Amigos>();
            AmigosIdrecibeNavigation = new HashSet<Amigos>();
            Comentario = new HashSet<Comentario>();
            Publicaciones = new HashSet<Publicaciones>();
            RespuestasCom = new HashSet<RespuestasCom>();
        }

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Estado { get; set; }
        public string Foto { get; set; }

        public virtual ICollection<Amigos> AmigosIdenviaNavigation { get; set; }
        public virtual ICollection<Amigos> AmigosIdrecibeNavigation { get; set; }
        public virtual ICollection<Comentario> Comentario { get; set; }
        public virtual ICollection<Publicaciones> Publicaciones { get; set; }
        public virtual ICollection<RespuestasCom> RespuestasCom { get; set; }
    }
}
