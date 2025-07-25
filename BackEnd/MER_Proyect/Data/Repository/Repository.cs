using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Entity.Model.Aud;
using Microsoft.EntityFrameworkCore;
using Utilities.Annotations;
using Utilities.Aud.CurrentUser;
using Utilities.Aud.Services;
using Utilities.Exceptions;
using Utilities.Helpers;
using static Dapper.SqlMapper;
using DataException = Utilities.Exceptions.DataException;

namespace Data.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly IAuditService _auditService;  // <-- Aquí la inyección
        private readonly ICurrentUserService _currentUserService;


        public Repository(ApplicationDbContext context, IAuditService auditService, ICurrentUserService currentUserService)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _auditService = auditService;
            _currentUserService = currentUserService;
        }


        protected async Task AuditAsync(string action, int entityId = 0, string changes = null)
        {
            var entry = new AuditLog
            {
                Action = action,
                EntityName = typeof(T).Name,
                EntityId = entityId,
                Timestamp = DateTime.UtcNow,
                UserName = _currentUserService.UserName ?? "SYSTEM",
                Changes = changes ?? "Sin detalles"
            };

            await _auditService.SaveAuditAsync(entry);
        }
     
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var entities = await _dbSet.ToListAsync();

                // Registrar auditoría
                await AuditAsync("GetAll");

                return entities;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DataException("Conflicto de concurrencia al acceder a la base de datos.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DataException("Error al consultar la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new DataException("Ocurrió un error inesperado al obtener los datos.", ex);
            }
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new DataException("El ID proporcionado no es válido.");

            try
            {
                var entity = await _dbSet.FindAsync(id);

                if (entity == null)
                    throw new DataException($"No se encontró ninguna entidad con el ID {id}.");

                // Registrar auditoría si encontró la entidad
                await AuditAsync("GetById", id);

                return entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DataException("Conflicto de concurrencia al acceder a la base de datos.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DataException("Error al consultar la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new DataException("Ocurrió un error inesperado al buscar la entidad.", ex);
            }
        }






        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                throw new DataException("La entidad proporcionada no puede ser nula.");

            var idProperty = entity.GetType().GetProperty("id");
            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(entity);

                if (idValue is int idInt && idInt != 0)
                    throw new DataException("El campo ID no debe ser asignado manualmente. Es autoincremental.");
            }

            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();

                // Obtener el ID generado
                int entityId = 0;
                if (idProperty != null)
                {
                    var newIdValue = idProperty.GetValue(entity);
                    if (newIdValue is int newIdInt)
                        entityId = newIdInt;
                }

                // Auditoría de la creación
                await AuditAsync("Create", entityId, $"Se creó una entidad de tipo {typeof(T).Name}");

                return entity;
            }
            catch (DbUpdateException dbEx)
            {
                throw new DatabaseConnectionException("Error de conexión al intentar crear la entidad.", dbEx);
            }
            catch (Exception ex)
            {
                throw new DataException("Ocurrió un error inesperado al crear la entidad.", ex);
            }
        }



        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new DataException("La entidad no puede ser nula.");

            var idProp = entity.GetType().GetProperty("id");
            if (idProp == null)
                throw new DataException("La entidad no tiene una propiedad 'id'.");

            var idValue = idProp.GetValue(entity);
            if (idValue is not int id || id <= 0)
                throw new DataException("El ID debe ser mayor que cero.");

            var exists = await _dbSet.AsNoTracking().AnyAsync(e => EF.Property<int>(e, "id") == id);
            if (!exists)
                throw new DataException($"No se encontró ninguna entidad con ID {id}.");

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Auditoría de la actualización
            await AuditAsync("Update", id, "Entidad actualizada");

            return true;
        }






        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new DataException("El ID proporcionado no es válido. Debe ser mayor a cero.");

            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                throw new DataException($"No se encontró un registro con el ID {id}.");

            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                await AuditAsync("Delete", id);
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DataException("Ocurrió un error al intentar eliminar el registro.", ex);
            }
            catch (Exception ex)
            {
                throw new DataException("Error inesperado durante la eliminación.", ex);
            }
        }

        public async Task<bool> DeleteLogicalAsync(int id)
        {
            if (id <= 0)
                throw new DataException("El ID proporcionado no es válido.");

            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new DataException($"No se encontró una entidad con ID {id}.");

            var activeProp = entity.GetType().GetProperty("active");
            if (activeProp == null)
                throw new DataException("La propiedad 'active' no existe en la entidad.");

            activeProp.SetValue(entity, false);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await AuditAsync("Logical Delete", id);


            return true;
        }




        public async Task<bool> Recoverlogic(int id)
        {
            if (id <= 0)
                throw new DataException("El ID proporcionado no es válido.");

            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new DataException($"No se encontró una entidad con ID {id}.");

            var activeProp = entity.GetType().GetProperty("active");
            if (activeProp == null)
                throw new DataException("La propiedad 'active' no existe en la entidad.");

            activeProp.SetValue(entity, true);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await AuditAsync("Recover", id);


            return true;
        }


        //meetodo nuevo 

        //public override async Task<List<ExpandoObject>> GetAllDynamicAsync()
        //{
        //    var entityType = typeof(T);
        //    var query = _context.Set<T>().AsQueryable();

        //    var foreignKeyProps = entityType
        //     .GetProperties()
        //     .Where(p => Attribute.IsDefined(p, typeof(ForeignIncludeAttribute)))
        //     .ToList();

        //    foreach (var prop in foreignKeyProps)
        //    {
        //        query = query.Include(prop.Name); // prop.Name ahora es "Form", no "FormId"
        //    }

        //    var resultList = await query.ToListAsync();
        //    var dynamicList = new List<ExpandoObject>();

        //    foreach (var entity in resultList)
        //    {
        //        dynamic dyn = new ExpandoObject();
        //        var dict = (IDictionary<string, object?>)dyn;

        //        // ID principal
        //        dict["Id"] = entityType.GetProperty("Id")?.GetValue(entity);

        //        foreach (var prop in foreignKeyProps)
        //        {
        //            var attr = prop.GetCustomAttribute<ForeignIncludeAttribute>()!;
        //            var foreignValue = prop.GetValue(entity);

        //            if (foreignValue == null) continue;

        //            if (!string.IsNullOrEmpty(attr.SelectPath))
        //            {
        //                var value = ReflectionHelper.GetNestedPropertyValue(foreignValue, attr.SelectPath);
        //                var key = ReflectionHelper.PascalJoin(prop.Name, attr.SelectPath);
        //                dict[key] = value;
        //            }
        //            else
        //            {
        //                dict[prop.Name] = foreignValue;
        //            }
        //        }

        //        dynamicList.Add(dyn);
        //    }

        //    return dynamicList;
        //}





    }
}
