﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Models
{
    public class Proveedor
    {
        public int ProveedorID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
