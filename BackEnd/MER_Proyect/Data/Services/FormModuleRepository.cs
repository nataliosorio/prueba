using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Utilities.Aud.CurrentUser;
using Utilities.Aud.Services;

namespace Data.Services
{
    public class FormModuleRepository : Repository<FormModule>
    {
        public FormModuleRepository(ApplicationDbContext context, IAuditService auditService, ICurrentUserService currentUserService) : base(context, auditService, currentUserService)
        {
        }


        public async Task<IEnumerable<FormModuleDto>> GetAllJoinAsync()
        {
            return await _dbSet
                .Include(x => x.Form)
                .Include(x => x.Module)
                .Select(ru => new FormModuleDto
                {
                    id = ru.id,
                    formid = ru.formid,
                    moduleid = ru.moduleid,
                    formname = ru.Form.name,
                    modulename = ru.Module.name
                })
                .ToListAsync();
        }

    }
}
