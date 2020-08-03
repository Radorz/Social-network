using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DTOS
{
    public class PublicacionesDTO
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public string Idusuario { get; set; }
        public DateTime? Fecha { get; set; }
        public string Imagen { get; set; }

        public List<ComentariosDTO> Comentarios { get; set; }

    }
}
