using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Infrastructure.Model
{
    public class HistorialCuentaResponse
    {
        public string SequenceCode { get; set; }
        public string AnswerCode { get; set; }

        public string AnswerDetail { get; set; }

        public List<Movimientos> Movimientos { get; set; }
    }
}
