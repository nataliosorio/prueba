using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entity.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class Pg_pizzasDbM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "segurity");

            migrationBuilder.EnsureSchema(
                name: "persons");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCompleto = table.Column<string>(type: "text", nullable: false),
                    Especialidad = table.Column<string>(type: "text", nullable: false),
                    NumeroDeColegiado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "form",
                schema: "segurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                schema: "segurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCompleto = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Genero = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "segurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person",
                schema: "persons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lastname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phonenumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pizzas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pizzas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rol",
                schema: "segurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "formmodule",
                schema: "segurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    formid = table.Column<int>(type: "integer", nullable: false),
                    moduleid = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formmodule", x => x.id);
                    table.ForeignKey(
                        name: "FK_FormModule_Form",
                        column: x => x.formid,
                        principalSchema: "segurity",
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Module_FormModules",
                        column: x => x.moduleid,
                        principalSchema: "segurity",
                        principalTable: "Module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MotivoConsulta = table.Column<string>(type: "text", nullable: false),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cita_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cita_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "segurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    personid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_Person_User",
                        column: x => x.personid,
                        principalSchema: "persons",
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    PizzaId = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rolformpermission",
                schema: "segurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rolid = table.Column<int>(type: "integer", nullable: false),
                    formid = table.Column<int>(type: "integer", nullable: false),
                    permissionid = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolformpermission", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Form",
                        column: x => x.formid,
                        principalSchema: "segurity",
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Permission",
                        column: x => x.permissionid,
                        principalSchema: "segurity",
                        principalTable: "Permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolFormPermission_Rol",
                        column: x => x.rolid,
                        principalSchema: "segurity",
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roluser",
                schema: "segurity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rolid = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roluser", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolUser_Rol",
                        column: x => x.rolid,
                        principalSchema: "segurity",
                        principalTable: "rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolUser_User",
                        column: x => x.userid,
                        principalSchema: "segurity",
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailsOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PedidoId = table.Column<int>(type: "integer", nullable: false),
                    OrdersId = table.Column<int>(type: "integer", nullable: false),
                    PizzaId = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailsOrder_orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailsOrder_pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "segurity",
                table: "Module",
                columns: new[] { "id", "active", "description", "name" },
                values: new object[] { 1, true, "Módulo de gestión", "Gestión" });

            migrationBuilder.InsertData(
                schema: "segurity",
                table: "Permission",
                columns: new[] { "id", "active", "description", "name" },
                values: new object[,]
                {
                    { 1, true, "Permiso para ver todos los registros", "GetAll" },
                    { 2, true, "Permiso para ver por id", "GetById" },
                    { 3, true, "Permiso para crear un registro", "Create" },
                    { 4, true, "Permiso para actualizar un registro", "Update" },
                    { 5, true, "Permiso para eliminar logicamente un registro", "DeleteLogic" },
                    { 6, true, "Permiso para eliminar permanentemente un registro", "Delete" },
                    { 7, true, "Permiso para recuperar un registro", "RecoverLogic" }
                });

            migrationBuilder.InsertData(
                schema: "segurity",
                table: "form",
                columns: new[] { "id", "active", "description", "name" },
                values: new object[,]
                {
                    { 1, true, "Formulario para lols Roles", "Rol" },
                    { 2, true, "Formulario para los Permisos", "Permission" },
                    { 3, true, "Formulario para las Personas", "Person" },
                    { 4, true, "Formulario de usuarios", "User" },
                    { 5, true, "Formulario para los modulos", "Module" },
                    { 6, true, "Formulario para los form", "FormControllerPrueba" },
                    { 7, true, "Formulario para los roles que tiene cada usuario", "RolUser" },
                    { 8, true, "Formulario para los formularios que pertenecen a cada modulo", "FormModule" },
                    { 9, true, "Formulario para los roles,formularios y permisos", "RolFormPermission" },
                    { 10, true, "Formulario par", "Customer" },
                    { 11, true, "Formulario para ", "Orders" },
                    { 12, true, "Formulario para", "Pizzas" }
                });

            migrationBuilder.InsertData(
                schema: "persons",
                table: "person",
                columns: new[] { "id", "active", "firstname", "lastname", "phonenumber" },
                values: new object[,]
                {
                    { 1, true, "Admin", "Principal", "111111111" },
                    { 2, true, "Usuario", "Demo", "222222222" },
                    { 3, true, "AsistenteJuan", "Rodir", "222222422" },
                    { 4, true, "PizzeroJulian", "Depp", "222222232" }
                });

            migrationBuilder.InsertData(
                schema: "segurity",
                table: "rol",
                columns: new[] { "id", "active", "description", "name" },
                values: new object[,]
                {
                    { 1, true, "Rol de administrador", "Administrador" },
                    { 2, true, "Rol de usuario estándar", "Usuario" },
                    { 3, true, "Rol de asistente", "Asistente" },
                    { 4, true, "Rol de pizzero", "Pizzero" }
                });

            migrationBuilder.InsertData(
                schema: "segurity",
                table: "User",
                columns: new[] { "id", "active", "email", "password", "personid", "username" },
                values: new object[,]
                {
                    { 1, true, "natiosoriopoveda@gmail.com", "admin1", 1, "admin" },
                    { 2, true, "marthapoveda59@gmail.com", "usuario123", 2, "usuario" },
                    { 3, true, "marthapveda579@gmail.com", "user1", 3, "user" },
                    { 4, true, "mathapveda579@gmail.com", "pizzero1", 4, "pizzero" }
                });

            migrationBuilder.InsertData(
                schema: "segurity",
                table: "formmodule",
                columns: new[] { "id", "active", "formid", "moduleid" },
                values: new object[] { 1, true, 1, 1 });

            migrationBuilder.InsertData(
                schema: "segurity",
                table: "rolformpermission",
                columns: new[] { "id", "active", "formid", "permissionid", "rolid" },
                values: new object[,]
                {
                    { 1, true, 1, 1, 1 },
                    { 2, true, 1, 2, 1 },
                    { 3, true, 1, 3, 1 },
                    { 4, true, 1, 4, 1 },
                    { 5, true, 1, 5, 1 },
                    { 6, true, 1, 6, 1 },
                    { 7, true, 1, 7, 1 },
                    { 8, true, 1, 1, 2 },
                    { 9, true, 1, 2, 2 },
                    { 10, true, 1, 3, 2 },
                    { 11, true, 1, 4, 2 },
                    { 12, true, 1, 5, 2 },
                    { 13, true, 1, 1, 3 },
                    { 14, true, 1, 2, 3 },
                    { 15, true, 1, 3, 3 },
                    { 16, true, 1, 4, 3 },
                    { 17, true, 1, 5, 3 },
                    { 18, true, 1, 1, 4 },
                    { 19, true, 1, 2, 4 },
                    { 20, true, 1, 3, 4 },
                    { 21, true, 1, 4, 4 },
                    { 22, true, 1, 5, 4 },
                    { 23, true, 2, 1, 1 },
                    { 24, true, 2, 2, 1 },
                    { 25, true, 2, 3, 1 },
                    { 26, true, 2, 4, 1 },
                    { 27, true, 2, 5, 1 },
                    { 28, true, 2, 6, 1 },
                    { 29, true, 2, 7, 1 },
                    { 30, true, 2, 1, 2 },
                    { 31, true, 2, 2, 2 },
                    { 32, true, 2, 3, 2 },
                    { 33, true, 2, 4, 2 },
                    { 34, true, 2, 5, 2 },
                    { 35, true, 2, 1, 3 },
                    { 36, true, 2, 2, 3 },
                    { 37, true, 2, 3, 3 },
                    { 38, true, 2, 4, 3 },
                    { 39, true, 2, 5, 3 },
                    { 40, true, 2, 1, 4 },
                    { 41, true, 2, 2, 4 },
                    { 42, true, 2, 3, 4 },
                    { 43, true, 2, 4, 4 },
                    { 44, true, 2, 5, 4 },
                    { 45, true, 3, 1, 1 },
                    { 46, true, 3, 2, 1 },
                    { 47, true, 3, 3, 1 },
                    { 48, true, 3, 4, 1 },
                    { 49, true, 3, 5, 1 },
                    { 50, true, 3, 6, 1 },
                    { 51, true, 3, 7, 1 },
                    { 52, true, 3, 1, 2 },
                    { 53, true, 3, 2, 2 },
                    { 54, true, 3, 3, 2 },
                    { 55, true, 3, 4, 2 },
                    { 56, true, 3, 5, 2 },
                    { 57, true, 3, 1, 3 },
                    { 58, true, 3, 2, 3 },
                    { 59, true, 3, 3, 3 },
                    { 60, true, 3, 4, 3 },
                    { 61, true, 3, 5, 3 },
                    { 62, true, 3, 1, 4 },
                    { 63, true, 3, 2, 4 },
                    { 64, true, 3, 3, 4 },
                    { 65, true, 3, 4, 4 },
                    { 66, true, 3, 5, 4 },
                    { 67, true, 4, 1, 1 },
                    { 68, true, 4, 2, 1 },
                    { 69, true, 4, 3, 1 },
                    { 70, true, 4, 4, 1 },
                    { 71, true, 4, 5, 1 },
                    { 72, true, 4, 6, 1 },
                    { 73, true, 4, 7, 1 },
                    { 74, true, 4, 1, 2 },
                    { 75, true, 4, 2, 2 },
                    { 76, true, 4, 3, 2 },
                    { 77, true, 4, 4, 2 },
                    { 78, true, 4, 5, 2 },
                    { 79, true, 4, 1, 3 },
                    { 80, true, 4, 2, 3 },
                    { 81, true, 4, 3, 3 },
                    { 82, true, 4, 4, 3 },
                    { 83, true, 4, 5, 3 },
                    { 84, true, 4, 1, 4 },
                    { 85, true, 4, 2, 4 },
                    { 86, true, 4, 3, 4 },
                    { 87, true, 4, 4, 4 },
                    { 88, true, 4, 5, 4 },
                    { 89, true, 5, 1, 1 },
                    { 90, true, 5, 2, 1 },
                    { 91, true, 5, 3, 1 },
                    { 92, true, 5, 4, 1 },
                    { 93, true, 5, 5, 1 },
                    { 94, true, 5, 6, 1 },
                    { 95, true, 5, 7, 1 },
                    { 96, true, 5, 1, 2 },
                    { 97, true, 5, 2, 2 },
                    { 98, true, 5, 3, 2 },
                    { 99, true, 5, 4, 2 },
                    { 100, true, 5, 5, 2 },
                    { 101, true, 5, 1, 3 },
                    { 102, true, 5, 2, 3 },
                    { 103, true, 5, 3, 3 },
                    { 104, true, 5, 4, 3 },
                    { 105, true, 5, 5, 3 },
                    { 106, true, 5, 1, 4 },
                    { 107, true, 5, 2, 4 },
                    { 108, true, 5, 3, 4 },
                    { 109, true, 5, 4, 4 },
                    { 110, true, 5, 5, 4 },
                    { 111, true, 6, 1, 1 },
                    { 112, true, 6, 2, 1 },
                    { 113, true, 6, 3, 1 },
                    { 114, true, 6, 4, 1 },
                    { 115, true, 6, 5, 1 },
                    { 116, true, 6, 6, 1 },
                    { 117, true, 6, 7, 1 },
                    { 118, true, 6, 1, 2 },
                    { 119, true, 6, 2, 2 },
                    { 120, true, 6, 3, 2 },
                    { 121, true, 6, 4, 2 },
                    { 122, true, 6, 5, 2 },
                    { 123, true, 6, 1, 3 },
                    { 124, true, 6, 2, 3 },
                    { 125, true, 6, 3, 3 },
                    { 126, true, 6, 4, 3 },
                    { 127, true, 6, 5, 3 },
                    { 128, true, 6, 1, 4 },
                    { 129, true, 6, 2, 4 },
                    { 130, true, 6, 3, 4 },
                    { 131, true, 6, 4, 4 },
                    { 132, true, 6, 5, 4 },
                    { 133, true, 7, 1, 1 },
                    { 134, true, 7, 2, 1 },
                    { 135, true, 7, 3, 1 },
                    { 136, true, 7, 4, 1 },
                    { 137, true, 7, 5, 1 },
                    { 138, true, 7, 6, 1 },
                    { 139, true, 7, 7, 1 },
                    { 140, true, 7, 1, 2 },
                    { 141, true, 7, 2, 2 },
                    { 142, true, 7, 3, 2 },
                    { 143, true, 7, 4, 2 },
                    { 144, true, 7, 5, 2 },
                    { 145, true, 7, 1, 3 },
                    { 146, true, 7, 2, 3 },
                    { 147, true, 7, 3, 3 },
                    { 148, true, 7, 4, 3 },
                    { 149, true, 7, 5, 3 },
                    { 150, true, 7, 1, 4 },
                    { 151, true, 7, 2, 4 },
                    { 152, true, 7, 3, 4 },
                    { 153, true, 7, 4, 4 },
                    { 154, true, 7, 5, 4 },
                    { 155, true, 8, 1, 1 },
                    { 156, true, 8, 2, 1 },
                    { 157, true, 8, 3, 1 },
                    { 158, true, 8, 4, 1 },
                    { 159, true, 8, 5, 1 },
                    { 160, true, 8, 6, 1 },
                    { 161, true, 8, 7, 1 },
                    { 162, true, 8, 1, 2 },
                    { 163, true, 8, 2, 2 },
                    { 164, true, 8, 3, 2 },
                    { 165, true, 8, 4, 2 },
                    { 166, true, 8, 5, 2 },
                    { 167, true, 8, 1, 3 },
                    { 168, true, 8, 2, 3 },
                    { 169, true, 8, 3, 3 },
                    { 170, true, 8, 4, 3 },
                    { 171, true, 8, 5, 3 },
                    { 172, true, 8, 1, 4 },
                    { 173, true, 8, 2, 4 },
                    { 174, true, 8, 3, 4 },
                    { 175, true, 8, 4, 4 },
                    { 176, true, 8, 5, 4 },
                    { 177, true, 9, 1, 1 },
                    { 178, true, 9, 2, 1 },
                    { 179, true, 9, 3, 1 },
                    { 180, true, 9, 4, 1 },
                    { 181, true, 9, 5, 1 },
                    { 182, true, 9, 6, 1 },
                    { 183, true, 9, 7, 1 },
                    { 184, true, 9, 1, 2 },
                    { 185, true, 9, 2, 2 },
                    { 186, true, 9, 3, 2 },
                    { 187, true, 9, 4, 2 },
                    { 188, true, 9, 5, 2 },
                    { 189, true, 9, 1, 3 },
                    { 190, true, 9, 2, 3 },
                    { 191, true, 9, 3, 3 },
                    { 192, true, 9, 4, 3 },
                    { 193, true, 9, 5, 3 },
                    { 194, true, 9, 1, 4 },
                    { 195, true, 9, 2, 4 },
                    { 196, true, 9, 3, 4 },
                    { 197, true, 9, 4, 4 },
                    { 198, true, 9, 5, 4 },
                    { 199, true, 10, 1, 1 },
                    { 200, true, 10, 2, 1 },
                    { 201, true, 10, 3, 1 },
                    { 202, true, 10, 4, 1 },
                    { 203, true, 10, 5, 1 },
                    { 204, true, 10, 6, 1 },
                    { 205, true, 10, 7, 1 },
                    { 206, true, 10, 1, 2 },
                    { 207, true, 10, 2, 2 },
                    { 208, true, 10, 3, 2 },
                    { 209, true, 10, 4, 2 },
                    { 210, true, 10, 5, 2 },
                    { 211, true, 10, 1, 3 },
                    { 212, true, 10, 2, 3 },
                    { 213, true, 10, 3, 3 },
                    { 214, true, 10, 4, 3 },
                    { 215, true, 10, 5, 3 },
                    { 216, true, 10, 1, 4 },
                    { 217, true, 10, 2, 4 },
                    { 218, true, 10, 3, 4 },
                    { 219, true, 10, 4, 4 },
                    { 220, true, 10, 5, 4 },
                    { 221, true, 11, 1, 1 },
                    { 222, true, 11, 2, 1 },
                    { 223, true, 11, 3, 1 },
                    { 224, true, 11, 4, 1 },
                    { 225, true, 11, 5, 1 },
                    { 226, true, 11, 6, 1 },
                    { 227, true, 11, 7, 1 },
                    { 228, true, 11, 1, 2 },
                    { 229, true, 11, 2, 2 },
                    { 230, true, 11, 3, 2 },
                    { 231, true, 11, 4, 2 },
                    { 232, true, 11, 5, 2 },
                    { 233, true, 11, 1, 3 },
                    { 234, true, 11, 2, 3 },
                    { 235, true, 11, 3, 3 },
                    { 236, true, 11, 4, 3 },
                    { 237, true, 11, 5, 3 },
                    { 238, true, 11, 1, 4 },
                    { 239, true, 11, 2, 4 },
                    { 240, true, 11, 3, 4 },
                    { 241, true, 11, 4, 4 },
                    { 242, true, 11, 5, 4 },
                    { 243, true, 12, 1, 1 },
                    { 244, true, 12, 2, 1 },
                    { 245, true, 12, 3, 1 },
                    { 246, true, 12, 4, 1 },
                    { 247, true, 12, 5, 1 },
                    { 248, true, 12, 6, 1 },
                    { 249, true, 12, 7, 1 },
                    { 250, true, 12, 1, 2 },
                    { 251, true, 12, 2, 2 },
                    { 252, true, 12, 3, 2 },
                    { 253, true, 12, 4, 2 },
                    { 254, true, 12, 5, 2 },
                    { 255, true, 12, 1, 3 },
                    { 256, true, 12, 2, 3 },
                    { 257, true, 12, 3, 3 },
                    { 258, true, 12, 4, 3 },
                    { 259, true, 12, 5, 3 },
                    { 260, true, 12, 1, 4 },
                    { 261, true, 12, 2, 4 },
                    { 262, true, 12, 3, 4 },
                    { 263, true, 12, 4, 4 },
                    { 264, true, 12, 5, 4 }
                });

            migrationBuilder.InsertData(
                schema: "segurity",
                table: "roluser",
                columns: new[] { "id", "active", "rolid", "userid" },
                values: new object[,]
                {
                    { 1, true, 1, 1 },
                    { 2, true, 2, 2 },
                    { 3, true, 3, 3 },
                    { 4, true, 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cita_DoctorId",
                table: "Cita",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_PacienteId",
                table: "Cita",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsOrder_OrdersId",
                table: "DetailsOrder",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsOrder_PizzaId",
                table: "DetailsOrder",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_formmodule_formid",
                schema: "segurity",
                table: "formmodule",
                column: "formid");

            migrationBuilder.CreateIndex(
                name: "IX_formmodule_moduleid",
                schema: "segurity",
                table: "formmodule",
                column: "moduleid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerId",
                table: "orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_PizzaId",
                table: "orders",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_rolformpermission_formid",
                schema: "segurity",
                table: "rolformpermission",
                column: "formid");

            migrationBuilder.CreateIndex(
                name: "IX_rolformpermission_permissionid",
                schema: "segurity",
                table: "rolformpermission",
                column: "permissionid");

            migrationBuilder.CreateIndex(
                name: "IX_rolformpermission_rolid",
                schema: "segurity",
                table: "rolformpermission",
                column: "rolid");

            migrationBuilder.CreateIndex(
                name: "IX_roluser_rolid",
                schema: "segurity",
                table: "roluser",
                column: "rolid");

            migrationBuilder.CreateIndex(
                name: "IX_roluser_userid",
                schema: "segurity",
                table: "roluser",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_User_personid",
                schema: "segurity",
                table: "User",
                column: "personid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "DetailsOrder");

            migrationBuilder.DropTable(
                name: "formmodule",
                schema: "segurity");

            migrationBuilder.DropTable(
                name: "rolformpermission",
                schema: "segurity");

            migrationBuilder.DropTable(
                name: "roluser",
                schema: "segurity");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "Module",
                schema: "segurity");

            migrationBuilder.DropTable(
                name: "form",
                schema: "segurity");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "segurity");

            migrationBuilder.DropTable(
                name: "rol",
                schema: "segurity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "segurity");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "pizzas");

            migrationBuilder.DropTable(
                name: "person",
                schema: "persons");
        }
    }
}
