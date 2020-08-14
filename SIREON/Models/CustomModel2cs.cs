using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIREON.Models
{
    public class CustomModel2cs
    {
        public IEnumerable<Cubiculo> cubiculos { get; set; }
        public IEnumerable<Cubiculo> disponibilidads { get; set; }
        public IEnumerable<Cubiculo> Ocupado { get; set; }
    }
}