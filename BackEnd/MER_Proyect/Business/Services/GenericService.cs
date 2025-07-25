using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Utilities.Exceptions;

namespace Business.Services
{
    public class GenericService<TDto, TEntity> : IGenericService<TDto>
    where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //public async Task<IEnumerable<TDto>> GetAllAsync()
        //{
        //    var entities = await _repository.GetAllAsync();
        //    return _mapper.Map<IEnumerable<TDto>>(entities);
        //}

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                return _mapper.Map<IEnumerable<TDto>>(entities);
            }
            catch (Exception ex)
            {
                throw new ExternalServiceException("Repositorio", "Error al obtener todos los registros", ex);
            }
        }

        //public async Task<TDto?> GetByIdAsync(int id)
        //{
        //    var entity = await _repository.GetByIdAsync(id);
        //    return entity == null ? default : _mapper.Map<TDto>(entity);
        //}

        public async Task<TDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException(nameof(id), "El ID debe ser mayor que cero.");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity).Name, id);

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            if (dto == null)
                throw new ValidationException(nameof(dto), "El objeto DTO no puede ser nulo.");

            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                var created = await _repository.CreateAsync(entity);
                return _mapper.Map<TDto>(created);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al crear la entidad.", ex);
            }
        }




        //public virtual async Task<TDto> CreateAsync(TDto dto)
        //{
        //    var entity = _mapper.Map<TEntity>(dto);
        //    var created = await _repository.CreateAsync(entity);
        //    return _mapper.Map<TDto>(created);
        //}

        //public async Task<bool> UpdateAsync(TDto dto)
        //{
        //    var entity = _mapper.Map<TEntity>(dto);
        //    return await _repository.UpdateAsync(entity);
        //}

        public async Task<bool> UpdateAsync(TDto dto)
        {
            if (dto == null)
                throw new ValidationException(nameof(dto), "El objeto DTO no puede ser nulo.");

            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                var updated = await _repository.UpdateAsync(entity);
                if (!updated)
                    throw new BusinessRuleViolationException("UPDATE_FAILED", "No se pudo actualizar la entidad.");

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al actualizar la entidad.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException(nameof(id), "El ID debe ser mayor que cero.");

            var success = await _repository.DeleteAsync(id);
            if (!success)
                throw new EntityNotFoundException(typeof(TEntity).Name, id);

            return true;
        }

        //public async Task<bool> DeleteAsync(int id)
        //{
        //    return await _repository.DeleteAsync(id);
        //}
        //public async Task<bool> DeleteAsyncLogic(int id)
        //{
        //    return await _repository.DeleteLogicalAsync(id);
        //}


        public async Task<bool> DeleteAsyncLogic(int id)
        {
            if (id <= 0)
                throw new ValidationException(nameof(id), "El ID debe ser mayor que cero.");

            var success = await _repository.DeleteLogicalAsync(id);
            if (!success)
                throw new BusinessRuleViolationException("DELETE_LOGIC_FAILED", $"No se pudo eliminar lógicamente la entidad con ID {id}.");

            return true;
        }

        public async Task<bool> Recoverlogic(int id)
        {
            if (id <= 0)
                throw new ValidationException(nameof(id), "El ID debe ser mayor que cero.");

            var success = await _repository.Recoverlogic(id);
            if (!success)
                throw new BusinessRuleViolationException("RECOVER_LOGIC_FAILED", $"No se pudo recuperar la entidad con ID {id}.");

            return true;
        }
        //public async Task<bool> Recoverlogic(int id)
        //{
        //    return await _repository.Recoverlogic(id);
        //}

    }
}
