using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIREON.Models
{
    public class ReservationViewModel
    {
        public DateTime Fecha { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime Horasalida { get; set; }
        public string IdEmpleado{ get; set; }
        public string IdSolicitante { get; set; }
        public DateTime HoraSolicitada { get; set; }
        public int IdCubiculo { get; set; }
        public List<IntegrantesViewModel> IntegrantesViewModel { get; set; }

           }
}