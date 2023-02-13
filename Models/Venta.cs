using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManuelGarettoDesafio4.Models
{
    public class Venta
    {
        public long Id { get; set; }
        public string Comentarios { get; set; }
        public long IdUsuario { get; set; }
    }
}
