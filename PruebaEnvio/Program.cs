using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEnvio
{
    class Program
    {
        static void Main(string[] args)
        {
             PostEnvio.send("bataclub@mk.batamailing.pe", "david.mendoza@bata.com", "Actualización de registro Bata Club", "", "<h1>Html body</h1><p>Rich HTML message body.</p>");            

        }
    }
}
