﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DTOS
{
    public class userinfoDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Foto { get; set; }



    }
}
