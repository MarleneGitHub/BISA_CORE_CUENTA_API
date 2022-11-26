using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Infrastructure.Model
{
    public class CoreCuentaError
    {
        /// <summary>
        /// Codigo de error.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Describe el codigo de error.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Contiene mas detalle o sub errores que pudieron ocurrir.
        /// </summary>
        public ErrorDetail Error { get; set; } = new ErrorDetail();
    }
    public class ErrorDetail
    {
        internal ErrorDetail() { }
        /// <summary>
        /// Colleccion de sub errores.
        /// </summary>
        public ErrorItem[] Errors { get; private set; } = { };
        public void AddError(string Reason, string Message)
        {
            this.Errors = this.Errors.Concat(Enumerable.Repeat(new ErrorItem() { Reason = Reason, Message = Message }, 1)).ToArray();
        }
    }
    public class ErrorItem
    {
        internal ErrorItem() { }
        /// <summary>
        /// Codigo o propiedad del origen del sub error.
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Describe la razon del sub error.
        /// </summary>
        public string Message { get; set; }
    }
}
