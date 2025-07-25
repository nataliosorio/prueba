using Business.Interfaces;
using Entity.DTOs;

namespace Web.Controllers
{
    public class DoctorController : GenericController<DoctorDto>
    {
        public DoctorController(IGenericService<DoctorDto> service) : base(service)
        {

        }
    }
}

