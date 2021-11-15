using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catedraticoapi.Models
{
    public class Catedratico
    {
        public static bool Activo { get; internal set; }
        [Key]
        public int Idcatedratico { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Edad { get; set; }

        public bool Estado { get; set; }
    }
}
