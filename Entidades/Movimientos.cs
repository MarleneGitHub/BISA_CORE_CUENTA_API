using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Movimientos
    {
        public string id { get; set; }
        public string NumeroCuenta { get; set; }
        public string Moneda { get; set; }
        public double Monto { get; set; }
        public string Tipo { get; set; }
    }
}
