using System.ComponentModel.DataAnnotations;

namespace DTOS
{
    public class NewPubDTO
    {
        [Required(ErrorMessage = "El usuario es requerido")]

        public string User { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]

        public string Contraseña { get; set; }

        [Required(ErrorMessage = "El contenido es requerida")]

        public string Contenido { get; set; }
        public string iduser{ get; set; }
        public string Imagen { get; set; }


    }
}
