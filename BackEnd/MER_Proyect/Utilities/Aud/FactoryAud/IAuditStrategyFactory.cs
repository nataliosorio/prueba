using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Aud.Strategy;

namespace Utilities.Aud.FactoryAud
{
    //contrato que asegura que podemos obtener una o más estrategias de auditoría para aplicar.
    //Así, podemos tener diferentes maneras de guardar auditoría sin modificar el código que las usa.

    
    public interface IAuditStrategyFactory
    {
        //Permite al servicio de auditoría (AuditService) obtener todas las estrategias que debe usar sin conocer detalles de implementación
        IAuditStrategy[] GetStrategies(); // Puedes retornar múltiples estrategias
    }
}
