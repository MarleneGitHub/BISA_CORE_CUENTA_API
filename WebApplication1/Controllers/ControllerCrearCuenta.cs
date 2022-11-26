using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CapaLogica;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using WebApplication1.Infrastructure.Extensions;
using WebApplication1.Infrastructure.Model;

namespace WebApplication1.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class ControllerCrearCuenta : Controller
    {
        readonly ILogger _Logger = Log.ForContext<ControllerCrearCuenta>();

        public ControllerCrearCuenta(
            IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private readonly AppSettings _appSettings;

        [AllowAnonymous]
        [HttpPost("CrearCuenta")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CrearCuentaResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CoreCuentaError))]

        public IActionResult CrearCuenta(CreacionCuentaRequest request)
        {
            var timerProces = Stopwatch.StartNew();
            timerProces.Start();
            try
            {

                CrearCuentaResponse result = new CrearCuentaResponse();
                _Logger.Error("CrearCuenta: {0}", " Request " + JsonConvert.SerializeObject(request)); 
                result.SequenceCode = request.SequenceCode;

                Logica lCrearCuenta = new Logica();
                string Code= "01";
                result.AnswerDetail=lCrearCuenta.CrearCuenta(request.NumeroCuenta, request.Moneda,  ref Code);
                result.AnswerCode = Code;

                timerProces.Stop();
                _Logger.Error("CrearCuenta: {0}", "Response Sent, processTime:[" + timerProces.Elapsed.ToString() + "]  Response " + JsonConvert.SerializeObject(result));
                return Ok(result);

            }
            catch (Exception ex)
            {
                _Logger.Error($"Ex:' {ex.ToString()}'");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [AllowAnonymous]
        [HttpPost("ConsultarSaldo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsultaSaldoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CoreCuentaError))]

        public IActionResult ConsultarSaldo(ConsultaSaldoRequest request)
        {
            var timerProces = Stopwatch.StartNew();
            timerProces.Start();
            try
            {

                ConsultaSaldoResponse result = new ConsultaSaldoResponse();
                _Logger.Error("ConsultarSaldo: {0}", " Request " + JsonConvert.SerializeObject(request));
                result.SequenceCode = request.SequenceCode;

                Logica lConsultaSaldo = new Logica();
                string AnswerCode = "01";
                string detalle = string.Empty;
                result.Cuenta = lConsultaSaldo.ConsultarSaldo(request.NumeroCuenta, ref AnswerCode, ref detalle);
                result.AnswerCode = AnswerCode;
                result.AnswerDetail = detalle;

                timerProces.Stop();
                _Logger.Error("ConsultarSaldo: {0}", "Response Sent, processTime:[" + timerProces.Elapsed.ToString() + "]  Response " + JsonConvert.SerializeObject(result));
                return Ok(result);

            }
            catch (Exception ex)
            {
                _Logger.Error($"Ex:' {ex.ToString()}'");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPost("DepositoCuenta")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepositoCuentaResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CoreCuentaError))]

        public IActionResult DepositoCuenta(DepositoCuentaRequest request)
        {
            var timerProces = Stopwatch.StartNew();
            timerProces.Start();
            try
            {

                DepositoCuentaResponse result = new DepositoCuentaResponse();
                _Logger.Error("DepositoCuenta: {0}", " Request " + JsonConvert.SerializeObject(request));
                result.SequenceCode = request.SequenceCode;

                Logica lDeposito = new Logica();
                string AnswerCode = "01";
                string detalle = string.Empty;
                lDeposito.Depositar(request.NumeroCuenta, request.Moneda, request.Monto,  ref AnswerCode, ref detalle);
                result.AnswerCode = AnswerCode;
                result.AnswerDetail = detalle;

                timerProces.Stop();
                _Logger.Error("DepositoCuenta: {0}", "Response Sent, processTime:[" + timerProces.Elapsed.ToString() + "]  Response " + JsonConvert.SerializeObject(result));
                return Ok(result);

            }
            catch (Exception ex)
            {
                _Logger.Error($"Ex:' {ex.ToString()}'");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [AllowAnonymous]
        [HttpPost("RetiroCuenta")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RetiroCuentaResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CoreCuentaError))]

        public IActionResult RetiroCuenta(RetiroCuentaRequest request)
        {
            var timerProces = Stopwatch.StartNew();
            timerProces.Start();
            try
            {

                RetiroCuentaResponse result = new RetiroCuentaResponse();
                _Logger.Error("RetiroCuenta: {0}", " Request " + JsonConvert.SerializeObject(request));
                result.SequenceCode = request.SequenceCode;

                Logica lRetiro= new Logica();
                string AnswerCode = "01";
                string detalle = string.Empty;
                lRetiro.Retirar(request.NumeroCuenta, request.Moneda, request.Monto, ref AnswerCode, ref detalle);
                result.AnswerCode = AnswerCode;
                result.AnswerDetail = detalle;

                timerProces.Stop();
                _Logger.Error("RetiroCuenta: {0}", "Response Sent, processTime:[" + timerProces.Elapsed.ToString() + "]  Response " + JsonConvert.SerializeObject(result));
                return Ok(result);

            }
            catch (Exception ex)
            {
                _Logger.Error($"Ex:' {ex.ToString()}'");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [AllowAnonymous]
        [HttpPost("HistorialCuenta")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HistorialCuentaResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CoreCuentaError))]

        public IActionResult HistorialCuenta(HistorialCuentaRequest request)
        {
            var timerProces = Stopwatch.StartNew();
            timerProces.Start();
            try
            {

                HistorialCuentaResponse result = new HistorialCuentaResponse();
                _Logger.Error("HistorialCuenta: {0}", " Request " + JsonConvert.SerializeObject(request));
                result.SequenceCode = request.SequenceCode;

                Logica lHistorial = new Logica();
                string AnswerCode = "01";
                string detalle = string.Empty;
                result.Movimientos=lHistorial.VerHistorico(request.NumeroCuenta, ref AnswerCode, ref detalle);
                result.AnswerCode = AnswerCode;
                result.AnswerDetail = detalle;

                timerProces.Stop();
                _Logger.Error("HistorialCuenta: {0}", "Response Sent, processTime:[" + timerProces.Elapsed.ToString() + "]  Response " + JsonConvert.SerializeObject(result));
                return Ok(result);

            }
            catch (Exception ex)
            {
                _Logger.Error($"Ex:' {ex.ToString()}'");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
