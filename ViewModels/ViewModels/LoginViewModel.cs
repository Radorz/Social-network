using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es requerido")]

        public string usuario { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]

        public string Contraseña { get; set; }
    }
}
