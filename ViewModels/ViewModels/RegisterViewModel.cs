using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace ViewModels.ViewModels
{
    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Remote(action: "VerifyUser", controller: "Account")]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string usuario { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]

        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Este campo es requerido")]


        public string Apellido { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [EmailAddress(ErrorMessage = "No ingresaste un correo valido")]

        public string Correo { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(7,ErrorMessage = "solo ingresar 7 digitos faltantes")]

        public string Telefono { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]


        public string Contraseña { get; set; }

        [Display(Name = "Repetir Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [Compare("Contraseña",ErrorMessage = "Las contraseña no coinciden")]
        public string RepeatContraseña { get; set; }

        public string Estado { get; set; }

        public string indicetel { get; set; }

        public IFormFile photo { get; set; }
        public string Foto { get; set; }



    }
}
