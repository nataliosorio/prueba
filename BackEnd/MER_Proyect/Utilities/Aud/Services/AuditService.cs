using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Aud;
using Utilities.Aud.FactoryAud;

namespace Utilities.Aud.Services
{

    //Implementa el servicio para guardar auditorías definido por la interfaz IAuditService.
    public class AuditService : IAuditService
    {
        //Fábrica que provee las diferentes estrategias para guardar auditoría (bases de datos, archivos, etc.)
        private readonly IAuditStrategyFactory _strategyFactory;

        public AuditService(IAuditStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        //guarda el registro usando todas las estrategias disponibles (por ejemplo, guardar en base de datos y en archivo).
        public async Task SaveAuditAsync(AuditLog entry)
        {
            //Obtiene todas las estrategias de auditoría con _strategyFactory.GetStrategies().
            var strategies = _strategyFactory.GetStrategies();

            //Recorre cada estrategia y llama a su método AuditAsync, pasando el registro de auditoría.
            foreach (var strategy in strategies)
            {
                //Cada estrategia realiza el guardado según su implementación (por ejemplo, base de datos o archivo).
                await strategy.AuditAsync(entry);
            }
        }
    }
}
