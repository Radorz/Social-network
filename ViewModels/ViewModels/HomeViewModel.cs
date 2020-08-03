using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.AspNetCore.Http;

namespace ViewModels.ViewModels
{
    public class HomeViewModel
    {
        public string idusuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Usuario { get; set; }
        public string Foto { get; set; }

        public List<Usuarios> usuarios { get; set; }
        public List<Publicaciones> publicacion { get; set; }

        public Publicaciones nuevapub { get; set; }

        public Comentario nuevoCom { get; set; }

        public List<List<ComentariosViewModel>> comentario { get; set; }
        public List<List<RepliesViewModel>> replies { get; set; }

        public int ideditpub { get; set; }
        public string editconpub { get; set; }
        public RespuestasCom newreply { get; set; }

        public IFormFile photo { get; set; }
        public List<RepliesViewModel> Last { get; set; }



    }
}
