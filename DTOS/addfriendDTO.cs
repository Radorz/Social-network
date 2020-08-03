using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DTOS
{
    public class addfriendDTO
    {
        [Required(ErrorMessage = "El usuario es requerido")]

        public string usuario { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]

        public string contraseña { get; set; }
        [Remote(action: "ReturnUser", controller: "Amigos")]

        public string Friend { get; set; }

       

    }
}
