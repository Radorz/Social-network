using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DTOS
{
    public class RepliesDTO
    {
        public int Id { get; set; }
        public string Idusuario { get; set; }
        public int? Idcomentario { get; set; }
        public string Contenido { get; set; }



    }
}
