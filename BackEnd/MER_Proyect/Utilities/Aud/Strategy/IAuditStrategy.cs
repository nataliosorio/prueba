using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Aud;

namespace Utilities.Aud.Strategy
{

    //IAuditStrategy define la forma en que una estrategia de auditoría debe funcionar: debe poder recibir un registro de auditoría
    //y guardarlo o procesarlo de alguna manera, de forma asíncrona.
    public interface IAuditStrategy
    {
        //Obliga a implementar un método llamado AuditAsync que recibe una entrada de auditoría (AuditLog).
        Task AuditAsync(AuditLog entry);
    }
}
