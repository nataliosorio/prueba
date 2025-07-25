using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Services;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Services
{
    public class CitaBusiness
    {
        private readonly CitaData _citaData;
        private readonly ILogger<CitaBusiness> _logger;

        public CitaBusiness(CitaData citaData, ILogger<CitaBusiness> logger)
        {
            _citaData = citaData;
            _logger = logger;
        }

        public async Task<IEnumerable<Cita>> GetAllCitasAsync()
        {
            return await _citaData.GetAllWithDetailsAsync();
        }

        public async Task<IEnumerable<CitaDto>> GetAllCitasDtoAsync()
        {
            try
            {
                var citas = await _citaData.GetAllWithDetailsAsync();

                return citas.Select(c => new CitaDto
                {
                    Id = c.Id,
                    FechaHora = c.FechaHora,
                    MotivoConsulta = c.MotivoConsulta,
                    Active = c.Active,
                    PacienteId = c.PacienteId,
                    DoctorId = c.DoctorId,
                    NombrePaciente = c.Paciente?.NombreCompleto,
                    NombreDoctor = c.Doctor?.NombreCompleto
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al mapear citas a DTO");
                throw;
            }
        }

        public async Task<CitaDto?> GetByIdDtoAsync(int id)
        {
            var cita = await _citaData.GetByIdWithDetailsAsync(id);

            if (cita == null)
                return null;

            return new CitaDto
            {
                Id = cita.Id,
                FechaHora = cita.FechaHora,
                MotivoConsulta = cita.MotivoConsulta,
                Active = cita.Active,
                PacienteId = cita.PacienteId,
                DoctorId = cita.DoctorId,
                NombrePaciente = cita.Paciente?.NombreCompleto,
                NombreDoctor = cita.Doctor?.NombreCompleto
            };
        }

        public async Task<Cita> CreateAsync(CitaCreate dto)
        {
            var cita = new Cita
            {
                FechaHora = dto.FechaHora,
                MotivoConsulta = dto.MotivoConsulta,
                Active = dto.Active,
                PacienteId = dto.PacienteId,
                DoctorId = dto.DoctorId
            };

            return await _citaData.CreateAsync(cita);
        }

        public async Task<bool> UpdateAsync(CitaCreate dto)
        {
            var cita = new Cita
            {
                Id = dto.Id,
                FechaHora = dto.FechaHora,
                MotivoConsulta = dto.MotivoConsulta,
                Active = dto.Active,
                PacienteId = dto.PacienteId,
                DoctorId = dto.DoctorId
            };

            return await _citaData.UpdateAsync(cita);
        }

        public async Task<bool> DeleteCitaAsync(int id)
        {
            return await _citaData.DeleteAsync(id);
        }

        public async Task<bool> DeleteCitaLogicAsync(int id)
        {
            return await _citaData.DeleteLogicAsync(id);
        }

        public async Task<bool> RecoverLogicAsync(int id)
        {
            return await _citaData.RecoverLogicAsync(id);
        }
    }
}

