using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Aud;

namespace Utilities.Aud.Strategy
{
    //Su función es combinar varias estrategias de auditoría y ejecutar todas en secuencia.
    public class CombinedAuditStrategy : IAuditStrategy
    {
        //Colección de estrategias que implementan IAuditStrategy

        private readonly IEnumerable<IAuditStrategy> _strategies;


        //Recibe una lista o conjunto de estrategias para almacenar y usar al auditar
        public CombinedAuditStrategy(IEnumerable<IAuditStrategy> strategies)
        {
            _strategies = strategies;
        }

        //	Ejecuta el método AuditAsync de cada estrategia, pasando el mismo registro de auditoría
        public async Task AuditAsync(AuditLog entry)
        {
            //Recorre todas las estrategias configuradas.
            foreach (var strategy in _strategies)
            {
                //Ejecuta la auditoría en cada una, guardando el registro según la lógica de cada estrategia (por ejemplo, base de datos, archivo, etc.).


                await strategy.AuditAsync(entry);
            }
        }
    }
}
