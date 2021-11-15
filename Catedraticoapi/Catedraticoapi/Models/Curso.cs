using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catedraticoapi.Models
{
    public class Curso
    {
        [Key]
        public int Idcurso { get; set; }

        public string Nombre { get; set; }

        public int Punteo { get; set; }
    }
}
