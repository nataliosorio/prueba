using System.Diagnostics;
using Business;
using Business.Email;
using Business.Services;
using Data.Services;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Utilities.BackgroundServicess;

namespace Web.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;
        private readonly UserData _userData;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthController> _logger;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly IServiceProvider _serviceProvider;


        public AuthController(ApplicationDbContext context, JwtService jwtService, UserData userData, IConfiguration configuration, ILogger<AuthController> logger, IEmailService emailService, IBackgroundTaskQueue backgroundTaskQueue, IServiceProvider serviceProvider)
        {
            _context = context;
            _jwtService = jwtService;
            _userData = userData;
            _configuration = configuration;
            _logger = logger;
            _emailService = emailService;
            _backgroundTaskQueue = backgroundTaskQueue;
            _serviceProvider = serviceProvider;

        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        //{

        //    try
        //    {
        //        //crear la persona
        //        var person = new Person
        //        {
        //            firstname = dto.Firstname,
        //            lastname = dto.Lastname,
        //            phonenumber = dto.Phonenumber,
        //            active = true
        //        };

        //        _context.person.Add(person);
        //        await _context.SaveChangesAsync(); //guardamos para obtener el ID

        //        //crear el usuario asociado

        //        var user = new User
        //        {
        //            username = dto.Username,
        //            email = dto.Email,
        //            password = dto.Password,
        //            personid = person.id,
        //            active = true
        //        };
        //        _context.User.Add(user);
        //        await _context.SaveChangesAsync();
        //        //crear el rol asociado

        //        var rol = new RolUser
        //        {
        //            userid = user.id,
        //            rolid = 2,
        //            active = true
        //        };
        //        _context.roluser.Add(rol);
        //        await _context.SaveChangesAsync();

        //        var roles = await _context.roluser
        //            .Where(r => r.userid == user.id && r.active)
        //            .Include(r => r.Rol)
        //            .Select(r => r.Rol.name)
        //            .ToListAsync();
        //        //Console.WriteLine("Roles son: " + roles);

        //        var token = _jwtService.GenerarToken(user, roles);

        //        return Ok(new LoginResponse
        //        {
        //            Usuario = user.username,
        //            Roles = roles,
        //            Token = token
        //        });
        //    }
        //    catch (Exception ex) {

        //        return StatusCode(500, $"Error interno: {ex.Message}");

        //    }
           

        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                _logger.LogInformation("📝 Iniciando registro de nuevo usuario...");
                var person = new Person
                {
                    firstname = dto.Firstname,
                    lastname = dto.Lastname,
                    phonenumber = dto.Phonenumber,
                    active = true
                };

                _context.person.Add(person);
                await _context.SaveChangesAsync();
                _logger.LogInformation("✅ Persona guardada en DB con ID {Id}", person.id);

                var user = new User
                {
                    username = dto.Username,
                    email = dto.Email,
                    password = dto.Password,
                    personid = person.id,
                    active = true
                };
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("✅ Usuario guardado en DB con ID {Id}", user.id);
                _logger.LogInformation("🧵 Registro persona ejecutado en hilo: {ThreadId}", Thread.CurrentThread.ManagedThreadId);


                // ASIGNACIÓN EN SEGUNDO PLANO
                _logger.LogInformation("📬 Encolando tarea para asignación de rol en segundo plano...");

                _backgroundTaskQueue.Enqueue(async token =>
                {
                    try
                    {
                        _logger.LogInformation("⚙️ Ejecutando tarea en segundo plano en hilo: {ThreadId}", Thread.CurrentThread.ManagedThreadId);
                        using var scope = _serviceProvider.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        var rol = new RolUser
                        {
                            userid = user.id,
                            rolid = 2,
                            active = true
                        };

                        db.roluser.Add(rol);
                        await db.SaveChangesAsync();

                        _logger.LogInformation("✅ Rol asignado correctamente al usuario con ID {UserId}", user.id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "❌ Error ejecutando tarea en segundo plano.");
                    }
                });

                //busqueda del rol
                var roles = await _context.roluser
                    .Where(r => r.userid == user.id && r.active)
                    .Include(r => r.Rol)
                    .Select(r => r.Rol.name)
                    .ToListAsync();

                var token = _jwtService.GenerarToken(user, roles);

                return Ok(new LoginResponse
                {
                    Usuario = user.username,
                    Roles = roles,
                    Token = token
                });

                //return Ok(new
                //{
                //    isSuccess = true,
                //    message = "Usuario registrado. El rol se asignará en breve."
                //});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error registrando el usuario.");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }



        [HttpPost("login")]
      
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _context.User
                    .Where(u => u.username == request.username && u.password == request.password && u.active)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Unauthorized("Credenciales inválidas.");
                }

                var roles = await _context.roluser
                    .Where(r => r.userid == user.id && r.active)
                    .Include(r => r.Rol)
                    .Select(r => r.Rol.name)
                    .ToListAsync();

                var token = _jwtService.GenerarToken(user, roles);

                return Ok(new LoginResponse
                {
                    Usuario = user.username,
                    Roles = roles,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleTokenDto tokenDto)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                // 1. Validar el token con Google
                var payload = await GoogleJsonWebSignature.ValidateAsync(tokenDto.Token, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _configuration["Google:ClientId"] }
                });

                // 2. Buscar usuario por su email
                var user = await _userData.GetByEmailAsync(payload.Email);

                if (user == null)
                {
                    stopwatch.Stop();
                    Console.WriteLine($"[GOOGLE LOGIN FALLIDO] Tiempo total: {stopwatch.ElapsedMilliseconds} ms");
                    return NotFound(new
                    {
                        isSucces = false,
                        message = "El usuario no existe"
                    });
                }

                // 3. Obtener roles (si es necesario)
                var roles = await _context.roluser
                   .Where(r => r.userid == user.id && r.active)
                   .Include(r => r.Rol)
                   .Select(r => r.Rol.name)
                   .ToListAsync();


                // 4. Generar el token JWT
                var token = _jwtService.GenerarToken(user, roles);
                //await _emailService.SendWelcomeEmailAsync(user.Email, user.username);

                var subject = "¡Bienvenido/a a nuestra plataforma!";
                var htmlContent = $@"
                            <div style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                                <div style='max-width: 600px; margin: auto; background-color: #ffffff; padding: 30px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
                                    <h2 style='color: #4CAF50;'>¡Hola {user.username}!</h2>
                                    <p style='font-size: 16px; color: #333;'>Has iniciado sesión correctamente en nuestra plataforma.</p>
                                    <p style='font-size: 16px; color: #333;'>Estamos muy felices de tenerte de vuelta 😊</p>
                                    <hr style='margin: 20px 0;' />
                                    <p style='font-size: 14px; color: #999;'>Si no fuiste tú, por favor comunícate con nuestro equipo de soporte.</p>
                                </div>
                            </div>
                        ";

                //await _emailService.SendEmailAsync(user.email, subject, htmlContent);
                _ = Task.Run(() => _emailService.SendEmailAsync(user.email, subject, htmlContent));


                stopwatch.Stop();
                Console.WriteLine($"[GOOGLE LOGIN ÉXITO] Tiempo total: {stopwatch.ElapsedMilliseconds} ms");



                return Ok(new LoginResponse
                {
                    Usuario = user.username,
                    Roles = roles,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"[ERROR LOGIN] Tiempo total: {stopwatch.ElapsedMilliseconds} ms - Error: {ex.Message}");
                _logger.LogError(ex, "Error al iniciar sesión con Google");
                return StatusCode(500, new { isSucces = false, message = "Error del servidor" });
            }
        }


    }
}
