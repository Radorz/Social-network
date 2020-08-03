using Database.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class AmigosViewModel
    {
        public string idusuario { get; set; }
        public string Foto { get; set; }

        public List<Usuarios> Amigos { get; set; }
        public List<PublicacionesCustom> publicacion { get; set; }
        public Comentario nuevoCom { get; set; }
        public List<List<ComentariosViewModel>> comentario { get; set; }

        [Remote(action: "ReturnUser", controller: "Amigos")]
        public string Añadiruser{ get; set; }
        public string BorrarAmix { get; set; }

        public List<RepliesViewModel> Last { get; set; }
        public RespuestasCom newreply { get; set; }

        public List<List<RepliesViewModel>> replies { get; set; }

    }
}
