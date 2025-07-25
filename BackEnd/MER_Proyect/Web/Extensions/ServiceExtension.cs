using Business.Email;
using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Services;
using Entity.DTOs;
using Entity.Model;
using Utilities.Aud.CurrentUser;
using Utilities.Aud.Services;
using Utilities.BackgroundServicess;

namespace Web.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {

            //Obtener Usuario
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            // Inyección de dependencias para auditoría con Strategy

            services.AddScoped<IAuditService, AuditService>();
            // Repositorio base genérico
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Servicios genéricos
            services.AddScoped(typeof(IGenericService<FormDto>), typeof(GenericService<FormDto, Form>));
            services.AddScoped(typeof(IGenericService<FormModuleDto>), typeof(GenericService<FormModuleDto, FormModule>));
            services.AddScoped(typeof(IGenericService<ModuleDto>), typeof(GenericService<ModuleDto, Module>));
            services.AddScoped(typeof(IGenericService<rolDto>), typeof(GenericService<rolDto, rol>));
            services.AddScoped(typeof(IGenericService<PersonDto>), typeof(GenericService<PersonDto, Person>));
            services.AddScoped(typeof(IGenericService<PermissionDto>), typeof(GenericService<PermissionDto, Permission>));
            services.AddScoped(typeof(IGenericService<RolFormPermissionDto>), typeof(GenericService<RolFormPermissionDto, RolFormPermission>));
            services.AddScoped(typeof(IGenericService<OrdersDto>), typeof(GenericService<OrdersDto, Orders>));
            services.AddScoped(typeof(IGenericService<CustomerDto>), typeof(GenericService<CustomerDto, Customer>));



            services.AddScoped(typeof(IGenericService<PacienteDto>), typeof(GenericService<PacienteDto, Paciente>));
            services.AddScoped(typeof(IGenericService<DoctorDto>), typeof(GenericService<DoctorDto, Doctor>));
            services.AddScoped(typeof(IGenericService<PizzaDto>), typeof(GenericService<PizzaDto, Pizza>));




            // SERVICIO EXTENDIDO
            services.AddScoped<RolFormPermissionRepository>();
            services.AddScoped<FormModuleRepository>();

            services.AddScoped<UsersBusiness>();
            services.AddScoped<OrdersBusiness>();

            services.AddScoped<UserData>();

            services.AddScoped<RolUserBusiness>();
            services.AddScoped<RolUserData>();
            services.AddScoped<CitaBusiness>();
            services.AddScoped<CitaData>();
            services.AddScoped<OrdersData>();




            //Automaper
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddTransient<EmailService>();
            services.AddScoped<JwtService>();







            return services;
        }
    }
}