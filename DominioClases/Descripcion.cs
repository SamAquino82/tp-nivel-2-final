using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioClases
{
    public class Descripcion
    {
        public int Id { get; set; }
        public string TipoArticulo { get; set; }

        public override string ToString()
        {
            return TipoArticulo;
        }
    }
}
