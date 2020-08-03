using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOS
{
    public class ComentariosDTO
    {

        public int Id { get; set; }
        public string Idusuario { get; set; }
        public int? Idpublicacion { get; set; }
        public string Contenido { get; set; }

        public List<RepliesDTO> Replies { get; set; }



    }
}
