using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace Entity.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Ejemplo de mapeo
            CreateMap<Form, FormDto>();
            CreateMap<FormDto, Form>();

            CreateMap<Paciente, PacienteDto>();
            CreateMap<PacienteDto, Paciente>();

            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();

            CreateMap<Pizza, PizzaDto>();
            CreateMap<PizzaDto, Pizza>();




            CreateMap<Module, ModuleDto>();
            CreateMap<ModuleDto, Module>();

            CreateMap<FormModule, FormModuleDto>();
            CreateMap<FormModuleDto, FormModule>();

            CreateMap<Module, ModuleDto>();
            CreateMap<ModuleDto, Module>();

            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionDto, Permission>();

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();


            CreateMap<rol, rolDto>();
            CreateMap<rolDto, rol>();

            CreateMap<RolFormPermission, RolFormPermissionDto>()
                .ForMember(dest => dest.formname, opt => opt.MapFrom(src => src.Form.name))
                .ForMember(dest => dest.rolname, opt => opt.MapFrom(src => src.Rol.name))
                .ForMember(dest => dest.permissionname, opt => opt.MapFrom(src => src.Permission.name));
            CreateMap<RolFormPermissionDto, RolFormPermission>();

            //CreateMap<RolUser, RolUserDto>();

            CreateMap<RolUser, RolUserDto>()
                        .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.User.username))
                        .ForMember(dest => dest.rolname, opt => opt.MapFrom(src => src.Rol.name));

            CreateMap<RolUserDto, RolUser>();

            CreateMap<User, UserDto>()
                 .ForMember(dest => dest.personname, opt => opt.MapFrom(src => src.Person.firstname));
            CreateMap<UserDto, User>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            // Mapeo de entidad a DTO
            CreateMap<Orders, OrdersDto>()
                .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Customer.Nombre))
                .ForMember(dest => dest.PizzaNombre, opt => opt.MapFrom(src => src.Pizza.Name))
                .ForMember(dest => dest.PizzaPrecio, opt => opt.MapFrom(src => src.Pizza.Price));

            // Mapeo de DTO a entidad (para casos donde edites o crees desde DTO)
            CreateMap<OrdersDto, Orders>();
            CreateMap<CreateOrderDto, Orders>();

        }
    }
}
