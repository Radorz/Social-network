using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Amigos
    {
        public int Id { get; set; }
        public string Idenvia { get; set; }
        public string Idrecibe { get; set; }

        public virtual Usuarios IdenviaNavigation { get; set; }
        public virtual Usuarios IdrecibeNavigation { get; set; }
    }
}
