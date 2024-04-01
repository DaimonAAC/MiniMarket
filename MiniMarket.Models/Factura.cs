using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMarket.Models
{
    public class Factura
    {
        public int FacturaID { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal Total { get; set; }
        public int ClienteID { get; set; }
        public int EmpleadoID { get; set; }
        // Agrega otras propiedades según sea necesario
    }
}
