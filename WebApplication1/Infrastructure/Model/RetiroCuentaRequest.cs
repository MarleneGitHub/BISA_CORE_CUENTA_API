using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Infrastructure.Model
{
    public class RetiroCuentaRequest
    {
        /// <summary>
        /// Codigo de secuencia o trace, valor unico que identifica la solictud. Logitud de 6 caracteres, rellenado con ceros a la izquierda.
        /// </summary>
        [Required]
        public string SequenceCode { get; set; }

        /// <summary>
        ///Codigo que identifica al tipo de solicitud 
        /// </summary>
        [Required]
        public string NumeroCuenta { get; set; }

        [Required]
        public string Moneda { get; set; }

        [Required]
        public double Monto { get; set; }
    }
}
