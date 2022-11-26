using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaLogica
{
    public class Logica
    {
        public string CrearCuenta(string numeroCuenta, string moneda, ref string codigoRespuesta)
        {
            //leer tablas
            string detalle = string.Empty;
            try
            {
                BD basededatos = new BD();
                var cuentas = basededatos.GetTablaCuentas();

                //proceso logico
                if (cuentas.Where(x => x.NumeroCuenta == numeroCuenta).Count() > 0)
                    detalle = "cuenta ya existe";
                else
                {
                    cuentas.Add(new Cuenta { NumeroCuenta = numeroCuenta, Estado = "ACTIVE", Moneda = moneda, Saldo = 0 });

                    //guardar tabla
                    basededatos.SetTablaCuentas(cuentas);
                    detalle = "registro exitoso";
                    codigoRespuesta = "00";
                }
            }
            catch (Exception ex)
            {

                detalle = ex.Message;

            }
            return detalle;
        }

        public Cuenta ConsultarSaldo(string numeroCuenta, ref string codigoRespuesta, ref string detalle)
        {
            //leer tablas
            BD basededatos = new BD();
            var cuentas = basededatos.GetTablaCuentas();

            //proceso logico
            var cuenta = cuentas.Where(x => x.NumeroCuenta == numeroCuenta).ToList();
            if (cuenta.Count() == 0)
                detalle = "cuenta NO existe";
            
            else
            {
                detalle = "Consulta exitosa";
                
            }

            return cuenta.FirstOrDefault();

        }



        public List<Movimientos> VerHistorico(string numeroCuenta, ref string codigoRespuesta, ref string detalle)
        {
            //leer tablas
            BD basededatos = new BD();
            var cuentas = basededatos.GetTablaCuentas();
            var cuenta = cuentas.Where(x => x.NumeroCuenta == numeroCuenta).ToList();
            if (cuenta.Count() == 0)
            {
                detalle = "cuenta NO existe"; 
                return null; 

            }
                

            var movimientos = basededatos.GetTablaMovimientos();

            //proceso logico
            var historico = movimientos.Where(x => x.NumeroCuenta == numeroCuenta).ToList();
            historico = historico.OrderByDescending(x => x.id).ToList();
            detalle = "Consulta exitosa";
            codigoRespuesta = "00";
            return historico;

            //guardar tabla
            //basededatos.SetTablaCuentas(cuentas);
        }

        public void Depositar(string numeroCuenta, string moneda, double monto, ref string codigoRespuesta, ref string detalle)
        {
            //leer tablas
            BD basededatos = new BD();
            var cuentas = basededatos.GetTablaCuentas();

            //proceso logico
            var listacuenta = cuentas.Where(x => x.NumeroCuenta == numeroCuenta).ToList();
            if (listacuenta.Count() == 0 || listacuenta.Count() > 1)
            {
                detalle = "cuenta NO existe";
                return;
            }
                
            Cuenta cuenta = listacuenta.FirstOrDefault();
            if (cuenta.Moneda != moneda)
            {
                detalle = "moneda NO coincide";
                return;
            }
                
            if (monto <= 0)
            {
                detalle="la transaccion no se puede hacer con un monto negativo o cero";
                return;
            }
              
                double calculado = cuenta.Saldo + monto; //solo se analiza el monto para el estado
                if (calculado < 0)
                    cuenta.Estado = "HOLD";
                else
                    cuenta.Estado = "ACTIVE";

                cuenta.Saldo = calculado;
                SetMovimiento(numeroCuenta, moneda, monto, "DEPOSITO");

                //guardar tabla
                basededatos.SetTablaCuentas(cuentas);
                detalle = "Deposito exitoso";
                codigoRespuesta = "00";
            
            

        }

        public void Retirar(string numeroCuenta, string moneda, double monto, ref string codigoRespuesta, ref string detalle)
        {
            //leer tablas
            BD basededatos = new BD();
            var cuentas = basededatos.GetTablaCuentas();

            //proceso logico
            var listacuenta = cuentas.Where(x => x.NumeroCuenta == numeroCuenta).ToList();
            if (listacuenta.Count() == 0 || listacuenta.Count() > 1)
            {
                detalle = "cuenta NO existe";
                return;
            }

            Cuenta cuenta = listacuenta.FirstOrDefault();
            if (cuenta.Moneda != moneda)
            {
                detalle = "moneda NO coincide";
                return;
            }
            if (monto <= 0)
            {
                detalle = "la transaccion no se puede hacer con un monto negativo o cero";
                return;
            }
                
            if (cuenta.Estado == "HOLD")
            {
                detalle = "no se puede retirar cuenta en estado HOLD";
                return;
            }
               

            double calculado = cuenta.Saldo - monto; //solo se analiza el monto para el estado

            if (calculado < 0)
                cuenta.Estado = "HOLD";
            else
                cuenta.Estado = "ACTIVE";

            cuenta.Saldo = calculado;
            SetMovimiento(numeroCuenta, moneda, monto, "RETIRO");

            //guardar tabla
            basededatos.SetTablaCuentas(cuentas);
            detalle = "Retiro exitoso";
            codigoRespuesta = "00";
        }

        public void SetMovimiento(string numeroCuenta, string moneda, double monto, string tipo)
        {
            //leer tablas
            BD basededatos = new BD();
            var movimientos = basededatos.GetTablaMovimientos();

            //proceso logico
            Movimientos mov = new Movimientos();
            mov.id = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            mov.Moneda = moneda;
            mov.NumeroCuenta = numeroCuenta;
            mov.Monto = monto;
            mov.Tipo = tipo;
            movimientos.Add(mov);

            //guardar tabla
            basededatos.SetTablaMovimientos(movimientos);
        }
    }
}
