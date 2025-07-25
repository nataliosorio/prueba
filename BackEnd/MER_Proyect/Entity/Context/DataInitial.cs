using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Entity.Context
{
    internal class DataInitial
    {
        public static void Data(ModelBuilder modelBuilder)
        {
            // Roles
            modelBuilder.Entity<rol>().HasData(
                new rol { id = 1, name = "Administrador", description = "Rol de administrador", active = true},
                new rol { id = 2, name = "Usuario", description = "Rol de usuario estándar", active = true },
                new rol { id = 3, name = "Asistente", description = "Rol de asistente", active = true },
                new rol { id = 4, name = "Pizzero", description = "Rol de pizzero", active = true }



            );

            // Permisos
            modelBuilder.Entity<Permission>().HasData(
                new Permission { id = 1, name = "GetAll", description = "Permiso para ver todos los registros", active = true },
                new Permission { id = 2, name = "GetById", description = "Permiso para ver por id", active = true },
                new Permission { id = 3, name = "Create", description = "Permiso para crear un registro", active = true },
                new Permission { id = 4, name = "Update", description = "Permiso para actualizar un registro", active = true },
                new Permission { id = 5, name = "DeleteLogic", description = "Permiso para eliminar logicamente un registro", active = true },
                new Permission { id = 6, name = "Delete", description = "Permiso para eliminar permanentemente un registro", active = true },
                new Permission { id = 7, name = "RecoverLogic", description = "Permiso para recuperar un registro", active = true }



            );

            // Personas
            modelBuilder.Entity<Person>().HasData(
                new Person { id = 1, firstname = "Admin", lastname = "Principal", phonenumber = "111111111", active = true },
                new Person { id = 2, firstname = "Usuario", lastname = "Demo", phonenumber = "222222222", active = true },
                new Person { id = 3, firstname = "AsistenteJuan", lastname = "Rodir", phonenumber = "222222422", active = true },
                new Person { id = 4, firstname = "PizzeroJulian", lastname = "Depp", phonenumber = "222222232", active = true }


            );

            // Usuarios
            modelBuilder.Entity<User>().HasData(
                new User { id = 1, username = "admin", email = "natiosoriopoveda@gmail.com", password = "admin1", personid = 1, active = true },
                new User { id = 2, username = "usuario", email = "marthapoveda59@gmail.com", password = "usuario123", personid = 2, active = true },
                new User { id = 3, username = "user", email = "marthapveda579@gmail.com", password = "user1", personid = 3, active = true },
                new User { id = 4, username = "pizzero", email = "mathapveda579@gmail.com", password = "pizzero1", personid = 4, active = true }

            );

            // Módulos
            modelBuilder.Entity<Module>().HasData(
                new Module { id = 1, name = "Gestión", description = "Módulo de gestión", active = true}
            );

            // Formularios
            modelBuilder.Entity<Form>().HasData(
                new Form { id = 1, name = "Rol", description = "Formulario para lols Roles", active = true },
                new Form { id = 2, name = "Permission", description = "Formulario para los Permisos", active = true },
                new Form { id = 3, name = "Person", description = "Formulario para las Personas", active = true },
                new Form { id = 4, name = "User", description = "Formulario de usuarios", active = true },
                new Form { id = 5, name = "Module", description = "Formulario para los modulos", active = true },
                new Form { id = 6, name = "FormControllerPrueba", description = "Formulario para los form", active = true },
                new Form { id = 7, name = "RolUser", description = "Formulario para los roles que tiene cada usuario", active = true },
                new Form { id = 8, name = "FormModule", description = "Formulario para los formularios que pertenecen a cada modulo", active = true },
                new Form { id = 9, name = "RolFormPermission", description = "Formulario para los roles,formularios y permisos", active = true },
                new Form { id = 10, name = "Customer", description = "Formulario par", active = true },
                new Form { id = 11, name = "Orders", description = "Formulario para ", active = true },
                new Form { id = 12, name = "Pizzas", description = "Formulario para", active = true }




            );

            // FormModule (relación muchos a muchos)
            modelBuilder.Entity<FormModule>().HasData( 
                new FormModule { id = 1, formid = 1, moduleid = 1 , active = true }
            );

            // RolUser (relación muchos a muchos)
            modelBuilder.Entity<RolUser>().HasData(
                new RolUser { id = 1, rolid = 1, userid = 1, active = true },
                new RolUser { id = 2, rolid = 2, userid = 2, active = true },
                new RolUser { id = 3, rolid = 3, userid = 3, active = true },
                new RolUser { id = 4, rolid = 4, userid = 4, active = true }



            );


            // RolFormPermission (relación muchos a muchos) - Generación dinámica
            var rolFormPermissions = new List<RolFormPermission>();
            int currentId = 1;

            // Todos los formularios (IDs 1 a 9)
            for (int formId = 1; formId <= 12; formId++)
            {
                // Permisos del rol administrador (rolid = 1): todos los permisos (1 al 7)
                for (int permId = 1; permId <= 7; permId++)
                {
                    rolFormPermissions.Add(new RolFormPermission
                    {
                        id = currentId++,
                        rolid = 1,
                        formid = formId,
                        permissionid = permId,
                        active = true
                    });
                }

                // Permisos del rol usuario (rolid = 2): solo permisos 1 a 5
                for (int permId = 1; permId <= 5; permId++)
                {
                    rolFormPermissions.Add(new RolFormPermission
                    {
                        id = currentId++,
                        rolid = 2,
                        formid = formId,
                        permissionid = permId,
                        active = true
                    });
                }
                // Permisos del rol asistente (rolid = 3): solo permisos 1 a 5
                for (int permId = 1; permId <= 5; permId++)
                {
                    rolFormPermissions.Add(new RolFormPermission
                    {
                        id = currentId++,
                        rolid = 3,
                        formid = formId,
                        permissionid = permId,
                        active = true
                    });
                }
                // Permisos del rol pizzero (rolid = 4): solo permisos 1 a 5
                for (int permId = 1; permId <= 5; permId++)
                {
                    rolFormPermissions.Add(new RolFormPermission
                    {
                        id = currentId++,
                        rolid = 4,
                        formid = formId,
                        permissionid = permId,
                        active = true
                    });
                }
            }

            modelBuilder.Entity<RolFormPermission>().HasData(rolFormPermissions);

        }
    }
}
