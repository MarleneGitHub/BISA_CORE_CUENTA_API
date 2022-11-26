using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CapaLogica
{
    public class BD
    {
        public List<Cuenta> GetTablaCuentas()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\Data\\" + "Cuentas.txt";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, null);
            }
            string data = File.ReadAllText(filePath);
            List<Cuenta> respuesta = JsonConvert.DeserializeObject<List<Cuenta>>(data);
            if (respuesta == null)
                respuesta = new List<Cuenta>();
            return respuesta;
        }
        public void SetTablaCuentas(List<Cuenta> listaCuentas)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\Data\\" + "Cuentas.txt";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, null);
            }
            if (listaCuentas == null)
                listaCuentas = new List<Cuenta>();
            string data = JsonConvert.SerializeObject(listaCuentas);
            File.WriteAllText(filePath, data);
        }
        public List<Movimientos> GetTablaMovimientos()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\Data\\" + "Movimientos.txt";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, null);
            }
            string data = File.ReadAllText(filePath);
            List<Movimientos> respuesta = JsonConvert.DeserializeObject<List<Movimientos>>(data);
            if (respuesta == null)
                respuesta = new List<Movimientos>();
            return respuesta;
        }
        public void SetTablaMovimientos(List<Movimientos> listaMovimientos)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\Data\\" + "Movimientos.txt";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, null);
            }
            if (listaMovimientos == null)
                listaMovimientos = new List<Movimientos>();
            string data = JsonConvert.SerializeObject(listaMovimientos);
            File.WriteAllText(filePath, data);
        }
    }
}
