using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace ViewModels.ViewModels
{
    public class RecuperarViewModel
    {
      
        [Required(ErrorMessage = "Este campo es requerido")]
        [Remote(action: "ReturnUser", controller: "Account")]
        public string usuario { get; set; }
            
        }
    }



